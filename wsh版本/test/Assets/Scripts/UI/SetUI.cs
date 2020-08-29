using UnityEngine;
using System.Collections;
using UICore;
using UnityEngine.UI;

public class SetUI : BaseUI
{
    private Button btn_Close;
    private Slider slider_AudioValue;
    private Toggle toggle_Mute;
    protected override void InitUiOnAwake()
    {
        base.InitUiOnAwake();
        btn_Close = GameTool.GetTheChildComponent<Button>(this.gameObject, "Btn_Close");
        slider_AudioValue = GameTool.GetTheChildComponent<Slider>(this.gameObject, "Slider_AudioValue");
        btn_Close.onClick.AddListener(Close);
        slider_AudioValue.onValueChanged.AddListener(ChangeAudio);
        toggle_Mute = GameTool.GetTheChildComponent<Toggle>(this.gameObject, "Toggle_Mute");
        toggle_Mute.onValueChanged.AddListener(ChangeMute);
       
    }
    protected override void InitDataOnAwake()
    {
        base.InitDataOnAwake();
        this.uiId = E_UiId.SetUI;
        this.uiType.showMode = E_ShowUIMode.DoNothing;
        this.uiType.uiRootType = E_UIRootType.KeepAbove;
    }
    protected override void InitOnStart()
    {
        base.InitOnStart();
        slider_AudioValue.value = AudioManager.Instance.GetAudioValue();
        toggle_Mute.isOn = AudioManager.Instance.GetMute();
    }
    private void Close()
    {
        UIManager.Instance.HideSingleUI(E_UiId.SetUI);
    }
    private void ChangeAudio(float value)
    {
        AudioManager.Instance.SetAudioValue(value);
    }
    private void ChangeMute(bool isMute)
    {
        AudioManager.Instance.SetMute(isMute);
    }
}
