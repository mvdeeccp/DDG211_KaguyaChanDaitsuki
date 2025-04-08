using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Game4Manager : MonoBehaviour
{
    public GameObject modelPanel;
    public GameObject gameplayPanel;
    public List<GameObject> modelBeads; 
    public int hiddenBeadCount = 6; 

    private HashSet<int> hiddenIndexes = new HashSet<int>(); 

    void Start()
    {
        StartCoroutine(ShowModelThenPlay());
    }

    IEnumerator ShowModelThenPlay()
    {
        modelPanel.SetActive(true);            
        gameplayPanel.SetActive(false);        

        
        hiddenIndexes.Clear();

        
        while (hiddenIndexes.Count < hiddenBeadCount)
        {
            int rand = Random.Range(0, modelBeads.Count);
            hiddenIndexes.Add(rand);
        }

        
        foreach (int index in hiddenIndexes)
        {
            modelBeads[index].SetActive(false);
        }

        yield return new WaitForSeconds(3f); 

        modelPanel.SetActive(false);         
        gameplayPanel.SetActive(true);       
    }

    
    public bool IsCorrect(List<bool> playerRemoved)
    {
        for (int i = 0; i < modelBeads.Count; i++)
        {
            bool shouldRemove = hiddenIndexes.Contains(i); 
            bool playerDidRemove = playerRemoved[i];       

            if (shouldRemove != playerDidRemove)
            {
                return false; 
            }
        }

        return true; 
    }
}
