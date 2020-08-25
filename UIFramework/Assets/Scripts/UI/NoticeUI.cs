using UnityEngine;
using System.Collections;
using UICore;
using UnityEngine.UI;

public class NoticeUI : BaseUI
{
    private Button btn_Close;
    private Text txt_NoticeDetails;
    protected override void InitUiOnAwake()
    {
        base.InitUiOnAwake();
        btn_Close = GameTool.GetTheChildComponent<Button>(this.gameObject, "Btn_Close");
        txt_NoticeDetails = GameTool.GetTheChildComponent<Text>(this.gameObject, "Txt_NoticeDetails");
        btn_Close.onClick.AddListener(Close);
        ShowNotice();
    }
    protected override void InitDataOnAwake()
    {
        base.InitDataOnAwake();
        this.uiId = E_UiId.NoticeUI;
        this.uiType.showMode = E_ShowUIMode.DoNothing;
        this.uiType.uiRootType = E_UIRootType.KeepAbove;
    }
    private void ShowNotice()
    {
        string details = DataController.Instance.ReadCfg("NoticeDetails", 1, DataController.Instance.dicNotice);
        txt_NoticeDetails.text = details;
    }
    private void Close()
    {
        UIManager.Instance.HideSingleUI(E_UiId.NoticeUI);
    }
}
