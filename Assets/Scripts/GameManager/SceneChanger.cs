using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;

public class SceneChanger : MonoBehaviour
{
    public void Start()
    {
        MatchingSceneState();
        if (GameManager.instance.playState == PlayState.InPlay) SoundManager.instance.PlaySound("inGameBGM");
        else if (GameManager.instance.playState == PlayState.Title) SoundManager.instance.PlaySound("titleBGM");
        else if (GameManager.instance.playState == PlayState.Lobby) SoundManager.instance.PlaySound("lobbyBGM");
    }
    public static void ToTitle()
    {
        if (GameManager.instance.playState != PlayState.Title)
        {
            Time.timeScale = 1;
            GameManager.instance.playState = PlayState.Title;
            SceneManager.LoadScene("1.TitleScene");
            SoundManager.instance.BGMChannel.Stop();
            SoundManager.instance.PlaySound("titleBGM");
        }
    }

    public void ToLobby()
    {
        if (GameManager.instance.playState != PlayState.Lobby)
        {
            Time.timeScale = 1;
            GameManager.instance.playState = PlayState.Lobby;
            GameManager.Init();
            SoundManager.instance.BGMChannel.Stop();
            SoundManager.instance.PlaySound("lobbyBGM");
            SceneManager.LoadScene("2.PlayScene");
        }
    }

    public static void ToPlay()
    {
        if (GameManager.instance.playState != PlayState.InPlay)
        {
            Time.timeScale = 1;
            SoundManager.instance.BGMChannel.Stop();
            SoundManager.instance.PlaySound("inGameBGM");
            GameManager.instance.playState = PlayState.InPlay;
        }
    }

    public static void ToGameOver()
    {
        if (GameManager.instance.playState != PlayState.GameOver)
        {
            Time.timeScale = 1;
            GameManager.instance.playState = PlayState.GameOver;
            SceneManager.LoadScene("3.GameOverScene");
        }
    }

    public static void ToClear()
    {
        if(GameManager.instance.playState != PlayState.Clear)
        {
            Time.timeScale = 1;
            GameManager.instance.playState = PlayState.Clear;
            SceneManager.LoadScene("4.ClearScene");
        }
    }

    public static string GetSceneName()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        return sceneName;
    }

    public static void MatchingSceneState()
    {
        string sceneName = GetSceneName();
        switch (sceneName)
        {
            case "1.TitleScene":
                GameManager.instance.playState = PlayState.Title;
                break;
            case "2.PlayScene":
                GameManager.instance.playState = PlayState.Lobby;
                break;
            case "3.GameOverScene":
                GameManager.instance.playState = PlayState.GameOver;
                break;
            case "4.ClearScene":
                GameManager.instance.playState = PlayState.Clear;
                break;
        }
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else 
    Application.Quit();
#endif
    }

    public void DebugButton()
    {
        Debug.Log(GameManager.instance.playState.ToString());
    }
}
