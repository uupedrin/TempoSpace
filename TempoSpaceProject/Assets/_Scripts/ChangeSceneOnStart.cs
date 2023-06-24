using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSceneOnStart : MonoBehaviour
{
    void OnEnable()
    {
        GameController.controller.ui_controller.SceneChange("Level01");
    }
}
