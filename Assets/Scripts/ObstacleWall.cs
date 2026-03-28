using UnityEngine;

public class ObstacleWall : MonoBehaviour
{
    [Header("Visuals (Juice)")]
    public Color healthyColor = new Color(1, 1, 1, 1);
    public Color dangerColor = new Color(1, 0, 0, 1);

    private SpriteRenderer sr;

    void Start()
    {
        // Force local Z to 0 so it aligns with the parent's Z-position
        Vector3 pos = transform.localPosition;
        pos.z = 0f;
        transform.localPosition = pos;

        sr = GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            // Enforce visual layering: Obstacle walls should be behind the Player (10) but above Background (-10)
            sr.sortingOrder = 5; 
        }

        // [RELIABILITY] Automatically fix collider if missing or wrong
        BoxCollider2D bc = GetComponent<BoxCollider2D>();
        if (bc == null) bc = gameObject.AddComponent<BoxCollider2D>();
        bc.isTrigger = true;
        
        // [FAIR PLAY] Shrink the collider to 80% of the sprite size.
        // This makes the gap feel "wider" and prevents "pixel-perfect" hits that feel unfair.
        if (sr != null) bc.size = sr.sprite.bounds.size * 0.8f;
    }

    void Update()
    {
        // Handle Color update based on Player Health (Juicy feature)
        if (PlayerController.Instance != null && PlayerController.Instance.maxHealth > 0 && sr != null)
        {
            float healthPercent = PlayerController.Instance.currentHealth / PlayerController.Instance.maxHealth;
            Color targetColor = Color.Lerp(dangerColor, healthyColor, healthPercent);
            sr.color = targetColor;
        }
    }
}
