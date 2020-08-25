using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UICore;
using System.Collections.Generic;

public class LevelEntity : MonoBehaviour
{
    public int levelId = 0;
    private Button btn_Level;
    void Start()
    {
        btn_Level= GameTool.GetTheChildComponent<Button>(this.gameObject, "Btn_Level");
        btn_Level.onClick.AddListener(LoadScene);
    }
    private void LoadScene()
    {
        //直接加载
        //string sceneName = DataController.Instance.ReadCfg("SceneName", levelId, DataController.Instance.dicLevel);
        //LoadSceneManager.Instance.LoadScene(sceneName, OpenUI);
        LevelManager.Instance.SetCurrentEnterLevel(levelId);
        string sceneName = DataController.Instance.ReadCfg("SceneName", levelId, DataController.Instance.dicLevel);
        LoadSceneManager.Instance.LoadSceneAsync(sceneName, delegate {
            UIManager.Instance.ShowUI(E_UiId.PlayUI);
        });

    }
    //private void OpenUI()
    //{
    //    UIManager.Instance.ShowUI(E_UiId.PlayUI);
    //}
}
