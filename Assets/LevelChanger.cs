using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SceneSwitcher : MonoBehaviour
{
    public string sceneToLoad = "SecondLevel";
    public int requiredCoins = 3;
    private TextMeshProUGUI levelMessage;


  private void Start()
    {
        GameObject messageObj = GameObject.Find("LevelMessage");
        if (messageObj != null)
        {
            levelMessage = messageObj.GetComponent<TextMeshProUGUI>();
            levelMessage.gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CoinManager coinManager = FindObjectOfType<CoinManager>();
            SceneFader fader = FindObjectOfType<SceneFader>();
            if (coinManager == null)
            {
                return;
            }

            if (coinManager.coinCount >= requiredCoins)
            {
                Debug.Log("Арбузов достаточно! Загружаем следующую сцену...");
                fader.FadeToScene(sceneToLoad);
            }
            else
            {

                {

                ShowMessage("Collect at least 3 melons!");

                }
            }
        }

    }
    private void ShowMessage(string text)
    {
        if (levelMessage != null)
        {
            levelMessage.text = text;
            levelMessage.gameObject.SetActive(true);
            CancelInvoke(); 
            Invoke(nameof(HideMessage), 2.5f); 
        }
    }
     private void HideMessage()
    {
        if (levelMessage != null)
            levelMessage.gameObject.SetActive(false);
    }
    
}
