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
    public bool isGameOver = false;
    public SaveObject so;

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
        isGameOver = true;
        so = SaveManager.Load();
        switch (levelNumber)
        {
            case 1:
                if (score > so.highScore1)
                {
                    so.highScore1 = score; SaveManager.Save(so);
                }
                break;
            case 2:
                if (score > so.highScore2)
                {
                    so.highScore1 = score; SaveManager.Save(so);
                }
                break;
            case 3:
                if (score > so.highScore3)
                {
                    so.highScore3 = score; SaveManager.Save(so);
                }
                break;
            case 4:
                if (score > so.highScore4)
                {
                    so.highScore1 = score; SaveManager.Save(so);
                }
                break;
            case 5:
                if (score > so.highScore5)
                {
                    so.highScore5 = score;
                    SaveManager.Save(so);
                }
                break;
        }
        SaveManager.Save(so);
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
            headerText.text = "Level " + levelNumber + ": Successful!";
            scoreText.text = "Score: " + score;
            buttonText.text = "Next Level";
            nextResetButton.onClick.AddListener(() => LoadNextLevel());
        }
    }

    public void LoadNextLevel()
    {
        SaveManager.Save(so);
        SceneManager.LoadScene("Level" + (levelNumber+1));
    }

    public void RestartLevel()
    {
        SaveManager.Save(so);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }

    public void GotoMenu()
    {
        SaveManager.Save(so);
        SceneManager.LoadScene("Menu");
        

    }
}
