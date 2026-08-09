using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance;
    private Vector3 originalPos;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        originalPos = transform.localPosition;
    }

    public void Shake(float duration = 0.25f, float magnitude = 0.4f)
    {
        StartCoroutine(ShakeRoutine(duration, magnitude));
    }

    private IEnumerator ShakeRoutine(float duration, float magnitude)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(originalPos.x + x, originalPos.y + y, originalPos.z);

            elapsed += Time.unscaledDeltaTime; // Time.timeScale = 0 હોય તો પણ વાઇબ્રેટ થશે
            yield return null;
        }

        transform.localPosition = originalPos;
    }
}