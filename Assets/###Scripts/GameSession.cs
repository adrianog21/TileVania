using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;    

public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLives = 3;
    [SerializeField] int scoreCount = 0;
    [SerializeField] Text livesText;
    [SerializeField] Text scoreText;

    
    private void Awake()
    {
        int numGameSessions = FindObjectsOfType<GameSession>().Length;
        if(numGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = scoreCount.ToString();
        livesText.text = playerLives.ToString();   
    }

    public void ProcessPlayer()
    {
        if(playerLives >1)
        {
            TakeLife();
        }
        else
        {
            ResetSession();
        }
    }

    public void AddToScore(int score)
    {
        scoreCount += score;
        scoreText.text = scoreCount.ToString();
    }

    private void ResetSession()
    {
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }

    private void TakeLife()
    {
        playerLives--;
        livesText.text = playerLives.ToString();
        var index = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(index);
    }
}
