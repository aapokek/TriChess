using UnityEngine;

public class Tile : MonoBehaviour
{
    public Vector3Int CubePosition { get; set; }
    public Color TileColour { get; set; }
    public bool IsOccupied { get; set; }
}
