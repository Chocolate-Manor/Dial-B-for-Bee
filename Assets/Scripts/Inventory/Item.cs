using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private int _id;
    [SerializeField] private string _name;
    [SerializeField] private string _description;
    [SerializeField] private Sprite _icon;

    public Item(int id, string name, string description, Sprite icon)
    {
        this._id = id;
        this._name = name;
        this._description = description;
        this._icon = icon;
    }

    public int GetId()
    {
        return this._id;
    }

    public string GetName()
    {
        return this._name;
    }

    public string GetDescription()
    {
        return this._description;
    }

    public Sprite GetIcon()
    {
        return this._icon;
    }
}