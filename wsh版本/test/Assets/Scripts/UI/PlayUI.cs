using UnityEngine;
using System.Collections;
using UICore;
using UnityEngine.UI;

public class PlayUI : BaseUI
{
    //private Button btn_Pass;
    //private Button btn_NoPass;
    private Text txt_CoinCount;
    private Button btn_Exit;


    //计时
    int timeDown = 10;
    private Text txt_Time;
    protected override void InitUiOnAwake()
    {
        base.InitUiOnAwake();

        btn_Exit = GameTool.GetTheChildComponent<Button>(this.gameObject, "Btn_Exit");
        btn_Exit.onClick.AddListener(ShowExitPanel);

        MessageCenter.AddMessageListener(E_MessageType.Pass, Pass);

        txt_CoinCount = GameTool.GetTheChildComponent<Text>(this.gameObject, "Txt_CoinCount");
        txt_CoinCount.text = InforData.Instance.GetCoinCount().ToString();
        MessageCenter.AddMessageListener(E_MessageType.getCoin, AddCoin);

        //倒计时相关
        //timeDown = 10;
        //txt_Time = GameTool.GetTheChildComponent<Text>(this.gameObject, "Txt_Time");
        //txt_Time.text = "" + timeDown;
       // InvokeRepeating("Time_count", 1.0f, 1.0F);
        //MessageCenter.AddMessageListener(E_MessageType.GameOver, notPass);
    }
    protected override void InitDataOnAwake()
    {
        base.InitDataOnAwake();
        this.uiId = E_UiId.PlayUI;
        this.uiType.showMode = E_ShowUIMode.HideAll;

    }
    protected override void OnEnable()
    {
        //每次进入场景时重新更新一次UI
        //倒计时相关
        timeDown = 10;
        txt_Time = GameTool.GetTheChildComponent<Text>(this.gameObject, "Txt_Time");
        txt_Time.text = "" + timeDown;
        InvokeRepeating("Time_count", 1.0f, 1.0F);
    }
    private void Pass(object obj = null)
    {
        CancelInvoke("Time_count");
        UIManager.Instance.ShowUI(E_UiId.PassUI, true);

    }

    /*==============================消息发送通关需要用到的代码(备份不删)=================================*/
    //private void PassLevel(object obj = null)
    //{
    //    Debug.Log("通过了！");
    //    //获取当前开启的最高关卡
    //    int maxLevel = LevelManager.Instance.GetCurrentMaxLevel();
    //    //当前进入的关卡
    //    int currentLevel = LevelManager.Instance.GetCurrentEnterLevel();
    //    if (currentLevel == maxLevel)
    //    {
    //        LevelManager.Instance.SetCurrentMaxLevel(++maxLevel);
    //    }

    //    ShowLevelPanel();
    //}
    ////private void NoPassLevel()
    ////{
    ////    ShowLevelPanel();
    ////}
    //private void ShowLevelPanel()
    //{
    //    LoadSceneManager.Instance.LoadSceneAsync("MainScene", delegate
    //    {
    //        UIManager.Instance.ShowUI(E_UiId.InforUI, false);
    //        UIManager.Instance.ShowUI(E_UiId.LevelUI, false);
    //    });
    //}
    /*==============================消息发送通关需要用到的代码(备份不删)=================================*/
    private void ShowExitPanel()
    {
        UIManager.Instance.ShowUI(E_UiId.ExitUI);
    }
    private void AddCoin(object value)
    {
        int lastCount = (int)value;
        txt_CoinCount.text = lastCount.ToString();
        InforData.Instance.EditorCoinCount(lastCount);
    }

    void Time_count()
    {
        if (timeDown > 0)
        {
            timeDown--;
            txt_Time.text = "" + timeDown;
            //Debug.Log(timeDown);
        }
        else
        {
            CancelInvoke("Time_count");
            MessageCenter.SendMessage(E_MessageType.NotPass);
            UIManager.Instance.ShowUI(E_UiId.OverUI, true);
        }
    }

}
