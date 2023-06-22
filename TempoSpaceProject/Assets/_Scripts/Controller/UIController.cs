using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{   
    [Header("Text")]
    public Text txt_score;
    public Text txt_health;
    public Text txt_powerup;
    public Text txt_tokens;


    [Header("Audio")]
    public Slider masterSlider;
    public Slider musicSlider;
    public Slider sfxSlider;
    

    [Header("Scene Management")]
    public string currentScene;
    public string previousScene;


    [Header("Hud")]
    public Image ShieldBarFull;
    public Image BombBarFull;


    [Header("Menus")]
    public GameObject optPanel;

    [Header("Color Management")]
    public Button HealthButton;

    private void Start() {
        GameController.controller.ui_controller = this;
        SetDefaultValues();
    }
    private void Update(){
        AudioSceneCheck();
    }

    public void SceneChange(string scene){
        SceneManager.LoadScene(scene);
    }
    public void SceneChangeByIndex(int sceneIndex){
        SceneManager.LoadScene(sceneIndex);
    }

    public int GetCurrentSceneIndex(){
        return SceneManager.GetActiveScene().buildIndex;
    }

    public int GetNextSceneIndex(){
        int numberOfScenes = SceneManager.sceneCountInBuildSettings;
        int currentScene = GetCurrentSceneIndex();
        return  currentScene + 1 < numberOfScenes ? currentScene + 1 : 0;
    }

    public void QuitGame(){
        Debug.Log("Quit");
        Application.Quit();
    }



    private void AudioSceneCheck(){
        currentScene = SceneManager.GetActiveScene().name;

        if (previousScene != currentScene){
            AudioController.controller.ChangeSong(currentScene);
            previousScene = currentScene;
        } 
    }
    public void SetDefaultValues(){
        AudioController.controller.mixer.GetFloat("MasterVol", out float masterValue);
        if(masterSlider!=null) masterSlider.value = masterValue;

        AudioController.controller.mixer.GetFloat("MusicVol", out float musicValue);
        if(musicSlider!=null) musicSlider.value = musicValue;

        AudioController.controller.mixer.GetFloat("SFXVol", out float sfxValue);
        if(sfxSlider!=null) sfxSlider.value = sfxValue;

        if(optPanel!=null) HideOptMenu();
    }
    public void ChangeMasterVol(){
        AudioController.controller.ChangeMasterVol(masterSlider.value);
    }
    public void ChangeMusicVol(){
        AudioController.controller.ChangeMusicVol(musicSlider.value);
    }
    public void ChangeSFXVol(){
        AudioController.controller.ChangeSFXVol(sfxSlider.value);
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
        CancelInvoke("_ClearPowerupText");
        txt_powerup.text = pwp_text;
        Invoke("_ClearPowerupText", 2);
    }
    private void _ClearPowerupText(){
        txt_powerup.text = "";
    }




    public void SetShieldBarValue(float value){
        ShieldBarFull.fillAmount = value / 100;
    }
    public void SetBombBarValue(float value){
        BombBarFull.fillAmount = value / 100;
    }
    public void HandleTokenButtons(int buttonIndex){
        GameController.controller.SpendToken(buttonIndex);   
    }


    public void ShowOptMenu(){
        optPanel.SetActive(true);
    }

    public void Unpause(){
        GameController.controller.HandlePause(true);
    }

    public void HideOptMenu(){
        optPanel.SetActive(false);
    }

    public void ImageColorChange()
    {

    }
    public void ImageColorNormal()
    {

    }
}
