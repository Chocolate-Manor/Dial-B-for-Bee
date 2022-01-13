using UnityEngine;

public class Bug : MonoBehaviour
{
    public string bugName;
    public bool isPickable = true;
    public AudioClip pickupSound;
    
    public void PickMeUp(B player)
    {
        if (isPickable)
        {
            int index = player.bugNames.FindIndex(x => x.Equals(bugName));
            player.bugCounts[index]++;
            GameManager.Instance.PlaySoundEffect(pickupSound);
        }
    }
}