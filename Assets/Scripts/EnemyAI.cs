using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;
    [SerializeField] float damage;
    [SerializeField] float engagingRange;
    [SerializeField] float attackRange;
    [SerializeField] float seekingTime;

    private Player player;
    private Vector3 startingPos;
    private float timeSinceEngaged = Mathf.Infinity;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        startingPos = transform.position;
    }

    private void Update()
    {
        timeSinceEngaged += Time.deltaTime;
        if (InRange())
            EngagePlayer();
        else if (timeSinceEngaged < seekingTime)
            agent.destination = transform.position;
        else
            ReturnToPosition();
    }

    private void ReturnToPosition()
    {
        agent.destination = startingPos;
        agent.stoppingDistance = 0;
    }

    private void EngagePlayer()
    {
        agent.destination = player.transform.position;
        agent.stoppingDistance = attackRange;
        timeSinceEngaged = 0;
        if (agent.remainingDistance <= agent.stoppingDistance)
            Attack();
    }

    private void Attack()
    {
        Debug.Log("Attacking Player");
    }

    private bool InRange()
    {
        var dis = Vector3.Distance(transform.position, player.transform.position);
        return dis < engagingRange;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, engagingRange);
    }
}
