using UnityEngine;

namespace Entities
{
    public class PickableEntity : MonoBehaviour
    {
        [SerializeField] private Item associatedItem;


        public void PickMeUp(GameObject other)
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