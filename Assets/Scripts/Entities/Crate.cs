using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour, IDamagable
{
    [SerializeField] private AudioClip crateBreak;
    [SerializeField] private GameObject boxBreakParticle;
    
    public void Damage()
    {
        GameManager.instance.PlaySoundEffect(crateBreak);
        Instantiate(boxBreakParticle, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}