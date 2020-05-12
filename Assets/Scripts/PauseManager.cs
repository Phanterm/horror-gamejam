using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    private bool isPaused;
    public GameObject menuPanel;
    public string mainMenu;
    // Start is called before the first frame update
    void Start()
    {
        isPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButton("select"))
        {
            isPaused = !isPaused;
            if(isPaused)
            {
                menuPanel.SetActive(true);
        
            }
            else
            {
                menuPanel.SetActive(false);
            }
        }
        
    }
    void Resume()
    {
        isPaused = !isPaused;
    }

    void QuitToMain()
    {
        SceneManager.LoadScene(mainMenu);
    }

}
