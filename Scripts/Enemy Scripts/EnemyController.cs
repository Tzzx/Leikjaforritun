﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public enum EnemyState
{
    PATROL,
    CHASE,
    ATTACK
}

public class EnemyController : MonoBehaviour
{
    private EnemyAnimations enemy_Anim;
    private NavMeshAgent navAgent;

    private EnemyState enemy_State;

    public float walk_Speed = 0.5f;
    public float run_Speed = 4f;
    public float chase_Distance = 7f;
    private float current_Chase_Distance;
    public float attack_Distance = 1.8f;
    public float chase_After_Attack_Distance = 2f;

    public float patrol_Radius_Min = 20f, patrol_Radius_Max = 60f;
    public float patrol_For_This_Time = 15f;
    private float patrole_timer;

    public float wait_Before_Attack = 2f;
    private float attack_Timer;

    private Transform target;

    void Awake()
    {
        enemy_Anim = GetComponent<EnemyAnimations>();
        navAgent = GetComponent<NavMeshAgent>();

        target = GameObject.FindWithTag(Tags.PLAYER_TAG).transform;
    }



    // Start is called before the first frame update
    void Start()
    {
        enemy_State = EnemyState.PATROL;

        patrole_timer = patrol_For_This_Time;
        // þegar óvinur kemst að þér þá bíður hann áður en að gera árás
        attack_Timer = wait_Before_Attack;

        //muna elti vegaleingd so við getum set það aftur til backa
        current_Chase_Distance = chase_Distance;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemy_State == EnemyState.PATROL)
        {
            Patrol();
        }

        if (enemy_State == EnemyState.CHASE)
        {
            Chase();
        }

        if (enemy_State == EnemyState.ATTACK)
        {
            Attack();
        }
        
    }//update


    void Patrol()
    {
        // segja nav agent að hann getur hreyft sig
        navAgent.isStopped = false;
        navAgent.speed = walk_Speed;

        patrole_timer += Time.deltaTime;

        if(patrole_timer > patrol_For_This_Time)
        {
            SetNewRandomDestination();
            patrole_timer = 0f;
        }

        if(navAgent.velocity.sqrMagnitude > 0)
        {
            enemy_Anim.Walk(true);
        }
        else
        {
            enemy_Anim.Walk(false);
        }

        //sjá distance muninn á milli míns og óvins
        if(Vector3.Distance(transform.position, target.position) <= chase_Distance)
        {
            enemy_Anim.Walk(false);

            enemy_State = EnemyState.CHASE;
        }
    }

    void Chase()
    {
        // getur hreyft sig aftur
        navAgent.isStopped = false;
        navAgent.speed = run_Speed;

        // láta hann elta mig
        navAgent.SetDestination(target.position);

        if (navAgent.velocity.sqrMagnitude > 0)
        {
            enemy_Anim.Run(true);
        }
        else
        {
            enemy_Anim.Run(false);
        }

        //ef distance á milli óvin og player/mín er minni en attack distance
        if(Vector3.Distance(transform.position, target.position) <= attack_Distance)
        {
            // hætta animations
            enemy_Anim.Run(false);
            enemy_Anim.Walk(false);
            enemy_State = EnemyState.ATTACK;

            if(chase_Distance != current_Chase_Distance)
            {
                chase_Distance = current_Chase_Distance;
            }
        }
        else if(Vector3.Distance(transform.position, target.position) > chase_Distance)
        {
            //spilar er að hlaupa í burtu

            // hætta að hlaupa
            enemy_Anim.Run(false);

            enemy_State = EnemyState.PATROL;

            patrole_timer = patrol_For_This_Time;

            if (chase_Distance != current_Chase_Distance)
            {
                chase_Distance = current_Chase_Distance;
            }

        }

    } // chase

    void Attack()
    {
        navAgent.velocity = Vector3.zero;
        navAgent.isStopped = true;

        attack_Timer += Time.deltaTime;

        if(attack_Timer > wait_Before_Attack)
        {
            enemy_Anim.Attack();

            attack_Timer = 0f;
        }

        if(Vector3.Distance(transform.position, target.position) >
            attack_Distance + chase_After_Attack_Distance)
        {
            enemy_State = EnemyState.CHASE;
        }
    }

    void SetNewRandomDestination()
    {
        float rand_Radius = Random.Range(patrol_Radius_Min, patrol_Radius_Max);

        Vector3 randDir = Random.insideUnitSphere * rand_Radius;
        randDir += transform.position;
        NavMeshHit navHit;

        NavMesh.SamplePosition(randDir,out navHit, rand_Radius, -1);
        navAgent.SetDestination(navHit.position);
    }








} //class
