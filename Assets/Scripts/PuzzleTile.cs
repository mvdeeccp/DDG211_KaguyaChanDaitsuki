﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PuzzleTile : MonoBehaviour
{
    public Vector3 targetPosition;
    private Vector3 correctPosition;
    private SpriteRenderer _sprite;
    public int number;
    public bool inRightPlace;

    private void Awake()
    {
        targetPosition = transform.position;
        correctPosition = transform.position;
        _sprite = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, targetPosition, 0.5f);
        if(targetPosition == correctPosition)
        {
            _sprite.color = Color.white;
            inRightPlace = true;
        }
        else
        {
            _sprite.color = Color.gray;
            inRightPlace = false;
        }
    }
}
