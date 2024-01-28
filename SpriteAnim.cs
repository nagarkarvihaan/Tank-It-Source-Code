using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteAnim : MonoBehaviour
{
    public float speed = 0.01f;
    public int FrameRate = 3;
    private Image image = null;
    private Sprite[] sprites = null;
    private float TimePerFrame = 0f;
    private float ElapsedTime = 0f;
    private int CurrentFrame = 0;
    private int FrameLimit = 0;
    private bool playAnimation = false;
    void Start()
    {
        image = GetComponent<Image>();
    }

    void OnEnable() { 
        playAnimation = true;
        if (this.gameObject.name.Contains("Tank1")) {
            CurrentFrame = GameManager.Tank1Score;
        }
        else if (this.gameObject.name.Contains("Tank2")) {
            CurrentFrame = GameManager.Tank2Score;
        }
        else if (this.gameObject.name.Contains("Tank3")) {
            CurrentFrame = GameManager.Tank3Score;
        }
        else if (this.gameObject.name.Contains("Tank4")) {
            CurrentFrame = GameManager.Tank4Score;
        }
        ElapsedTime = 0;
        LoadSpriteSheet();
    }

    void LoadSpriteSheet() {
        sprites = Resources.LoadAll<Sprite>("PointCounter");
        if (sprites != null && sprites.Length > 0) {
            TimePerFrame = 1f/FrameRate;
            Play();
        }
    }
    void Play() {
        
    }
    void Update()
    {
        if (playAnimation) {
            if (this.gameObject.name.Contains("Tank1")) {
                FrameLimit = GameManager.Tank1Score;
            }
            else if (this.gameObject.name.Contains("Tank2")) {
                FrameLimit = GameManager.Tank2Score;
            }
            else if (this.gameObject.name.Contains("Tank3")) {
                FrameLimit = GameManager.Tank3Score;
            }
            else if (this.gameObject.name.Contains("Tank4")) {
                FrameLimit = GameManager.Tank4Score;
            }
            ElapsedTime += Time.deltaTime * speed;
            if (ElapsedTime >= TimePerFrame && CurrentFrame < FrameLimit) {
                ElapsedTime = 0;
                ++CurrentFrame;
                SetSprite();
                if (CurrentFrame >= FrameLimit) {
                    playAnimation = false;    
                }
            }   
        }
    }

    void SetSprite() {
        if (CurrentFrame >= 0 && CurrentFrame < sprites.Length) {
            image.sprite = sprites[CurrentFrame];
        }
    }
}
