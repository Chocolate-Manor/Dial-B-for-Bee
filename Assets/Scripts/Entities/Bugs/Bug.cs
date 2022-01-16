using Entities;
using UnityEngine;

public abstract class Bug : PickableEntity
{
    public string bugName;
    public bool isPickable = true;
    public AudioClip pickupSound;
}