using UnityEngine;

namespace Entities
{
    public abstract class PickableEntity : MonoBehaviour
    {
        [SerializeField] private Item associatedItem;


        protected void PickMeUp(GameObject other)
        {
            Inventory.Inventory inventory = other.GetComponent<Inventory.Inventory>();
            if (inventory != null)
            {
                inventory.AddItem(associatedItem);
                Destroy(gameObject);
            }
        }
    }
}