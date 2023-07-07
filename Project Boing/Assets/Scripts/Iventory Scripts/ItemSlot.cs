using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, ISelectHandler
{
    public ItemData itemData;

    private InventoryViewController _viewController;

    public void OnSelect(BaseEventData eventData)
    {
        _viewController.OnSlotSelected(this);
    }

    private void Awake() 
    {
        _viewController = FindObjectOfType<InventoryViewController>();

        if(itemData == null) return;

        var spawnedSprite = Instantiate<Image>(itemData.Sprite, transform.position, Quaternion.identity, transform);    
    }
}
