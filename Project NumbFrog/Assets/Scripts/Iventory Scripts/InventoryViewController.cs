using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class InventoryViewController : MonoBehaviour
{
    [SerializeField] private TMP_Text _itemDescriptionText;
    [SerializeField] private TMP_Text _itemNameText;

    [SerializeField] private GameObject _inventoryViewObject;
    [SerializeField] private GameObject _contextMenuObject;
    [SerializeField] private GameObject _firstContextMenuOption;

    [SerializeField] private List<ItemSlot> _slots;
    [SerializeField] public ItemSlot _currentSlot;

    [SerializeField] private ScreenFader _fader;

    [SerializeField] private List<Button> _contextMenuIgnore;

    public DialoguePrinter dialogueControl;

    private enum State
    {
        menuClosed,

        menuOpen,

        contextMenu,
    };

    private State _state;

    public void UseItem()
    {
        if (_currentSlot.itemData.Name.Contains("Key"))
        {
            _fader.FadeToBlack(1f, FadeToUseItemCallback);

            foreach (var slot in _slots)
            {
                if (slot == _currentSlot)
                {
                    slot.itemData = null;
                }
            }
        }
        else
        {
            return;
        }     
    }

    public void FadeToUseItemCallback()
    {
        _contextMenuObject.SetActive(false);
        _inventoryViewObject.SetActive(false);
        _fader.FadeFromBlack(1f, () => EventBus.Instance.UseItem(_currentSlot.itemData));
        _state = State.menuClosed;
    }

    public void OnSlotSelected(ItemSlot selectedSlot)
   {
        //if the inventory slot is empty then dont display any info if not empty display the name and description in the info box

        _currentSlot = selectedSlot;
        if(selectedSlot.itemData == null)
        {
            _itemNameText.ClearMesh();
            _itemDescriptionText.ClearMesh();
            return;
        }

        _itemNameText.SetText(selectedSlot.itemData.Name);
        _itemDescriptionText.SetText(selectedSlot.itemData.Description[0]);
   }

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

   private void Update() 
   {
        //Opens and closes inventory using tab
        if (dialogueControl.isWriting) return;

        if(Input.GetKeyDown(KeyCode.Tab))
        {
            if(_state == State.menuClosed)
            {
                foreach (var button in _contextMenuIgnore)
                {
                    button.interactable = true;
                }

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
            else if (_state == State.contextMenu)
            {
                _contextMenuObject.SetActive(false);
                    
                foreach (var button in _contextMenuIgnore)
                {
                    button.interactable = true;
                }
                EventSystem.current.SetSelectedGameObject(_currentSlot.gameObject);
                _state = State.menuOpen;
            }
        }

        //Open Context Menu
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (_state == State.menuOpen)
            {
                if (EventSystem.current.currentSelectedGameObject.TryGetComponent<ItemSlot>(out var slot))
                {
                    _state = State.contextMenu;
                    _contextMenuObject.SetActive(true);
                    EventSystem.current.SetSelectedGameObject(_firstContextMenuOption);

                    foreach (var button in _contextMenuIgnore)
                    {
                        button.interactable = false;
                    }
                }
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
