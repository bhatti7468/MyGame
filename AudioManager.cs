using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource sfxSource;
    public AudioSource uiSource;

    [Header("Audio Clips")]
    public AudioClip backgroundMusic;
    public AudioClip hitSound;
    public AudioClip coinSound;
    public AudioClip gameOverSound;
    public AudioClip buttonClickSound;

    private bool gameOver = false;

    private void Awake()
    {
        // Singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        StartBGM();
    }

    // -------------------------
    // BACKGROUND MUSIC
    // -------------------------

    public void StartBGM()
    {
        if (gameOver)
            return;

        if (musicSource != null && backgroundMusic != null)
        {
            musicSource.clip = backgroundMusic;
            musicSource.loop = true;

            if (!musicSource.isPlaying)
            {
                musicSource.Play();
            }
        }
    }

    public void StopBGM()
    {
        if (musicSource != null)
        {
            musicSource.Stop();
        }
    }

    // -------------------------
    // GAME OVER
    // -------------------------

    public void PlayGameOver()
    {
        gameOver = true;

        StopBGM();

        if (sfxSource != null && gameOverSound != null)
        {
            sfxSource.PlayOneShot(gameOverSound);
        }
    }

    // -------------------------
    // HIT
    // -------------------------

    public void PlayHit()
    {
        if (sfxSource != null && hitSound != null)
        {
            sfxSource.PlayOneShot(hitSound);
        }
    }

    // -------------------------
    // COIN
    // -------------------------

    public void PlayCoin()
    {
        if (sfxSource != null && coinSound != null)
        {
            sfxSource.PlayOneShot(coinSound);
        }
    }

    // -------------------------
    // BUTTON CLICK
    // -------------------------

    public void PlayButtonClick()
    {
        if (uiSource != null && buttonClickSound != null)
        {
            uiSource.PlayOneShot(buttonClickSound);
        }
    }

    // -------------------------
    // RESTART
    // -------------------------

    public void RestartAudio()
    {
        gameOver = false;

        StopBGM();
        StartBGM();
    }
}