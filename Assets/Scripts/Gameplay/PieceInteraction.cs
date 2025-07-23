using UnityEngine;

public class PieceInteraction : MonoBehaviour
{
    private GameManager gameManager;
    private Piece piece;
    private Renderer pieceRenderer;
    private Color originalColor;
    private bool isHovering = false;

    /// <summary>
    /// Initializes the piece interaction by setting up references to GameManager, Piece, and 
    /// Renderer components, storing the original material color, and verifying the presence of a 
    /// Collider. Logs errors if any required components are missing.
    /// </summary>
    private void Start()
    {
        // Get GameManager
        gameManager = GameManager.Instance;
        if (gameManager == null)
        {
            Debug.LogError("GameManager not found!");
        }

        // Get Piece component
        piece = GetComponent<Piece>();
        if (piece == null)
        {
            Debug.LogError($"{gameObject.name} is missing a Piece component!");
        }

        // Get Renderer and store original color
        pieceRenderer = GetComponent<Renderer>();
        if (pieceRenderer == null)
        {
            Debug.LogError($"{gameObject.name} is missing a Renderer component!");
        }
        else
        {
            originalColor = pieceRenderer.material.color;
        }

        // Verify Collider
        if (!GetComponent<Collider>())
        {
            Debug.LogError($"{gameObject.name} is missing a Collider component!");
        }
    }

    /// <summary>
    /// Checks each frame if the piece is being hovered but no longer belongs to the current turns
    /// player. If the turn has changed, resets the hover state and reverts the piece's color to 
    /// its original state.
    /// </summary>
    private void Update()
    {
        // Check if the piece is being hovered but no longer belongs to the current player
        if (isHovering && piece != null && gameManager != null && piece.whosePiece != gameManager.
            getWhoseTurn())
        {
            Debug.Log($"Turn changed, resetting {gameObject.name} (Owner: {piece.whosePiece}, " +
                      $"Current turn: {gameManager.getWhoseTurn()})");
            isHovering = false;
            if (pieceRenderer != null)
            {
                pieceRenderer.material.color = originalColor;
            }
        }
    }

    /// <summary>
    /// Handles mouse hover over the piece. If the piece belongs to the current player and is not
    /// already hovered, highlights the piece green and logs the hover event.
    /// </summary>
    private void OnMouseOver()
    {
        // Validate selection possibilities based on the current turn
        if (!isHovering && piece != null && gameManager != null && piece.whosePiece == gameManager.
            getWhoseTurn())
        {
            Debug.Log($"Mouse is over {gameObject.name} (Owner: {piece.whosePiece}, " +
                      $"Current turn: {gameManager.getWhoseTurn()})");
            isHovering = true;

            // Change color to green if Renderer exists
            if (pieceRenderer != null)
            {
                pieceRenderer.material.color = Color.green;
            }
        }
    }

    /// <summary>
    /// Handles the mouse exiting the piece. If the piece was being hovered, reverts the piece's
    /// color to its original state and logs the exit event.
    /// </summary>
    private void OnMouseExit()
    {
        if (isHovering)
        {
            Debug.Log($"Mouse is no longer over {gameObject.name}");
            isHovering = false;

            // Restore original color if Renderer exists
            if (pieceRenderer != null)
            {
                pieceRenderer.material.color = originalColor;
            }
        }
    }
}