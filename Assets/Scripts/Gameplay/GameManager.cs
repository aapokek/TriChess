using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int turnCounter;
    private turnStates turnState;
    private GameConstants.PLAYERS whoseTurn;
    
    private enum turnStates
    {
        PieceNotSelected,
        PieceSelected,
        Idle
    }

    private void Start()
    {
        // Initial gameplay counters
        turnCounter = 0;
        turnState = turnStates.PieceNotSelected;
        whoseTurn = GameConstants.PLAYERS.WhitePlayer;
    }

    void Update()
    {
        
    }
}
