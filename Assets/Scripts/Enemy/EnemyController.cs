using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 3f;
    public Transform target;
    public float attackRange = 1.5f;
    public float attackDamage = 10f;
    public float attackRate = 1f;
    public float distanceToAttack = 5f; 

    private float nextAttackTime = 0f;

    void Update()
    {
        if (target == null)
        {
            Debug.LogWarning("Target is not assigned to enemy!");
            return;
        }

        float distanceToTarget = Vector3.Distance(transform.position, target.position);

        if (distanceToTarget <= distanceToAttack)
        {
            transform.LookAt(target.position);
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        if (distanceToTarget <= attackRange && Time.time >= nextAttackTime)
        {
            Attack();
            nextAttackTime = Time.time + 1f / attackRate;
        }
    }

    void Attack()
    {
        Debug.Log("Enemy attacked!");
    }
}
