using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float damageToPlayer = 20f;
    public int scoreValue = 10;
    public AudioClip destroySound;

    void Start()
    {
        // Force Z-position to -1 for 100% collision reliability!
        Vector3 pos = transform.position;
        pos.z = -1f;
        transform.position = pos;

        // [FAIR PLAY] Shrink the collider to 80% of the sprite size.
        // This makes dodging feel more rewarding and prevents "ghost hits."
        SpriteRenderer sr = GetComponentInChildren<SpriteRenderer>();
        BoxCollider2D bc = GetComponent<BoxCollider2D>();
        if (bc == null) bc = gameObject.AddComponent<BoxCollider2D>();
        bc.isTrigger = true;
        if (sr != null) bc.size = sr.sprite.bounds.size * 0.8f;
    }

    void Update()
    {
        if (GameManager.Instance == null || !GameManager.Instance.isGameStarted || GameManager.Instance.isGameOver) return;
        transform.Translate(Vector3.left * GameManager.Instance.globalSpeed * Time.deltaTime);
        if (transform.position.x < -12f) Destroy(gameObject);
    }

    public void TakeDamageFromProjectile()
    {
        if (GameManager.Instance != null) GameManager.Instance.AddScore(scoreValue);
        if (destroySound != null) AudioSource.PlayClipAtPoint(destroySound, transform.position);
        Destroy(gameObject);
    }
}
