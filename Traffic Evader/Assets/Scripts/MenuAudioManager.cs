using Unity.VisualScripting;
using UnityEngine;

public class MenuAudioManager : MonoBehaviour
{
    public static MenuAudioManager instance;

    [SerializeField] private AudioClip lowEngineSound;
    [SerializeField] private AudioClip fastEngineSound;
    [SerializeField] private AudioClip gameMusic;
    [SerializeField] private AudioClip menuMusic;
    [SerializeField] private AudioClip crashSound;
    [SerializeField] private AudioClip podiumSound;
    [SerializeField] private AudioSource[] audioSources;
    private bool isChangedToFast;
    public bool isGameMusic;

    void Awake()
    {
        if (instance == null)
        {
            instance = this; // Set the instance to this GameManager
            DontDestroyOnLoad(gameObject); // Make sure the GameManager persists across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy any duplicate GameManager instances
        }
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSources[1].clip = lowEngineSound;
        audioSources[1].PlayDelayed(1);
    }

    // Update is called once per frame
    void Update()
    {
        MenuVehicleMoving();
        ChangeToGameMusic();
        ChangeToMenuMusic();
    }

    private void MenuVehicleMoving()
    {
        if (GameManager.instance.currentState == GameState.StartingPlay && isChangedToFast == false)
        {
            audioSources[1].clip = fastEngineSound;
            audioSources[1].Play();
            isChangedToFast = true;
        }
    }

    public void ChangeToGameMusic()
    {
        if (GameManager.instance.currentState == GameState.Playing && isGameMusic == false)
        {
            audioSources[0].clip = gameMusic;
            audioSources[0].Play();
            audioSources[1].clip = lowEngineSound;
            audioSources[1].Play();
            isGameMusic = true;
        }

        if (GameManager.instance.currentState == GameState.GameOver)
        {
            isGameMusic = false;
        }
    }

    public void ChangeToMenuMusic()
    {
        if (GameManager.instance.currentState == GameState.MainMenu && isGameMusic == true)
        {
            audioSources[0].clip = menuMusic;
            audioSources[0].Play();
            audioSources[1].clip = lowEngineSound;
            audioSources[1].Play();
            isGameMusic = false;
        }
    }

    public void PlayFastSound()
    {
        if (isChangedToFast == false)
        {
            audioSources[1].clip = fastEngineSound;
            audioSources[1].Play();
            isChangedToFast = true;
        }
    }

    public void PlayLowSound()
    {
        if (isChangedToFast == true)
        {
            audioSources[1].clip = lowEngineSound;
            audioSources[1].Play();
            isChangedToFast = false;
        }
    }

    public void PlayCrashSound()
    {
        foreach (var audio in audioSources)
        {
            audio.Stop();
        }
        audioSources[1].PlayOneShot(crashSound);
    }

    public void PlayPodiumSound()
    {
        if (GameManager.instance.currentState == GameState.GameOver)
        {
            audioSources[0].PlayOneShot(podiumSound);
        }
    }
}
