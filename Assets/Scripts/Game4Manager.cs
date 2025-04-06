using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Game4Manager : MonoBehaviour
{
    public GameObject[] beadPrefabs; 
    public Transform modelPanel, playerPanel;
    public Button checkButton;
    public int beadCount = 10;

    private List<Color> modelColors = new List<Color>();
    private List<Bead> playerBeads = new List<Bead>();

    void Start()
    {
        checkButton.gameObject.SetActive(false);
        StartCoroutine(ShowModelThenPlay());
    }

    IEnumerator ShowModelThenPlay()
    {
        for (int i = 0; i < beadCount; i++)
        {
            
            int prefabIndex = (i % beadPrefabs.Length);
            GameObject bead = Instantiate(beadPrefabs[prefabIndex], modelPanel);
            Bead beadScript = bead.GetComponent<Bead>();

            Color color = (i % 2 == 0) ? Color.red : Color.blue;
            beadScript.SetColor(color);
            modelColors.Add(color);
        }

        yield return new WaitForSeconds(3f);
        modelPanel.gameObject.SetActive(false);
        GeneratePlayerBeads();
        checkButton.gameObject.SetActive(true);
    }

    void GeneratePlayerBeads()
    {
        for (int i = 0; i < beadCount; i++)
        {
            
            int prefabIndex = Random.Range(0, beadPrefabs.Length);
            GameObject bead = Instantiate(beadPrefabs[prefabIndex], playerPanel);
            Bead beadScript = bead.GetComponent<Bead>();

            
            Color color = (Random.value > 0.5f) ? Color.red : Color.blue;
            beadScript.SetColor(color);
            playerBeads.Add(beadScript);
        }
    }

    public void CheckAnswer()
    {
        bool correct = true;

        for (int i = 0; i < beadCount; i++)
        {
            Color modelColor = modelColors[i];
            Color playerColor = playerBeads[i].GetComponent<Image>().color;
            bool isSame = playerColor == modelColor;
            bool isDimmed = playerBeads[i].IsDimmed();

            
            if ((!isSame && !isDimmed) || (isSame && isDimmed))
            {
                correct = false;
            }
        }

        if (correct)
        {
            Debug.Log("Correct!");
        }
        else
        {
            Debug.Log("Incorrect!");
        }
    }
}
