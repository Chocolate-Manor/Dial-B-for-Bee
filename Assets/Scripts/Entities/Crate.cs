using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour, IDamagable
{
    [SerializeField] private AudioClip crateBreak;

    public void Damage()
    {
        GameManager.instance.PlaySoundEffect(crateBreak);
        Destroy(gameObject);
    }
}