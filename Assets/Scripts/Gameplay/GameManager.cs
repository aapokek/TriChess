using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    // Singleton instance
    public static GameManager Instance { get; private set; }

    private int turnCounter;
    private turnStates turnState;
    private GameConstants.PLAYERS whoseTurn;

    private enum turnStates
    {
        PieceSelected,
        Idle
    }

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        // Initial gameplay counters
        turnCounter = 0;
        turnState = turnStates.Idle;
        whoseTurn = GameConstants.PLAYERS.WhitePlayer;
        Debug.Log("Game started, White player begins");
    }

    void Update()
    {
        
    }
    
    public void iterateTurn()
    {
        switch(whoseTurn)
        {
            case GameConstants.PLAYERS.WhitePlayer:
                whoseTurn = GameConstants.PLAYERS.BrownPlayer;
                Debug.Log("Now it's Brown's turn");
                break;

            case GameConstants.PLAYERS.BrownPlayer:
                whoseTurn = GameConstants.PLAYERS.BlackPlayer;
                Debug.Log("Now it's Black's turn");
                break;

            case GameConstants.PLAYERS.BlackPlayer:
                whoseTurn = GameConstants.PLAYERS.WhitePlayer;
                Debug.Log("Now it's White's turn");
                break;

            default:
                Debug.LogError("It seems to be no one's turn");
                break;
        }
    }
}
