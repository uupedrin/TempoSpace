using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Text txt_score, txt_health, txt_powerup, txt_tokens;
    private void Start() {
        GameController.controller.ui_controller = this;
    }
    public void UpdateScore(int score){
        txt_score.text = score.ToString("0000000");
    }
    public void UpdateHealth(int health){
        txt_health.text = "<>" + health;
    }
    public void UpdateTokens(int tokens){
        txt_tokens.text = tokens.ToString("0000");
    }

    public void SetPowerupText(string pwp_text){
        txt_powerup.text = pwp_text;
        Invoke("_ClearPowerupText", 2);
    }
    private void _ClearPowerupText(){
        txt_powerup.text = "";
    }

    public Image ShieldBarFull;
    public void SetShieldBarValue(float value){
        ShieldBarFull.fillAmount = value / 100;
    }

    public void SceneChange(string scene){
        SceneManager.LoadScene(scene);
    }
    public void QuitGame(){
        Debug.Log("Quit");
        Application.Quit();
    }

    public void HandleTokenButtons(int buttonIndex){
        GameController.controller.SpendToken(buttonIndex);   
    }
}
