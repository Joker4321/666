using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class ChangePage : MonoBehaviour,IBeginDragHandler, IEndDragHandler
{

    //翻页的速度
    private float speed = 3f;
    //滚动列表
    private ScrollRect levelRect;
    //三个复选框的父物体
    private Transform allToggle;
    //三个复选框
    Toggle[] arrToggle;
    //true代表开始拖拽，false代表结束拖拽
    private bool isDrag = true;//不会进入翻页的逻辑
    //当前页
    private int currentPage = 0;//0 1 2
    //分页值
    private float[] pagePosArr = new float[] { 0,0.5f,1f};
    //开始拖拽的瞬间，滚动列表的滚动值
    private float scrollerValue = 0;
    void Awake ()
    {
        levelRect = this.gameObject.GetComponent<ScrollRect>();
        allToggle = GameTool.FindTheChild(this.transform.parent.gameObject, "AllToggle");
        arrToggle = new Toggle[allToggle.childCount];
        for (int i = 0; i < arrToggle.Length; i++)
        {
            arrToggle[i] = allToggle.GetChild(i).GetComponent<Toggle>();
            arrToggle[i].onValueChanged.AddListener(OnChangePage);

        }
    }
    //选中任意一个复选框，进行翻页
    private void OnChangePage(bool isCheck)
    {
       // Debug.Log("复选框被选中，要进行翻页");
        for (int i = 0; i < arrToggle.Length; i++)
        {
            if (arrToggle[i].isOn)
            {
                currentPage = i;
                break;
            }
        }
        isDrag = false;
    }

	void Update ()
    {
        if (isDrag==false)
        {
            //说明拖拽结束，要开始翻页了
            levelRect.horizontalNormalizedPosition = Mathf.Lerp(levelRect.horizontalNormalizedPosition, pagePosArr[currentPage], speed * Time.deltaTime);
            arrToggle[currentPage].isOn = true;
        }
	}
    //开始拖拽
    public void OnBeginDrag(PointerEventData eventData)
    {
       // Debug.Log("开始拖拽");
        isDrag = true;
        scrollerValue = levelRect.horizontalNormalizedPosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
       // Debug.Log("结束拖拽");
        isDrag = false;
        //拖拽后的位置-拖拽前的位置
        float offset = levelRect.horizontalNormalizedPosition - scrollerValue;
        if (offset>0)//滚动条往左移动，鼠标往右移动，向右翻页
        {
            if (currentPage< pagePosArr.Length-1)
            {
                currentPage++;
            }
          
        }
        else if (offset<0)//滚动条往右移动，鼠标往左移动，向左翻页
        {
            if (currentPage>0)
            {
                currentPage--;
            }
        }
    }

    void OnDisable()
    {
        currentPage = 0;
        levelRect.horizontalNormalizedPosition = 0;
        arrToggle[0].isOn = true;
        arrToggle[1].isOn = false;
        arrToggle[2].isOn = false;
    }
}
