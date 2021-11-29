using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager instance { get; private set; }
    
    private void Awake()
    {
        CheckInstance(); //avoid duplicates
        lightsYouAreIlluminatedBy = new List<Collider2D>();
        DontDestroyOnLoad(gameObject);

    }

    void CheckInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

    }

    //-----------------------------------------//

    public List<Collider2D> lightsYouAreIlluminatedBy;
    public AudioSource mainAudioSource;
    
    public void GameOver()
    {
        Destroy(GameObject.FindWithTag("Player"));
    }

 

    public bool IsIlluminateed()
    {
        if (lightsYouAreIlluminatedBy.Count > 0)
        {
            return true;
        }

        return false;
    }

    public void ReloadAfterDelay()
    {
        StartCoroutine(ReloadAfterDelayCoroutine());
    }
    
    IEnumerator ReloadAfterDelayCoroutine()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void PlaySoundEffect(AudioClip clip)
    {
        mainAudioSource.PlayOneShot(clip);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        } 
    }


}