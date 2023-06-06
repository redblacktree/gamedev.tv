using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLives = 3;
    [SerializeField] int coins = 0;
    [SerializeField] float gameOverDelay = 2f;
    [SerializeField] float playerDeathDelay = 2f;
    [SerializeField] AudioClip coinPickupSFX;

    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] TextMeshProUGUI coinsText;

    void Awake()
    {
        int numGameSessions = FindObjectsOfType<GameSession>().Length;
        if (numGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }        
    }

    void Update()
    {
        livesText.text = playerLives.ToString();
        coinsText.text = coins.ToString();
    }

    public void ProcessPlayerDeath()
    {
        if (playerLives <= 0)
        {
            StartCoroutine(GameOver());
        }
        else
        {
            StartCoroutine(TakeLife());
        }
    }

    public void AddCoin()
    {
        coins++;
    }

    void ResetGameSession()
    {
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }

    IEnumerator TakeLife()
    {
        playerLives--;
        yield return new WaitForSecondsRealtime(playerDeathDelay);
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    IEnumerator GameOver()
    {
        yield return new WaitForSecondsRealtime(gameOverDelay);
        //SceneManager.LoadScene("Game Over");
        ResetGameSession();
    }
}
