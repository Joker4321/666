using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UICore;

public class Bullet : MonoBehaviour {

    int CoinCount;
    int Value=100;

    void Start () {
        ////单例模式的调用
        CoinCount = InforData.Instance.GetCoinCount();
        
    }
	
	// Update is called once per frame
	void Update () {

    }
    //如果要检测物体的碰撞，就使用碰撞检测函数
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            MessageCenter.SendMessage(E_MessageType.getCoin, CoinCount + Value);
            MessageCenter.SendMessage(E_MessageType.Pass);            
            Destroy(other.gameObject);            
        }
        Destroy(this.gameObject);
        
    }


}
