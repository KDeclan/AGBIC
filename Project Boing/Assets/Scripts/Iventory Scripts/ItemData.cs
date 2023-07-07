using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Inventory/ItemData")]

public class ItemData : ScriptableObject 
{

    public string Name => _name;
    public List<string> Description => _description;
    public Image Sprite => _sprite;

    [SerializeField] private string _name;

    [SerializeField] private List<string> _description;

    [SerializeField] private Image _sprite;
}

