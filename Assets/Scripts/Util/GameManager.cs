using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SingletonMonoBehavior<GameManager>
{

    public static event Action OnInventoryUpdated;
    
    //base.awake is called to make it singleton.
    protected override void Awake()
    {   
        base.Awake();
        lightsYouAreIlluminatedBy = new List<Collider2D>();
        DontDestroyOnLoad(gameObject);

    }

    public void CallEventOnInventoryUpdated()
    {
        OnInventoryUpdated();
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
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
    
    /// <summary>
    /// Changes the current background music.
    /// </summary>
    /// <param name="clip"></param>
    public void PlayBackgroundMusic(AudioClip clip)
    {
        if (clip != mainAudioSource.clip)
        {
            mainAudioSource.clip = clip;
            mainAudioSource.Play();
        }
    }


}