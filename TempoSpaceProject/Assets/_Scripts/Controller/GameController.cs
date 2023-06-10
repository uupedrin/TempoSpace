using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController controller;
    public UIController ui_controller;
    public int score, health, maxHealth, tokens;

    public int upgradesFireRate, upgradesWeapons, upgradesSpeed;

    public float sceneSpeed;

    public GameObject PowerUpObject;

    public bool isPaused = false, unlimitedHealth = false, unlimitedTokens = false;

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
        if (!unlimitedHealth)
        {
            health--;
            if(health<=0){
                ResetHealthAndScore();
                ui_controller.SceneChange("DefeatScene");
            }
            ui_controller.UpdateHealth(health);
        }
    }

    public void Win(){
        ResetHealthAndScore();
        ui_controller.SceneChange("VictoryScene");
    }

    public void ResetHealthAndScore(){
        health = maxHealth;
        score = 0;
    }

    public void HandlePause(){
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            switch(Time.timeScale){
                case 0:
                    Time.timeScale = 1;
                    isPaused = false;
                    break;
                case 1:
                    Time.timeScale = 0;
                    isPaused = true;
                    break;
            }
        }
    }

    void Update()
    {
        HandlePause();
        HandleCheats();
    }

    public void EarnToken(int tokensAmount = 1){
        tokens += tokensAmount;
    }

    public void SpendToken(int pressedButton){
        if (tokens > 0)
        {
            tokens--;
            switch (pressedButton)
            {
                case 0: //Firerate Button
                    upgradesFireRate++;
                    break;

                case 1: //More Weapons Button
                    upgradesWeapons++;
                    break;

                case 2: //Speed Button
                    upgradesSpeed++;
                    break;

                case 3: //Heal Drone Button
                    print("Heal Press");
                    break;
            }
            ui_controller.UpdateTokens(tokens);
        }
    }

    void HandleCheats(){
        if(!isPaused){
            if (Input.GetButtonDown("Skip Level"))
            {
                //Handle Level Skip
            }

            if (Input.GetButtonDown("Infinite Health"))
            {
                //Toggle unlimited health
                switch (unlimitedHealth)
                {
                    case true:
                        unlimitedHealth = false;
                        break;
                    case false:
                        unlimitedHealth = true;
                        break;
                }
            }

            if (Input.GetButtonDown("Infinite Tokens"))
            {
                //Toggle unlimited tokens
                switch (unlimitedTokens)
                {
                    case true:
                        unlimitedTokens = false;
                        break;
                    case false:
                        unlimitedTokens = true;
                        break;
                }
            }
        }
    }

}