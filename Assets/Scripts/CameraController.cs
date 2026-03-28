using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance { get; private set; }
    private Vector3 originalPos;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
        
        originalPos = transform.localPosition;
    }

    private Coroutine currentShake;

    public IEnumerator Shake(float duration, float magnitude)
    {
        if (currentShake != null) StopCoroutine(currentShake);
        currentShake = StartCoroutine(DoShake(duration, magnitude));
        yield return currentShake;
    }

    private IEnumerator DoShake(float duration, float magnitude)
    {
        float elapsed = 0.0f;
        while (elapsed < duration)
        {
            float x = Random.Range(-0.8f, 0.8f) * magnitude;
            float y = Random.Range(-0.8f, 0.8f) * magnitude;

            transform.localPosition = new Vector3(originalPos.x + x, originalPos.y + y, originalPos.z);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = originalPos;
        currentShake = null;
    }
}
