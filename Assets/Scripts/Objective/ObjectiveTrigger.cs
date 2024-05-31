using UnityEngine;

public class ObjectiveTrigger : MonoBehaviour
{
    [SerializeField] private ObjectiveManager objectiveManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            objectiveManager.enabled = true;
            Destroy(gameObject);
        }
    }
}