using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public event Action enemyDestroyedEvent;

    [SerializeField]
    Text pointText;

    [SerializeField]
    GameObject lostUI;
    [SerializeField]
    GameObject winUI;
    [SerializeField]
    GameObject mainMenuButton;

    private int score = 0;

    [SerializeField]
    Text scoreboard;

    void Start()
    {
        enemyDestroyedEvent += GetPoint;
        enemyDestroyedEvent += CheckGameOver;

        //ClearScores();
        UpdateScoreboard();
    }

    void GetPoint()
    {
        score++;
        RefreshPoints();
    }

    public void EnemyDestroyed() => enemyDestroyedEvent?.Invoke();

    public void LoosePoints(int points)
    {
        score -= points;
        RefreshPoints();
    }

    void RefreshPoints() => pointText.text = "Points: " + score;

    void CheckGameOver()
    {
        if (GameObject.FindGameObjectsWithTag("Enemy").Length == 1)
            GameOver();
    }

    public void GameOver(bool won = true)
    {
        if (won)
            if (winUI)
                winUI.SetActive(true);
        else
            if (lostUI) 
                lostUI.SetActive(true);

        if (mainMenuButton)
            mainMenuButton.SetActive(true);

        InsertScore(score);
        UpdateScoreboard();
        if (scoreboard)
            scoreboard.gameObject.transform.parent.gameObject.SetActive(true);
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ClearScores()
    {
        PlayerPrefs.DeleteKey("Scoreboard");
    }

    private void UpdateScoreboard()
    {
        if (scoreboard)
        {
            String[] scores = PlayerPrefs.GetString("Scoreboard").Split(' ');

            scoreboard.text = "1. " + scores[0];
            for (int i = 1; i < 10 && i < scores.Length; i++)
            {

                scoreboard.text += "\n " + (i + 1) + ". " + scores[i];
            }
        }
    }

    private void InsertScore(int value)
    {
        if (scoreboard)
        {
            String[] scoresStrings = PlayerPrefs.GetString("Scoreboard").Split(' ');

            String scoresToSave;

            if (!(scoresStrings.Length == 1 && scoresStrings[0] == "")) //Clear PlayerPrefs key return array[0] == ""
            {
                int[] scores = Array.ConvertAll(scoresStrings, int.Parse);

                Array.Resize(ref scores, scores.Length + 1);

                scores[scores.Length - 1] = value;

                Array.Sort(scores);
                Array.Reverse(scores);

                scoresToSave = scores[0].ToString();
                for (int i = 1; i < 10 && i < scores.Length; i++)
                {

                    scoresToSave += " " + scores[i];
                }
            }
            else
                scoresToSave = value.ToString();

            PlayerPrefs.SetString("Scoreboard", scoresToSave);
        }
    }
}
