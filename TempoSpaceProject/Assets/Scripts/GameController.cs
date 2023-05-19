using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController controller;
    public UIController ui_controller;
    public int score, health;
    public GameObject[] PowerUps; //Aqui você coloca todos os Powerups que existem.

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
            score = 0;
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
            health = 10;
            ui_controller.SceneChange("DefeatScene");
        }
        ui_controller.UpdateHealth(health);
    }

}