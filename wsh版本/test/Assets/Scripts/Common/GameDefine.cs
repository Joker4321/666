using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace UICore
{
    //窗体的ID
    public enum E_UiId
    {
        NullUI,
        MainUI,
        LevelUI,
        PackUI,
        InforUI,
        PlayUI,
        ExitUI,
        LoadingUI,
        NoticeUI,
        SetUI,
        LogoUI,
        OverUI,
        PassUI
    }
    //窗体的层级类型（父节点的类型）
    public enum E_UIRootType
    {
        KeepAbove,//保持在最前方的窗体
        Normal//普通窗体
    }
    //窗体的显示方式
    public enum E_ShowUIMode
    {
        //窗体显示出来的时候，不会去隐藏任何窗体
        DoNothing,
        //窗体显示出来的时候,会隐藏掉所有的普通窗体，但是不会隐藏保持在最前方的窗体
        HideOther,
        //窗体显示出来的时候,会隐藏所有的窗体，不管是普通的还是保持在最前方的
        HideAll
    }
    //消息类型
    public enum E_MessageType
    {
        ItemBeChoose,
        ItemBeSell,
        getCoin,
        Pass,
        NotPass,
        Stop,
        GameOver
    }
    //物品类型
    public enum E_GoodsType
    {
        Default,//全部类型
        Equipment,//装备
        Potions,//药水
        Rune,//符文
        Material//材料
    }

    public class GameDefine
    {
        public static Dictionary<E_UiId, string> dicPath = new Dictionary<E_UiId, string>()
        {
            { E_UiId.MainUI,"UIPrefab/"+"MainPanel"},
            { E_UiId.InforUI,"UIPrefab/"+"InforPanel"},
            { E_UiId.PackUI,"UIPrefab/"+"PackPanel"},
            { E_UiId.LevelUI,"UIPrefab/"+"LevelPanel"},
            { E_UiId.PlayUI,"UIPrefab/"+"PlayPanel"},
            { E_UiId.ExitUI,"UIPrefab/"+"ExitPanel"},
            { E_UiId.LoadingUI,"UIPrefab/"+"LoadingPanel"},
            { E_UiId.NoticeUI,"UIPrefab/"+"NoticePanel"},
            { E_UiId.SetUI,"UIPrefab/"+"SetPanel"},
            { E_UiId.LogoUI,"UIPrefab/"+"LogoPanel"},
            { E_UiId.OverUI,"UIPrefab/"+"OverPanel"},
            { E_UiId.PassUI,"UIPrefab/"+"PassPanel"},
        };
        public static Type GetUIScriptType(E_UiId uiId)
        {
            Type scriptType = null;
            switch (uiId)
            {
                case E_UiId.NullUI:
                    break;
                case E_UiId.MainUI:
                    scriptType = typeof(MainUI);
                    break;
                case E_UiId.LevelUI:
                    scriptType = typeof(LevelUI);
                    break;
                case E_UiId.PackUI:
                    scriptType = typeof(PackUI);
                    break;
                case E_UiId.InforUI:
                    scriptType = typeof(InforUI);
                    break;
                case E_UiId.PlayUI:
                    scriptType = typeof(PlayUI);
                    break;
                case E_UiId.ExitUI:
                    scriptType = typeof(ExitUI);
                    break;
                case E_UiId.LoadingUI:
                    scriptType = typeof(LoadingUI);
                    break;
                case E_UiId.NoticeUI:
                    scriptType = typeof(NoticeUI);
                    break;
                case E_UiId.SetUI:
                    scriptType = typeof(SetUI);
                    break;
                case E_UiId.LogoUI:
                    scriptType = typeof(LogoUI);
                    break;
                case E_UiId.OverUI:
                    scriptType = typeof(OverUI);
                    break;
                case E_UiId.PassUI:
                    scriptType = typeof(PassUI);
                    break;
                default:
                    break;
            }
            return scriptType;
        }
    }

}
