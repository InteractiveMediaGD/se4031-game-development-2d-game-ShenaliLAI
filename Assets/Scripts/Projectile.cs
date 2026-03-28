using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    public float speed = 15f;
    public float lifetime = 3f;
    
    private Rigidbody2D rb;
    private Vector2 direction;
    
    public void Initialize(Vector2 dir)
    {
        direction = dir.normalized;
        rb = GetComponent<Rigidbody2D>();
        
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        
        Destroy(gameObject, lifetime);
    }
    
    void FixedUpdate()
    {
        if (rb != null)
        {
            rb.velocity = direction * speed;
        }
    }
    
    void OnTriggerEnter2D(Collider2D col)
    {
        // Try to get components instead of relying on tags
        Enemy enemy = col.GetComponent<Enemy>();
        Obstacle obstacle = col.GetComponentInParent<Obstacle>();

        if (enemy != null)
        {
            enemy.TakeDamageFromProjectile();
            Destroy(gameObject);
        }
        else if (obstacle != null)
        {
            // Projectiles hit obstacles but don't damage them
            Destroy(gameObject);
        }
    }
}
