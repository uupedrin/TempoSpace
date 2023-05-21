using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelChanger : MonoBehaviour
{
    private string sceneToFade;
    public UIController ui;
    private Animator animator;
    public void FadeScene(string scene){
        sceneToFade = scene;
        animator.SetTrigger("FadeOut");
    }
    public void OnFadeComplete(){
        ui.SceneChange(sceneToFade);
    }
    private void Start() {
        animator = GetComponent<Animator>();
    }
}
