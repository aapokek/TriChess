using UnityEngine;

public class Tile : MonoBehaviour
{
    public Vector3Int CubePosition { get; set; }
    public Color TileColour { get; set; }
    public Piece OccupyingPiece { get; set; }
}
