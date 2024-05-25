using UnityEngine;

public class GunController : MonoBehaviour
{
    public Transform muzzle;
    public GameObject bulletPrefab;
    public float bulletSpeed = 20f;
    public float fireRate = 0.5f;
    public Camera playerCamera;

    private float nextFireTime = 0f;

    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate;
            Shoot();
        }
    }

    void Shoot()
    {
        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Vector3 targetPoint = hit.point;
            Vector3 direction = targetPoint - muzzle.position;
            GameObject bullet = Instantiate(bulletPrefab, muzzle.position, Quaternion.identity);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            rb.velocity = direction.normalized * bulletSpeed;
        }
        else
        {
            Vector3 targetPoint = ray.GetPoint(1000);
            Vector3 direction = targetPoint - muzzle.position;
            GameObject bullet = Instantiate(bulletPrefab, muzzle.position, Quaternion.identity);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            rb.velocity = direction.normalized * bulletSpeed;
        }
    }
}
