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

    //玩家是否能移动
    bool isStop=false;
	// Use this for initialization
	void Start () {
        playerRigidbody = GetComponent<Rigidbody>();
        layerMask = LayerMask.GetMask("Floor");
        anim = GetComponent<Animator>();
        MessageCenter.AddMessageListener(UICore.E_MessageType.Stop, StopMove);
    }
	
	// Update is called once per frame
	void Update () {
        if (!isStop)
        {
            //控制人物移动
            //获取输入 -1~1
            //获取水平   左右
            float h = Input.GetAxis("Horizontal");
            //垂直    上下
            float v = Input.GetAxis("Vertical");
            //Debug.Log(h);
            //Debug.Log(v);

            Vector3 vector = new Vector3(h, 0, v);


            //主角的位置加上要移动的距离
            playerRigidbody.MovePosition(transform.position + vector * Time.deltaTime * 5);
            Turn();
            Animation(h, v);
            Shoot();
        }
        
    }

    private void StopMove(object obj = null)
    {
        isStop = GetStop();
    }

    private bool GetStop()
    {
        return true;
    }

    //旋转
    void Turn()
    {
        //通过camera创建射线
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //绘制射线
        //Debug.DrawRay(ray.origin,ray.direction*1000);

        //保存射线碰撞到的物体信息
        RaycastHit floorHit;

        //发射射线
        if(Physics.Raycast(ray,out floorHit,1000, layerMask))
        {
            //让人物朝向鼠标的方向
            //向量减
            Vector3 playerToMouse = floorHit.point - transform.position;
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            playerRigidbody.MoveRotation(newRotation);

        }
       
    }
    //动画
    void Animation(float h, float v)
    {
        //判断玩家是否有按下了方向键，如果有按下，就要播放move动画，否则播放Idle动画
        bool isWalking = h != 0 || v != 0;
        anim.SetBool("IsWalking", isWalking);

    }
    //子弹
    void Shoot()
    {
        //0鼠标左键 1鼠标右键 2鼠标左键
        if (Input.GetMouseButtonDown(0))
        {
            //创建子弹
            GameObject go = Instantiate(bullet, bulletStart.position, bulletStart.rotation) as GameObject;
            //通过addforce给物体施加一个力，这个力是有方向
            go.GetComponent<Rigidbody>().AddForce(go.transform.forward * 1000);
        }
    }
}
