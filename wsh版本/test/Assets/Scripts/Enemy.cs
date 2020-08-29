using UnityEngine;
using System.Collections;
using UICore;

public class Enemy : MonoBehaviour {

    NavMeshAgent nav;
    Transform player;
    //判断是否停止移动
    bool isStop=false;
	// Use this for initialization
	void Start () {
        nav = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player").transform;
        MessageCenter.AddMessageListener(E_MessageType.Stop, StopMove);
	}
	
	// Update is called once per frame
	void Update () {
        if (!isStop)
        {
            nav.SetDestination(player.position);
        }
        else
        {
            nav.Stop();
        }
        
	}
    private void StopMove(object obj = null)
    {
        isStop = getStop();
    }
    private bool getStop()
    {
        return true;
    }
}
