using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Gameplay4UI : MonoBehaviour
{
    public Game4Manager gameManager;
    public List<Bead> gameplayBeads;

    public void OnCheckAnswer()
    {
        if (gameplayBeads == null || gameManager == null)
        {
            Debug.LogError("gameplayBeads or gameManager is null!");
            return;
        }

        List<bool> playerRemoved = new List<bool>();

        foreach (Bead bead in gameplayBeads)
        {
            if (bead != null)
            {
                
                bead.gameObject.SetActive(false); 
                playerRemoved.Add(bead.IsDimmed());
            }
            else
            {
                Debug.LogWarning("find Bead is null!");
            }
        }

        bool result = gameManager.IsCorrect(playerRemoved);

        if (result)
        {
            Debug.Log("Correct!");
        }
        else
        {
            Debug.Log("Incorrect!");
        }
    }
}
