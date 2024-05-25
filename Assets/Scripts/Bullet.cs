using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage = 10f;
    public float lifetime = 2f;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void OnCollisionEnter(Collision collision)
    {
        // Bu noktada hasar verecek hedefi kontrol edebilirsiniz.
        Debug.Log("Bullet hit: " + collision.gameObject.name);

        // Hedefe hasar verme kodu buraya eklenebilir.
        
        Destroy(gameObject);
    }
}
