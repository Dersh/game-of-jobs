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
            _cells[i].onClick.AddListener(() => { onClick((byte)i); });
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
    public void HighlightCell(int index)
    {
        foreach (Button but in _cells)
        {
            but.GetComponent<Image>().color = Color.white;
        }
        _cells[index].GetComponent<Image>().color = new Color(0.75f, 0.75f, 0.75f, 1);
    }
}
