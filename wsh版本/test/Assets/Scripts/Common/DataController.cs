using UnityEngine;
using System.Collections;
using UICore;
using System.Collections.Generic;

public class DataController:Singleton<DataController>
{

    //存储物品配置表数据
    public Dictionary<string, Dictionary<string, string>> dicItem;
    //存储关卡配置表数据
    public Dictionary<string, Dictionary<string, string>> dicLevel;
    //存储公告配置表数据
    public Dictionary<string, Dictionary<string, string>> dicNotice;
    public void LoadAllCfg()
    {

        LoadLevelCfg();
        LoadItemCfg();
        LoadNoticeCfg();
    }

    //加载关卡配置表
    private void LoadLevelCfg()
    {
        ExcelData.LoadExcelFormCSV("LevelCfg", out dicLevel);
    }
    private void LoadItemCfg()
    {
        ExcelData.LoadExcelFormCSV("ItemCfg", out dicItem);
    }
    private void LoadNoticeCfg()
    {
        ExcelData.LoadExcelFormCSV("NoticeCfg", out dicNotice);
    }
    //对外提供的，读取字段值的方法
    public string ReadCfg(string keyName,int id, Dictionary<string, Dictionary<string, string>> dic)
    {
        return dic[keyName][id.ToString()];
    }
    //对外提供的，读取配置表数据量的方法（几行真实数据，要排除前3行）
    public int GetCfgCount(Dictionary<string, Dictionary<string, string>> dic)
    {
        int count = 0;
        foreach (KeyValuePair<string, Dictionary<string, string>> item in dic)
        {
            count = item.Value.Count;
            break;
        }
        return count;
    }

}
