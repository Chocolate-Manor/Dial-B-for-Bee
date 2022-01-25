using Entities;
using UnityEngine;

public abstract class Bug : PickableEntity
{
    public string bugName;

    protected override void PickMeUp(GameObject other)
    {
        GameManager.Instance.CallEventOnInventoryUpdated();
        base.PickMeUp(other);
    }
}