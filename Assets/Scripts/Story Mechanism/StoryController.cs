using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoryController : MonoBehaviour
{
    [SerializeField] private Conversation conversation;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Image image;

    [SerializeField] private AudioClip projectorClick;
    
    // Start is called before the first frame update
    void Start()
    {
        //play projector click
        GameManager.Instance.PlaySoundEffect(projectorClick);

        //play background music
       GameManager.Instance.PlayBackgroundMusic(conversation.backgroundMusic);
       
       //play all dialogues in sequence
       StartCoroutine(PlayAllDialogues());
    }

    private IEnumerator PlayAllDialogues()
    {
        for (int i = 0; i < conversation.dialogueLines.Count; i++)
        {
            DialogueLine curLine = conversation.dialogueLines[i];
            if (curLine.isImage)
            {
                image.enabled = true;
                text.enabled = false;
                
                image.sprite = curLine.sprite;
            }
            else
            {
                text.enabled = true;
                image.enabled = false;

                text.fontSize = curLine.textSize;
                text.text = curLine.text;
            }

            if (curLine.switchMusic)
            {
                GameManager.Instance.PlayBackgroundMusic(curLine.musicToSwitchTo);
            }

            if (curLine.playClip)
            {
                GameManager.Instance.PlaySoundEffect(curLine.clipToPlay);
            }

            yield return new WaitForSeconds(curLine.duration);
        }
        //automatically disable
        Exit();
    }
    
    // Update is called once per frame
    void Update()
    {
    }
    
    /// <summary>
    /// Skip button functionality
    /// </summary>
    public void Exit()
    {
        GameManager.Instance.PlaySoundEffect(projectorClick);
        gameObject.SetActive(false);
    }
}
