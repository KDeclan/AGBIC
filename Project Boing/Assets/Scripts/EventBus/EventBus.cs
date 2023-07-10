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

    public event Action onGameplayPaused;
    public event Action onGameplayResumed;

    public event Action<ItemData> onItemUsed;

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

    public void PauseGameplay()
    {
        onGameplayPaused?.Invoke();
    }

    public void ResumeGameplay()
    {
        onGameplayResumed?.Invoke();
    }

    public void UseItem(ItemData item)
    {
        onItemUsed?.Invoke(item);
    }

    private void Awake() 
    {
        Instance = this;
    }
}
