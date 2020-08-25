using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {

    NavMeshAgent nav;

    Transform player;
	// Use this for initialization
	void Start () {
        nav = GetComponent<NavMeshAgent>();

        player = GameObject.Find("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
        nav.SetDestination(player.position);
	}
}
