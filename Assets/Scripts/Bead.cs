using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bead : MonoBehaviour
{
    public Image image;
    private bool isDimmed = false;

    private Color originalColor;
    public Color dimmedColor = new Color(1, 1, 1, 0.3f); 

    private void Start()
    {
        originalColor = image.color;
    }

    public void OnClick()
    {
        isDimmed = !isDimmed;
        image.color = isDimmed ? dimmedColor : originalColor;
    }

    public bool IsDimmed()
    {
        return isDimmed;
    }

    public void SetColor(Color color)
    {
        image.color = color;
        originalColor = color;
    }
}
