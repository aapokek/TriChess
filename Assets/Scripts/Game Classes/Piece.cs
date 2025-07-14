using UnityEngine;
using System.Collections.Generic;

public abstract class Piece : MonoBehaviour
{
    public Vector3Int CubePosition { get; set; }
    public bool IsCaptured { get; set; }
    public GameConstants.PLAYERS whosePiece { get; set; }
    public abstract (List<Vector3Int>, bool) GetPossibleMoves();

    public Vector3Int forwardDirection { get; set; }
    public Vector3Int rightDirection { get; set; }
    public Vector3Int leftDirection { get; set; }
}
