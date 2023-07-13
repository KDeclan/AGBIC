using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    [SerializeField] private ItemData _itemData;

    private void OnTriggerStay(Collider other)
    {
        //make sure the player is the one picking up item
        if(!other.CompareTag("Player")) return;

        //when F is pressed pick up the item and destroy it
        if(Input.GetKey(KeyCode.F))
        {
            EventBus.Instance.PickUpItem(_itemData);
            Destroy(gameObject);
        }
    }
}
