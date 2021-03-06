﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class ScoreManager : MonoBehaviour
{
    public static int score;        // The player's score.
    Text text;                      // Reference to the Text component.


    void Awake()
    {
        // Set up the reference.
        text = GetComponent<Text>();
    }


    void Update()
    {
        // Set the displayed text to be the word "Score" followed by the score value.
        text.text = "" + score + "  ";
    }
}
