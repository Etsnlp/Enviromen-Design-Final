using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField] private Transform muzzle;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed = 20f;
    [SerializeField] private float fireRate = 0.5f;
    [SerializeField] private float attackDamage = 20f;
    [SerializeField] private Camera playerCamera;

    private float nextFireTime = 0f;

    private void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate;
            Shoot();
        }
    }

    private void Shoot()
    {
        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        Vector3 targetPoint;

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            targetPoint = hit.point;
            HandleHit(hit);
        }
        else
        {
            targetPoint = ray.GetPoint(1000);
        }

        FireBullet(targetPoint);
    }

    private void HandleHit(RaycastHit hit)
    {
        if (hit.collider != null)
        {
            Enemy enemyHealth = hit.collider.GetComponent<Enemy>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(attackDamage);
            }
        }
    }

    private void FireBullet(Vector3 targetPoint)
    {
        Vector3 direction = targetPoint - muzzle.position;
        GameObject bullet = Instantiate(bulletPrefab, muzzle.position, Quaternion.identity);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.velocity = direction.normalized * bulletSpeed;
    }
}