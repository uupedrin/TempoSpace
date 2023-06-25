using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioController : MonoBehaviour
{
    public static AudioController controller;
    private void Singleton(){
        if(controller == null){
            controller = this;
        }
        else{
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this);
    }


    public AudioMixer mixer;
    public AudioClip[] musics;
    private AudioSource source;

    private Coroutine currentSongRoutine;


    private void Awake(){
        Singleton();
    }

    private void Start(){
        GetReferences();
    }

    

    private void GetReferences(){
        source = GetComponent<AudioSource>();
    }

    public void StopMusic(){
        source.Stop();
    }

    public void PlayMusic(){
        source.Play();
    }


    public void ChangeMasterVol(float volume){
        mixer.SetFloat("MasterVol", volume);
    }
    public void ChangeMusicVol(float volume){
        mixer.SetFloat("MusicVol", volume);
    }
    public void ChangeSFXVol(float volume){
        mixer.SetFloat("SFXVol", volume);
    }


    public void ChangeSong(string contextName){
        StopMusic();
        StopAllCoroutines();
        
        switch(contextName){
            case "MainMenu":
                source.clip = musics[0];
                source.loop = false;
                StartCoroutine(MainMenuLoop(source.clip.length));
                break;
            case "Level01":
                source.clip = musics[2];
                source.loop = false;
                StartCoroutine(Level01Loop(source.clip.length));
                break;
            case "Level02":
                source.clip = musics[4];
                source.loop = false;
                StartCoroutine(Level02Loop(source.clip.length));
                break;
            case "FirstScene":
                source.clip = musics[7];
                source.loop = true;
                break;
            case "Introduction":
                source.clip = musics[6];
                source.loop = true;
                break;
            case "Credits":
                source.clip = musics[8];
                source.loop = true;
                break;
            case "DefeatScene":
                source.clip = musics[8];
                source.loop = true;
                break;
        }
        PlayMusic();
    }

    IEnumerator MainMenuLoop(float clipLength){
        yield return new WaitForSecondsRealtime(clipLength);
        source.clip = musics[1];
        source.loop = true;
        PlayMusic();
    }

    IEnumerator Level01Loop(float clipLength){
        yield return new WaitForSecondsRealtime(clipLength);
        source.clip = musics[3];
        source.loop = true;
        PlayMusic();
    }

    IEnumerator Level02Loop(float clipLength){
        yield return new WaitForSecondsRealtime(clipLength);
        source.clip = musics[5];
        source.loop = true;
        PlayMusic();
    }




}
