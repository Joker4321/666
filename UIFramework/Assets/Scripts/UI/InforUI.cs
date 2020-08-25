using UnityEngine;
using System.Collections;
using UICore;
using UnityEngine.UI;

public class InforUI : BaseUI
{
    private Text txt_CoinCount;
    private Text txt_RedCount;
    protected override void InitUiOnAwake()
    {
        base.InitUiOnAwake();
        txt_CoinCount = GameTool.GetTheChildComponent<Text>(this.gameObject, "Txt_CoinCount");
        txt_RedCount = GameTool.GetTheChildComponent<Text>(this.gameObject, "Txt_RedCount");
        txt_CoinCount.text = InforData.Instance.GetCoinCount().ToString();
        txt_RedCount.text = InforData.Instance.GetRedCount().ToString();
        MessageCenter.AddMessageListener(E_MessageType.ItemBeSell, AddCoin);
    }
    protected override void InitDataOnAwake()
    {
        base.InitDataOnAwake();
        this.uiId = E_UiId.InforUI;
        this.uiType.showMode = E_ShowUIMode.DoNothing;
        this.uiType.uiRootType = E_UIRootType.KeepAbove;
    }
    private void AddCoin(object value)
    {
        //计算出售后一共有多少金币
        int lastCount = (int)value + InforData.Instance.GetCoinCount();
        txt_CoinCount.text = lastCount.ToString();
        InforData.Instance.EditorCoinCount(lastCount);
    }
}
