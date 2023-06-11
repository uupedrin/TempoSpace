using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    
    public int powerupIndex = 0;
    GrayscaleColorSelect grayscaller;
    
    void Start()
    {
        grayscaller = GetComponent<GrayscaleColorSelect>();

        if(powerupIndex <= 0){//No Powerup
            powerupIndex = Random.Range(1,3);
        }

        switch(powerupIndex){
            case 1: //Shield
                gameObject.tag = "PWPShield";
                grayscaller.objectColor = Color.blue;
                grayscaller.UpdateMaterialColors();
            break;
            case 2:
                gameObject.tag = "PWPBomb";
                grayscaller.objectColor = Color.red;
                grayscaller.UpdateMaterialColors();
            break;
            
        }
        transform.position = new Vector3(transform.position.x,transform.position.y,100);
    }

    void Update(){
        Movement();
    }

    void Movement(){
        if(GameController.controller.isPaused) return;

        transform.position += Vector3.back * Time.deltaTime * GameController.controller.sceneSpeed / 3f;
    }
}
