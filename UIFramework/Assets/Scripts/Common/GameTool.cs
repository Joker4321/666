using UnityEngine;
using System.Collections;
using System;
using UICore;
using UnityEngine.SceneManagement;

//游戏工具类，把一些会被外界经常用到的方法放在这个类里面
//为了方便外界调用，通常把里面的方法设置为静态方法
public class GameTool :MonoBehaviour
{
    //清理内存的方法（一般在切换场景的时候调用）
    public static void ClearMemory()
    {  
        //垃圾回收，不能去频繁的调用，应该在适当的情况下才去调用
        //因为垃圾回收会消耗很大的性能，频繁调用会导致卡顿
        GC.Collect();
        //卸载内存中没用的资源
        Resources.UnloadUnusedAssets();
    }
    //操作内存，数据持久化（PlayerPrefs）
    //判断系统内存里面是否有某个键
    public static bool HasKey(string key)
    {
        return PlayerPrefs.HasKey(key);
    }
    //去内存里面根据键来取值
    public static int GetInt(string key)
    {
        return PlayerPrefs.GetInt(key);
    }
    public static float GetFloat(string key)
    {
        return PlayerPrefs.GetFloat(key);
    }
    public static string GetString(string key)
    {
        return PlayerPrefs.GetString(key);
    }
    //往系统内存里面去存值
    public static void SetInt(string key,int value)
    {
        PlayerPrefs.SetInt(key,value);
    }
    public static void SetFloat(string key, float value)
    {
        PlayerPrefs.SetFloat(key, value);
    }
    public static void SetString(string key, string value)
    {
        PlayerPrefs.SetString(key, value);
    }
    //在内存里面删除指定的数据（键值对）
    public static void DeleteKey(string key)
    {
        PlayerPrefs.DeleteKey(key);
    }
    //删除内存里面所有的数据（键值对）
    public static void DeleteAll()
    {
        PlayerPrefs.DeleteAll();
    }
    //查找子物体
    public static Transform FindTheChild(GameObject goParnent,string chilidName)
    {
        //Debug.Log("父物体的名称"+goParnent.name);
        Transform searchTrans = goParnent.transform.Find(chilidName);
        if (searchTrans==null)
        {
            //遍历goParnent的所有的一级子物体
            foreach (Transform trans in goParnent.transform)
            {            
                //递归操作
                searchTrans = FindTheChild(trans.gameObject, chilidName);
                if (searchTrans!=null)
                {
                    return searchTrans;
                }
            }
        }
        return searchTrans;
    }
    //获取子物体上面的组件
    public static T GetTheChildComponent<T>(GameObject goParnent, string chilidName) where T:Component
    {
       Transform searchTrans=  FindTheChild(goParnent,chilidName);
        if (searchTrans != null)
        {
            return searchTrans.GetComponent<T>();
        }
        else
        {
             return null;
            //return default(T);
        }

    }
    //给子物体添加组件
    public static T AddTheChildComponent<T>(GameObject goParnent, string chilidName) where T : Component
    {
        Transform searchTrans = FindTheChild(goParnent, chilidName);
        if (searchTrans != null)
        {
            T[] arr = searchTrans.GetComponents<T>();
            for (int i = 0; i < arr.Length; i++)
            {
                //Destroy也是销毁，但是不是立刻销毁，当前帧结束前销毁
                // Destroy(arr[i]);
                //立刻销毁
                DestroyImmediate(arr[i],true);
            }
            return searchTrans.gameObject.AddComponent<T>();
        }
        else
        {
            return null;
        }
    }
    //添加子物体
    public static void AddChildToParent(Transform parentTrans,Transform childTrans)
    {
        childTrans.SetParent(parentTrans);
        childTrans.localPosition = Vector3.zero;
        childTrans.localScale = Vector3.one;
    }
    //记录游戏是否有初始化数据了
    public static bool isInitData = false;
  
}
