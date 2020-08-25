using UnityEngine;
using System.Collections;
using UICore;
using UnityEngine.UI;

public class PlayUI : BaseUI
{
    private Button btn_Pass;
    private Button btn_NoPass;

    private Button btn_Exit;
    protected override void InitUiOnAwake()
    {
        base.InitUiOnAwake();
        btn_Exit = GameTool.GetTheChildComponent<Button>(this.gameObject,"Btn_Exit");
        btn_Exit.onClick.AddListener(ShowExitPanel);

        btn_Pass = GameTool.GetTheChildComponent<Button>(this.gameObject, "Btn_Pass");
        btn_NoPass = GameTool.GetTheChildComponent<Button>(this.gameObject, "Btn_NoPass");
        btn_Pass.onClick.AddListener(PassLevel);
        btn_NoPass.onClick.AddListener(NoPassLevel);
    }
    protected override void InitDataOnAwake()
    {
        base.InitDataOnAwake();
        this.uiId = E_UiId.PlayUI;
        this.uiType.showMode = E_ShowUIMode.HideAll;
    }
    private void PassLevel()
    {
        //获取当前开启的最高关卡
        int maxLevel = LevelManager.Instance.GetCurrentMaxLevel();
        //当前进入的关卡
        int currentLevel = LevelManager.Instance.GetCurrentEnterLevel();
        if (currentLevel== maxLevel)
        {
            LevelManager.Instance.SetCurrentMaxLevel(++maxLevel);
        }
      
        ShowLevelPanel();
    }
    private void NoPassLevel()
    {
        ShowLevelPanel();
    }
    private void ShowLevelPanel()
    {
        LoadSceneManager.Instance.LoadSceneAsync("MainScene", delegate {
            UIManager.Instance.ShowUI(E_UiId.InforUI, false);
            UIManager.Instance.ShowUI(E_UiId.LevelUI, false);
        });
    }
    private void ShowExitPanel()
    {
        UIManager.Instance.ShowUI(E_UiId.ExitUI);
    }
}
