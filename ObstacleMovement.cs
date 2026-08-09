using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    public float speed = 5f;
    public GameObject explosionPrefab;

    void Update()
    {
        // Game Over અથવા Pause હોય ત્યારે Obstacle મૂવ નહીં થાય
        if (Time.timeScale == 0f) return;

        transform.Translate(Vector2.down * speed * Time.deltaTime);

        if (transform.position.y < -6f)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // explosionPrefab અસાઇન કરેલું હોય તો જ ઇન્સ્ટન્ટિએટ થશે (Safety Check)
        if (explosionPrefab != null)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}