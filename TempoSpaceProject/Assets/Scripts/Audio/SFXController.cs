using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXController : MonoBehaviour
{
    public static SFXController controller;
    public AudioSource[] sfxs;
    public void PlaySFX(int sfxIndex){
        sfxs[sfxIndex].Play();
    }
}
