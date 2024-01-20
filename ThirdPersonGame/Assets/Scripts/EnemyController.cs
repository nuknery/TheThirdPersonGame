using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private int health = 3;
    private float prevHitTime, ignoreDamageWindow = 1.5f;
    [SerializeField]
    private Animator animator;

    [SerializeField]
    private NavMeshAgent agent;
    [SerializeField]
    private Transform playerT;
    private float prevAttackTime, pauseAttackWindow = 2.5f;
    [SerializeField]
    private Transform patrolTargets;
    private int currentTargetIndex = 0;
    public bool isAttacking = false;

    void Start()
    {       
        prevHitTime = 0f;      
    }

    private void Update()
    {
        isAttacking = animator.GHetCurrentAnimatorStateInfo(0).isName("Sword_Attack_R");
        if (health > 1)
        {
            float dictanceToPlayer = Vector3.Distance(transform.position, playerT.position);
            if (distanceToPlayer < 2.5f)
            {
                //attack
            }
            else if (dictanceToPlayer > 30f)
            {
                //patrol
            }
            else
            {
                MoveToPlayer();
            }
        }
    }

    private void MoveToPlayer()
    {
        animator.SetBool("isWalk", true);
        agent.destination = playerT.position;
    }

    private void Attack()
    {
        animator.SetBool("isWalk", false);
        agent.destination = transform.position;
        transform.LookAt(playerT.position);
        if (Time.time > prevAttackTime + pauseAttackWindow && !animator.GetCurrentAnimatorStateInfo(0).IsName("KnockdownRight"))
        {
            animator.Play("Sword_Attack_R");
            prevAttackTime = Time.time;
        }
    }

    private void PatrolBehavior()
    {
        if(patrolTargets.Length > 0)
        {
            animator.SetBool("isWalk", true);
            agent.destination = patrolTargets[currentTargetIndex].position;
            CheckNewPatrolTarget();
        }
    }

    private void CheckNewPatrolTarget()
    {

    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Weapon" && Time.time > prevHitTime + ignoreDamageWindow)
        {
            health--;
            prevHitTime = Time.time;
            if(health > 1)
            {
                animator.Play("KnockdownRight");
            }
            else if (health == 1)
            {
                animator.Play("Sword_Defeat_1_Start");
            }
            else
            {
                animator.SetTrigger("isDead");
            }
        }
    }
}
