using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaughingBall : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(StartDeletion());
    }
    IEnumerator StartDeletion() {
        yield return new WaitForSeconds(10.0f);
        Destroy(this.gameObject);
    }
}
