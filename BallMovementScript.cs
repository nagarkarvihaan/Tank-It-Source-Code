using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovementScript : MonoBehaviour
{
    private BoxCollider myCollider;
    private PhysicMaterial originalmaterial;
    private float minYLimit = 0f;
    private float originaldfriction;
    private float originalsfriction;
    private float originalbounciness;

    void Start() {
        Vector3 StartingPosition = new Vector3(0,2,0);
        transform.position = StartingPosition;
        myCollider = GetComponent<BoxCollider>();
        originalmaterial = myCollider.material;
        originalbounciness = originalmaterial.bounciness;
        originaldfriction = originalmaterial.dynamicFriction;
        originalsfriction = originalmaterial.staticFriction;
        originalmaterial.bounciness = 0f;
        originalmaterial.dynamicFriction = 0f;
        originalmaterial.staticFriction = 0f;
    }
    void Update() {
        Vector3 currentPosition = transform.position;
        currentPosition.y = Mathf.Clamp(currentPosition.y, minYLimit, 2f);
        transform.position = currentPosition;

        if (GameManager.FirstHit) {
            originalmaterial.bounciness = originalbounciness;
            originalmaterial.dynamicFriction = originaldfriction;
            originalmaterial.staticFriction = originalsfriction;
        }
    }
}
