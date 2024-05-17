using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitions : MonoBehaviour
{
    public string levelToLoad;

    public LevelManager levelManager;

    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }
    
    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.tag == "Player")
        {
            PlayerPrefs.SetInt("Coins", levelManager.coins);
            SceneManager.LoadScene(levelToLoad);
        } 
    }
}
