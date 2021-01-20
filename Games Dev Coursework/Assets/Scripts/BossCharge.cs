using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossCharge : MonoBehaviour
{
    GameObject player;
    NavMeshAgent na;
    Animator banim;
    float chasespeed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        na = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        banim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Chasing();
    }

    void Chasing()
    {
        na.SetDestination(player.transform.position);
        na.isStopped = false;
        na.speed= chasespeed;
        banim.SetBool("Run Forward", true);
    }
}
