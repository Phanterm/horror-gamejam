using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSettings : MonoBehaviour
{
    public Collider2D levelBounds;
    public AudioManager audioManager;
    public string backgroundMusic;
    public bool noMusic = false;

    private void Start()
    {
        if(noMusic)
        {
            backgroundMusic = "";
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (backgroundMusic != "" && audioManager.currentBGM != backgroundMusic)
        {
            audioManager.ChangeBGM(backgroundMusic);
        }

        else if (noMusic)
        {
            audioManager.StopAll();
        }
    }
}
