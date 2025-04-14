using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PuzzleTile : MonoBehaviour
{
    [SerializeField] private Transform[] tiles; 
    [SerializeField] private TextMeshProUGUI resultText;
    [SerializeField] private Vector2 gridOrigin = Vector2.zero; 
    [SerializeField] private float cellSize = 1f; 
    [SerializeField] private int gridWidth = 4; 

    public void CheckPuzzle()
    {
        for (int i = 0; i < tiles.Length; i++)
        {
            Vector2 localPos = tiles[i].position - (Vector3)gridOrigin;
            int col = Mathf.RoundToInt(localPos.x / cellSize);
            int row = Mathf.RoundToInt(-localPos.y / cellSize);

            int currentIndex = row * gridWidth + col;
            int expectedIndex = i;

            if (currentIndex != expectedIndex)
            {
                resultText.text = "Wrong!";
                resultText.color = Color.red;
                return;
            }
        }

        resultText.text = "Pass!";
        resultText.color = Color.green;
    }
}
