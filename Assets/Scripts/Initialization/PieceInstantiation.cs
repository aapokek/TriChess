using UnityEngine;
using System.Collections.Generic;

public class PieceInstantiation : MonoBehaviour
{
    // Game pieces
    public GameObject pawnWhite;
    public GameObject knightWhite;
    public GameObject bishopWhite;
    public GameObject rookWhite;
    public GameObject queenWhite;
    public GameObject kingWhite;

    public GameObject pawnBlack;
    public GameObject knightBlack;
    public GameObject bishopBlack;
    public GameObject rookBlack;
    public GameObject queenBlack;
    public GameObject kingBlack;

    public GameObject pawnBrown;
    public GameObject knightBrown;
    public GameObject bishopBrown;
    public GameObject rookBrown;
    public GameObject queenBrown;
    public GameObject kingBrown;

    public Dictionary<Vector3Int, Piece> pieceMap = new Dictionary<Vector3Int, Piece>();

    /// <summary>
    /// Instantiates a piece prefab at a given world position with specified rotation and color, 
    /// assigns it a cube position, and adds it to the piece map.
    /// </summary>
    /// <param name="piecePrefab">The piece prefab to instantiate.</param>
    /// <param name="rotation">The rotation to apply to the piece object.</param>
    /// <param name="cubePos">The cube coordinates where the piece will be placed.
    /// </param>
    /// <param name="heightCorrection">Contains fine tune adjustments to the piece location height.
    /// </param>
    /// <param name="colour">The color to assign to the piece.</param>
    /// <remarks>
    /// Logs an error if the instantiated prefab does not contain a Piece component.
    /// </remarks>
    private void InstantiateAndPopulatePiece(GameObject piecePrefab, Quaternion rotation, 
        Vector3Int cubePos, Vector3 heightCorrection, Color colour)
    {
        Vector3 worldPos = Utils.CubeToWorldPosition(cubePos);
        // TODO: Add Parent Transform.
        GameObject newPieceObj = Instantiate(piecePrefab, worldPos + heightCorrection, rotation);
        
        Piece newPiece = newPieceObj.GetComponent<Piece>();
        if (newPiece != null)
        {
            newPiece.CubePosition = cubePos;
            newPiece.PieceColour = colour;
            newPiece.IsCaptured = false;
            pieceMap[cubePos] = newPiece;

            Debug.Log($"Added a piece with world position of {worldPos} and cube position " +
                $"of {cubePos} to the piece map");

            // TODO: Make tiles where new piece is added ocupied.
        }
        else
        {
            Debug.LogError("The instantiated piece prefab does not have a Piece component " +
                "attached.");
        }
    }


    /// <summary>
    /// Instantiates game pieces in a specific arrangement starting from the given coordinate.
    /// Arranges pieces in three rows: 
    /// - Row 0: Rook, Queen, King, Rook
    /// - Row 1: Knight, Bishop, Bishop, Bishop, Knight
    /// - Row 2: Six Pawns
    /// </summary>
    /// <param name="startCoordinate">The starting position in cube coordinates.</param>
    /// <param name="rightDirection">The direction to move to the next piece in the row.</param>
    /// <param name="upDirection">The direction to move to the next row.</param>
    /// <param name="rotation">The rotation to apply to the pieces.</param>
    /// <param name="pawn">The pawn GameObject to instantiate.</param>
    /// <param name="knight">The knight GameObject to instantiate.</param>
    /// <param name="bishop">The bishop GameObject to instantiate.</param>
    /// <param name="rook">The rook GameObject to instantiate.</param>
    /// <param name="queen">The queen GameObject to instantiate.</param>
    /// <param name="king">The king GameObject to instantiate.</param>
    /// <param name="colour">The colour of instantiated pieces.</param>
    void InstantiatePieces(Vector3Int startCoordinate, Vector3Int rightDirection, Vector3Int 
        upDirection, Quaternion rotation, GameObject pawn, GameObject knight, GameObject bishop, 
        GameObject rook, GameObject queen, GameObject king, Color colour)
    {
        Vector3 heightCorrection = new Vector3(0, 0.25f, 0);

        // Iterate each row
        for (int rowNum = 0; rowNum < 3; rowNum++)
        {
            // Update instantiation position to the first position of the row.
            Vector3Int pos = startCoordinate + rowNum * upDirection;

            if (rowNum == 0) // First row: Rook, Queen, King, Rook
            {
                InstantiateAndPopulatePiece(rook, rotation, pos, heightCorrection, colour);

                // Proceed to the next tile
                pos += rightDirection;

                InstantiateAndPopulatePiece(queen, rotation, pos, heightCorrection, colour);

                // Proceed to the next tile
                pos += rightDirection;

                InstantiateAndPopulatePiece(king, rotation, pos, heightCorrection, colour);

                // Proceed to the next tile
                pos += rightDirection;

                InstantiateAndPopulatePiece(rook, rotation, pos, heightCorrection, colour);
            }
            else if (rowNum == 1) // Second row: Knight, Bishop, Bishop, Bishop, Knight
            {
                InstantiateAndPopulatePiece(knight, rotation, pos, heightCorrection, colour);

                // Proceed to the next tile
                pos += rightDirection;

                InstantiateAndPopulatePiece(bishop, rotation, pos, heightCorrection, colour);

                // Proceed to the next tile
                pos += rightDirection;

                InstantiateAndPopulatePiece(bishop, rotation, pos, heightCorrection, colour);

                // Proceed to the next tile
                pos += rightDirection;

                InstantiateAndPopulatePiece(bishop, rotation, pos, heightCorrection, colour);

                // Proceed to the next tile
                pos += rightDirection;

                InstantiateAndPopulatePiece(knight, rotation, pos, heightCorrection, colour);
            }
            else // Third row: Six Pawns
            {
                Vector3 pawnHeightCorrection = new Vector3(0, 0.2f, 0) + heightCorrection;

                for (int k = 0; k < 6; k++)
                {
                    InstantiateAndPopulatePiece(pawn, rotation, pos, pawnHeightCorrection, colour);

                    // Proceed to the next tile
                    pos += rightDirection;
                }
            }
        }
    }


    void Start()
    {
        // White pieces
        InstantiatePieces(new Vector3Int(-5, 1, 4), new Vector3Int(0, 1, -1), new
            Vector3Int(1, -1, 0), Quaternion.Euler(-90, -90, 0), pawnWhite, knightWhite,
            bishopWhite, rookWhite, queenWhite, kingWhite, GameConstants.WHITE);
        
        // Brown pieces
        InstantiatePieces(new Vector3Int(5, -6, 1), new Vector3Int(-1, 0, 1), new
            Vector3Int(0, 1, -1), Quaternion.Euler(-90, 30, 0), pawnBrown, knightBrown,
            bishopBrown, rookBrown, queenBrown, kingBrown, GameConstants.BROWN);
        
        // Black pieces
        InstantiatePieces(new Vector3Int(2, 4, -6), new Vector3Int(1, -1, 0), new
            Vector3Int(-1, 0, 1), Quaternion.Euler(-90, 150, 0), pawnBlack, knightBlack,
            bishopBlack, rookBlack, queenBlack, kingBlack, GameConstants.BLACK);

        Debug.Log("Piece instantiation complete");
    }
}