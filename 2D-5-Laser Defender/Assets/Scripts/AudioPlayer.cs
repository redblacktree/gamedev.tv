using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [Header("Volume")]
    [SerializeField] [Range(0, 1)] float masterVolume = 0.5f;
    [SerializeField] [Range(0, 1)] float musicVolume = 0.5f;
    [SerializeField] [Range(0, 1)] float sfxVolume = 0.5f;

    [Header("Player")]
    [SerializeField] AudioClip playerExplosionClip;
    [SerializeField] [Range(0, 1)] float playerExplosionVolume = 0.5f;
    [SerializeField] AudioClip playerShootingClip;
    [SerializeField] [Range(0, 1)] float playerShootingVolume = 0.5f;
    [SerializeField] AudioClip playerHitClip;
    [SerializeField] [Range(0, 1)] float playerHitVolume = 0.5f;

    [Header("Enemy")]
    [SerializeField] AudioClip enemyExplosionClip;
    [SerializeField] [Range(0, 1)] float enemyExplosionVolume = 0.5f;
    [SerializeField] AudioClip enemyShootingClip;
    [SerializeField] [Range(0, 1)] float enemyShootingVolume = 0.25f;
    [SerializeField] AudioClip enemyHitClip;
    [SerializeField] [Range(0, 1)] float enemyHitVolume = 0.5f;

    AudioSource musicAudioSource;
    static AudioPlayer instance;

    void Awake()
    {
        ManageSingleton();
        musicAudioSource = GetComponent<AudioSource>();        
    }

    void ManageSingleton()
    {
        if (instance != null)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        musicAudioSource.volume = masterVolume * musicVolume;
    }

    void Update()
    {
        // TODO: This seems wasteful. Probably there is a better way to do this.
        musicAudioSource.volume = masterVolume * musicVolume;   
    }

    public void PlayShooting(bool isPlayer)
    {
        if (isPlayer)
        {
            PlayClip(playerShootingClip, playerShootingVolume);
        }
        else
        {
            PlayClip(enemyShootingClip, enemyShootingVolume);
        }        
    }

    public void PlayHit(bool isPlayer)
    {
        if (isPlayer)
        {
            PlayClip(playerHitClip, playerHitVolume);
        }
        else
        {
            PlayClip(enemyHitClip, enemyHitVolume);
        }
    }

    public void PlayExplosion(bool isPlayer)
    {
        if (isPlayer)
        {
            PlayClip(playerExplosionClip, playerExplosionVolume);
        }
        else
        {
            PlayClip(enemyExplosionClip, enemyExplosionVolume);
        }
    }

    public void PlayClip(AudioClip clip, float volume)
    {
        if (clip != null) 
        {
            AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position, volume * masterVolume * sfxVolume);
        }
    }
}
