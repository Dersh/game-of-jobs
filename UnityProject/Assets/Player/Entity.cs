using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Entity : MonoBehaviour
{
    private NavMeshAgent _agent;
    private Animator _animator;
    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _path = new NavMeshPath();
        _animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, float.PositiveInfinity))
            {
                MoveTo(hit.point);
            }
        }
    }
    private NavMeshPath _path;
    protected void MoveTo(Vector3 pos)
    {
        if (NavMesh.CalculatePath(_agent.nextPosition, pos, NavMesh.AllAreas, _path))
            _agent.SetPath(_path);
    }
    private Vector3 _lastPos;
    private void FixedUpdate()
    {
        _animator.SetFloat("Speed", (transform.position - _lastPos).magnitude / Time.fixedDeltaTime);
        _lastPos = transform.position;
    }
}
