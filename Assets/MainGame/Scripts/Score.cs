using System;
using UnityEngine;

public class Score : MonoBehaviour
{
    public static Score Instance;

    public int score = 0;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    private void Start()
    {
        AddScore(0);
    }

    public void AddScore(int amount)
    {
        score += amount;
        if (score > PlayerPrefsManager.Instance.GetScore())
        {
            PlayerPrefsManager.Instance.SetScore(score);
        }
        UIManager.Instance.SetTextScore(score);
    }

    public void ResetScore()
    {
        score = 0;
    }
}
