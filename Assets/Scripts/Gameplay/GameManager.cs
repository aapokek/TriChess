using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    // Singleton instance
    public static GameManager Instance { get; private set; }

    private int turnCounter;
    private turnStates turnState;
    private GameConstants.PLAYERS whoseTurn;

    public GameConstants.PLAYERS getWhoseTurn()
    {
        return whoseTurn;
    }

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
        turnCounter = 1;
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
                turnCounter++;
                Debug.Log("Now it's Brown's turn");
                break;

            case GameConstants.PLAYERS.BrownPlayer:
                whoseTurn = GameConstants.PLAYERS.BlackPlayer;
                turnCounter++;
                Debug.Log("Now it's Black's turn");
                break;

            case GameConstants.PLAYERS.BlackPlayer:
                whoseTurn = GameConstants.PLAYERS.WhitePlayer;
                turnCounter++;
                Debug.Log("Now it's White's turn");
                break;

            default:
                Debug.LogError("It seems to be no one's turn");
                break;
        }
    }
}
