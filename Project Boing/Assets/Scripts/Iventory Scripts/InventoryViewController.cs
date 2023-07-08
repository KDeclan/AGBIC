using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryViewController : MonoBehaviour
{
    [SerializeField] private TMP_Text _itemDescriptionText;
    [SerializeField] private TMP_Text _itemNameText;

    [SerializeField] private GameObject _inventoryViewObject;

    [SerializeField] private List<ItemSlot> _slots;

    private void OnEnable() 
    {
        EventBus.Instance.onPickupItem += OnItemPickedUp;
    }

    private void OnDisable() 
    {
        EventBus.Instance.onPickupItem -= OnItemPickedUp;   
    }

    private void OnItemPickedUp(ItemData itemData)
    {
        //check inventory if the slot is empty put item in slot and break out of loop
        foreach(var slot in _slots)
        {
            if(slot.itemData == null)
            {
                slot.itemData = itemData;
                break;
            }
        }
    }

   public void OnSlotSelected(ItemSlot selectedSlot)
   {
        //if the inventory slot is empty then dont display any info if not empty display the name and description in the info box

        if(selectedSlot.itemData == null)
        {
            _itemNameText.ClearMesh();
            _itemDescriptionText.ClearMesh();
            return;
        }

        _itemNameText.SetText(selectedSlot.itemData.Name);
        _itemDescriptionText.SetText(selectedSlot.itemData.Description[0]);
   }

   private void Update() 
   {
        //Opens and closes inventory using tab

        if(Input.GetKeyDown(KeyCode.Tab))
        {
            if(_inventoryViewObject.activeSelf)
            {
                EventBus.Instance.OpenInventory();
            }
            else
            {
                EventBus.Instance.CloseInventory();
            }

            _inventoryViewObject.SetActive(!_inventoryViewObject.activeSelf);
        }
   }
}
