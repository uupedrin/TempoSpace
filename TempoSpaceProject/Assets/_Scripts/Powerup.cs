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
            powerupIndex = Random.Range(1,4);
        }

        switch(powerupIndex){
            case 1: //Shield
                grayscaller.objectColor = Color.blue;
                grayscaller.UpdateMaterialColors();
            break;
            case 2:
                grayscaller.objectColor = Color.red;
                grayscaller.UpdateMaterialColors();
            break;
            case 3:
                grayscaller.objectColor = Color.green;
                grayscaller.UpdateMaterialColors();
            break;
        }
    }
}
