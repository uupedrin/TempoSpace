using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Text txt_score, txt_health, txt_powerup;
    private void Start() {
        GameController.controller.ui_controller = this;
    }
    public void UpdateScore(int score){
        txt_score.text = "Score: " + score;
    }
    public void UpdateHealth(int health){
        txt_health.text = "Health: " + health;
    }
    public void SetPowerupText(string pwp_text){
        txt_powerup.text = pwp_text;
        Invoke("_ClearPowerupText", 2);
    }
    private void _ClearPowerupText(){
        txt_powerup.text = "";
    }
    public void SceneChange(string scene){
        SceneManager.LoadScene(scene);
    }
    public void QuitGame(){
        Debug.Log("Quit");
        Application.Quit();
    }
}
