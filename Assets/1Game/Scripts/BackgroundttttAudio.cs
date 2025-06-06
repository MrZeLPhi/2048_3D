
using UnityEngine;
using UnityEngine.UI;

public class BackgroundttttAudio : MonoBehaviour
{
    public Slider musicSlider;
    public Slider sfxSlider;
    public Slider buttonSlider;
    
    public AudioClip musicClip;
    private AudioSource musicAudioSource;
    public static BackgroundttttAudio _instance;

    public AudioSource jumpSound;
    public AudioSource buttonSound;
    public AudioSource collisionSound;
    public AudioSource healSound;

    // Константа для максимального значення слайдера
    private const float SLIDER_MAX_VALUE = 100f; 

    private const string MUSIC_VOLUME_KEY = "MusicVolume";
    private const string SFX_VOLUME_KEY = "SFXVolume";
    private const string BUTTON_VOLUME_KEY = "ButtonVolume";

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }

        musicAudioSource = gameObject.AddComponent<AudioSource>();
        musicAudioSource.clip = musicClip;
        musicAudioSource.loop = true;
        
        if (musicSlider != null)
        {
            // Встановлюємо максимальне значення слайдера програмно, щоб уникнути помилок
            musicSlider.maxValue = SLIDER_MAX_VALUE; 
            musicSlider.onValueChanged.AddListener(SetMusicVolume);
        }
        if (sfxSlider != null)
        {
            sfxSlider.maxValue = SLIDER_MAX_VALUE;
            sfxSlider.onValueChanged.AddListener(SetSFXVolume);
        }
        if (buttonSlider != null)
        {
            buttonSlider.maxValue = SLIDER_MAX_VALUE;
            buttonSlider.onValueChanged.AddListener(SetButtonVolume);
        }
    }

    void Start()
    {
        LoadVolumes();
        ApplyVolumes();
        musicAudioSource.Play();
    }

    // Метод для оновлення гучності музики через слайдер
    public void SetMusicVolume(float sliderValue)
    {
        // Перетворюємо значення слайдера (0-100) на діапазон (0-1)
        musicAudioSource.volume = sliderValue / SLIDER_MAX_VALUE; 
        SaveVolume(MUSIC_VOLUME_KEY, sliderValue);
    }

    // Метод для оновлення гучності SFX через слайдер
    public void SetSFXVolume(float sliderValue)
    {
        float actualVolume = sliderValue / SLIDER_MAX_VALUE;
        jumpSound.volume = actualVolume;
        collisionSound.volume = actualVolume;
        healSound.volume = actualVolume;
        SaveVolume(SFX_VOLUME_KEY, sliderValue);
    }

    // Метод для оновлення гучності кнопок через слайдер
    public void SetButtonVolume(float sliderValue)
    {
        float actualVolume = sliderValue / SLIDER_MAX_VALUE;
        buttonSound.volume = actualVolume;
        SaveVolume(BUTTON_VOLUME_KEY, sliderValue);
    }

    // Загальний метод для збереження гучності
    private void SaveVolume(string key, float volume)
    {
        PlayerPrefs.SetFloat(key, volume);
        PlayerPrefs.Save();
    }

    // Метод для завантаження збережених значень гучності
    private void LoadVolumes()
    {
        // Встановлюємо значення слайдерів з PlayerPrefs
        // Значення за замовчуванням: 50 (що відповідає 0.5f в діапазоні 0-1)
        if (musicSlider != null)
        {
            musicSlider.value = PlayerPrefs.GetFloat(MUSIC_VOLUME_KEY, SLIDER_MAX_VALUE / 2f);
        }
        if (sfxSlider != null)
        {
            sfxSlider.value = PlayerPrefs.GetFloat(SFX_VOLUME_KEY, SLIDER_MAX_VALUE / 2f);
        }
        if (buttonSlider != null)
        {
            buttonSlider.value = PlayerPrefs.GetFloat(BUTTON_VOLUME_KEY, SLIDER_MAX_VALUE / 2f);
        }
    }

    // Метод для застосування завантажених значень гучності
    private void ApplyVolumes()
    {
        // Застосовуємо значення гучності, перетворені з діапазону 0-100 на 0-1
        if (musicSlider != null)
        {
            musicAudioSource.volume = musicSlider.value / SLIDER_MAX_VALUE;
        }
        else
        {
            musicAudioSource.volume = PlayerPrefs.GetFloat(MUSIC_VOLUME_KEY, SLIDER_MAX_VALUE / 2f) / SLIDER_MAX_VALUE;
        }

        if (sfxSlider != null)
        {
            float sfxActualVolume = sfxSlider.value / SLIDER_MAX_VALUE;
            jumpSound.volume = sfxActualVolume;
            collisionSound.volume = sfxActualVolume;
            healSound.volume = sfxActualVolume;
        }
        else
        {
            float defaultSFXSliderValue = PlayerPrefs.GetFloat(SFX_VOLUME_KEY, SLIDER_MAX_VALUE / 2f);
            float defaultSFXActualVolume = defaultSFXSliderValue / SLIDER_MAX_VALUE;
            jumpSound.volume = defaultSFXActualVolume;
            collisionSound.volume = defaultSFXActualVolume;
            healSound.volume = defaultSFXActualVolume;
        }

        if (buttonSlider != null)
        {
            buttonSound.volume = buttonSlider.value / SLIDER_MAX_VALUE;
        }
        else
        {
            float defaultButtonSliderValue = PlayerPrefs.GetFloat(BUTTON_VOLUME_KEY, SLIDER_MAX_VALUE / 2f);
            float defaultButtonActualVolume = defaultButtonSliderValue / SLIDER_MAX_VALUE;
            buttonSound.volume = defaultButtonActualVolume;
        }
    }

    public void JumpSound()
    {
        jumpSound.Play();
    }
    public void ButtonSound() {
        buttonSound.Play();
    }
    public void CollisionSound()
    {
        collisionSound.Play();
        
    }

    public void HealSound()
    {
        healSound.Play();
    }
}
