using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public Slider m_slider;
    public Slider SFX_slider;
    


    [Header("------------ Audio Source ------------")]
    [SerializeField] AudioSource m_Source;
    [SerializeField] AudioSource SFX_Source;
    [SerializeField] AudioSource Looping_Source;

    [Header("------------ Audio Clip ------------")]
    [SerializeField] public AudioClip background;
    [SerializeField] public AudioClip[] slices;
    [SerializeField] public AudioClip throwing;
    [SerializeField] public AudioClip button_click;
    [SerializeField] public AudioClip explode;
    [SerializeField] public AudioClip missed_fruit;
    [SerializeField] public AudioClip fuse;
    [SerializeField] public AudioClip whoosh;
    [SerializeField] public AudioClip miss;

    public float fadeDuration = 1.5f;
    public float silenceDuration = 1f;

    public float startVolume = 0.12f;

    private bool isLoaded = false;

    private void Start()
    {
        PlayBackGroundMusic();
        Load();

    }
    public void PlayBackGroundMusic()
    {
        m_Source.clip = background;
        m_Source.loop = true;
        m_Source.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        if (SFX_Source != null)
        {
            SFX_Source.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning("AudioSource is missing or has been destroyed!");
        }
    }

    public void PlayButtonClickedSound()
    {
        PlaySFX(button_click);
    }
    public void ChangeVolume()
    {
        m_Source.volume = m_slider.value;
        SFX_Source.volume = SFX_slider.value;
        if (SFX_Source != null )
            Looping_Source.volume = SFX_slider.value;
        Save();
    }
    public void PlayRandomSound(AudioClip[] audioClips)
    {
        int randomIndex = Random.Range(0, audioClips.Length);
        PlaySFX(audioClips[randomIndex]);
    }

    private void Save()
    {
        if (!isLoaded) return;
        PlayerPrefs.SetFloat("musicVolume", m_slider.value);
        PlayerPrefs.SetFloat("SFXVolume", SFX_slider.value);
        SFX_slider.value = PlayerPrefs.GetFloat("SFXVolume", startVolume);
        m_slider.value = PlayerPrefs.GetFloat("musicVolume", startVolume);
    }
    private void Load()
    {
        SFX_slider.value = PlayerPrefs.GetFloat("SFXVolume", startVolume);
        m_slider.value = PlayerPrefs.GetFloat("musicVolume", startVolume);
        isLoaded = true;
    }

    public void PlayLoopingSound(AudioClip clip)
    {
        if (!Looping_Source.isPlaying)
        {
            Looping_Source.clip = clip;
            Looping_Source.loop = true;
            Looping_Source.Play();
        }
    }

    public void StopLoopingSound()
    {
        if (Looping_Source.isPlaying)
        {
            Looping_Source.Stop();
        }
    }
    public IEnumerator FadeOutInRoutineBG()
    {
        float startVolume = m_Source.volume;

        float time = 0f;
        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            m_Source.volume = Mathf.Lerp(startVolume, 0f, time / fadeDuration);
            yield return null;
        }

        yield return new WaitForSeconds(silenceDuration);
        m_Source.volume = 0f;
        time = 0f;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            m_Source.volume = Mathf.Lerp(0f, startVolume, time / fadeDuration);
            yield return null;
        }

        m_Source.volume = startVolume;
    }

}
