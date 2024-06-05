using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using UnityEngine.UI;
using System.IO;

public class BoxTowerGameController : MonoBehaviour
{
    public static BoxTowerGameController instance;
    public BoxTowerBoxSpawn boxSpawner;
    public BoxTowerBox currentBox;
    public BoxTowerCameraFollow cameraFollow;
    public int score;
    public Text scoretxt;
    public int moveCount;
    public GameObject gameOverPanel;
    private bool gameOver = false;
    private bool isWrited;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        Time.timeScale = 1f;
        score = 0;
        isWrited = false;

    }

    // Update is called once per frame
    void Update()
    {
        GetMouseInput();
        if (gameOver)
        {
            Time.timeScale = 0f;
            gameOverPanel.SetActive(true);
            if (!isWrited)
            {
                SaveScoreToFile(score);
                isWrited = true;   
            }
        }
    }

    void GetMouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            currentBox.DropBox();
        }
    }
    public void SetGameOver()
    {
        gameOver = true;
    }
    public void SpawnNewBox()
    {
        Invoke("NextBox", 0f);
    }
    public void NextBox()
    {
        boxSpawner.SpawnBox();
    }
    public void addScore()
    {
        score++;
        scoretxt.text = "" + score;
    }
    public void MoveCamera()
    {
        moveCount++;
        if (moveCount == 2)
        {
            moveCount = 0;
            cameraFollow.targetPos.y += 2f;
        }
    }
    public void RestartGame()
    {
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void SaveScoreToFile(int score)
    {
        string path = "C://Users//serha//Desktop/score.txt";

        if (!File.Exists(path))
        {
            File.WriteAllText(path, "Game Scores\n");
        }

        File.AppendAllText(path, "Score: " + score.ToString() + "\n");
    }

}
