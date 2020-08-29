using UnityEngine;
using System.Collections;
using UICore;
using UnityEngine.UI;

public class OverUI : BaseUI {

    private Button btn_Main;
    private Button btn_Level;
    private Button btn_Pack;

    protected override void InitUiOnAwake()
    {
        base.InitUiOnAwake();
   
        btn_Main = GameTool.GetTheChildComponent<Button>(this.gameObject, "Btn_Main");
        btn_Level = GameTool.GetTheChildComponent<Button>(this.gameObject, "Btn_Level");
        btn_Pack = GameTool.GetTheChildComponent<Button>(this.gameObject, "Btn_Pack");
        btn_Main.onClick.AddListener(ShowMainPanel);
        btn_Level.onClick.AddListener(ShowLevelPanel);
        btn_Pack.onClick.AddListener(ShowPackPanel);

        MessageCenter.SendMessage(E_MessageType.Stop);

        
    }
    protected override void InitDataOnAwake()
    {
        base.InitDataOnAwake();
        this.uiId = E_UiId.ExitUI;
        this.uiType.showMode = E_ShowUIMode.DoNothing;
    }
    private void ShowMainPanel()
    {
        LoadSceneManager.Instance.LoadSceneAsync("MainScene", delegate {
            UIManager.Instance.ShowUI(E_UiId.MainUI, false);
            UIManager.Instance.ShowUI(E_UiId.InforUI, false);
        });
    }

    private void ShowLevelPanel()
    {

        LoadSceneManager.Instance.LoadSceneAsync("MainScene", delegate
        {
            UIManager.Instance.ShowUI(E_UiId.LevelUI, false);
            UIManager.Instance.ShowUI(E_UiId.InforUI, false);
        });
    }
    private void ShowPackPanel()
    {
        //LoadSceneManager.Instance.LoadScene("MainScene", delegate {
        //    UIManager.Instance.ShowUI(E_UiId.PackUI, false);
        //    UIManager.Instance.ShowUI(E_UiId.InforUI, false);
        //});
        LoadSceneManager.Instance.LoadSceneAsync("MainScene", delegate {
            UIManager.Instance.ShowUI(E_UiId.PackUI, false);
            UIManager.Instance.ShowUI(E_UiId.InforUI, false);
        });
    }
    //private void Close()
    //{
    //    UIManager.Instance.HideSingleUI(E_UiId.ExitUI);
    //}


}
