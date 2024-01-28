using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ManagerScript : MonoBehaviour
{
    public TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        if((GameManager.Tank1Score > GameManager.Tank2Score) && (GameManager.Tank1Score > GameManager.Tank3Score) && (GameManager.Tank1Score > GameManager.Tank4Score)) {
            text.text = "Red Tank Wins!";
        }
        else if ((GameManager.Tank2Score > GameManager.Tank1Score) && (GameManager.Tank2Score > GameManager.Tank3Score) && (GameManager.Tank2Score > GameManager.Tank4Score)) {
            text.text = "Blue Tank Wins!";
        }
        else if ((GameManager.Tank3Score > GameManager.Tank1Score) && (GameManager.Tank3Score > GameManager.Tank2Score) && (GameManager.Tank3Score > GameManager.Tank4Score)) {
            text.text = "Green Tank Wins!";
        }
        else if ((GameManager.Tank4Score > GameManager.Tank1Score) && (GameManager.Tank4Score > GameManager.Tank2Score) && (GameManager.Tank4Score > GameManager.Tank3Score)) {
            text.text = "Yellow Tank Wins!";
        }
        else {
            text.text = "Tie!";
        }
    }
}
