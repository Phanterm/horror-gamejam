using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHUD : MonoBehaviour
{
    public Sprite healthyImage;
    public Sprite sadImage;
    public Sprite deathImage;

    private SpriteRenderer sr;
    public int health = 0;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        //float health = HoweverYouRetrieveYourHealthValue();
        if (health > 50f)
        {
            sr.sprite = healthyImage;
        }
        else if (health > 10f)
        {
            sr.sprite = sadImage;
        }
        else
        {
            sr.sprite = deathImage;
        }
    }
}
