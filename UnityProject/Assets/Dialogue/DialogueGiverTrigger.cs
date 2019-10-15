using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class DialogueGiverTrigger : MonoBehaviour
{
    public System.Action<GameObject> onTriggerEnter;
    public System.Action<GameObject> onTriggerExit;
    private bool _state;
    private Transform _player;
    private void Awake()
    {
        _player = GameObject.Find("Player").transform;
    }
    private void Update()
    {
        if ((_player.position - transform.position).magnitude > 2.5f)
        {
            if (_state == true)
            {
                onTriggerExit.Invoke(_player.gameObject);
                _state = false;
            }
        }
        else
        {
            if (_state == false)
            {
                onTriggerEnter.Invoke(_player.gameObject);
                _state = true;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        onTriggerEnter.Invoke(other.gameObject);
        Debug.Log("Enter");
    }
    private void OnTriggerExit(Collider other)
    {
        onTriggerExit.Invoke(other.gameObject);
        Debug.Log("Exit");
    }
}
