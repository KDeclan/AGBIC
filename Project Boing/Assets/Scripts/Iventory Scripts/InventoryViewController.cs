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

    [SerializeField] private ScreenFader _fader;

    private enum State
    {
        menuClosed,

        menuOpen,
    };

    private State _state;

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
            if(_state == State.menuClosed)
            {
                EventBus.Instance.PauseGameplay();
                _fader.FadeToBlack(0.3f, FadeToMenuCallback); 
                _state = State.menuOpen;
            }
            else if (_state == State.menuOpen)
            {
                EventBus.Instance.ResumeGameplay();
                _fader.FadeToBlack(0.3f, FadeFromMenuCallback);
                _state = State.menuClosed;
            }
        }
   }

   private void FadeToMenuCallback()
   {
        _inventoryViewObject.SetActive(true);
        _fader.FadeFromBlack(0.3f, null);
   }

   private void FadeFromMenuCallback()
   {
        _inventoryViewObject.SetActive(false);
        _fader.FadeFromBlack(0.3f, null);
   }
}
