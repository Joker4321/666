using UnityEngine;
using System.Collections;
using UICore;
using UnityEngine.SceneManagement;
using System;
using System.Collections.Generic;

namespace UICore
{
    public class LoadSceneManager : Singleton<LoadSceneManager>
    {
        //直接加载场景的方法(不带有Loading)
        public void LoadScene(string sceneName, Action callBack = null)
        {
            //SceneManager.LoadScene(sceneName);
            SceneManager.LoadSceneAsync(sceneName);
            if (callBack != null)
            {
                callBack();
            }
        }

        //异步加载完成后所要执行的逻辑（回调）
        public Action callBack;
        //异步加载场景的名称
        public string asyncSceneName;
        //异步加载场景(带有Loading)
        public void LoadSceneAsync(string asyncSceneName, Action callBack = null)
        {
            this.asyncSceneName = asyncSceneName;
            this.callBack = callBack;
            SceneManager.LoadScene("LoadingScene");
            UIManager.Instance.ShowUI(E_UiId.LoadingUI);

        }
    }

}
