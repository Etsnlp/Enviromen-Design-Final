using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] private Enemy enemyHealth; // Burada Enemy bileşenini atıyoruz
    [SerializeField] private Slider healthSlider;
    [SerializeField] private TMP_Text healthText;

    private void Awake()
    {
        if (enemyHealth == null)
        {
            Debug.LogError("Enemy component is not assigned.");
            enabled = false;
            return;
        }

        if (healthSlider == null)
        {
            Debug.LogError("HealthSlider component is not assigned.");
            enabled = false;
            return;
        }

        if (healthText == null)
        {
            Debug.LogError("HealthText component is not assigned.");
            enabled = false;
            return;
        }

        healthSlider.maxValue = enemyHealth.MaxHealth; // MaxHealth özelliğini kullanıyoruz
    }

    private void Start()
    {
        UpdateHealthBar();
    }

    private void Update()
    {
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        if (enemyHealth != null)
        {
            healthSlider.value = enemyHealth.CurrentHealth; // CurrentHealth özelliğini kullanıyoruz
            healthText.text = "Health: " + enemyHealth.CurrentHealth.ToString();
        }
    }
}