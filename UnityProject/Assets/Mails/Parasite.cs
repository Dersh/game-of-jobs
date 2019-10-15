using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Parasite : MonoBehaviour, IParasiteAnimatorHandler
{
    public void ComingCycle()
    {
        _animator.SetTrigger("ComeHere");
    }
    public void OnBringing()
    {
        CreateMails();
    }
    public GameObject MailsPrefab;
    private void CreateMails()
    {

    }

    private Animator _animator;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

}
