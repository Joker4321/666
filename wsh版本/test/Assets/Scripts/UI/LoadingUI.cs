using UnityEngine;
using System.Collections;
using UICore;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class LoadingUI : BaseUI
{
    private Slider slider_Progress;
    private Text txt_Progress;

    //异步加载的场景的名称
    public string asynSceneName = null;
   // public List<OpenUiType> listOpen =null;
    //异步加载的对象
    private AsyncOperation asyn;
    //加载场景的实际进度
    private int theProgress = 0;
    //用来显示的进度
    private int showProgress = 0;
    protected override void InitUiOnAwake()
    {
        base.InitUiOnAwake();
        slider_Progress = GameTool.GetTheChildComponent<Slider>(this.gameObject, "Slider_Progress");
        txt_Progress = GameTool.GetTheChildComponent<Text>(this.gameObject, "Txt_Progress");
        slider_Progress.onValueChanged.AddListener(ShowProgress);
    }
    protected override void InitDataOnAwake()
    {
        base.InitDataOnAwake();
        this.uiId = E_UiId.LoadingUI;
        this.uiType.showMode = E_ShowUIMode.HideAll;
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        asynSceneName = LoadSceneManager.Instance.asyncSceneName;
        //开始异步加载场景
        StartCoroutine(LoadScene());
        //Debug.Log("开始异步加载场景");
    }

    private IEnumerator LoadScene()
    {

        //asyn就是我们要加载的场景了
        // Debug.Log("要加载的场景"+ asynSceneName);
        yield return null;
        asyn = SceneManager.LoadSceneAsync(asynSceneName);
        asyn.allowSceneActivation = false;
        yield return asyn;
    }
    void Update()
    {
        if (asyn==null)
        {
            return;
        }
       // Debug.Log("*********************"+ asyn.allowSceneActivation);
        //asyn.progress范围0~1，但是要注意，这个值顶多检测到0.9
        if (asyn.progress < 0.9f)
        {
            //场景未加载完成
            theProgress = (int)asyn.progress*100;
        }
        else
        {
            //加载完成
            theProgress = 100;
        }
        if (showProgress< theProgress)
        {
            showProgress++;
        }
        //Debug.Log("showProgress ："+ showProgress);
        //让进度条走动起来
        slider_Progress.value = showProgress / 100f;
        if (showProgress == 100 )//&& asyn.isDone)
        {
            //场景加载完成，显示出来
            asyn.allowSceneActivation = true;
         
        }
        if (asyn.isDone)
        {
            if (LoadSceneManager.Instance.callBack != null)
            {
                LoadSceneManager.Instance.callBack();
            }
            asyn = null;
            slider_Progress.value = 0;
            showProgress = 0;
            theProgress = 0;
            GameTool.ClearMemory();
        }

    }
    //显示进度百分比
    private void ShowProgress(float value)
    {
        //Debug.Log("更新进度显示");
        txt_Progress.text = ((int)(value*100)).ToString() + "%";
    }
}
