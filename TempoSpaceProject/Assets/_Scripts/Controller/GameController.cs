using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController controller;
    public UIController ui_controller;
    public int score, health, maxHealth;
    public GameObject PowerUpObject;

    private void Awake() {
        if(controller == null){
            controller = this;
        }else{
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this);

        health = maxHealth;
    }
    public void AddScore(){
        score+=200;
        ui_controller.UpdateScore(score);

    }
    public void AddHealth(){
        health++;
        if (health >= maxHealth)
        {
            health = maxHealth;
        }
        ui_controller.UpdateHealth(health);
        ui_controller.SetPowerupText("HEALTH REGEN");
    }
    public void ReduceHealth(){
        health--;
        if(health<=0){
            ResetHealthAndScore();
            ui_controller.SceneChange("DefeatScene");
        }
        ui_controller.UpdateHealth(health);
    }

    public void Win(){
        ResetHealthAndScore();
        ui_controller.SceneChange("VictoryScene");
    }

    public void ResetHealthAndScore(){
        health = maxHealth;
        score = 0;
    }

}