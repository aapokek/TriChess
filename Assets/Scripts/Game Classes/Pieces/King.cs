using System.Collections.Generic;
using UnityEngine;

public class King : Piece
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

        // "basis positions" in cube coordinates, one bishop-like move:
        Vector3Int oneBishopMoveUpLeft = forwardDirection - 2 * rightDirection + leftDirection;
        Vector3Int oneBishopMoveUp = 2 * forwardDirection - rightDirection - leftDirection;
        Vector3Int oneBishopMoveUpRight = forwardDirection + rightDirection - 2 * leftDirection;
        Vector3Int oneBishopMoveDownRight = -oneBishopMoveUpLeft;
        Vector3Int oneBishopMoveDown = -oneBishopMoveUp;
        Vector3Int oneBishopMoveDownLeft = -oneBishopMoveUpRight;

        // one rook-like move:
        Vector3Int oneRookMoveLeftUp = forwardDirection - rightDirection;
        Vector3Int oneRookMoveRightUp = forwardDirection - leftDirection;
        Vector3Int oneRookMoveRight = rightDirection - leftDirection;
        Vector3Int oneRookMoveRightDown = -oneRookMoveLeftUp;
        Vector3Int oneRookMoveLeftDown = -oneRookMoveRightUp;
        Vector3Int oneRookMoveLeft = -oneRookMoveRight;

        allowedMovingDirections.Add(oneBishopMoveUpLeft); // bishop \,
        allowedMovingDirections.Add(oneRookMoveLeftUp); // rook \,
        allowedMovingDirections.Add(oneBishopMoveUp); // bishop |,
        allowedMovingDirections.Add(oneRookMoveRightUp); // rook ,/
        allowedMovingDirections.Add(oneBishopMoveUpRight); // bishop ,/
        allowedMovingDirections.Add(oneRookMoveRight); // rook ,-
        allowedMovingDirections.Add(oneBishopMoveDownRight); // bishop `\
        allowedMovingDirections.Add(oneRookMoveRightDown); // rook `\
        allowedMovingDirections.Add(oneBishopMoveDown); // bishop `|
        allowedMovingDirections.Add(oneRookMoveLeftDown); // rook /`
        allowedMovingDirections.Add(oneBishopMoveDownLeft); // bishop /`
        allowedMovingDirections.Add(oneRookMoveLeft); // rook -`

        return (allowedMovingDirections, false);
    }
}
