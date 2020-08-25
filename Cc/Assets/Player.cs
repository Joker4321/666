 using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    Rigidbody playerRigidbody;
    //地板的层级
    int layerMask;

    Animator anim;

    public GameObject bullet;
    //子弹发射的位置
    public Transform bulletStart;
	void Start () {
        playerRigidbody = GetComponent<Rigidbody>();
        layerMask = LayerMask.GetMask("Floor");
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
	    //控制人物移动
        //获取输入 -1~1
        float h = Input.GetAxis("Horizontal");//水平
        float v = Input.GetAxis("Vertical");//垂直

        Vector3 vector = new Vector3(h, 0, v);

        //主角的位置加上要移动的距离
        playerRigidbody.MovePosition(transform.position + vector * Time.deltaTime * 5);
        Turn();
        Animation(h, v);
        Shoot();
	}
    //旋转
    void Turn()
    {
        //通过Camera创建涉嫌
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //绘制射线

        //保存射线碰撞到的物体信息
        RaycastHit floorHit;
        //发射
        if(Physics.Raycast(ray,out floorHit,1000,layerMask))
        {

            //向量减
            Vector3 playerToMouse = floorHit.point - transform.position;
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            playerRigidbody.MoveRotation(newRotation);
        }
    }
    //动画
    void Animation(float h,float v)
    {
        //判断玩家是否有往下方向键，若按下，就播放move动画，否则播放idle动画
        bool isWalking = h != 0 || v != 0;
        anim.SetBool("IsWalking", isWalking);
    }

    //子弹
    void Shoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //创建子弹
            GameObject go = Instantiate(bullet, bulletStart.position, bulletStart.rotation) as GameObject;
            //通过addforce给物体施加一个力，这个力是有方向
            go.GetComponent<Rigidbody>().AddForce(go.transform.forward*1000);
        }
    }
}
