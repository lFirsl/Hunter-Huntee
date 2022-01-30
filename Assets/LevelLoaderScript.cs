using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelLoaderScript : MonoBehaviour
{
    //IF statement for conditioning level change

    public float transitionTime = 1f;
    public Animator transition;
    public bool trigger = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (trigger)
        {
            LoadNextLevel();
        }
    }

    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));    
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        //Play anim
        transition.SetTrigger("Start");
        //wait
        yield return new WaitForSeconds(1);
        //Load Scene
        SceneManager.LoadScene(levelIndex);
    }
}
