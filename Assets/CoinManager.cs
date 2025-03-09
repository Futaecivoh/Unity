using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public int coinCount;
    public AudioClip pickupSound;
    private AudioSource audioSource;
    void Start()
    {
         audioSource = gameObject.AddComponent<AudioSource>();
    }
     private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) 
        {
              Debug.Log("Player collided with coin!");
              PlayPickupSound();
        }  
      
    }
     private void PlayPickupSound()
        {
            if(pickupSound != null)
            {
                audioSource.PlayOneShot(pickupSound);
                 Debug.Log("Playing pickup sound!");
            }
        }
    void Update()
    {
        
    }
}
