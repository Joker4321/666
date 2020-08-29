using UnityEngine;
using System.Collections;
using UICore;
using UnityEngine.UI;

public class MainUI : BaseUI
{
    private Button btn_Set;
    private Button btn_Pack;
    private Button btn_Notice;
    //滚动的字幕
    private Transform noticeText;
    private float noticeTextWidth;
    //滚动字幕的初始位置
    private Vector3 startPs;
    //字幕滚动的速度
    private float speed = 100;
    private RectTransform content;
    //content的宽度
    private float contentWidth;
    //移动结束的为位置点
    private Vector3 finishPs;
    protected override void InitUiOnAwake()
    {
        base.InitUiOnAwake();
        btn_Pack = GameTool.GetTheChildComponent<Button>(this.gameObject, "Btn_Pack");
        btn_Notice = GameTool.GetTheChildComponent<Button>(this.gameObject, "Btn_Notice");
        btn_Set = GameTool.GetTheChildComponent<Button>(this.gameObject, "Btn_Set");
        btn_Pack.onClick.AddListener(GotoPackPanel);
        btn_Notice.onClick.AddListener(GotoNoticePanel);
        btn_Set.onClick.AddListener(GotoSetPanel);
        noticeText = GameTool.FindTheChild(this.gameObject, "Txt_Notice");          
        content = GameTool.GetTheChildComponent<RectTransform>(this.gameObject, "Content");
        JudgeFinishPs();
    }
    protected override void InitDataOnAwake()
    {
        base.InitDataOnAwake();
        this.uiId = E_UiId.MainUI;
    }
    private void JudgeFinishPs()
    {
        //记录滚动字幕的初始位置
        startPs = noticeText.localPosition;
        //获取coent的宽度
        contentWidth = content.sizeDelta.x;
        //字体的宽度
        noticeTextWidth = noticeText.GetComponent<RectTransform>().sizeDelta.x;
        //计算滚动字幕滚动完成时的位置
        finishPs = new Vector3(startPs.x - noticeTextWidth - contentWidth, startPs.y, startPs.z);
    }
    void Update()
    {
        if (noticeText.localPosition.x < finishPs.x)
        {
            noticeText.localPosition = startPs;
        }
        else
        {
            noticeText.localPosition = new Vector3(noticeText.localPosition.x-speed*Time.deltaTime, startPs.y,startPs.z);
        }
    }
    private void GotoPackPanel()
    {
        UIManager.Instance.ShowUI(E_UiId.PackUI);
        //GameObject.Find("UIManager").GetComponent<UIManager>().ShowUI(E_UiId.PackUI);
    }
    private void GotoNoticePanel()
    {
        UIManager.Instance.ShowUI(E_UiId.NoticeUI);
    }
    private void GotoSetPanel()
    {
        UIManager.Instance.ShowUI(E_UiId.SetUI);
    }
}
