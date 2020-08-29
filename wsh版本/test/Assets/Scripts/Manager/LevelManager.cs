using UnityEngine;
using System.Collections;

public class LevelManager : UnitySingleton<LevelManager>
{
    private int currentMaxLevel = 0;
    //当前进入的关卡
    private int currentEnterLevel = 0;
    void Awake()
    {
        //GameTool.DeleteAll();//测试用
        if (!GameTool.HasKey("CurrentLevel"))
        {
            GameTool.SetInt("CurrentLevel", 1);
            currentMaxLevel = 1;
        }
        else
        {
            currentMaxLevel = GameTool.GetInt("CurrentLevel");
        }
    }
    public void SetCurrentEnterLevel(int current)
    {
        currentEnterLevel = current;
    }
    public int GetCurrentEnterLevel()
    {
        return currentEnterLevel;
    }
    public int GetCurrentMaxLevel()
    {
        return currentMaxLevel;
    }
    public void SetCurrentMaxLevel(int currentLevel)
    {
        this.currentMaxLevel = currentLevel;
        GameTool.SetInt("CurrentLevel", currentLevel);
    }
}
