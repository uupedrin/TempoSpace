using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    private Player script;

    private void OnTriggerEnter(Collider other) {
            switch (other.tag)
            {
            case "Life":
                Destroy(this.gameObject);
                GameController.controller.AddHealth();
                break;

            case "FireRateIncrease": // Não sei dar Get Component pra resolver o firerate :D

                break;

            case "Shield": //Criar o escudo e colocar a tag dele aqui pra ele ser ativdado.

                break;

            default:
                break;
            }
        Destroy(gameObject);
    }
}
