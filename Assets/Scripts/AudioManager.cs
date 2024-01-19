using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("----------- Audio Source -----------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("----------- Audio Clip -----------")]
    public AudioClip background;
    public AudioClip Dead;
    public AudioClip Win;
    public AudioClip Gameover;
    public AudioClip Can;

    private void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

    public void StopMusic()
    {
        if (musicSource.isPlaying)
        {
            musicSource.Stop();
        }
    }

    public void PlayMusic()
    {
        if (!musicSource.isPlaying) // Eğer müzik çalmıyorsa
        {
            musicSource.clip = background; // Burada gameMusic, oynatmak istediğiniz müzik parçasıdır.
            musicSource.Play(); // Müziği başlat
        }
    }

}
