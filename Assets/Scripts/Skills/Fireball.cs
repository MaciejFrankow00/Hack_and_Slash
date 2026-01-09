using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float speed = 10f;
    public float damage = 50f;
    public float explosionRadius = 3f;
    public GameObject explosionEffect;
    public LayerMask enemyLayer;

    private Rigidbody rb;
    private bool hasExploded = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        // Zabezpieczenie przed tunelowaniem
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        rb.interpolation = RigidbodyInterpolation.Interpolate;
    }

    void Start()
    {
        rb.velocity = transform.forward * speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (hasExploded) return;

        Debug.Log("Fireball uderzy³ w: " + collision.collider.name);

        // Jeœli trafi³ w przeciwnika, zadaj mu obra¿enia
        if (collision.collider.CompareTag("Enemy"))
        {
            var health = collision.collider.GetComponent<HealthController>();
            if (health != null)
            {
                health.TakeDamage(damage);
            }
        }

        Explode();
    }

    void Explode()
    {
        hasExploded = true;

        if (explosionEffect)
        {
            GameObject effect = Instantiate(explosionEffect, transform.position, Quaternion.identity);
            Destroy(effect, 2f);
        }

        // AoE – obra¿enia w promieniu eksplozji
        Collider[] hitEnemies = Physics.OverlapSphere(transform.position, explosionRadius, enemyLayer);
        Debug.Log("Znaleziono " + hitEnemies.Length + " przeciwników w promieniu eksplozji.");

        foreach (Collider col in hitEnemies)
        {
            if (col.CompareTag("Enemy"))
            {
                var health = col.GetComponent<HealthController>();
                if (health != null)
                {
                    health.TakeDamage(damage);
                }
            }
        }

        Destroy(gameObject);
    }
}
