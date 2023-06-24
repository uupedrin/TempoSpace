using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController controller;
    public UIController ui_controller;
    public int score, health, maxHealth, tokens;

    public int upgradesFireRate, upgradesWeapons = 0, upgradesSpeed;

    public float sceneSpeed = 30f;

    public float ivulnerabilityTime = 1.5f;

    public GameObject PowerUpObject;

    public List<Enemy> enemiesInScene = new List<Enemy>();

    public bool isPaused = false, unlimitedHealth = false, unlimitedTokens = false, ivulnerable = false;

    public int killsNeededForToken = 5, tokensSpawned = 0, enemiesKillCount = 0;

    bool CanCallRepair = true;

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
        if (!unlimitedHealth || ivulnerable)
        {
            ResetAllUpgrades();
            health--;
            ivulnerable = true;
            if(health<=0){
                ResetHealthAndScore();
                ui_controller.SceneChange("DefeatScene");
            }
            ui_controller.UpdateHealth(health);
            Invoke("DeactivateIvulnerability", ivulnerabilityTime);
        }
    }

    void DeactivateIvulnerability(){
        ivulnerable = false;
    }

    public void ResetAllUpgrades(){
        upgradesFireRate = 0;
        upgradesSpeed = 0;
        upgradesWeapons = 0;
    }

    public void Win(){
        ResetHealthAndScore();
        ui_controller.SceneChange("VictoryScene");
    }

    public void ResetHealthAndScore(){
        health = maxHealth;
        score = 0;
    }

    public void HandlePause(bool togglePause = false){
        if (Input.GetKeyDown(KeyCode.Escape) || togglePause)
        {
            switch(Time.timeScale){
                case 0:
                    Time.timeScale = 1;
                    isPaused = false;
                    if(ui_controller.optPanel != null) ui_controller.HideOptMenu();
                    break;
                case 1:
                    Time.timeScale = 0;
                    isPaused = true;
                    if(ui_controller.optPanel != null) ui_controller.ShowOptMenu();
                    break;
            }
        }
    }

    void Update()
    {
        HandlePause();
        HandleCheats();
        HandleBuying();
    }



    public void EarnToken(int tokensAmount = 1){
        tokens += tokensAmount;
        ui_controller.UpdateTokens(tokens);
    }
    public void SpendToken(int pressedButton){
        if (tokens > 0 || unlimitedTokens)
        { 
            switch (pressedButton)
            {
                case 0: //Firerate Button
                    if(upgradesFireRate >= 4) return;
                    ui_controller.SetPowerupText("FIRERATE INCREASED");
                    upgradesFireRate++;
                    break;

                case 1: //More Weapons Button
                    if(upgradesWeapons >= 3) return;
                    ui_controller.SetPowerupText("MORE WEAPONS");
                    upgradesWeapons++;
                    break;

                case 2: //Speed Button
                    if(upgradesSpeed >= 4) return;
                    ui_controller.SetPowerupText("SPEED INCREASED");
                    upgradesSpeed++;
                    break;

                case 3: //Heal Drone Button
                    if (CanCallRepair == true)
                    {
                        AddHealth();
                        ui_controller.HealthButton.interactable = false;
                        CanCallRepair = false;
                        Invoke("RepairTimerTrue", 4f);
                    }
                    break;
            }
            if(!unlimitedTokens) tokens--;
            ui_controller.UpdateTokens(tokens);
        }
    }

    void RepairTimerTrue()
    {
        CanCallRepair = true;
        ui_controller.HealthButton.interactable = true;
    }

    public bool CheckTokens(){
        if(enemiesKillCount >= killsNeededForToken + tokensSpawned){
            enemiesKillCount = 0;
            tokensSpawned++;
            return true;
        }
        return false;
    }
    public void EnemyKillIncrease(){
        enemiesKillCount++;
    }
    public void ResetEnemyKills(){
        enemiesKillCount = 0;
        tokensSpawned = 0;
    }



    void HandleCheats(){
        if(!isPaused){
            if (Input.GetButtonDown("Skip Level"))
            {
                //Handle Level Skip
                ui_controller.SceneChangeByIndex(ui_controller.GetNextSceneIndex());
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

    void HandleBuying(){
        if(isPaused) return;

        if(Input.GetButtonDown("Firerate")) SpendToken(0);
        if(Input.GetButtonDown("More Weapons")) SpendToken(1);
        if(Input.GetButtonDown("Speed")) SpendToken(2);
        if(Input.GetButtonDown("Call Heal Drone")) SpendToken(3);
    }

}