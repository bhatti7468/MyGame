using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public GameObject hitParticlePrefab;

    [Header("Hit Flash Color")]
    public Color flashColor = new Color(1f, 0f, 0f, 0.8f); // Red Flash Color

    private bool hasCollided = false;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle") && !hasCollided)
        {
            hasCollided = true;

            // 1. Particle Effect Spawn કરો
            if (hitParticlePrefab != null)
            {
                Instantiate(hitParticlePrefab, transform.position, Quaternion.identity);
            }

            // 2. Camera Shake & Screen Flash કૉલ કરો
            if (CameraShake.Instance != null)
            {
                CameraShake.Instance.Shake(0.4f, 1f);
            }

            if (ScreenFlash.Instance != null)
            {
                ScreenFlash.Instance.Flash(flashColor);
            }

            // 3. Player નો Collider બંધ કરો
            GetComponent<Collider2D>().enabled = false;

            // 4. Player Movement બંધ કરો
            PlayerMovement movement = GetComponent<PlayerMovement>();
            if (movement != null)
            {
                movement.enabled = false;
            }

            // 5. Game Over કૉલ કરો
            GameManager.Instance.GameOver();
        }
    }
}