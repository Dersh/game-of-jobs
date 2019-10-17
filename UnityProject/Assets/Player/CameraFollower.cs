using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    private Transform _cameraTransform;
    private Transform _anchor;
    [SerializeField] private Transform _player;

    private void Awake()
    {
        _cameraTransform = transform;
        _anchor = _cameraTransform.parent;
    }
    private enum Direction
    {
        Left,
        Right,
        Stay
    }
    private Direction _direction;
    [SerializeField] private KeyCode _moveLeft;
    [SerializeField] private KeyCode _moveRight;
    [SerializeField] private float _rotatingSpeed;

    private void Update()
    {
        if (Input.GetKey(_moveLeft))
            _direction = Direction.Left;
        else if (Input.GetKey(_moveRight))
            _direction = Direction.Right;
        else
            _direction = Direction.Stay;
    }
    [SerializeField] private float _height;
    [SerializeField] private float _smoothness;
    private void FixedUpdate()
    {
        if (_direction == Direction.Stay)
            return;
        _anchor.Rotate(Vector2.up * _rotatingSpeed * (_direction == Direction.Left ? 1 : -1), Space.World);
    }
}
