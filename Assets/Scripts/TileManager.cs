using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using JetBrains.Annotations;

public class TileManager : MonoBehaviour
{
    [SerializeField] private Transform emptySpace = null;
    private Camera _camera;
    [SerializeField] private PuzzleTile[] tiles;
    private int emptySpaceIndex = 15;
    private bool _isFinished;
    [SerializeField] private GameObject endPanel;
    [SerializeField] private TextMeshProUGUI endPanelTimeText;

    private void Start()
    {
        _camera = Camera.main;
        endPanel.SetActive(false);
        Shuffle();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
            if (hit)
            {
                if (Vector2.Distance(emptySpace.position, hit.transform.position) < 2)
                {
                    Vector2 lastEmptySpacePosition = emptySpace.position;
                    PuzzleTile thisTile = hit.transform.GetComponent<PuzzleTile>();
                    emptySpace.position = thisTile.targetPosition;
                    thisTile.targetPosition = lastEmptySpacePosition;
                    int tileIndex = findIndex(thisTile);
                    tiles[emptySpaceIndex] = tiles[tileIndex];
                    tiles[tileIndex] = null;
                    emptySpaceIndex = tileIndex;
                }
            }
        }
        if (!_isFinished)
        {
            int correctTiles = 0;
            foreach (var a in tiles)
            {
                if (a != null)
                {
                    if (a.inRightPlace) correctTiles++;
                }
            }

            if (correctTiles == tiles.Length - 1)
            {
                _isFinished = true;
                endPanel.SetActive(true);
                var a = GetComponent<TimerScript>();
                a.StopTimer();
                endPanelTimeText.text = (a.minutes < 10?"0":"") + a.minutes + ":" + (a.seconds < 10?"0":"") + a.seconds;
            }
        }
    }

    public void Next()
    {
        SceneManager.LoadScene("Game5");
    }

    public void Shuffle()
    {
        if (emptySpaceIndex != 15)
        {
            var tileOn15LastPos = tiles[15].targetPosition;
            tiles[15].targetPosition = emptySpace.position;
            emptySpace.position = tileOn15LastPos;
            tiles[emptySpaceIndex] = tiles[15];
            tiles[15] = null;
            emptySpaceIndex = 15;
        }

        int invertion;
        do
        {
            for (int i = 0; i < 14; i++)
            {
                if (tiles[i] != null)
                {
                    var lastPos = tiles[i].targetPosition;
                    int randomIndex = Random.Range(0, 14);
                    tiles[i].targetPosition = tiles[randomIndex].targetPosition;
                    tiles[randomIndex].targetPosition = lastPos;
                    var tile = tiles[i];
                    tiles[i] = tiles[randomIndex];
                    tiles[randomIndex] = tile;
                }

            }

            invertion = GetInversion();
            Debug.Log("Puzzle Shuffled");

        } while (invertion%2 !=0);
    }
    public int findIndex(PuzzleTile ts)
    {
        for(int i = 0; i < tiles.Length ; i++)
        {
            if (tiles[i] != null)
            {
                if (tiles[i] == ts)
                {
                    return i;
                }
            }
        }
        return -1;
    }

    int GetInversion()
    {
        int inversionSum = 0;
        for(int i = 0;i < tiles.Length ; i++)
        {
            int thisTileInvertion = 0;
            for(int j = i; j< tiles.Length ; j++)
            {
                if (tiles[j] != null)
                {
                    if (tiles[i].number > tiles[j].number)
                    {
                        thisTileInvertion ++;
                    }
                }
            }
            inversionSum += thisTileInvertion;
        }
        return inversionSum;
    }
}
