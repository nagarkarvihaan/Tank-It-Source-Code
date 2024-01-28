using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class TankBodyMovement : MonoBehaviour
{
    public GameObject Myself;
    public Rigidbody rigidbody;
    private PistonMovement pm;
    private float speed = 6f;
    private float turn_speed = 180f;
    private string movementAxisName;
    private string turnAxisName;
    private float movementInputValue;
    private float turnInputValue;
    public bool skidStart = false;
    public float skidTimer = 10.0f;
    private float skidForce = 2000f;
    private float skidTorque = 500f;
    public TextMeshProUGUI Mytimer;
    [SerializeField] private int playerIndex;

    public AudioSource SlipAndFall, CrowdLaugh;

    public int GetPlayerIndex() {
        return playerIndex;    
    }

    void Start()
    {
        movementAxisName = "Vertical";
        turnAxisName = "Horizontal";
        Myself.SetActive(true);
        pm = transform.GetChild(2).GetComponent<PistonMovement>();
        Mytimer.text = "";
    } 

    void onEnable() {
        rigidbody.isKinematic = false;
        movementInputValue = 0f;
        turnInputValue = 0f;
    }

    void onDisable() {
        rigidbody.isKinematic = true;
    }

    void Update()
    {
        movementInputValue = Input.GetAxis(movementAxisName);
        turnInputValue = Input.GetAxis(turnAxisName);

        if(GameManager.GameisOn) {
            if (GameManager.ConnectedPlayers == 2) {
                if (Myself.name == "Tank3" || Myself.name == "Tank4") {
                    Myself.SetActive(false);
                }
            }
            else if (GameManager.ConnectedPlayers == 3) {
                if (Myself.name == "Tank4") {
                    Myself.SetActive(false);
                }    
            }
        }

        if(skidStart && GameManager.GameisOn) {
            skidTimer -= 1.0f * Time.deltaTime;
            Mytimer.text = skidTimer.ToString("0");
        }

        if(skidTimer <= 0.0f || !this.gameObject.activeSelf) {
            skidStart = false;
            Mytimer.text = "";
        }

        if (!GameManager.GameisOn) {
            skidStart = false;
            Mytimer.text = "";
        }
    }

    public void OnMove(InputAction.CallbackContext context) {
        if (!skidStart) {
            Vector2 inputVector = context.ReadValue<Vector2>();
            Vector3 movement = transform.forward * inputVector.y * speed * Time.deltaTime;
            rigidbody.MovePosition(rigidbody.position + movement);
        }
        else if (skidStart) {
            Vector2 inputVector = context.ReadValue<Vector2>();
            Vector3 skidForceVector = transform.forward * skidForce * inputVector.y;
            Vector3 skidTorqueVector = transform.up * skidTorque * inputVector.x;
            rigidbody.AddForce(skidForceVector);
            rigidbody.AddTorque(skidTorqueVector);
        }
    }

    public void OnTurn(InputAction.CallbackContext context) {
        Vector2 rotationVector = context.ReadValue<Vector2>();
        float turn = rotationVector.x * turn_speed * Time.deltaTime;
        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
        rigidbody.MoveRotation(rigidbody.rotation * turnRotation);
    }

    public void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("TargetTag") && !pm.GetShot()) {
            if (other.rigidbody.velocity.magnitude >= 15f && GameManager.AlivePlayers > 1) {
                Myself.SetActive(false);
                GameManager.AlivePlayers -= 1;
            }
        } else if (other.gameObject.CompareTag("TargetTag") && pm.GetShot()) {
            Vector3 forward = transform.forward;
            other.rigidbody.AddForce(forward * 1000f);
        }
        else if (other.gameObject.CompareTag("LaughingBall") && skidStart == false) {
            Destroy(other.gameObject);
            skidTimer = 10.0f;
            skidStart = true;
            this.SlipAndFall.Play();
            this.CrowdLaugh.Play();
        }
        else if (other.gameObject.CompareTag("LaughingBall") && skidStart == true) {
            Destroy(other.gameObject);
            skidTimer = 10.0f;
        }
    }
}
