using UnityEngine;

public class Bug : MonoBehaviour
{
    public string name;
    public bool isPickable = true;

    public void PickMeUp(B player)
    {
        if (isPickable)
        {
            int index = player.bugNames.FindIndex(x => x.Equals(name));
            player.bugCounts[index]++;
        }
    }
}