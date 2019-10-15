using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class InteractableItem : MonoBehaviour
{
    private static Transform _player;
    private void Awake()
    {
        if (_player == null)
            _player = GameObject.Find("Player").transform;
    }

    public enum Sort : int
    {
        Green = 0,
        Yellow = 1,
        Red = 2
    }
    [SerializeField] protected Sort Type;

    public bool TypeMatches(Sort type)
    {
        return Type == type;
    }
    [SerializeField] private Sprite[] _icons;
    public Sprite GetIcon()
    {
        return _icons[(int)Type];
    }
    protected abstract void OnInteract();
    private const float MinDistance = 5;
    private bool IsAvailableForPlayer()
    {
        if (Vector3.Distance(_player.position, transform.position) > MinDistance)
            return false;
        else
            return true;
    }
    private void OnMouseUpAsButton()
    {
        if (IsAvailableForPlayer())
            OnInteract();
    }
}
