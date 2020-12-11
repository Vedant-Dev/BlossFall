using Chronos;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public int CurrentScore;
    public int HighScore;
    public bool isDead = false;
    public Text scoreText,highScoreText, HIGHSCOREBOARD;

    public GameObject OnGamePanel, PausePanel, GameOverPanel,HighScorePanel, GameOverPanelInsider,TutPanel;

    public Text gameOverHighScore, gameOverScore;

    string _highscore = "_highscore";

    public GameObject gamesound;

    public GlobalClock clock;

    private void Start()
    {

        Time.timeScale = 1;
        CurrentScore = 0;
        HighScore = PlayerPrefs.GetInt(_highscore,0);
        highScoreText.text = "High Score - " + HighScore.ToString();
        scoreText.text = CurrentScore.ToString();
        gamesound = GameObject.FindGameObjectWithTag("GameSound");
        gamesound.AddComponent<Timeline>();
        Timeline timeline = gamesound.GetComponent<Timeline>();
        timeline.mode = TimelineMode.Global;
        timeline.globalClockKey = "Root";
        timeline.rewindable = true;
        timeline.recordingDuration = 7f;
        timeline.recordingInterval = .2f;

    }

    public void AddScore()
    {
        CurrentScore = CurrentScore + 1;
        scoreText.text = CurrentScore.ToString();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && !isDead)
        {
            StartCoroutine("Rewind");
        }
    }

    IEnumerator Rewind()
    {
        clock.localTimeScale = -0.5f;
        yield return new WaitForSeconds(1f);
        clock.localTimeScale = 1f;
    }

    public void RewindButtonPressed()
    {
        if (!isDead)
        {
            StartCoroutine("Rewind");
        }
    }

    public void RegisterHighScore(int score)
    {
        PlayerPrefs.SetInt(_highscore, score);
        HighScore = score;
    }

    public int GetScore()
    {
        return CurrentScore;
    }

    public int GetHighScore()
    {
        return HighScore;
    }

    public void SetDeath()
    {
        if (CurrentScore > HighScore)
        {
            HIGHSCOREBOARD.text = "New High Score - " + CurrentScore.ToString();
            HighScorePanel.SetActive(true);
            RegisterHighScore(CurrentScore);
        }
        else 
        {
            GameOverPanelInsider.SetActive(true);
        }
        isDead = true;
        OnGamePanel.SetActive(false);
        GameOverPanel.SetActive(true);

        gameOverHighScore.text = "High Score - " + HighScore.ToString();
        gameOverScore.text = "Current Score - " + CurrentScore.ToString();
    }

    public bool GetDeath()
    {
        return isDead;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void PauseGame() 
    {
        Time.timeScale = 0;
        OnGamePanel.SetActive(false);
        PausePanel.SetActive(true);
    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
        OnGamePanel.SetActive(true);
        PausePanel.SetActive(false);
    }

    public void ToMenu()
    {
        Destroy(GameObject.FindGameObjectWithTag("GameSound"));
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void EnterTut()
    {
        Time.timeScale = 0f;
        OnGamePanel.SetActive(false);
        TutPanel.SetActive(true);
    }
    public void ExitTut()
    {
        Time.timeScale = 1f;
        OnGamePanel.SetActive(true);
        TutPanel.SetActive(false);
    }
}
