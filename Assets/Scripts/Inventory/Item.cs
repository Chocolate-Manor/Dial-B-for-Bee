using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory Item", menuName = "Inventory Item")]
public class Item : ScriptableObject
{
    public int id;
    public new string name;
    public string description;
    public Sprite icon;
    public GameObject associatedPrefab;
}