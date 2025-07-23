using UnityEngine;

public class KeyboardInteraction : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log("T key was pressed");
            GameManager.Instance.iterateTurn();
        }
    }
}
