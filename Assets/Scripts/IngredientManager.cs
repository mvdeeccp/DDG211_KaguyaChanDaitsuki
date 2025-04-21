using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class IngredientManager : MonoBehaviour
{
    public List<SpriteRenderer> ingredientSprites; // Drag SpriteRenderer A–F in Inspector
    public TextMeshProUGUI recipeText;
    public Button checkButton;

    private List<string> ingredients = new List<string> { "A", "B", "C", "D", "E", "F" };
    private string target1, target2;
    private List<string> selectedIngredients = new List<string>();

    public float timeLimit = 5f;
    private float timer;
    private bool isTiming = false;
    public TextMeshProUGUI timerText;

    public List<GameObject> plantPots;
    private int correctCount = 0; 
    private int currentPotIndex = 0;

    void Start()
    {
        GenerateRandomRecipe();
        UpdatePotDisplay();
        checkButton.onClick.AddListener(CheckAnswer);
    }

    void Update()
    {
        if (isTiming)
        {
            timer -= Time.deltaTime;
            timerText.text = timer.ToString("F1");

            if (timer <= 0f)
            {
                timerText.text = "Time: 0.0";
                isTiming = false;
                Debug.Log("Time Out");
                GenerateRandomRecipe(); 
            }
        }
    }

    void GenerateRandomRecipe()
    {
        // Random 2 ingredient
        int index1 = Random.Range(0, ingredients.Count);
        int index2;
        do
        {
            index2 = Random.Range(0, ingredients.Count);
        } while (index2 == index1);

        target1 = ingredients[index1];
        target2 = ingredients[index2];

        recipeText.text = $"{target1}+{target2}";
        selectedIngredients.Clear();

        foreach (var sprite in ingredientSprites)
        {
            sprite.color = Color.white;
        }

        timer = timeLimit;
        isTiming = true;
    }

    public void SelectIngredient(string ingredientName, SpriteRenderer sprite)
    {
        if (selectedIngredients.Contains(ingredientName))
        {
            selectedIngredients.Remove(ingredientName);
            sprite.color = Color.white;
        }
        else if (selectedIngredients.Count < 2)
        {
            selectedIngredients.Add(ingredientName);
            sprite.color = new Color(1f, 1f, 1f, 0.5f); // Fade down
        }
    }

    void CheckAnswer()
    {
        if (!isTiming)
        {
            Debug.Log("Late");
            return;
        }

        if (selectedIngredients.Count != 2)
        {
            Debug.Log("Choose 2 ingredients");
            return;
        }

        // Order just have 2 ingredient
        if (selectedIngredients[0] == target1 && selectedIngredients[1] == target2)
        {
            Debug.Log("Correct!");
            correctCount++;

            if (correctCount % 3 == 0)
            {
                currentPotIndex++;

                if (currentPotIndex >= plantPots.Count)
                {
                    currentPotIndex = plantPots.Count - 1;
                    Debug.Log("Last!");
                }

                UpdatePotDisplay(); // Change
            }
        }
        
        else
        {
            Debug.Log("Wrong Try again!");
        }

        isTiming = false;
        GenerateRandomRecipe();
    }

    void UpdatePotDisplay()
    {
        
        foreach (var pot in plantPots)
        {
            pot.SetActive(false);
        }

        if (currentPotIndex < plantPots.Count)
        {
            plantPots[currentPotIndex].SetActive(true);
        }
    }
}
