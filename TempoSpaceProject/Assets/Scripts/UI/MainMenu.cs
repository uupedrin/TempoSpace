using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public SFXController sfx;
    public GameObject secondMenu;
    private void Update() {
        if(Input.GetAxisRaw("Submit") == 1){
            sfx.PlaySFX(0);
            secondMenu.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
