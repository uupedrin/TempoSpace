using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    bool selfActive = false;
    Collider selfCollider;
    ParticleSystem selfParticles;

    void Start()
    {
        selfCollider = GetComponent<Collider>();
        selfParticles = GetComponent<ParticleSystem>();
        StartCoroutine(LateStart(.2f));
    }

    IEnumerator LateStart(float waitTime)
    {   
        yield return new WaitForSeconds(waitTime);
        DisableShield();
    }

    void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "EBullet":
            Destroy(other.gameObject);
            DisableShield();
            break;
        }
            
    }

    public void EnableShield(){
        selfActive = true;
        selfCollider.enabled = true;
        selfParticles.Play();
        SetShieldBarValue(100);
    }
    public void DisableShield(){
        selfActive = false;
        selfCollider.enabled = false;
        selfParticles.Stop();
        SetShieldBarValue(0);
    }

    public bool getShieldActive(){
        return selfActive;
    }

    void SetShieldBarValue(float value){
        GameController.controller.ui_controller.SetShieldBarValue(value);
    }
}

