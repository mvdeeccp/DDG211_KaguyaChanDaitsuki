using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bead : MonoBehaviour
{
    private Image image;
    private bool isDimmed = false;
    private Color originalColor;

    void Awake()
    {
        image = GetComponent<Image>();
        originalColor = image.color;
    }

    public void OnClick()
    {
        isDimmed = !isDimmed;

        if (isDimmed)
        {
      
            Color fadedColor = originalColor;
            fadedColor.a = 0.3f;
            image.color = fadedColor;
        }
        else
        {
           
            image.color = originalColor;
        }
    }

    public bool IsDimmed()
    {
        return isDimmed;
    }
}
