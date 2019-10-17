using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PickableItem : InteractableItem
{
    [SerializeField] private Sprite _icon;
    public Sprite GetIcon()
    {
        return _icon;
    }
}
