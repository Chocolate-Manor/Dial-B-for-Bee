using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Conversation", menuName = "Scritable Objects/Conversation")]
public class Conversation : ScriptableObject
{
    public AudioClip backgroundMusic;

    public List<DialogueLine> dialogueLines;
}
