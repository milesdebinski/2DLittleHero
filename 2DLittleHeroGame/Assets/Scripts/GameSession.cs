using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
  [SerializeField] int playerLives = 3;
  [SerializeField] int deathDelay = 3;
  [SerializeField] int score = 0;

  [SerializeField] TextMeshProUGUI livesText;
  [SerializeField] TextMeshProUGUI scoreText;

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
  void Start()
  {
    livesText.text = playerLives.ToString();
    scoreText.text = score.ToString();
  }

  public void AddToScore(int pointsToAdd)
  {
    score += 100;
    scoreText.text = score.ToString();

  }

  public void ProcessPlayerDeath()
  {
    if (playerLives > 1)
    {
      //   TakeLife();
      StartCoroutine(TakeLife());
    }
    else
    {
      StartCoroutine(ResetGameSession());
      //   ResetGameSession();
    }
  }

  IEnumerator TakeLife()
  {
    yield return new WaitForSecondsRealtime(deathDelay);
    playerLives--;
    int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    SceneManager.LoadScene(currentSceneIndex);
    livesText.text = playerLives.ToString();
    // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
  }

  IEnumerator ResetGameSession()
  {
    yield return new WaitForSecondsRealtime(deathDelay);
    FindObjectOfType<ScenePersist>().ResetScenePersist();
    SceneManager.LoadScene(0);
    Destroy(gameObject);
  }
}
