using UnityEngine;
using TMPro; 
using UnityEngine.Audio;

public class CoinManager : MonoBehaviour
{
    public int coinCount;
    public AudioClip pickupSound;
    private AudioSource audioSource;
    public TextMeshProUGUI coinText; 
     [SerializeField] private AudioMixerGroup sfxMixerGroup;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.outputAudioMixerGroup = sfxMixerGroup;
        UpdateCoinText(); 
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) 
        {
            PlayPickupSound();
            coinCount++; 
            UpdateCoinText(); 
        }
    }

    private void PlayPickupSound()
    {
        if (pickupSound != null)
        {
            audioSource.PlayOneShot(pickupSound);
        }
    }

    private void UpdateCoinText()
    {
        if (coinText != null)
        {
            coinText.text = "Melons: " + coinCount;
        }
    }
}
