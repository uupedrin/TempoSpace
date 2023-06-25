using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour
{
    Animator myAnimator;
    Collider myCollider;

    void Start()
    {
        myAnimator = GetComponentInChildren<Animator>();
        myCollider = GetComponent<Collider>();
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player")){
            print("Player Enter");
            myAnimator.SetBool("doorOpen", true);
            Destroy(myCollider);
        }
    }
}
