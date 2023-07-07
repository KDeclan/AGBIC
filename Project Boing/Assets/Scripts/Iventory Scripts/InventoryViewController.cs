using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryViewController : MonoBehaviour
{
    [SerializeField] private TMP_Text _itemDescriptionText;
    [SerializeField] private TMP_Text _itemNameText;

   public void OnSlotSelected(ItemSlot selectedSlot)
   {
        if(selectedSlot.itemData == null)
        {
            _itemNameText.ClearMesh();
            _itemDescriptionText.ClearMesh();
            return;
        }

        _itemNameText.SetText(selectedSlot.itemData.Name);
        _itemDescriptionText.SetText(selectedSlot.itemData.Description[0]);
   }
}
