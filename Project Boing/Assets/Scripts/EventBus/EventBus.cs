using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventBus : MonoBehaviour
{
    public static EventBus Instance {get; private set;}

    public event Action onOpenInventory;
    public event Action onCloseInventory;

    public event Action<ItemData> onPickupItem;

    public void OpenInventory()
    {
        onOpenInventory?.Invoke();
    }

    public void CloseInventory()
    {
        onCloseInventory?.Invoke();
    }

    public void PickUpItem(ItemData itemData)
    {
        onPickupItem?.Invoke(itemData);
    }

    private void Awake() 
    {
        Instance = this;
    }
}
