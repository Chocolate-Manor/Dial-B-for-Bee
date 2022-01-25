using UnityEngine;

namespace Entities
{
    public abstract class PickableEntity : MonoBehaviour
    {
        [SerializeField] private Item associatedItem;
        public bool isPickable = true;
        public AudioClip pickupSound;
        
        /// <summary>
        /// Responsible for adding this PickableEntity to the inventory of
        /// another object and destroying this instance.
        /// If the other object does not have an inventory
        /// attached nothing happens.
        /// </summary>
        /// <param name="other">The object to whose inventory this entity needs to be added.</param>
        protected virtual void PickMeUp(GameObject other)
        {
            Inventory.Inventory inventory = other.GetComponent<Inventory.Inventory>();
            if (inventory != null)
            {
                GameManager.Instance.PlaySoundEffect(pickupSound);
                inventory.AddItem(associatedItem);
                Destroy(gameObject);
            }
        }
    }
}