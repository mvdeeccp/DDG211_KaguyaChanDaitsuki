using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class IngredientManager : MonoBehaviour
{
    public List<SpriteRenderer> ingredientSprites; // Drag SpriteRenderer A–F in Inspector
    public TextMeshProUGUI recipeText;
    public Button checkButton;

    private List<string> ingredients = new List<string> { "A", "B", "C", "D", "E", "F" };
    private string target1, target2;
    private List<string> selectedIngredients = new List<string>();

    public float timeLimit = 7f; //max time
    private float timer; //timeRemaining
   
    private bool isTiming = false;
    public TextMeshProUGUI timerText;

    public List<GameObject> plantPots;
    private int correctCount = 0; 
    private int currentPotIndex = 0;

    public int maxRounds = 15;
    private int currentRound = 0;
    public GameObject endPanel;

    [SerializeField] private Image TimerImage; //timerLinaerImage

    private int targetNumber;   
    private int currentNumber = 0; 
    public TextMeshProUGUI numberText;

    public GameObject clickArea;


    void Start()
    {
        timeLimit = 10f;
        timer = timeLimit;
        GenerateRandomRecipe();
        UpdatePotDisplay();
        checkButton.onClick.AddListener(CheckAnswer);
    }

    void Update()
    {
        if (IsMouseInClickArea())
        {
            if (Input.GetMouseButtonDown(0))
            {
                currentNumber = (currentNumber + 1) % 4;
                UpdateNumberText();
            }

            if (Input.GetMouseButtonDown(1))
            {
                currentNumber = (currentNumber - 1 + 4) % 4;
                UpdateNumberText();
            }
        }

        

        if (isTiming)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
                //timer -= Time.deltaTime;
                timerText.text = timer.ToString("F1");
                TimerImage.fillAmount = timer / timeLimit;
            }
            

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
        if (currentRound >= maxRounds)
        {
            Debug.Log("Finshed!");
            endPanel.SetActive(true);
            return;
        }

        currentRound++;

        // Random 2 ingredient
        int index1 = Random.Range(0, ingredients.Count);
        int index2;
        do
        {
            index2 = Random.Range(0, ingredients.Count);
        } while (index2 == index1);

        target1 = ingredients[index1];
        target2 = ingredients[index2];
        targetNumber = Random.Range(0, 4);

        recipeText.text = $"{target1}+{target2} + {targetNumber}";

        currentNumber = 0;
        UpdateNumberText();

        //ResetIngredientColors();
        selectedIngredients.Clear();
        //StartCoroutine(RecipeTimer());

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

    void UpdateNumberText()
    {
        numberText.text = $"{currentNumber}";
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

        bool ingredientsCorrect = selectedIngredients[0] == target1 && selectedIngredients[1] == target2;
        bool numberCorrect = currentNumber == targetNumber;


        if (ingredientsCorrect && numberCorrect)
        {
            Debug.Log("Correct");

            correctCount++;

            if (correctCount % 3 == 0)
            {
                currentPotIndex++;
                if (currentPotIndex >= plantPots.Count)
                    currentPotIndex = plantPots.Count - 1;
                UpdatePotDisplay();
            }
        }

        else
        {
            Debug.Log("Wrong Try again!");
        }

        isTiming = false;
        GenerateRandomRecipe();
    }

    bool IsMouseInClickArea()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

        if (hit.collider != null && hit.collider.gameObject == clickArea.gameObject)
        {
            return true;
        }
        return false;
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

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void GoToNextLevel()
    {
        SceneManager.LoadScene("Game3");
    }
}
