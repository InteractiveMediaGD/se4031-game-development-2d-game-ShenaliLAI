using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ScoreZone : MonoBehaviour
{
    private bool hasScored = false;
    
    void Start()
    {
        Collider2D col = GetComponent<Collider2D>();
        if (col != null) col.isTrigger = true;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player") && !hasScored)
        {
            // Check the parent Obstacle to see if the player hit a wall
            Obstacle parentObstacle = GetComponentInParent<Obstacle>();
            if (parentObstacle != null && parentObstacle.hasPlayerHitWall)
            {
                Debug.Log("<color=orange><b>DIRTY PASS:</b></color> No score because you hit a wall!");
                return; // SKIP score addition
            }

            hasScored = true;
            if (GameManager.Instance != null)
            {
                GameManager.Instance.AddScore(10);
            }
        }
    }
}
