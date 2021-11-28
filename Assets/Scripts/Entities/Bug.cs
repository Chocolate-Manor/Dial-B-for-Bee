using UnityEngine;

public class Bug : MonoBehaviour
{

    public string name;
    
    public void PickMeUp(B player)
    {
        int index = player.bugNames.FindIndex(x => x.Equals(name));
        player.bugCounts[index]++;
    }
}