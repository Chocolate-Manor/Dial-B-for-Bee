using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class DialogueLine
{
    public Sprite sprite;
    public bool isImage;
    
    public float textSize = 80;

    public bool switchMusic;
    public AudioClip musicToSwitchTo;

    public bool playClip;
    public AudioClip clipToPlay;
    
    public float duration;
    
    [TextArea(2, 20)]
    public string text;
}
