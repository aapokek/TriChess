using UnityEngine;
using System.Collections.Generic;

public class TileInstantiation : MonoBehaviour
{
    public GameObject tileWhite;
    public GameObject tileBlack;
    public GameObject tileBrown;

    public InstantiationSettings settings;

    private Dictionary<Vector3Int, Tile> tileMap = new Dictionary<Vector3Int, Tile>();
    private float whiteCenterX, whiteCenterZ;
    private float blackCenterX, blackCenterZ;
    private float brownCenterX, brownCenterZ;
    private float tileX, tileZ;

    
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

                    if (j % 2 == 0)
                    {
                        if (i % 2 == 0)
                        {
                            Instantiate(tile, newPosition, rotation);
                        }
                    }
                    else
                    {
                        if (Mathf.Abs(i) % 2 == 1)
                        {
                            Instantiate(tile, newPosition, rotation);
                        }
                    }
                }
            }
        }
    }


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

                    if (j % 2 == 0)
                    {
                        if (i % 2 == 0)
                        {
                            Instantiate(tile, newPosition, rotation);
                        }
                    }
                    else
                    {
                        if (Mathf.Abs(i) % 2 == 1)
                        {
                            Instantiate(tile, new Vector3(centerX + 1.5f * tileWidth * i,
                                0, centerY + 0.75f * tileHeight * j), rotation);
                        }
                    }
                }
            }
        }
    }


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

                    if (j % 2 == 0)
                    {
                        if (i % 2 == 0)
                        {
                            Instantiate(tile, newPosition, rotation);
                        }
                    }
                    else
                    {
                        if (Mathf.Abs(i) % 2 == 1)
                        {
                            Instantiate(tile, new Vector3(centerX + 1.5f * tileWidth * i,
                                0, centerY + 0.75f * tileHeight * j), rotation);
                        }
                    }
                }
            }
        }
    }
}