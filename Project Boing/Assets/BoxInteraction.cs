using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxInteraction : MonoBehaviour
{
    [SerializeField] private ItemData _requiredItem;
    private Renderer _renderer;

    private void Awake() 
    {
        _renderer =  GetComponent<Renderer>();   
    }

   private void OnEnable() 
   {
        EventBus.Instance.onItemUsed += onItemUsed;
   }
   
   private void OnDisable() 
   {
        EventBus.Instance.onItemUsed -= onItemUsed;
   }

   private void onItemUsed(ItemData item)
   {
        if (item == _requiredItem)
        {
            _renderer.material.color = new Color(1,0,0,1);
        }
   }
}
