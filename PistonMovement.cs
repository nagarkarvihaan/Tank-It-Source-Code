using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PistonMovement : MonoBehaviour
{
    public Rigidbody rigidbody;
    private float forceMagnifier = 1000f;
    private float pistonSpeed = 5f;
    private float pistonRange = 0.8f;
    public Transform parentTransform;
    private Vector3 initialLocalPosition;
    private Vector3 targetPosition;

    private bool followParent = true;
    private bool longPress = false;
    private bool recoilJustHappenend = false;
    private bool Shot = false;

    private float timer = 0f;
    private float temptimer = 0f;

    [SerializeField] private int playerIndex;
    public int GetPlayerIndex() {
        return playerIndex;    
    }

    public bool GetShot() {
        return Shot;
    }

    void Start() {
        parentTransform = transform.parent;
        initialLocalPosition = transform.localPosition;
        targetPosition = initialLocalPosition;
    }
    void Update() {
        transform.rotation = parentTransform.rotation;
        if (Shot == true) {
            temptimer += 0.4f * Time.deltaTime * 2;
            if (temptimer >= 0.5f) {
                Shot = false;
                temptimer = 0f;
            }
        }
    }
    public void MyUpdate() {
        if (!followParent && !longPress) {
            timer += Time.fixedDeltaTime * pistonSpeed;
            float newPosition = recoilJustHappenend ? Mathf.Lerp(-0.09f, pistonRange*4, timer) : Mathf.Lerp(0, pistonRange, timer);
            targetPosition.z = initialLocalPosition.z + newPosition;
            transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, Time.fixedDeltaTime * pistonSpeed);
            forceMagnifier = recoilJustHappenend ? 3000f : 1500f;
            recoilJustHappenend = false;

            if (timer >= 1f) {
                timer = 0f;
                followParent = true;
            }
        }
        else if (!followParent && longPress) {
            timer += Time.fixedDeltaTime * pistonSpeed;
            float newPosition = Mathf.Lerp(0, -0.09f, timer);
            targetPosition.z = initialLocalPosition.z + newPosition;
            transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, Time.fixedDeltaTime * pistonSpeed);
        }
        else if (followParent) {
            timer = 0f;
            float followSpeed = pistonSpeed * 2f;
            transform.localPosition = Vector3.Lerp(transform.localPosition, initialLocalPosition, Time.fixedDeltaTime * followSpeed);
        }
    }
    public void OnHit(InputAction.CallbackContext context) {
        if (context.canceled) {
            followParent = false;
            longPress = false;
            Shot = true;
            if (!GameManager.FirstHit) {
                GameManager.FirstHit = true;
            }
        }
    }
    public void OnRecoil(InputAction.CallbackContext context) {
        if (context.performed) {
            followParent = false;
            longPress = true;
            recoilJustHappenend = true;
        }   
    }
    void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("TargetTag") && Shot) {
            Vector3 forward = transform.forward;
            rigidbody.AddForce(forward * forceMagnifier);
        }
    }
}
