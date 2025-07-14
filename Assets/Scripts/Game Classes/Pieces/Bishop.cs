using System.Collections.Generic;
using UnityEngine;

public class Bishop : Piece
{
    /// <summary>
    /// Determines the possible moves for this chess piece based on the rules of tri-chess.
    /// </summary>
    /// <returns>
    /// A tuple where:
    /// <list type="bullet">
    /// <item>
    /// <description>The first element is a list of <see cref="Vector3Int"/> representing the nearest positions
    /// to which the piece can legally move.</description>
    /// </item>
    /// <item>
    /// <description>The second element is a boolean indicating whether the piece is capable of indefinite
    /// moves (e.g., true for sliding pieces like Bishop, Rook, Queen; false for fixed-move pieces
    /// like Knight, King, Pawn).</description>
    /// </item>
    /// </list>
    /// </returns>
    public override (List<Vector3Int>, bool) GetPossibleMoves()
    {
        List<Vector3Int> allowedMovingDirections = new List<Vector3Int>();

        // "basis positions" in cube coordinates:
        Vector3Int oneBishopMoveUpLeft = forwardDirection - 2 * rightDirection + leftDirection;
        Vector3Int oneBishopMoveUp = 2 * forwardDirection - rightDirection - leftDirection;
        Vector3Int oneBishopMoveUpRight = forwardDirection + rightDirection - 2 * leftDirection;
        Vector3Int oneBishopMoveDownRight = -oneBishopMoveUpLeft;
        Vector3Int oneBishopMoveDown = -oneBishopMoveUp;
        Vector3Int oneBishopMoveDownLeft = -oneBishopMoveUpRight;

        allowedMovingDirections.Add(oneBishopMoveUpLeft);
        allowedMovingDirections.Add(oneBishopMoveUp);
        allowedMovingDirections.Add(oneBishopMoveUpRight);
        allowedMovingDirections.Add(oneBishopMoveDownRight);
        allowedMovingDirections.Add(oneBishopMoveDown);
        allowedMovingDirections.Add(oneBishopMoveDownLeft);

        return (allowedMovingDirections, true);
    }
}
