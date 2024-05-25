using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 3f;
    public Transform target;
    public float attackRange = 1.5f;
    public float attackDamage = 10f;
    public float attackRate = 1f;
    public float distanceToAttack = 5f; // Düşmanın saldırmak için hedefe olan maksimum uzaklığı

    private float nextAttackTime = 0f;

    void Update()
    {
        if (target == null)
        {
            Debug.LogWarning("Target is not assigned to enemy!");
            return;
        }

        float distanceToTarget = Vector3.Distance(transform.position, target.position);

        // Hedefe doğru ilerle
        if (distanceToTarget <= distanceToAttack)
        {
            transform.LookAt(target.position);
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

        // Hedefe saldır
        if (distanceToTarget <= attackRange && Time.time >= nextAttackTime)
        {
            Attack();
            nextAttackTime = Time.time + 1f / attackRate;
        }
    }

    void Attack()
    {
        // Hedefe hasar verme işlemi buraya eklenebilir
        Debug.Log("Enemy attacked!");

        // Örneğin:
        // target.GetComponent<PlayerHealth>().TakeDamage(attackDamage);
    }
}
