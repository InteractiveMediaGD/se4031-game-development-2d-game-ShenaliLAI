using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [Header("Settings")]
    public float damage = 20f;
    public int scorePenalty = -10;
    public AudioClip hitSound;

    // Track if the player has touched any wall of this obstacle.
    [HideInInspector] public bool hasPlayerHitWall = false;

    void Start()
    {
        // Force Z-position to -1 so it's in front of the background Quad (at Z=0)
        Vector3 pos = transform.position;
        pos.z = -1f;
        transform.position = pos;
    }

    void Update()
    {
        // Move to the left based on global game speed
        if (GameManager.Instance != null && GameManager.Instance.isGameStarted && !GameManager.Instance.isGameOver)
        {
            transform.Translate(Vector3.left * GameManager.Instance.globalSpeed * Time.deltaTime);
            
            // Destroy if off-screen (left)
            if (transform.position.x < -15f)
            {
                Destroy(gameObject);
            }
        }
    }
}
