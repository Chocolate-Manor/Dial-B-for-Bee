using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This class is purely a timed explosion, same as original ladybug
/// </summary>
public class TimedExplosion : MonoBehaviour
{
    [SerializeField] private float lifeSpan = 5;
    
    [SerializeField] private GameObject exploder;
    
    [SerializeField] private new GameObject light;
    void Start()
    {
        StartCoroutine(LightTicking());
        
        //have it explode after a certain time
        Instantiate(exploder).GetComponent<Explosion>().Explode(lifeSpan, gameObject);
        Destroy(gameObject, lifeSpan + 0.1f);
    }


    /// <summary>
    /// Visuals of light ticking
    /// </summary>
    /// <returns></returns>
    IEnumerator LightTicking()
    {
        while (true)
        {
            light.SetActive(false);
            yield return new WaitForSeconds(0.4f);
            light.SetActive(true);
            yield return new WaitForSeconds(0.4f);
        }
    }

}
