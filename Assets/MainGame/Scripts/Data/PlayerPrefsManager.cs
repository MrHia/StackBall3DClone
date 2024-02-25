using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsManager : IPlayerPrefsManager
{
    public static IPlayerPrefsManager Instance => new PlayerPrefsManager();
    private const string KeyLevel = "user_Level";
    private const string KeyScore = "user_Score";
    private const string KeySettingSound = "user_SettingSound";

    public int GetLevel()
    {
        return PlayerPrefs.GetInt(KeyLevel, 1);
    }
    public void SetLevel(int levelPlus)
    {
        PlayerPrefs.SetInt(KeyLevel, GetLevel() + levelPlus);
    }
    public int GetScore()
    {
        return PlayerPrefs.GetInt(KeyScore, 0);
    }
    public void SetScore(int amount)
    {
        PlayerPrefs.SetInt(KeyScore, amount);
    }

    public bool GetSettingSound()
    {
        return GetBool(KeySettingSound);
    }

    public void SetSettingSound(bool value)
    {
        SetGetBool(KeySettingSound, value);
    }

    public bool GetBool(string key)
    {
        var result = PlayerPrefs.GetInt(KeySettingSound, 1);
        return result != 0;
    }
    
    public void SetGetBool(string key, bool value)
    {
        if (value)
        {
            PlayerPrefs.SetInt(key, 1);
        }
        else
        {
            PlayerPrefs.SetInt(key, 0);
        }
    }
}
