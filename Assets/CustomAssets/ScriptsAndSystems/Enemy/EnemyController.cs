﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float lookRadius = 5f;
    public float attackRadius = 2f;
    public float rotateSpeed = 3f;

    private bool isDead = false;

    public Transform target;
    NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = PlayerManager.instance.Player.transform;
    }

    private void Update()
    {
        if (!isDead)
        {
            Move();
        }
    }

    public void Move()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        if (IsInLookRadius(distance))
        {
            agent.SetDestination(target.position);

            if (IsInAttackRadius(distance))
            {

            }
            else
            {

            }
        }
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x,0,direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotateSpeed);
    }

    private bool IsInLookRadius(float distanceToTarget)
    {
        if (distanceToTarget <= lookRadius)
            return true;
        return false;
    }
    private bool IsInAttackRadius(float distanceToTarget)
    {
        if (distanceToTarget <= attackRadius)
            return true;
        return false;
    }
    public void OnDeath()
    {
        isDead = true;
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
