using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public static MusicController controller;
    public MusicStruct[] musics;
    private AudioSource source;
    private int index = 0;
    
    private void PlaySong(){
        source.clip = musics[index].audio;
        source.loop = musics[index].loop;
        source.Play();
        if(index +1 < musics.Length && source.loop == false){
            Invoke("NextSong", source.clip.length);
        }
    }

    private void NextSong(){
        index++;
        source.Stop();
        PlaySong();
    }
    
    private void Start() {
        source = this.GetComponent<AudioSource>();
        PlaySong();
    }

    public void MusicControllerStop(){
        source.Stop();
    }
    
}
