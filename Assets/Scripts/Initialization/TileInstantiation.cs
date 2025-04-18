using UnityEngine;
using System.Collections.Generic;

public class TileInstantiation : MonoBehaviour
{
    public GameObject tileWhite;
    public GameObject tileBlack;
    public GameObject tileBrown;

    private Dictionary<Vector3Int, Tile> tileMap = new Dictionary<Vector3Int, Tile>();


    // Singleton instance
    public static TileInstantiation Instance { get; private set; }


    /// <summary>
    /// Ensures that only one instance of the object exists by implementing a 
    /// singleton pattern. Preserves the object across scene loads.
    /// </summary>
    /// <remarks>
    /// If an instance already exists, the duplicate is destroyed. Otherwise,
    /// the current object is set as the instance and marked to persist between scenes.
    /// </remarks>
    private void Awake()
    {
        // Ensure only one instance exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    /* 
     * Conversion MATH:
     *      Basis vectors:
     *            a0 = [           0          1/2];
     *            b0 = [ cos(pi/6)/2 -sin(pi/6)/2];
     *            c0 = [-cos(pi/6)/2 -sin(pi/6)/2];
     *      
     *      Coordinate(x y) is a linear combination of these unit vectors so
     *            (x, y) = a* a0+b* b0+c* c0;
     *      
     *      Given the condition a+b+c == 0 we get c == -a-b, hence
     *            (x, y) = a* a0+b* b0+(-a-b)*c0;
     *      
     *      Computing this with unit vector values gives
     *            x = sqrt(3)/2*(a/2+b);
     *            y = 3/4*a;
     *      
     *      Solving for a, b and c gives
     *            a = 4/3*y;
     *            b = 2/sqrt(3)*x-2/3*y;
     *            c = -a-b;
     *      
     *      Finally, replacing y with z (for Unity) gives
     *            x = sqrt(3)/2*(a/2+b);
     *            z = 3/4*a;
     *      and
     *            a = 4/3*z;
     *            b = 2/sqrt(3)*x-2/3*z;
     *            c = -a-b;
     *      
     *      a, b and c is rounded for discrete integer coordinates.
     */


    /// <summary>
    /// Converts a 3D world position to a cube coordinate in a hexagonal grid system.
    /// See the MATH above.
    /// </summary>
    /// <param name="worldPos">The world position to convert.</param>
    /// <returns>
    /// A Vector3Int representing the corresponding cube coordinates (a, b, c).
    /// </returns>
    /// <remarks>
    /// Assumes a specific layout and origin correction for accurate conversion.
    /// </remarks>
    public Vector3Int WorldToCubePosition(Vector3 worldPos)
    {
        float x = worldPos.x;
        float z = worldPos.z + 0.5f; // Correct the origin position

        int a = Mathf.RoundToInt((4.0f / 3.0f) * z);
        int b = Mathf.RoundToInt((2.0f / Mathf.Sqrt(3f)) * x - (2.0f / 3.0f) * z);
        int c = -a - b;

        return new Vector3Int(a, b, c);
    }


    /// <summary>
    /// Converts cube coordinates from a hexagonal grid to a 3D world position.
    /// See the MATH above.
    /// </summary>
    /// <param name="cubePos">The cube coordinates (a, b, c) to convert.</param>
    /// <returns>
    /// A Vector3 representing the corresponding world position, with y set to 0
    /// for a flat board.
    /// </returns>
    /// <remarks>
    /// Applies layout-specific scaling and origin correction during conversion.
    /// </remarks>
    public Vector3 CubeToWorldPosition(Vector3Int cubePos)
    {
        int a = cubePos.x;
        int b = cubePos.y;
        int c = cubePos.z;

        float x = Mathf.Sqrt(3) / 2.0f * (a / 2.0f + b);
        float z = 3.0f / 4.0f * a - 0.5f; // Correct the origin position

        return new Vector3(x, 0, z); // y = 0 assuming flat board
    }

    /// <summary>
    /// Instantiates a tile prefab at a given world position with specified rotation 
    /// and color, assigns it a cube position, and adds it to the tile map.
    /// </summary>
    /// <param name="tilePrefab">The tile prefab to instantiate.</param>
    /// <param name="rotation">The rotation to apply to the tile object.</param>
    /// <param name="worldCoordinates">The world coordinates where the tile will be placed.</param>
    /// <param name="colour">The color to assign to the tile.</param>
    /// <remarks>
    /// Logs an error if the instantiated prefab does not contain a Tile component.
    /// </remarks>
    private void InstantiateAndPopulateTile(GameObject tilePrefab, Quaternion rotation, Vector3 worldCoordinates, Color colour)
    {
        Vector3Int cubePos = WorldToCubePosition(worldCoordinates);
        GameObject newTileObj = Instantiate(tilePrefab, worldCoordinates, rotation);
        Tile newTile = newTileObj.GetComponent<Tile>();
        if (newTile != null)
        {
            newTile.CubePosition = cubePos;
            newTile.TileColor = colour;
            newTile.IsOccupied = false; // The tile starts unoccupied
            tileMap[cubePos] = newTile;

            Debug.Log("Added tile with world position of " + worldCoordinates + " and cube position of " + cubePos + " to the tile map");
        }
        else
        {
            Debug.LogError("The instantiated tile prefab does not have a Tile component attached.");
        }
    }


    /// <summary>
    /// Instantiates and places white tiles on a hexagonal grid with specific layout rules.
    /// </summary>
    /// <param name="tile">The tile prefab to instantiate.</param>
    /// <param name="centerX">The X coordinate of the grid's center.</param>
    /// <param name="centerY">The Y coordinate of the grid's center.</param>
    /// <param name="tileWidth">The width of a single tile.</param>
    /// <param name="tileHeight">The height of a single tile.</param>
    /// <remarks>
    /// Tiles are placed in a staggered hex grid pattern, excluding specific
    /// coordinates. Only tiles where i and j share the same parity are instantiated.
    /// </remarks>
    private void InstantateWhiteTiles(GameObject tile, float centerX, float centerY,
        float tileWidth, float tileHeight)
    {
        // Correct the rotation of tiles
        Quaternion rotation = Quaternion.Euler(0, 30, 0);

        HashSet<(int, int)> excludedTiles = new HashSet<(int, int)>
        {
            (-3,  3),
            (-3, -1),
            (-3, -3),
            (-3, -5),
            (-2, -4),
            (-2, -6),
            ( 2, -6),
            ( 3, -3),
            ( 3, -5),
        };

        // Outer loop: bottom to top
        for (int j = -6; j <= 4; j++)
        {
            // Inner loop: left to right conditionally
            for (int i = -3; i <= 3; i++)
            {
                (int, int) tileVec = (i, j);
                if (!excludedTiles.Contains(tileVec))
                {
                    float newX = centerX + 1.5f * tileWidth * i;
                    float newZ = centerY + 0.75f * tileHeight * j;
                    Vector3 newPosition = new Vector3(newX, 0, newZ);

                    // Instantiate tile if i and j have matching parity
                    if ((j % 2 == 0 && i % 2 == 0) || (j % 2 != 0 && i % 2 != 0))
                    {
                        InstantiateAndPopulateTile(tile, rotation, newPosition, Color.white);
                    }
                }
            }
        }
    }


    /// <summary>
    /// Instantiates and places black tiles on a hexagonal grid with specific layout rules.
    /// </summary>
    /// <param name="tile">The tile prefab to instantiate.</param>
    /// <param name="centerX">The X coordinate of the grid's center.</param>
    /// <param name="centerY">The Y coordinate of the grid's center.</param>
    /// <param name="tileWidth">The width of a single tile.</param>
    /// <param name="tileHeight">The height of a single tile.</param>
    /// <remarks>
    /// Tiles are placed in a staggered hex grid pattern, excluding specific
    /// coordinates. Only tiles where i and j share the same parity are instantiated.
    /// </remarks>
    private void InstantateBlackTiles(GameObject tile, float centerX, float centerY,
        float tileWidth, float tileHeight)
    {
        // Correct the rotation of tiles
        Quaternion rotation = Quaternion.Euler(0, 30, 0);

        HashSet<(int, int)> excludedTiles = new HashSet<(int, int)>
        {
            (-3,  5),
            (-3, -1),
            (-3, -3),
            (-3, -5),
            (-2, -4),
            (-2, -6),
            ( 0, -6),
            ( 2, -4),
            ( 2, -6),
            ( 3,  5),
            ( 3, -1),
            ( 3, -3),
            ( 3, -5),
        };

        // Outer loop: bottom to top
        for (int j = -6; j <= 5; j++)
        {
            // Inner loop: left to right conditionally
            for (int i = -3; i <= 3; i++)
            {
                (int, int) tileVec = (i, j);
                if (!excludedTiles.Contains(tileVec))
                {
                    float newX = centerX + 1.5f * tileWidth * i;
                    float newZ = centerY + 0.75f * tileHeight * j;
                    Vector3 newPosition = new Vector3(newX, 0, newZ);

                    if ((j % 2 == 0 && i % 2 == 0) || (j % 2 != 0 && i % 2 != 0))
                    {
                        InstantiateAndPopulateTile(tile, rotation, newPosition, Color.black);
                    }
                }
            }
        }
    }


    /// <summary>
    /// Instantiates and places brown tiles on a hexagonal grid with specific layout rules.
    /// </summary>
    /// <param name="tile">The tile prefab to instantiate.</param>
    /// <param name="centerX">The X coordinate of the grid's center.</param>
    /// <param name="centerY">The Y coordinate of the grid's center.</param>
    /// <param name="tileWidth">The width of a single tile.</param>
    /// <param name="tileHeight">The height of a single tile.</param>
    /// <remarks>
    /// Tiles are placed in a staggered hex grid pattern, excluding specific
    /// coordinates. Only tiles where i and j share the same parity are instantiated,
    /// and each is assigned a brown color.
    /// </remarks>
    private void InstantateBrownTiles(GameObject tile, float centerX, float centerY,
        float tileWidth, float tileHeight)
    {
        // Correct the rotation of tiles
        Quaternion rotation = Quaternion.Euler(0, 30, 0);

        HashSet<(int, int)> excludedTiles = new HashSet<(int, int)>
        {
            (-3, -3),
            (-3, -5),
            (-2, -6),
            ( 2, -4),
            ( 2, -6),
            ( 3,  3),
            ( 3, -1),
            ( 3, -3),
            ( 3, -5),
        };

        // Outer loop: bottom to top
        for (int j = -6; j <= 4; j++)
        {
            // Inner loop: left to right conditionally
            for (int i = -3; i <= 3; i++)
            {
                (int, int) tileVec = (i, j);
                if (!excludedTiles.Contains(tileVec))
                {
                    float newX = centerX + 1.5f * tileWidth * i;
                    float newZ = centerY + 0.75f * tileHeight * j;
                    Vector3 newPosition = new Vector3(newX, 0, newZ);

                    if ((j % 2 == 0 && i % 2 == 0) || (j % 2 != 0 && i % 2 != 0))
                    {
                        InstantiateAndPopulateTile(tile, rotation, newPosition, new Color(0.647f, 0.165f, 0.165f)); // Brown
                    }
                }
            }
        }
    }


    /// <summary>
    /// Initializes the tile grid by calculating dimensions from a sample tile and
    /// instantiating white, black, and brown tiles in their respective positions.
    /// </summary>
    /// <remarks>
    /// Temporarily instantiates a tile to determine its size, then calculates center
    /// positions for each tile group. Tiles are placed using specific instantiation
    /// functions for each color. Temporary tile is destroyed after size is measured.
    /// </remarks>
    void Start()
    {
        // Save prefab tile's dimensions
        GameObject tempTile = Instantiate(tileWhite);
        Vector3 tileSize = tempTile.GetComponent<Renderer>().bounds.size;
        float tileX = tileSize.z;
        float tileZ = tileSize.x;
        Destroy(tempTile); // Clean up

        // Center points
        float whiteCenterX = -tileX / 2;
        float whiteCenterZ = 0.25f;
        float blackCenterX = 0;
        float blackCenterZ = -tileZ / 2;
        float brownCenterX = tileX / 2;
        float brownCenterZ = 0.25f;

        InstantateWhiteTiles(tileWhite, whiteCenterX, whiteCenterZ, tileX, tileZ);
        InstantateBlackTiles(tileBlack, blackCenterX, blackCenterZ, tileX, tileZ);
        InstantateBrownTiles(tileBrown, brownCenterX, brownCenterZ, tileX, tileZ);

        Debug.Log("Piece instantiation complete");
    }

}