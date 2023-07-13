using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, ISelectHandler
{
    public ItemData itemData;

    private InventoryViewController _viewController;

    private Image _spawnedItemSprite;

    public void OnSelect(BaseEventData eventData)
    {
        _viewController.OnSlotSelected(this);
    }

    private void OnEnable() 
    {
        _viewController = FindObjectOfType<InventoryViewController>();

        if(itemData == null) return;

        _spawnedItemSprite = Instantiate<Image>(itemData.Sprite, transform.position, Quaternion.identity, transform);    
    }

    private void OnDisable() 
    {
        if(_spawnedItemSprite != null)
        {
            Destroy(_spawnedItemSprite);
        }
    }
}
