using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientClick : MonoBehaviour
{
    public string ingredientName; // A, B, C, D, E, F
    private IngredientManager manager;
    private SpriteRenderer sprite;

    void Start()
    {
        manager = FindObjectOfType<IngredientManager>();
        sprite = GetComponent<SpriteRenderer>();
    }

    void OnMouseDown()
    {
        manager.SelectIngredient(ingredientName, sprite);
    }
}
