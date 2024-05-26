using UnityEngine;
using TMPro;

public class GemStand : MonoBehaviour
{
    public Transform[] gemPositions; // Mücevherlerin yerleştirileceği pozisyonlar
    public GameObject exitDoor; // Çıkış kapısı
    public TextMeshProUGUI interactionText; // Ekranda gösterilecek yazı

    private int currentGemCount = 0; // Şu anda yerleştirilen mücevher sayısı
    private bool isPlayerNear = false; // Oyuncu yakında mı?

    private ObjectPickup objectPickup; // Oyuncunun pickup scripti referansı

    void Start()
    {
        interactionText.enabled = false; // Yazıyı başlangıçta devre dışı bırak
        exitDoor.SetActive(false); // Kapıyı başlangıçta kapalı yap

        // Oyuncunun pickup scriptini bul
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            objectPickup = player.GetComponent<ObjectPickup>();
        }
    }

    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E) && objectPickup.GetHeldObject() != null)
        {
            PlaceGem();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
            //interactionText.enabled = true;
            //interactionText.text = "Mücevheri yerleştirmek için E tuşuna bas";
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            interactionText.enabled = false;
        }
    }

    void PlaceGem()
    {
        if (currentGemCount < gemPositions.Length)
        {
            GameObject gem = objectPickup.GetHeldObject();
            gem.transform.position = gemPositions[currentGemCount].position;
            gem.transform.rotation = gemPositions[currentGemCount].rotation;
            gem.transform.parent = gemPositions[currentGemCount];
            gem.GetComponent<Collider>().enabled = false; // Collider'ı devre dışı bırak
            objectPickup.DropObject(gemPositions[currentGemCount].position);
            currentGemCount++;

            if (currentGemCount >= gemPositions.Length)
            {
                ActivateExitDoor();
            }
        }
    }

    void ActivateExitDoor()
    {
        exitDoor.SetActive(true); // Kapıyı aç
        interactionText.text = "Exit Gate Opened!";
    }
}
