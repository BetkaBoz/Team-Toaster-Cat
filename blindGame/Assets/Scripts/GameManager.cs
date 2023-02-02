using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int score;
    public SceneManager sceneManager;
    public int levelNumber;
    public TMP_Text headerText;
    public TMP_Text scoreText;
    public TMP_Text buttonText;
    public Button nextResetButton;
    public GameObject scoreBoard;

    public void SubstractScore(int amount)
    {
        if (score - amount <= 0)
        {
            score = 0;
        }
        else
        {
            score -= amount;
        }
    }

    public void GameOver(bool isDead)
    {
        SaveObject saveObject = SaveManager.Load();
        switch (levelNumber)
        {
            case 1:
                if (score > saveObject.highScore1)
                {
                    saveObject.highScore1 = score;
                }
                break;
            case 2:
                if (score > saveObject.highScore2)
                {
                    saveObject.highScore2 = score;
                }
                break;
            case 3:
                if (score > saveObject.highScore3)
                {
                    saveObject.highScore3 = score;
                }
                break;
            case 4:
                if (score > saveObject.highScore4)
                {
                    saveObject.highScore4 = score;
                }
                break;
            case 5:
                if (score > saveObject.highScore5)
                {
                    saveObject.highScore5 = score;
                }
                break;
        }
        SaveManager.Save(saveObject);
        scoreBoard.SetActive(true);
        if (isDead)
        {
            headerText.text = "Level " + levelNumber + ": Failure!";
            scoreText.text = "Score: " + score;
            buttonText.text = "Restart Level";
            nextResetButton.onClick.AddListener(()=>RestartLevel());
        }
        else
        {
            headerText.text = "Level " + levelNumber + ": Failure!";
            scoreText.text = "Score: " + score;
            buttonText.text = "Next Level";
            nextResetButton.onClick.AddListener(() => LoadNextLevel());
        }
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene("Level" + (levelNumber+1));
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GotoMenu()
    {
        SceneManager.LoadScene("Menu");

    }
}
