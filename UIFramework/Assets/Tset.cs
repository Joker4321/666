using UnityEngine;
using System.Collections;
//C#通过多线程来实现异步
//但是，Unity里面不支持多线程，所以使用协程来实现异步
//协程：协助程序（偷帧）
public class Tset : MonoBehaviour {

	void Start ()
    {
        StartCoroutine(Show());
         
        //ShowOther();
        //StartCoroutine(ShowInfor());
    }
    //IEnumerator枚举器
    private IEnumerator ShowInfor()
    {
        //yield return null;//或者yield return 0 下一帧再去执行（隔一阵）
        //yield return new WaitForSeconds(3);//隔秒
        //yield return new WaitForEndOfFrame();//等待摄像机和UI渲染完毕，显示之前
        //yield return StartCoroutine("xxxx");//等待xxxx这个协程执行完以后，再来执行
        //yield return WWW();//等待www操作完成后再执行
        for (int i = 0; i < 100; i++)
        {
            Debug.Log("Infor**********************"+i);
        }
        yield return null;
    }
    private void ShowOther()
    {
        for (int i = 0; i < 50; i++)
        {
            Debug.Log("Other###"+i);
        }
    }
	void Update ()
    {
	
	}
    private IEnumerator Show()
    {
        Debug.Log(1);
        yield return new WaitForSeconds(1);
        Debug.Log(2);
        yield return new WaitForSeconds(1);
        Debug.Log(3);
        yield return new WaitForSeconds(1);
        Debug.Log("*********************");
    }
}
