using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField] TMP_Text scoreLabel;
    [SerializeField] SettingsPopup settingsPopup;

    private int score;

    //Starts the program with the popup disabled
    void Start()
    {
        settingsPopup.Close();
        score = 0;
        scoreLabel.text = score.ToString();
    }
    //Adds the listener to broadcast when an enemy is hit
    private void OnEnable()
    {
        Messenger.AddListener(GameEvent.ENEMY_HIT, OnEnemyHit);
    }

    //Removes the listener
    private void OnDisable()
    {
        Messenger.RemoveListener(GameEvent.ENEMY_HIT, OnEnemyHit);
    }
    //If an enemy is hit, then the score will be increased by one.
    void OnEnemyHit()
    {
        score += 1;
        scoreLabel.text = score.ToString();
    }

    //Opens the popup when the setting is clicked
    public void OnOpenSettings()
    {
        settingsPopup.Open();
    }

    public void OnPointerDown()
    {
        Debug.Log("pointer down");
    }
}
