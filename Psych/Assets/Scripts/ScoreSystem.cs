using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreSystem : MonoBehaviour
{
    private static ScoreSystem _instance;
    public static ScoreSystem Instance { get { return _instance; } }
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
            _instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public int[] scores;
    public int[] highScore;
    public int currentLevel;
    public int levelCount;
    public Text scoreText;   //DEBUG
    // Start is called before the first frame update
    void Start()
    {
        if (levelCount < 1)
            Debug.LogError("Score System Needs Level Count!");
        else
        {
            scores = new int[levelCount];
            highScore = new int[levelCount];
        }
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Current Score : " + GetCurrentScore().ToString();   //DEBUG
    }

    public void AddScore(int score)
    {
        scores[currentLevel] += score;
        if (scores[currentLevel] > highScore[currentLevel])
            highScore[currentLevel] = scores[currentLevel];
    }

    public void SetScore(int score, int level)
    {
        scores[level - 1] = score;
    }

    public int GetCurrentScore()
    {
        return scores[currentLevel];
    }

    public void GetCurrentScore(Text text)
    {
        text.text = scores[currentLevel].ToString();
    }

    public int GetHightScore(int level)
    {
        return highScore[level - 1];
    }

    public void ResetHighScore()
    {
        for (int i = 0; i < levelCount; i++)
        {
            highScore[i] = 0;
        }
    }

    public void SetCurrentLevel(int level)
    {
        currentLevel = level - 1;
        scores[level - 1] = 0;
    }
}
