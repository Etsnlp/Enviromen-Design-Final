using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;
    private float currentHealth;
    public ObjectiveManager ObjectiveManager { get; set; } // ObjectiveManager property

    public float MaxHealth { get { return maxHealth; } } // MaxHealth özelliği
    public float CurrentHealth { get { return currentHealth; } } // CurrentHealth özelliği

    private void Start()
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

    private void Die()
    {
        if (ObjectiveManager != null)
        {
            ObjectiveManager.EnemyKilled();
        }
        Destroy(gameObject);
    }
}