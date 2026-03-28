using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    public float scrollSpeed = 0.5f;
    private Material mat;
    private Vector2 offset;

    void Start()
    {
        // Grab the material component off the 3D Quad
        mat = GetComponent<Renderer>().material;
    }

    void Update()
    {
        if (GameManager.Instance != null && (!GameManager.Instance.isGameStarted || GameManager.Instance.isGameOver)) return;

        // The background will correctly scroll faster as the game's global speed increases!
        float currentSpeed = scrollSpeed * (GameManager.Instance != null ? (GameManager.Instance.globalSpeed / 5f) : 1f);
        
        offset.x += currentSpeed * Time.deltaTime;
        mat.mainTextureOffset = offset;
    }
}
