using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class PlayerHUD : MonoBehaviour
{
    public Animation maxHealth;
    public Animation hiHealth;
    public Animation midHealth;
    public Animation lowHealth;
    public PlayerMain player;
    private Animation anim;

    void Awake()
    {
        anim = gameObject.GetComponent<Animation>();
        
    }

    void Update()
    {
        //float health = HoweverYouRetrieveYourHealthValue();
        
        
        if (player.health > 80f)
        {
            anim.Play("healthmax");
        }
        else if (player.health > 50f)
        {
          anim.Play("healthhi");
        }
        else if (player.health > 30f)
        {
            anim.Play("healthmed");
        }
        else if (player.health > 10f)
        {
            anim.Play("healthlow");
        }
        else if (player.health > 5f)
        {
            anim.Play("healthlow");
        }
        else
        {
            //sr.sprite = critHealth;
        }
    }
}
