using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public SaveObject so;

    public TMP_Text text1;
    public TMP_Text text2;
    public TMP_Text text3;
    public TMP_Text text4;
    public TMP_Text text5;

    public void Awake()
    {
        LoadVar();
    }
    public void LoadVar()
    {
        SaveManager.Load();
        text1.text = "" + so.highScore1;
        text2.text = "" + so.highScore2;
        text3.text = "" + so.highScore3;
        text4.text = "" + so.highScore4;
        text5.text = "" + so.highScore5;
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void Level1()
    {
        SceneManager.LoadScene(1);
    }
    public void Level2()
    {
        SceneManager.LoadScene(2);
    }
    public void Level3()
    {
        SceneManager.LoadScene(3);
    }

    public void Level4()
    {
        SceneManager.LoadScene(4);
    }
    public void Level5()
    {
        SceneManager.LoadScene(5);
    }
}
