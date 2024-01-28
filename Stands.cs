using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stands : MonoBehaviour
{
    private float resizeFactor;
    private Vector3 initialScale;
    private float resizeSpeed = 1.0f;
    void Start()
    {
        initialScale = transform.localScale;
        if (GameManager.ConnectedPlayers == 4) {
            if ((GameManager.Tank1Score == 0) || (GameManager.Tank2Score == 0) || (GameManager.Tank3Score == 0) || (GameManager.Tank4Score == 0)) {
                if (this.name.Contains("Tank1")) {
                    resizeFactor = ((GameManager.Tank1Score + 1) == 4) ? GameManager.Tank1Score : GameManager.Tank1Score + 1;
                    this.transform.position = new Vector3(-6f, 0, 0);
                }
                else if (this.name.Contains("Tank2")) {
                    resizeFactor = ((GameManager.Tank2Score + 1) == 4) ? GameManager.Tank2Score : GameManager.Tank2Score + 1;
                    this.transform.position = new Vector3(-2f, 0, 0);
                }
                else if (this.name.Contains("Tank3")) {
                    resizeFactor = ((GameManager.Tank3Score + 1) == 4) ? GameManager.Tank3Score : GameManager.Tank3Score + 1;
                    this.transform.position = new Vector3(2f, 0, 0);
                }
                else if (this.name.Contains("Tank4")) {
                    resizeFactor = ((GameManager.Tank4Score + 1) == 4) ? GameManager.Tank4Score : GameManager.Tank4Score + 1;
                    this.transform.position = new Vector3(6f, 0, 0);
                }
            }
            else {
                if (this.name.Contains("Tank1")) {
                    resizeFactor = GameManager.Tank1Score;
                    this.transform.position = new Vector3(-6f, 0, 0);
                }
                else if (this.name.Contains("Tank2")) {
                    resizeFactor = GameManager.Tank2Score;
                    this.transform.position = new Vector3(-2f, 0, 0);
                }
                else if (this.name.Contains("Tank3")) {
                    resizeFactor = GameManager.Tank3Score;
                    this.transform.position = new Vector3(2f, 0, 0);
                }
                else if (this.name.Contains("Tank4")) {
                    resizeFactor = GameManager.Tank4Score;
                    this.transform.position = new Vector3(6f, 0, 0);
                }    
            }
        }
        else if (GameManager.ConnectedPlayers == 3){
            if ((GameManager.Tank1Score == 0) || (GameManager.Tank2Score == 0) || (GameManager.Tank3Score == 0)) {
                if (this.name.Contains("Tank1")) {
                    resizeFactor = ((GameManager.Tank1Score + 1) == 4) ? GameManager.Tank1Score : GameManager.Tank1Score + 1;
                    this.transform.position = new Vector3(-5f, 0, 0);
                }
                else if (this.name.Contains("Tank2")) {
                    resizeFactor = ((GameManager.Tank2Score + 1) == 4) ? GameManager.Tank2Score : GameManager.Tank2Score + 1;
                    this.transform.position = new Vector3(0, 0, 0);
                }
                else if (this.name.Contains("Tank3")) {
                    resizeFactor = ((GameManager.Tank3Score + 1) == 4) ? GameManager.Tank3Score : GameManager.Tank3Score + 1;
                    this.transform.position = new Vector3(5f, 0, 0);
                }
                else if (this.name.Contains("Tank4")) {
                    this.gameObject.SetActive(false);
                }    
            }
            else {
                if (this.name.Contains("Tank1")) {
                    resizeFactor = GameManager.Tank1Score;
                    this.transform.position = new Vector3(-5f, 0, 0);
                }
                else if (this.name.Contains("Tank2")) {
                    resizeFactor = GameManager.Tank2Score;
                    this.transform.position = new Vector3(0, 0, 0);
                }
                else if (this.name.Contains("Tank3")) {
                    resizeFactor = GameManager.Tank3Score;
                    this.transform.position = new Vector3(5f, 0, 0);
                }
                else if (this.name.Contains("Tank4")) {
                    this.gameObject.SetActive(false);
                }
            }
        }
        else if (GameManager.ConnectedPlayers == 2) {
            if ((GameManager.Tank1Score == 0) || (GameManager.Tank2Score == 0)) {
                if (this.name.Contains("Tank1")) {
                    resizeFactor = ((GameManager.Tank1Score + 1) == 4) ? GameManager.Tank1Score : GameManager.Tank1Score + 1;
                    this.transform.position = new Vector3(-3f, 0, 0);
                }
                else if (this.name.Contains("Tank2")) {
                    resizeFactor = ((GameManager.Tank2Score + 1) == 4) ? GameManager.Tank2Score : GameManager.Tank2Score + 1;
                    this.transform.position = new Vector3(3f, 0, 0);
                } 
                else if (this.name.Contains("Tank3")) {
                    this.gameObject.SetActive(false);
                }
                else if (this.name.Contains("Tank4")) {
                    this.gameObject.SetActive(false);
                }    
            }
            else {
                if (this.name.Contains("Tank1")) {
                    resizeFactor = GameManager.Tank1Score;
                    this.transform.position = new Vector3(-3f, 0, 0);
                }
                else if (this.name.Contains("Tank2")) {
                    resizeFactor = GameManager.Tank2Score;
                    this.transform.position = new Vector3(3f, 0, 0);
                }
                else if (this.name.Contains("Tank3")) {
                    this.gameObject.SetActive(false);
                }
                else if (this.name.Contains("Tank4")) {
                    this.gameObject.SetActive(false);
                }     
            }    
        }

        ResizeOverTime(resizeFactor, resizeSpeed);
    }

    void ResizeOverTime(float targetFactor, float speed)
    {
        Vector3 targetScale = initialScale * targetFactor;

        if (this.gameObject.activeSelf) {
            StartCoroutine(ResizeCoroutine(targetScale, speed));
        }
    }

    IEnumerator ResizeCoroutine(Vector3 targetScale, float speed)
    {
        float elapsedTime = 0f;
        float duration = 1f / speed; 
        while (elapsedTime < duration)
        {
            transform.localScale = Vector3.Lerp(initialScale, targetScale, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.localScale = targetScale;
    }
}
