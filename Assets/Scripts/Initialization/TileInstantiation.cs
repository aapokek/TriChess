using UnityEngine;
using System.Collections.Generic;

public class TileInstantiation : MonoBehaviour
{
    public GameObject tileWhite;
    public GameObject tileBlack;
    public GameObject tileBrown;

    public Dictionary<Vector3Int, Tile> tileMap = new Dictionary<Vector3Int, Tile>();


    /// <summary>
    /// Instantiates a tile prefab at a given world position with specified rotation and color, 
    /// assigns it a cube position, and adds it to the tile map.
    /// </summary>
    /// <param name="tilePrefab">The tile prefab to instantiate.</param>
    /// <param name="rotation">The rotation to apply to the tile object.</param>
    /// <param name="wordlPos">The world coordinates where the tile will be placed.</param>
    /// <param name="colour">The color to assign to the tile.</param>
    /// <remarks>
    /// Logs an error if the instantiated prefab does not contain a Tile component.
    /// </remarks>
    private void InstantiateAndPopulateTile(GameObject tilePrefab, Quaternion rotation, Vector3 
        wordlPos, Color colour)
    {
        Vector3Int cubePos = Utils.WorldToCubePosition(wordlPos);
        // TODO: Add Parent Transform.
        GameObject newTileObj = Instantiate(tilePrefab, wordlPos, rotation);
        Tile newTile = newTileObj.GetComponent<Tile>();
        if (newTile != null)
        {
            newTile.CubePosition = cubePos;
            newTile.TileColour = colour;
            newTile.IsOccupied = false; // The tile starts unoccupied
            tileMap[cubePos] = newTile;

            Debug.Log($"Added a tile with world position of {wordlPos} and cube position " +
                $"of {cubePos} to the tile map");
        }
        else
        {
            Debug.LogError("The instantiated tile prefab does not have a Tile component " +
                "attached.");
        }
    }


    /// <summary>
    /// Instantiates tiles of a specified type (white, brown, or black) in a hexagonal grid 
    /// pattern. Tiles are placed based on a center position, tile dimensions, and a set of 
    /// excluded coordinates. Only tiles with matching parity (even or odd) for row and column 
    /// indices are instantiated.
    /// </summary>
    /// <param name="tile">The GameObject representing the tile to instantiate.</param>
    /// <param name="centerX">The x-coordinate of the grid's center in world space.</param>
    /// <param name="centerY">The z-coordinate of the grid's center in world space.</param>
    /// <param name="tileWidth">The width of a single tile, used for calculating x positions.
    /// </param>
    /// <param name="tileHeight">The height of a single tile, used for calculating z positions.
    /// </param>
    private void InstantateTiles(GameObject tile, float centerX, float centerY, float tileWidth, 
        float tileHeight)
    {
        // Correct the rotation of tiles
        Quaternion rotation = Quaternion.Euler(0, 30, 0);

        if (tile == tileWhite)
        {
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
                            InstantiateAndPopulateTile(tile, rotation,
                                newPosition, GameConstants.WHITE);
                        }
                    }
                }
            }
        }

        else if (tile == tileBrown)
        {
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
                            InstantiateAndPopulateTile(tile, rotation,
                                newPosition, GameConstants.BROWN);
                        }
                    }
                }
            }
        }

        else if (tile == tileBlack)
        {
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
                            InstantiateAndPopulateTile(tile, rotation,
                                newPosition, GameConstants.BLACK);
                        }
                    }
                }
            }
        }

        else
        {
            Debug.LogError("Unknown tile GameObject");
        }
    }

    /// <summary>
    /// Initializes the tile grid by calculating dimensions from a sample tile and instantiating 
    /// white, black, and brown tiles in their respective positions.
    /// </summary>
    /// <remarks>
    /// Temporarily instantiates a tile to determine its size. Temporary tile is destroyed after 
    /// the size is measured. Center positions are are calculated for each tile group separately. 
    /// Tiles are instantiated using specific rules for each color.
    /// </remarks>
    void Start()
    {
        // Save prefab tile's dimensions
        GameObject tempTile = Instantiate(tileWhite);
        Vector3 tileSize = tempTile.GetComponent<Renderer>().bounds.size;
        float tileWidth = tileSize.z;
        float tileHeight = tileSize.x;
        Destroy(tempTile); // Clean up

        // Center points
        float whiteCenterX = -tileWidth / 2;
        float whiteCenterZ = 0.25f;
        float blackCenterX = 0;
        float blackCenterZ = -tileHeight / 2;
        float brownCenterX = tileWidth / 2;
        float brownCenterZ = 0.25f;

        // White tiles
        InstantateTiles(tileWhite, whiteCenterX, whiteCenterZ, tileWidth, tileHeight);

        // Brown tiles
        InstantateTiles(tileBrown, brownCenterX, brownCenterZ, tileWidth, tileHeight);

        // Black tiles
        InstantateTiles(tileBlack, blackCenterX, blackCenterZ, tileWidth, tileHeight);

        Debug.Log("Tile instantiation complete");
    }

}