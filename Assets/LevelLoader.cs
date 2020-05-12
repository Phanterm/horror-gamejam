using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{

    public Animator transition;
    public float transitionTime = 1f;

    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1)); //References the build order index in Build Settings.
        //SceneManager.LoadScene("Hospital"); If you were loading a specific scene.
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");
            //Play our animation.
        yield return new WaitForSeconds(transitionTime);
            //Wait for animation to stop playing.
        SceneManager.LoadScene(levelIndex);
            //Load the next scene.
    }
}
