using UnityEngine;

public class KeyboardInteraction : MonoBehaviour
{
    private GameManager gameManager;

    private void Start()
    {
        // Get GameManager
        gameManager = GameManager.Instance;
        if (gameManager == null)
        {
            Debug.LogError("GameManager not found!");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log("T key was pressed");
            gameManager.iterateTurn();
        }
    }
}
