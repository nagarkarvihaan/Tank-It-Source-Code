using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;
using UnityEngine.SceneManagement;

public class PlayerInputHandler : MonoBehaviour
{
    private TankBodyMovement tbm;
    private PistonMovement pm;
    private PlayerInput playerInput;
    PlayerControls controls;
    InputAction.CallbackContext myMovecontext;
    InputAction.CallbackContext myTurncontext;
    private static bool now = false;

    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        var movers = FindObjectsOfType<TankBodyMovement>();
        var pistons = FindObjectsOfType<PistonMovement>();
        var index = playerInput.playerIndex;
        tbm = movers.FirstOrDefault(m => m.GetPlayerIndex() == index);
        pm = pistons.FirstOrDefault(p => p.GetPlayerIndex() == index);
    }

    void FixedUpdate()
    {
        if (tbm != null && GameManager.GameisOn) {
            tbm.OnMove(myMovecontext);
            tbm.OnTurn(myTurncontext);
        }
        if (pm != null && GameManager.GameisOn) {
            pm.MyUpdate();
        }
    }

    public void Move(InputAction.CallbackContext context) {
        myMovecontext = context;
        if (tbm != null && GameManager.GameisOn) {
            tbm.OnMove(context);
        }
    }
    public void Turn(InputAction.CallbackContext context) {
        myTurncontext = context;
        if (tbm != null && GameManager.GameisOn) {
            tbm.OnTurn(context);
        }
    }
    public void Recoil(InputAction.CallbackContext context) {
        if (pm != null && GameManager.GameisOn) {
            pm.OnRecoil(context);
        }
    }
    public void Hit(InputAction.CallbackContext context) {
        if (pm != null && GameManager.GameisOn) {
            pm.OnHit(context);
        }
    }    

    public void StartGame() {
        if (GameManager.GameisOn == false && GameManager.ConnectedPlayers >= 2 && GameManager.Rounds == 0 && GameManager.ShowInstructions == false) {
            GameManager.ShowInstructions = true;
            StartCoroutine(StartLag());
        }
        else if (GameManager.GameisOn == false && GameManager.ConnectedPlayers >= 2 && GameManager.Rounds == 0 && GameManager.ShowInstructions == true && now) {
            GameManager.ShowInstructions = false;
            GameManager.GameisOn = true;
            GameManager.Rounds += 1;
            GameObject.Find("GameManager").SendMessage("NextRound");
        }
        else if (GameManager.GameisOn == false && GameManager.ConnectedPlayers >= 2 && GameManager.Rounds > 0) {
            GameManager.GameisOn = true;
            GameManager.AlivePlayers = GameManager.ConnectedPlayers;
            GameManager.Rounds += 1;
            GameObject.Find("GameManager").SendMessage("NextRound");
        }  
        
        
        
        
        /*if (GameManager.GameisOn == false && GameManager.ConnectedPlayers >= 2 && GameManager.Rounds == 0 && GameManager.ShowInstructions == false) {
            GameManager.ShowInstructions = true;
            GameManager.GameisOn = false;
        }
        else if (GameManager.GameisOn == false && GameManager.ConnectedPlayers >= 2 && GameManager.Rounds == 0 && GameManager.ShowInstructions == true) {
            GameManager.ShowInstructions = false;
            GameManager.Rounds += 1;
            GameObject.Find("GameManager").SendMessage("NextRound");    
        } 
        else if (GameManager.GameisOn == false && GameManager.ConnectedPlayers >= 2 && GameManager.Rounds > 0 && GameManager.ShowInstructions == false) {
            GameManager.AlivePlayers = GameManager.ConnectedPlayers;
            GameManager.Rounds += 1;
            GameObject.Find("GameManager").SendMessage("NextRound");
        }*/

        if (GameManager.Rounds < 6 && ((GameManager.Tank1Score == 3) || (GameManager.Tank2Score == 3) || (GameManager.Tank3Score == 3) || (GameManager.Tank4Score == 3))) {
            SceneManager.LoadScene("GameOverScene");
        }
        else if (GameManager.Rounds == 6) {
            SceneManager.LoadScene("GameOverScene");    
        }
    }

    public void Connect(InputAction.CallbackContext context) {
        if (context.performed && !GameManager.GameisOn) {
            if(tbm.gameObject.name == "Tank1") {
                GameObject.Find("GameManager").SendMessage("ConnectionStatus", "Red Tank Connected!");
            } else if(tbm.gameObject.name == "Tank2") {
                GameObject.Find("GameManager").SendMessage("ConnectionStatus", "Blue Tank Connected!");
            } else if(tbm.gameObject.name == "Tank3") {
                GameObject.Find("GameManager").SendMessage("ConnectionStatus", "Green Tank Connected!");
            } else if(tbm.gameObject.name == "Tank4") {
                GameObject.Find("GameManager").SendMessage("ConnectionStatus", "Yellow Tank Connected!");
            }
        }
    }

    IEnumerator StartLag() {
        yield return new WaitForSeconds(0.5f);
        now = true;
    }
}
