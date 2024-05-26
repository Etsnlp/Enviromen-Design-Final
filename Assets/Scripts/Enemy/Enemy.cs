using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;
    public ObjectiveManager objectiveManager;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (objectiveManager != null)
        {
            objectiveManager.EnemyKilled();
        }
        Destroy(gameObject);
    }
}
