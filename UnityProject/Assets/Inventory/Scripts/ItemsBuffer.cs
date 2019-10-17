using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ItemsBuffer : MonoBehaviour
{
    private static ItemsBuffer _instance;
    public static ItemsBuffer Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<ItemsBuffer>();
            return _instance;
        }
    }

    [SerializeField]private List<GameObject> _buffer;
    private int _chosen;
    private void Awake()
    {
        _buffer = new List<GameObject>();
        _buffer.Capacity = 3;
        if (GameObject.Find("Inventory") == null)
            throw new System.Exception("Can't find \'Inventory\' object");
        InventoryUI.Instance.SetOnClickButtons((byte b) => {
            ChooseItem(b);
        });
    }

    public bool TryInsertBuffer(GameObject target)
    {
        
        if (_buffer.Count == 3)
        {
            return false;
        }
        else
        {
            InsertBuffer(target);
            return true;
        }
    }
    public GameObject GetChosenItem()
    {
        if (_chosen < _buffer.Count)
            return _buffer[_chosen];
        else
            return null;
    }
    public bool PullFromBuffer(GameObject target)
    {
        if (_buffer.Contains(target))
        {
            _buffer.Remove(target);
            target.SetActive(true);
            UpdateInventory();
            return true;
        }
        else return false;
    }
    public bool PullFromBuffer(GameObject target, Vector3 pos)
    {
        if (_buffer.Contains(target))
        {
            _buffer.Remove(target);
            target.SetActive(true);
            target.transform.position = pos;
            UpdateInventory();
            return true;
        }
        else return false;
    }

    private void InsertBuffer(GameObject target)
    {
        _buffer.Add(target);
        target.SetActive(false);
        UpdateInventory();
    }
    private void UpdateInventory()
    {
        InventoryUI.Instance.ClearInventory();
        for (int i = 0; i < _buffer.Count; i++)
        {
            InventoryUI.Instance.UpdateInventory(_buffer[i].GetComponent<PickableItem>().GetIcon(), i);
        }
    }
    private bool ChooseItem(int index)
    {
        if (_buffer.Count < index)
            return false;
        _chosen = index;
        InventoryUI.Instance.HighlightCell(index);
        return true;
    }
}