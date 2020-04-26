using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public Slider HealthBar;
    public float Health = 100;

    private float CurrentHealth; 
    void Start ()
    {
        CurrentHealth = Health;
    }

    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage;
        HealthBar.value = CurrentHealth; 
    }

}
