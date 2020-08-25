using UnityEngine;
using System.Collections;

public class InforData : Singleton<InforData>
{
    //所有金币的数量
    private int coinCount = 0;
    //所有红药水的数量
    private int redCount = 0;

    public void InitData()
    {
        if (!GameTool.HasKey("CoinCount"))
        {
            GameTool.SetInt("CoinCount",100);
        }
        coinCount = GameTool.GetInt("CoinCount");
        if (!GameTool.HasKey("RedCount"))
        {
            GameTool.SetInt("RedCount", 20);
        }
        redCount = GameTool.GetInt("RedCount");
    }
    //对外提供的，获取当前金币数的方法
    public int GetCoinCount()
    {
        return coinCount;
    }
    //对外提供的，获取当前红药水数量的方法
    public int GetRedCount()
    {
        return redCount;
    }
    //对外提供的,修改金币数量的方法
    public void EditorCoinCount(int newCoinCount)
    {
        coinCount = newCoinCount;
        GameTool.SetInt("CoinCount", newCoinCount);
    }
    //对外提供的，修改红药水的方法
    public void EditorRedCount(int newRedCount)
    {
        redCount = newRedCount;
        GameTool.SetInt("RedCount", newRedCount);
    }
}
