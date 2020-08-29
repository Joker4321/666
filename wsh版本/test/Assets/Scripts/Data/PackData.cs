using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//背包数据类
public class PackData:Singleton<PackData>
{
    //用于存放物品数量的字典<物品的ID,该物品的数量>
    private Dictionary<int, int> packDataDic = new Dictionary<int, int>();
    //初始化背包数据的方法
    public void InitData()
    {
       // GameTool.DeleteAll();//测试用
        if (!GameTool.HasKey("PackData"))
        {
            GameTool.SetString("PackData", "1'0;2'30;3'10;4'20;5'60;6'0;7'0;8'0;9'0;10'0;11'0;12'0;13'0;14'0;15'0;16'0;17'0;18'0;19'0;20'0;21'0;22'0");
        }
        string data = GameTool.GetString("PackData");
        string[] arrData = data.Split(';');
        ToDictionary(arrData);
    }
    private void ToDictionary(string[] arr)
    {
        for (int i = 0; i < arr.Length; i++)
        {
            string[] theArr = arr[i].Split('\'');
            packDataDic.Add(int.Parse(theArr[0]),int.Parse( theArr[1]));
        }
    }
    //对外提供的，根据物品ID获取物品数量
    public int ReadCountById(int itemId)
    {
        if (packDataDic.ContainsKey(itemId))
        {
            return packDataDic[itemId];
        }
        return 0;
    }
    public void EditorItemCount(int itemId,int newCount)
    {
        if (packDataDic.ContainsKey(itemId))
        {
            packDataDic[itemId] = newCount;
        }
        SaveData();
    }
    private void SaveData()
    {
        int index = 0;
        string strData = "";
        Dictionary<int, int>.KeyCollection allKey = packDataDic.Keys;
        foreach (int key in allKey)
        {
            index++;
            strData += key + "'" + packDataDic[key];
            if (index!= packDataDic.Count)
            {
                strData += ";";
            }
        }
        GameTool.SetString("PackData", strData);
    }
}
