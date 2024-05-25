using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    public EnemyHealth enemyHealth;
    public Slider healthSlider;

    void Start()
    {
        if (enemyHealth == null)
        {
            Debug.LogError("EnemyHealth component is not assigned.");
            return;
        }

        if (healthSlider == null)
        {
            Debug.LogError("HealthSlider component is not assigned.");
            return;
        }

        healthSlider.maxValue = enemyHealth.maxHealth;
        healthSlider.value = enemyHealth.currentHealth;
    }

    void Update()
    {
        if (enemyHealth != null && healthSlider != null)
        {
            healthSlider.value = enemyHealth.currentHealth;
        }
    }
}
