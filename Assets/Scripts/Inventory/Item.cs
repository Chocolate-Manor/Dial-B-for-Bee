using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private int id;
    [SerializeField] private string itemName;
    [SerializeField] private string description;
    [SerializeField] private Sprite icon;

    public Item(int id, string itemName, string description, Sprite icon)
    {
        this.id = id;
        this.itemName = itemName;
        this.description = description;
        this.icon = icon;
    }

    public int GetId()
    {
        return this.id;
    }

    public string GetName()
    {
        return this.itemName;
    }

    public string GetDescription()
    {
        return this.description;
    }

    public Sprite GetIcon()
    {
        return this.icon;
    }
}