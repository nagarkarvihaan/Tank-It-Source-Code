using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    private bool RedCheck = false;
    private bool BlueCheck = false;
    private bool GreenCheck = false;
    private bool YellowCheck = false;
    public GameObject Text;
    public GameObject InstructionsText;
    public TextMeshProUGUI Instructions, RestartText;
    public TextMeshProUGUI Connection;
    public static bool GameisOn = false;
    public static bool ShowInstructions = false;
    public static bool FirstHit = false;
    public GameObject Panel;
    public GameObject GameOverPanel;
    public GameObject pin;
    private GameObject[] pinInstances;
    public GameObject Tank1, Tank2, Tank3, Tank4;
    public GameObject Ball;
    public GameObject LaughingBall;
    public GameObject Tank1Panel, Tank2Panel, Tank3Panel, Tank4Panel;
    public GameObject InstructionsPanel;
    public static int Tank1Score = 0, Tank2Score = 0, Tank3Score = 0, Tank4Score = 0;
    private int layout;
    public static int ConnectedPlayers = 0;
    public static int AlivePlayers = 0;
    public static int Rounds = 0;

    void Awake() {
        GameOverPanel.SetActive(false);
    }
    void Start()
    {
        Panel.SetActive(true); 
        InstructionsPanel.SetActive(false);   
        Text.SetActive(false);
        InstructionsText.SetActive(true);
        Instructions.text = "Double tap       to add upto 4 controllers and tap      to start!";
        NextRound();
        SpawnPrefabRandomly();
    }

    void Update()
    {
        if (ShowInstructions) {
            InstructionsPanel.SetActive(true);
            Panel.SetActive(false);
        }
        if(GameisOn) {
            Panel.SetActive(false);
            InstructionsPanel.SetActive(false);
        }

        if(!Text.activeSelf) {
            InstructionsText.SetActive(true);
        }
        else {
            InstructionsText.SetActive(false);
        }

        if(AlivePlayers == 1 && GameisOn) {
            GameOverPanel.SetActive(true);
            GameisOn = false;
            if (Tank1.activeSelf) {
                Tank1Score += 1;
            }
            else if (Tank2.activeSelf) {
                Tank2Score += 1;
            }
            else if (Tank3.activeSelf) {
                Tank3Score += 1;
            }
            else if (Tank4.activeSelf) {
                Tank4Score += 1;
            }

            if (Rounds < 5 && ((Tank1Score == 3) || (Tank2Score == 3) || (Tank3Score == 3) || (Tank4Score == 3))) {
                RestartText.text = ("Press          to continue");
            }
            else if (Rounds < 5) {
                RestartText.text = ("Press          for round " + (Rounds+1));    
            }
            else if (Rounds == 6) {
                RestartText.text = ("Press          to continue");    
            }
        }
        else if (AlivePlayers > 1) {
            GameOverPanel.SetActive(false);    
        }

        if(GameOverPanel.activeSelf) {
            if (ConnectedPlayers == 2) {
                Tank3Panel.SetActive(false);
                Tank4Panel.SetActive(false);
            }
            else if (ConnectedPlayers == 3) {
                Tank4Panel.SetActive(false);
            }
        }
    }

    public void ConnectionStatus(string message) {
        if (message.Contains("Red") && !RedCheck) {
            Text.SetActive(true);
            Connection.text = message;
            RedCheck = true;
            ConnectedPlayers += 1;
            AlivePlayers += 1;
            StartCoroutine(Delay());
        } else if (message.Contains("Blue") && !BlueCheck) {
            Text.SetActive(true);
            Connection.text = message;
            BlueCheck = true;
            ConnectedPlayers += 1;
            AlivePlayers += 1;
            StartCoroutine(Delay());
        } else if (message.Contains("Green") && !GreenCheck) {
            Text.SetActive(true);
            Connection.text = message;
            GreenCheck = true;
            ConnectedPlayers += 1;
            AlivePlayers += 1;
            StartCoroutine(Delay());
        } else if (message.Contains("Yellow") && !YellowCheck) {
            Text.SetActive(true);
            Connection.text = message;
            YellowCheck = true;
            ConnectedPlayers += 1;
            AlivePlayers += 1;
            StartCoroutine(Delay());
        }
    }

    public void NextRound() {
        pinInstances = GameObject.FindGameObjectsWithTag("Pin");
        foreach(GameObject instance in pinInstances) {
            Destroy(instance);
        }
        layout = Random.Range(1, 6);
        switch (layout) {
            case 1:
                Instantiate(pin, new Vector3(3.75f, 0f, 3.75f), Quaternion.identity);
                Instantiate(pin, new Vector3(-3.75f, 0f, 3.75f), Quaternion.identity);
                Instantiate(pin, new Vector3(-3.75f, 0f, -3.75f), Quaternion.identity);
                Instantiate(pin, new Vector3(3.75f, 0f, -3.75f), Quaternion.identity);
                break;
            case 2:
                Instantiate(pin, new Vector3(0f, 0f, 3.75f), Quaternion.identity);
                Instantiate(pin, new Vector3(-3.75f, 0f, 0f), Quaternion.identity);
                Instantiate(pin, new Vector3(3.75f, 0f, 0f), Quaternion.identity);
                Instantiate(pin, new Vector3(0f, 0f, -3.75f), Quaternion.identity);
                break;
            case 3:
                Instantiate(pin, new Vector3(3.75f, 0f, 3.75f), Quaternion.identity);
                Instantiate(pin, new Vector3(-3.75f, 0f, -3.75f), Quaternion.identity);
                break;
            case 4:
                Instantiate(pin, new Vector3(3.75f, 0f, 0f), Quaternion.identity);
                Instantiate(pin, new Vector3(-3.75f, 0f, 0f), Quaternion.identity);
                break; 
            case 5:
                Instantiate(pin, new Vector3(0f, 0f, 3.75f), Quaternion.identity);
                Instantiate(pin, new Vector3(-3.75f, 0f, -3.75f), Quaternion.identity);
                Instantiate(pin, new Vector3(3.75f, 0f, -3.75f), Quaternion.identity);
                break;      
            default:
                break;
        }

        if(GameisOn) {
            Tank1.transform.position = new Vector3(-6f, 0f, -6f);
            Tank1.transform.rotation = Quaternion.Euler(0f, 45f, 0f);
            Tank2.transform.position = new Vector3(6f, 0f, 6f);
            Tank2.transform.rotation = Quaternion.Euler(0f, 225f, 0f);
            Tank3.transform.position = new Vector3(-6f, 0f, 6f);
            Tank3.transform.rotation = Quaternion.Euler(0f, 135f, 0f);
            Tank4.transform.position = new Vector3(6f, 0f, -6f);
            Tank4.transform.rotation = Quaternion.Euler(0f, 315f, 0f);
            Ball.transform.position = new Vector3(0f, 2f, 0f);
            if (ConnectedPlayers == 2) {
                if(!Tank2.activeSelf) {
                    Tank2.SetActive(true);
                }
                else {
                    Tank1.SetActive(true);
                }
                Tank3.SetActive(false);
                Tank4.SetActive(false);
            }
            else if (ConnectedPlayers == 3) {
                if(!Tank1.activeSelf) {
                    Tank1.SetActive(true);
                }
                if(!Tank2.activeSelf) {
                    Tank2.SetActive(true);
                }
                if(!Tank3.activeSelf){
                    Tank3.SetActive(true);
                }
                Tank4.SetActive(false);  
            }
            else if (ConnectedPlayers == 4) {
                if(!Tank1.activeSelf) {
                    Tank1.SetActive(true);
                }
                if(!Tank2.activeSelf) {
                    Tank2.SetActive(true);
                }
                if(!Tank3.activeSelf){
                    Tank3.SetActive(true);
                }
                if(!Tank4.activeSelf) {
                    Tank4.SetActive(true);
                }    
            }
        }  
    }

    public void SpawnPrefabRandomly() {
        float randomDelay = Random.Range(5, 11);
        Invoke("SpawnPrefab", randomDelay);
    }
    void SpawnPrefab()
    {
        Instantiate(LaughingBall, new Vector3(Random.Range(-6f, 6.01f), 2f, Random.Range(-6f, 6.01f)), Quaternion.Euler(0, -90f, 0));
        SpawnPrefabRandomly();
    }

    IEnumerator Delay(){
        yield return new WaitForSeconds(2f);
        Text.SetActive(false);
    }
}
