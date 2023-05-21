using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    private Player script;

    // Eu j� fiz todos os 3 primeiros PowerUps como prefabs dentro das pastas, j� est�o com tags tamb�m, s� falta colocar cor e colocar a fun��o do shield. 

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player")
        {

            switch (other.tag)
            {
            case "Life":
                GameController.controller.AddHealth();
                break;

            case "FireRateIncrease": // Arranjei um jeito de fazer isso funcionar, e pelo visto deu certo. Ainda tem que testar
                    Player.player.fireRate -= 2;
                break;

            case "ShieldUp": //Criar o escudo e colocar a tag dele aqui pra ele ser ativadado.

                break;

            default:
                break;
            }

            Destroy(gameObject);
        } 
    }
}
