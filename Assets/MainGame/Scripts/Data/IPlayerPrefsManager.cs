using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerPrefsManager
{
    public int GetLevel();
    public void SetLevel(int levelPlus);
    public int GetScore();
    public void SetScore(int amount);
    public bool GetSettingSound();
    public void SetSettingSound(bool value);
    public bool GetBool(string key);
    public void SetGetBool(string key, bool value);
}
