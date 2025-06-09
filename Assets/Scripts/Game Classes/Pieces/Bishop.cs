using System.Collections.Generic;
using UnityEngine;

public class Bishop : Piece
{
    /// <summary>
    /// Determines the possible moves for this chess piece based on the rules of tri-chess.
    /// </summary>
    /// <returns>
    /// A tuple where:
    /// - The first element is a list of <see cref="Vector3Int"/> representing the positions to 
    /// which the piece can legally move.
    /// - The second element is a boolean indicating whether the piece is capable of indefinite
    /// moves (e.g., true for sliding pieces like Bishop, Rook, Queen; false for fixed-move pieces
    /// like Knight, King, Pawn).
    /// </returns>
    public override (List<Vector3Int>, bool) GetPossibleMoves()
    {
        // TODO: Add actual logic
        return (new List<Vector3Int>(), false);
    }
}
