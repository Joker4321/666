using UnityEngine;
using System.Collections;
using UICore;
using UnityEngine.SceneManagement;

public class InitGame : MonoBehaviour
{
    //Logo动画的播放时间
    private float playTime = 7f;
    //计时器
    private float timer = 0;
    //异步加载的
    AsyncOperation asyn;
    void Start()
    {

        GameObject canvas = Resources.Load<GameObject>("UIPrefab/Canvas");
        Instantiate(canvas);
        //加载所有的配置表到内存中
        DataController.Instance.LoadAllCfg();
        //初始化背包物品数据
        PackData.Instance.InitData();
        //初始化信息数据
        InforData.Instance.InitData();
        UIManager.Instance.ShowUI(E_UiId.LogoUI);
        StartCoroutine(LoadMainScene());
    }
    private IEnumerator LoadMainScene()
    {
        asyn = SceneManager.LoadSceneAsync("MainScene");
        asyn.allowSceneActivation = false;
        yield return asyn;
    }

    void Update()
    {
        if (asyn==null)
        {
            return;
        }
        timer += Time.deltaTime;
        if (asyn.progress==0.9f&& timer>= playTime)
        {
           // Debug.Log("切换到主场景");
            asyn.allowSceneActivation = true;
            UIManager.Instance.ShowUI(E_UiId.InforUI);
            UIManager.Instance.ShowUI(E_UiId.MainUI);
        }
        if (asyn.isDone)
        {           
            asyn = null;
        }
    }
}
