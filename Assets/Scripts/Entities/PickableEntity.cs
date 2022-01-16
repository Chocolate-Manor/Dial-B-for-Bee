using UnityEngine;

namespace Entities
{
    public abstract class PickableEntity : MonoBehaviour
    {
        [SerializeField] private Item associatedItem;

        /// <summary>
        /// Responsible for adding this PickableEntity to the inventory of
        /// another object and destroying this instance.
        /// If the other object does not have an inventory
        /// attached nothing happens.
        /// </summary>
        /// <param name="other">The object to whose inventory this entity needs to be added.</param>
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