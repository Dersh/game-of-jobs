using System.Collections;
using System.Linq;
using System.Linq.Expressions;
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

    [SerializeField] private int _count;
    private GameObject[] _buffer;
    private int _chosen;
    private void Awake()
    {
        _buffer = new GameObject[_count];
        if (GameObject.Find("Inventory") == null)
            throw new System.Exception("Can't find \'Inventory\' object");
        InventoryUI.Instance.SetOnClickButtons((byte b) => {
            ChooseItem(b);
        });
    }

    public bool TryInsertBuffer(GameObject target)
    {
        int index = GetFirstIndex();
        if (index == -1)
            return false;
        _buffer[index] = target;
        target.SetActive(false);
        UpdateInventory();
        return true;
    }
    public GameObject GetChosenItem()
    {
        if (_chosen < _buffer.Length)
            return _buffer[_chosen];
        else
            return null;
    }
    [System.Obsolete]
    public bool PullFromBuffer(GameObject target)
    {
        if (_buffer.Contains(target))
        {
            for (int i = 0; i < _buffer.Length; i++)
                if (target == _buffer[i])
                    _buffer[i] = null;
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
            for (int i = 0; i < _buffer.Length; i++)
                if (target == _buffer[i])
                    _buffer[i] = null;
            target.SetActive(true);
            target.transform.position = pos;
            UpdateInventory();
            return true;
        }
        else return false;
    }
    private int GetFirstIndex()
    {
        for (int i = 0; i < _buffer.Length; i++)
            if (_buffer[i] == null)
                return i;
        return -1;
    }
    private void UpdateInventory()
    {
        InventoryUI.Instance.ClearInventory();
        for (int i = 0; i < _buffer.Length; i++)
            if (_buffer[i] != null)
                InventoryUI.Instance.UpdateInventory(_buffer[i].GetComponent<PickableItem>().GetIcon(), i);
    }
    private bool ChooseItem(int index)
    {
        Debug.Log(index);
        if (_buffer[index] == null)
            return false;
        _chosen = index;
        InventoryUI.Instance.HighlightCell(index);
        return true;
    }
}