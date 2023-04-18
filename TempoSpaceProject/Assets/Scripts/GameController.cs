using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController controller;
    public UIController ui_controller;
    public int score, health;

    private void Awake() {
        if(controller == null){
            controller = this;
        }else{
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this);
    }
    public void AddScore(){
        score+=200;
        ui_controller.UpdateScore(score);

        if(score>=2000){
            ResetValues();
            ui_controller.SceneChange("VictoryScene");
        }
    }
    public void AddHealth(){
        health++;
        ui_controller.UpdateHealth(health);
        ui_controller.SetPowerupText("HEALTH REGEN");
    }
    public void ReduceHealth(){
        health--;
        if(health<=0){
            ResetValues();
            ui_controller.SceneChange("DefeatScene");
        }
        ui_controller.UpdateHealth(health);
    }

    private void ResetValues()
    {
        health = 10;
        score = 0;
    }

}
