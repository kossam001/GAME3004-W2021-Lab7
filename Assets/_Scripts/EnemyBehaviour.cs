using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum ZombieState
{
    IDLE,
    RUN,
    JUMP,
    HIT,
    DIE
}

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
public class EnemyBehaviour : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public PlayerBehaviour player;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        player = FindObjectOfType<PlayerBehaviour>();
        animator = GetComponent<Animator>();
        animator.SetInteger("AnimState", (int)ZombieState.RUN);
    }

    // Update is called once per frame
    void Update()
    {
        if (player)
        {
            navMeshAgent.SetDestination(player.transform.position);

            if ((animator) && Vector3.Distance(player.transform.position, transform.position) <= 3.0f)
            {
                StartCoroutine(RandomZombieHitTime());
            }
        }
    }

    IEnumerator RandomZombieHitTime()
    {
        yield return new WaitForSeconds(Random.Range(0.2f, 0.4f));
        animator.SetInteger("AnimState", (int)ZombieState.HIT);
        StopAllCoroutines();
    }
}
