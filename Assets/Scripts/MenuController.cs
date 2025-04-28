using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


public class MenuController : MonoBehaviour
{
    private GameObject panelMenu;
    private MovementCreator movementCreator;
    private bool inPause = false;
    [SerializeField] private Slider soundSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private AudioMixer soundMixer;
    [SerializeField] private AudioMixer musicMixer;

    void Start()
    {
        panelMenu = GameObject.Find("Canvas").transform.GetChild(1).gameObject;

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            movementCreator = player.GetComponent<MovementCreator>();
        }
        else
        {
            Debug.LogError("Player с тегом 'Player' не найден!");
        }
        musicSlider.value = 0.5f;
        soundSlider.value = 0.5f;
        soundMixer.SetFloat("SoundVolume", - 5);
        musicMixer.SetFloat("MusicVolume", - 5);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            inPause = !inPause;
            panelMenu.SetActive(inPause);
            if (movementCreator != null)
            {
                movementCreator.Playing = !inPause;
            }
        }
    }
    public void onClickBack()
    {
        inPause = false;
        panelMenu.SetActive(false);
        if (movementCreator != null)
        {
            movementCreator.Playing = true;
        }
    }

    public void onClickExit()
    {
        Application.Quit();
    }
    public void onChangeSoundSlider()
    {
        float value = soundSlider.value;
        Debug.Log("SoundSlider value:" + value);
        if (value == 0)
            soundMixer.SetFloat("SoundVolume", -80);
        else
            soundMixer.SetFloat("SoundVolume", Mathf.Log10(value) * 20);
    }

    public void onChangeMusicSlider()
    {
        float value = musicSlider.value;
        if(value ==0)
            musicMixer.SetFloat("MusicVolume", -80);
        else
            musicMixer.SetFloat("MusicVolume", Mathf.Log10(value) * 20);
    }
}
