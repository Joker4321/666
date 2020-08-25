using System.Collections;
using System.Collections.Generic;
using UICore;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : BaseUI {

    private Button btn_Return;

    protected override void InitUiOnAwake()
    {
        base.InitUiOnAwake();
        btn_Return = GameTool.GetTheChildComponent<Button>(this.gameObject, "Btn_Return");
        btn_Return.onClick.AddListener(ReturnUI);
    }
    protected override void InitDataOnAwake()
    {
        base.InitDataOnAwake();
        //this.uiId = E_UiId.LogoUI;
        this.uiId = E_UiId.ShopUI;
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        UIManager.Instance.DestroyUI(this.uiId);
    }
    private void ReturnUI()
    {
        // GameObject.Find("UIManager").GetComponent<UIManager>().ReturnBeforeUI(beforeUiId);
        UIManager.Instance.ReturnBeforeUI(beforeUiId);
    }
}
