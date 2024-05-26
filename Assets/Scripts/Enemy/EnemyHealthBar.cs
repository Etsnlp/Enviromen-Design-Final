using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyHealthBar : MonoBehaviour
{
    public EnemyHealth enemyHealth;
    public Slider healthSlider;
    public TMP_Text healthText;

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

        if (healthText == null)
        {
            Debug.LogError("HealthText component is not assigned.");
            return;
        }

        healthSlider.maxValue = enemyHealth.maxHealth;
        healthSlider.value = enemyHealth.currentHealth;
    }

    void Update()
    {
        if (enemyHealth != null && healthSlider != null && healthText != null)
        {
            healthSlider.value = enemyHealth.currentHealth;
            healthText.text = "Health: " + enemyHealth.currentHealth.ToString();
        }
    }
}
