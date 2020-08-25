using UnityEngine;
using System.Collections;
using UICore;

public class LogoUI : BaseUI
{
    protected override void InitUiOnAwake()
    {
        base.InitUiOnAwake();
    }
    protected override void InitDataOnAwake()
    {
        base.InitDataOnAwake();
        this.uiId = E_UiId.ShopUI;
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        UIManager.Instance.DestroyUI(this.uiId);
    }
}
