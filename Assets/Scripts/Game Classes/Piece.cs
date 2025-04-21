using UnityEngine;
using System.Collections.Generic;

// Base Piece class
public abstract class Piece : MonoBehaviour
{

    public Vector3Int CubePosition { get; set; }
    public Color PieceColour { get; set; }
    public bool IsCaptured { get; set; }

    // Abstract method to be implemented by each piece type
    public abstract List<Vector3Int> GetPossibleMoves();
}
