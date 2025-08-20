using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpyScript : MonoBehaviour
{
    [SerializeField] List<Sprite> helpySprites = new List<Sprite>();
    [SerializeField] Image currentImage;
    [SerializeField] int imageLimit = 2;
    [SerializeField] float intervall = 0.5f;

    private int currentSprite = 0;
    private float timer = 0.0f;

    void Start()
    {
        currentImage.sprite = helpySprites[0];
    }

    void Update()
    {
        timer += Time.deltaTime;

        if(timer >= intervall)
        {
            currentImage.sprite = helpySprites[currentSprite];
            currentSprite++;
            if(currentSprite >= imageLimit)
            {
                currentSprite = 0;
            }
            timer = 0.0f;
        }
    }
}
