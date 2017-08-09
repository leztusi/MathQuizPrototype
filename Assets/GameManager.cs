using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    public enum GameState
    {
        Menu, Playing, GameOver
    }
    public GameState State;
    public GameObject Menu_GO, Game_GO, GameOver_GO;

    public int Score, HighScore;
    public Text MenuHighScore_txt, Score_txt, HighScore_txt;
    public Text GameOver_Score_txt, GameOver_HighScore_txt;

    private void Awake()
    {
        instance = this;
        State = GameState.Menu;
    }

    private void Update()
    {
        //get highscore from playerprefs
        HighScore = PlayerPrefs.GetInt("HighScore");

        //display the high score in menu
        MenuHighScore_txt.text = "High Score: " + HighScore;

        //display the score in game
        Score_txt.text = "Score: " + Score;
        HighScore_txt.text = "High Score: " + HighScore;

        //display the score in game over
        GameOver_Score_txt.text = "Score: " + Score;
        GameOver_HighScore_txt.text = "High Score: " + HighScore;

        //just a simple switch can manage the game state
        switch (State)
        {
            case GameState.Menu:
                Menu_GO.SetActive(true);
                Game_GO.SetActive(false);
                GameOver_GO.SetActive(false);
                break;
            case GameState.Playing:
                Menu_GO.SetActive(false);
                Game_GO.SetActive(true);
                GameOver_GO.SetActive(false);
                break;
            case GameState.GameOver:
                Menu_GO.SetActive(false);
                Game_GO.SetActive(false);
                GameOver_GO.SetActive(true);
                break;
        }
    }

    //for button
    public void StartGame()
    {
        Score = 0;
        State = GameState.Playing;
    }

    public void AddScore() {
        Score++;
        if (Score > HighScore) {
            HighScore = Score;
            PlayerPrefs.SetInt("HighScore", HighScore);
        }
    }

}
