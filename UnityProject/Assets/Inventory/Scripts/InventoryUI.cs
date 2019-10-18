using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class InventoryUI : MonoBehaviour
{
    private static InventoryUI _instance;
    public static InventoryUI Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<InventoryUI>();
            return _instance;
        }
    }
    [SerializeField] private List<Button> _cells;
    [SerializeField] private List<Image> _icons;
    public void SetOnClickButtons(Action<byte> onClick)
    {
        for (int i = 0; i < _cells.Count; i++)
        {
            byte y = (byte)i;
            _cells[i].onClick.AddListener(() => { onClick(y); });
            if (i != 0)
                _cells[i - 1].onClick.Invoke();
        }
    }
    [SerializeField] private Sprite[] _cellSprites;
    public void HighlightCell(int index)
    {
        for (int i = 0; i < _cells.Count; i++)
        {
            Debug.Log(i == index);
            if (i == index)
                _cells[index].image.sprite = _cellSprites[1];
            else
                _cells[index].image.sprite = _cellSprites[0];
        }
    }
    public void ClearInventory()
    {
        foreach (Image image in _icons.ToArray())
        {
            image.sprite = null;
            image.enabled = false;
        }
    }
    public void UpdateInventory(Sprite icon, int index)
    {
        _icons[index].sprite = icon;
        _icons[index].enabled = true;
    }
}
