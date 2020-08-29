using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UICore;

public class PassUI : BaseUI {

    private Button btn_Pass;
    private Text txt_CoinCount;

    // Use this for initialization
    protected override void Start () {

        btn_Pass = GameTool.GetTheChildComponent<Button>(this.gameObject, "Btn_Pass");
        btn_Pass.onClick.AddListener(PassLevel);

        txt_CoinCount = GameTool.GetTheChildComponent<Text>(this.gameObject, "Txt_CoinCount");
        txt_CoinCount.text = InforData.Instance.GetCoinCount().ToString();
        MessageCenter.AddMessageListener(E_MessageType.getCoin, getCoin);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
    private void PassLevel()
    {
        //Debug.Log("通过了！");
        //获取当前开启的最高关卡
        int maxLevel = LevelManager.Instance.GetCurrentMaxLevel();
        //当前进入的关卡
        int currentLevel = LevelManager.Instance.GetCurrentEnterLevel();
        if (currentLevel == maxLevel)
        {
            LevelManager.Instance.SetCurrentMaxLevel(++maxLevel);
        }
  
        ShowLevelPanel();
    }
    private void ShowLevelPanel()
    {
        LoadSceneManager.Instance.LoadSceneAsync("MainScene", delegate
        {
            UIManager.Instance.ShowUI(E_UiId.InforUI, false);
            UIManager.Instance.ShowUI(E_UiId.LevelUI, false);
        });
    }
    private void getCoin(object value)
    {
        int lastCount = (int)value;
        txt_CoinCount.text = lastCount.ToString();
        InforData.Instance.EditorCoinCount(lastCount);
    }
}
