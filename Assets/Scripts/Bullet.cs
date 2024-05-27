using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifetime = 1f;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    // void OnCollisionEnter(Collision collision)
    // {
    //     Debug.Log("Bullet hit: " + collision.gameObject.name);
    //     Destroy(this.gameObject);
    // }

    void OnTriggerStay(Collider other) 
    {
        Debug.Log("Bullet hit: " + other.gameObject.name);
        Destroy(this.gameObject);
        
    }
}