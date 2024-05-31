using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    [SerializeField] private Transform target;
    [SerializeField] private float attackRange = 1.5f;
    [SerializeField] private float attackDamage = 10f;
    [SerializeField] private float attackRate = 1f;
    [SerializeField] private float distanceToAttack = 5f; 

    private float nextAttackTime = 0f;

    private void Update()
    {
        if (target == null)
        {
            Debug.LogWarning("Target is not assigned to enemy!");
            return;
        }

        float distanceToTarget = Vector3.Distance(transform.position, target.position);

        if (distanceToTarget <= distanceToAttack)
        {
            MoveTowardsTarget(distanceToTarget);
        }

        if (distanceToTarget <= attackRange && Time.time >= nextAttackTime)
        {
            Attack();
        }
    }

    private void MoveTowardsTarget(float distanceToTarget)
    {
        if (distanceToTarget > attackRange)
        {
            transform.LookAt(target.position);
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
    }

    private void Attack()
    {
        Debug.Log("Enemy attacked!");
        nextAttackTime = Time.time + 1f / attackRate;
    }
}