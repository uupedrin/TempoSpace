using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Music", menuName = "ScriptableOBJ/AudioOBJ/Music", order = 1)]
public class MusicStruct : ScriptableObject
{
    public AudioClip audio;
    public bool loop;
}