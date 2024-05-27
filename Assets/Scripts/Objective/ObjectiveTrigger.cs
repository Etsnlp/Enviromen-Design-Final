using UnityEngine;

public class ObjectiveTrigger : MonoBehaviour
{
    public ObjectiveManager objectiveManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            objectiveManager.enabled = true;
            Destroy(this.gameObject);
        }
    }
}
