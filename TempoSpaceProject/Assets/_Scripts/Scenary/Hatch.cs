using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hatch : MonoBehaviour
{
    
    Animation myAnimator;
    Collider smokeCollision;
    AudioSource[] sources;
    ParticleSystem particles;
    public float activationZ = 100;

    bool isActive = false;


    //Animations
    string anim0 = "Hatch|HatchOpening";
    string anim1 = "Hatch|HatchClosing";



    void GetReferences(){
        smokeCollision = GetComponent<Collider>();
        myAnimator = GetComponentInChildren<Animation>();
        sources = GetComponentsInChildren<AudioSource>();
        particles = GetComponentInChildren<ParticleSystem>();
    }

    void DisableComponents(){
        smokeCollision.enabled = false;
    }

    void OpenHatch(){
        myAnimator.Play(anim0);
        sources[1].Play();
        particles.Play();
        smokeCollision.enabled = true;
        Invoke("CloseHatch",1f);
    }

    void CloseHatch(){
        smokeCollision.enabled = false;
        myAnimator.Play(anim1);
        sources[0].Play();
        particles.Stop();
    }

    public void HitTaken(){
        smokeCollision.enabled = false;
    }

    void Start()
    {
        GetReferences();
        DisableComponents();
    }

    void CheckPosition(){
        if(isActive) return;

        if(transform.position.z <= activationZ){
            isActive = true;
            InvokeRepeating("OpenHatch",Random.Range(2,4), Random.Range(2,4));
        }
    }

    
    void Update()
    {
        CheckPosition();
    }
}
