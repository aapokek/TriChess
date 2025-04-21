using UnityEngine;

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


    void InstantiatePlayerPieces(Color pieceColour)
    {
        Vector3 heightCorrection = new Vector3(0, 0.25f, 0);

        if (pieceColour == GameConstants.WHITE)
        {
            Vector3Int startCoordinate = new Vector3Int(-5, 1, 4);

            Vector3Int rightDirection = new Vector3Int(0, 1, -1);
            Vector3Int upDirection = new Vector3Int(1, -1, 0);
            Quaternion rotation = Quaternion.Euler(-90, -90, 0);

            for (int i = 0; i < 3; i++)
            {
                Vector3Int rowStart = startCoordinate + i * upDirection;
                Vector3Int pos = rowStart;

                if (i == 0) // Rook, Queen, King, Rook
                {
                    Instantiate(rookWhite, TileInstantiation.Instance.CubeToWorldPosition(pos)
                        + heightCorrection, rotation);
                    pos += rightDirection; // Proceed to next the tile
                    Instantiate(queenWhite, TileInstantiation.Instance.CubeToWorldPosition(pos)
                        + heightCorrection, rotation);
                    pos += rightDirection; // Proceed to next the tile
                    Instantiate(kingWhite, TileInstantiation.Instance.CubeToWorldPosition(pos)
                        + heightCorrection, rotation);
                    pos += rightDirection; // Proceed to next the tile
                    Instantiate(rookWhite, TileInstantiation.Instance.CubeToWorldPosition(pos)
                        + heightCorrection, rotation);
                }
                else if (i == 1) // Knight, Bishop, Bishop, Bishop, Knight
                {
                    Instantiate(knightWhite, TileInstantiation.Instance.CubeToWorldPosition(pos)
                        + heightCorrection, rotation);
                    pos += rightDirection; // Proceed to next the tile
                    Instantiate(bishopWhite, TileInstantiation.Instance.CubeToWorldPosition(pos)
                        + heightCorrection, rotation);
                    pos += rightDirection; // Proceed to next the tile
                    Instantiate(bishopWhite, TileInstantiation.Instance.CubeToWorldPosition(pos)
                        + heightCorrection, rotation);
                    pos += rightDirection; // Proceed to next the tile
                    Instantiate(bishopWhite, TileInstantiation.Instance.CubeToWorldPosition(pos)
                        + heightCorrection, rotation);
                    pos += rightDirection; // Proceed to next the tile
                    Instantiate(knightWhite, TileInstantiation.Instance.CubeToWorldPosition(pos)
                        + heightCorrection, rotation);
                }
                else // Six pawns
                {
                    for (int k = 0; k < 6; k++)
                    {
                        Instantiate(pawnWhite, TileInstantiation.Instance.CubeToWorldPosition(pos)
                            + heightCorrection + new Vector3(0, 0.2f, 0), rotation);
                        pos += rightDirection; // Proceed to the next tile
                    }
                }
            }
        }

        else if (pieceColour == GameConstants.BROWN)
        {
            Vector3Int startCoordinate = new Vector3Int(5, -6, 1);

            Vector3Int rightDirection = new Vector3Int(-1, 0, 1);
            Vector3Int upDirection = new Vector3Int(0, 1, -1);
            Quaternion rotation = Quaternion.Euler(-90, 30, 0);

            for (int i = 0; i < 3; i++)
            {
                Vector3Int rowStart = startCoordinate + i * upDirection;
                Vector3Int pos = rowStart;

                if (i == 0) // Rook, Queen, King, Rook
                {
                    Instantiate(rookBrown, TileInstantiation.Instance.CubeToWorldPosition(pos)
                        + heightCorrection, rotation);
                    pos += rightDirection; // Proceed to next the tile
                    Instantiate(queenBrown, TileInstantiation.Instance.CubeToWorldPosition(pos)
                        + heightCorrection, rotation);
                    pos += rightDirection; // Proceed to next the tile
                    Instantiate(kingBrown, TileInstantiation.Instance.CubeToWorldPosition(pos)
                        + heightCorrection, rotation);
                    pos += rightDirection; // Proceed to next the tile
                    Instantiate(rookBrown, TileInstantiation.Instance.CubeToWorldPosition(pos)
                        + heightCorrection, rotation);
                }
                else if (i == 1) // Knight, Bishop, Bishop, Bishop, Knight
                {
                    Instantiate(knightBrown, TileInstantiation.Instance.CubeToWorldPosition(pos)
                        + heightCorrection, rotation);
                    pos += rightDirection; // Proceed to next the tile
                    Instantiate(bishopBrown, TileInstantiation.Instance.CubeToWorldPosition(pos)
                        + heightCorrection, rotation);
                    pos += rightDirection; // Proceed to next the tile
                    Instantiate(bishopBrown, TileInstantiation.Instance.CubeToWorldPosition(pos)
                        + heightCorrection, rotation);
                    pos += rightDirection; // Proceed to next the tile
                    Instantiate(bishopBrown, TileInstantiation.Instance.CubeToWorldPosition(pos)
                        + heightCorrection, rotation);
                    pos += rightDirection; // Proceed to next the tile
                    Instantiate(knightBrown, TileInstantiation.Instance.CubeToWorldPosition(pos)
                        + heightCorrection, rotation);
                }
                else // Six pawns
                {
                    for (int k = 0; k < 6; k++)
                    {
                        Instantiate(pawnBrown, TileInstantiation.Instance.CubeToWorldPosition(pos)
                            + heightCorrection + new Vector3(0, 0.2f, 0), rotation);
                        pos += rightDirection; // Proceed to the next tile
                    }
                }
            }
        }

        else if (pieceColour == GameConstants.BLACK)
        {
            Vector3Int startCoordinate = new Vector3Int(2, 4, -6);

            Vector3Int rightDirection = new Vector3Int(1, -1, 0);
            Vector3Int upDirection = new Vector3Int(-1, 0, 1);
            Quaternion rotation = Quaternion.Euler(-90, 150, 0);

            for (int i = 0; i < 3; i++)
            {
                Vector3Int rowStart = startCoordinate + i * upDirection;
                Vector3Int pos = rowStart;

                if (i == 0) // Rook, Queen, King, Rook
                {
                    Instantiate(rookBlack, TileInstantiation.Instance.CubeToWorldPosition(pos)
                        + heightCorrection, rotation);
                    pos += rightDirection; // Proceed to next the tile
                    Instantiate(queenBlack, TileInstantiation.Instance.CubeToWorldPosition(pos)
                        + heightCorrection, rotation);
                    pos += rightDirection; // Proceed to next the tile
                    Instantiate(kingBlack, TileInstantiation.Instance.CubeToWorldPosition(pos)
                        + heightCorrection, rotation);
                    pos += rightDirection; // Proceed to next the tile
                    Instantiate(rookBlack, TileInstantiation.Instance.CubeToWorldPosition(pos)
                        + heightCorrection, rotation);
                }
                else if (i == 1) // Knight, Bishop, Bishop, Bishop, Knight
                {
                    Instantiate(knightBlack, TileInstantiation.Instance.CubeToWorldPosition(pos)
                        + heightCorrection, rotation);
                    pos += rightDirection; // Proceed to next the tile
                    Instantiate(bishopBlack, TileInstantiation.Instance.CubeToWorldPosition(pos)
                        + heightCorrection, rotation);
                    pos += rightDirection; // Proceed to next the tile
                    Instantiate(bishopBlack, TileInstantiation.Instance.CubeToWorldPosition(pos)
                        + heightCorrection, rotation);
                    pos += rightDirection; // Proceed to next the tile
                    Instantiate(bishopBlack, TileInstantiation.Instance.CubeToWorldPosition(pos)
                        + heightCorrection, rotation);
                    pos += rightDirection; // Proceed to next the tile
                    Instantiate(knightBlack, TileInstantiation.Instance.CubeToWorldPosition(pos)
                        + heightCorrection, rotation);
                }
                else // Six pawns
                {
                    for (int k = 0; k < 6; k++)
                    {
                        Instantiate(pawnBlack, TileInstantiation.Instance.CubeToWorldPosition(pos)
                            + heightCorrection + new Vector3(0, 0.2f, 0), rotation);
                        pos += rightDirection; // Proceed to the next tile
                    }
                }
            }
        }

        else
        {
            Debug.Log("Invalid player colour (InstantiatePlayerPieces)");
        }
    }


    void Start()
    {
        InstantiatePlayerPieces(GameConstants.WHITE);
        InstantiatePlayerPieces(GameConstants.BROWN);
        InstantiatePlayerPieces(GameConstants.BLACK);

        Debug.Log("Piece instantiation complete");
    }
}