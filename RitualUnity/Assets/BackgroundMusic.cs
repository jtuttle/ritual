using UnityEngine;
using System.Collections;

public class BackgroundMusic : MonoBehaviour {

    public AudioClip[] clips;

    void Awake()
    {
        int randomclips = Random.Range(0, clips.Length);
        AudioSource source = gameObject.AddComponent<AudioSource>();
        source.clip = clips[randomclips];
        source.volume = 1.0f;
        source.Play();
        Destroy(source, clips[randomclips].length);
        Invoke("PlayNextSong", source.clip.length);
    }


    void PlayNextSong()
    {
        AudioSource source = gameObject.AddComponent<AudioSource>();
        
            int randomclips = Random.Range(0, clips.Length);

            source.clip = clips[randomclips];
            source.volume = .01f;
            source.Play();
            Destroy(source, clips[randomclips].length);
            Invoke("PlayNextSong", source.clip.length);
        }
    }