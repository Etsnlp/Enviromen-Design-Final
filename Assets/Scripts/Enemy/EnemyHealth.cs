using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth; // currentHealth özelliğini public olarak tanımlayın

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
        // Düşmanın ölümüyle ilgili işlemler burada yapılır
        Destroy(gameObject);
    }
}
