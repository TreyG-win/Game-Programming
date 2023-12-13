using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    //public int newScore;
    public TMP_Text scoreTxt;
    private int points;
    void Start()
    {
        scoreTxt = FindAnyObjectByType<TMP_Text>();
        points = RayShooter.totalScore;
    }
    void Update()
    {
        
        scoreTxt.text = points.ToString() + " points";
    }
}
