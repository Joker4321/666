using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 实现时间倒计时功能
/// </summary>

public class Practice000 : MonoBehaviour {


    //image控件
    Image image;
    //text控件
    Text text;
    //计时器
    float timer = 1f;
    //text上显示的数字
    int count = 5;

	// Use this for initialization
	void Start () {
        //得到Image控件
        image = GetComponent<Image>();
        //得到text控件
        text = GetComponentInChildren<Text>();

		//控件上显示的字符
        text.text = count.ToString();
        //image图片填充初始值为0
        image.fillAmount = 0;
    
    }
	
	// Update is called once per frame
	void Update () {
		//如果text上数字的值为0时
        if (count == 0)
        {
            //图片填充值为1
            image.fillAmount = 1;
            //text字体大小为20
            text.fontSize = 20;
            //显示开始
            text.text = "开始";
            return;
        }
        timer -= Time.deltaTime;
        //计时器每过一秒钟，count的值减去一
        if (timer <= 0)
        {
            timer = 1;
            count--;
            text.text = count.ToString();
        }
        image.fillAmount += Time.deltaTime;
        //如果图片填充值大于等于1时，把图片填充的值设为0；
        if (image.fillAmount >= 1)
        {
            image.fillAmount = 0;
        }

	}

}
