using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCharacter : MonoBehaviour
{
    [SerializeField] int health;

    void Start()
    {
        health = 5;
    }

    public void Hurt(int damage) {
        health -= damage;
       if(health <= 0) health = 0;
        if (health == 0)
        {

            SceneManager.LoadScene("GameOver", LoadSceneMode.Single);

            Debug.Log("You Died ");

        }
        else {
                Debug.Log($"Health: {health}");
        }
    }

}
