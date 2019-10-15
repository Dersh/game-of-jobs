using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MailsGameplay : MonoBehaviour
{
    [SerializeField] private Parasite _parasite;

    private void Start()
    {
        _parasite.ComingCycle();
    }
    
}
