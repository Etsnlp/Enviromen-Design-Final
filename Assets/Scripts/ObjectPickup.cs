using UnityEngine;
using TMPro;

public class ObjectPickup : MonoBehaviour
{
    public float pickupRange = 2f; // Nesneleri alabileceğimiz mesafe
    public Transform holdPoint; // Oyuncunun elindeki pozisyon
    public float dropDistance = 2f; // Bırakma mesafesi
    public float dropHeightOffset = 0.5f; // Bırakılan objenin yerden yüksekliği
    public TextMeshProUGUI pickupText; // Ekranda gösterilecek "E tuşuna bas" yazısı
    public TextMeshProUGUI dropText; // Ekranda gösterilecek "Objeyi bırakmak için E tuşuna bas" yazısı
    private GameObject heldObject; // Şu anda elde tutulan nesne
    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        pickupText.enabled = false; // Yazıyı başlangıçta devre dışı bırak
        dropText.enabled = false; // Yazıyı başlangıçta devre dışı bırak
    }

    void Update()
    {
        if (heldObject == null)
        {
            // Nesne almadıysak, etkileşim yazısını kontrol et
            ShowPickupText();
        }
        else
        {
            // Nesne aldıysak, bırakma yazısını göster
            ShowDropText();
        }

        // Nesneyi al veya bırak
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (heldObject == null)
            {
                TryPickupObject();
            }
            else
            {
                DropObject(holdPoint.position); // Objeyi eldeki pozisyona bırak
            }
        }
    }

    void LateUpdate()
    {
        // Tutulan nesnenin pozisyonunu güncelle
        if (heldObject != null)
        {
            HoldObject();
        }
    }

    void ShowPickupText()
    {
        RaycastHit hit;
        if (Physics.Raycast(player.transform.position, player.transform.forward, out hit, pickupRange))
        {
            // Sadece "Pickup" tag'ine sahip nesneleri kontrol et
            if (hit.collider != null && hit.collider.gameObject.CompareTag("Pickup"))
            {
                pickupText.enabled = true; // Yazıyı etkinleştir
            }
            else
            {
                pickupText.enabled = false; // Yazıyı devre dışı bırak
            }
        }
        else
        {
            pickupText.enabled = false; // Yazıyı devre dışı bırak
        }
    }

    void ShowDropText()
    {
        // Nesne eldeyse bırakma yazısını göster
        dropText.enabled = heldObject != null;
    }

    void TryPickupObject()
    {
        RaycastHit hit;
        if (Physics.Raycast(player.transform.position, player.transform.forward, out hit, pickupRange))
        {
            // Sadece "Pickup" tag'ine sahip nesneleri al
            if (hit.collider != null && hit.collider.gameObject.CompareTag("Pickup"))
            {
                PickupObject(hit.collider.gameObject);
                pickupText.enabled = false; // "E tuşuna bas" yazısını devre dışı bırak
            }
        }
    }

    void PickupObject(GameObject obj)
    {
        // Nesneyi al ve elde tut
        heldObject = obj;
        Rigidbody rb = obj.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true; // Fizik hesaplamalarını durdur
        }
        Collider col = obj.GetComponent<Collider>();
        if (col != null)
        {
            col.enabled = false; // Collider'ı devre dışı bırak
        }
        obj.transform.position = holdPoint.position;
        obj.transform.rotation = holdPoint.rotation;
        obj.transform.parent = holdPoint;
        dropText.enabled = true; // "Objeyi bırakmak için E tuşuna bas" yazısını etkinleştir
    }

    void HoldObject()
    {
        // Tutulan nesneyi eldeki pozisyonda ve rotasyonda tut
        heldObject.transform.position = holdPoint.position;
        heldObject.transform.rotation = holdPoint.rotation;
    }

    public void DropObject(Vector3 dropPosition)
    {
        // Nesneyi bırak
        if (heldObject != null)
        {
            Rigidbody rb = heldObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false;
            }
            Collider col = heldObject.GetComponent<Collider>();
            if (col != null)
            {
                col.enabled = true; // Collider'ı tekrar etkinleştir
            }

            heldObject.transform.position = dropPosition;
            heldObject.transform.rotation = Quaternion.identity; // Objeyi yere düz bir şekilde yerleştir
            heldObject.transform.parent = null;
            heldObject = null;
            dropText.enabled = false; // "Objeyi bırakmak için E tuşuna bas" yazısını devre dışı bırak
        }
    }

    public GameObject GetHeldObject()
    {
        return heldObject;
    }
}
