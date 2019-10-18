using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class MailsReceiver : InteractableItem
{
    public Action putInWrongBox;
    public Action putInRightBox;

    protected override void OnInteract()
    {
        try
        {
            if (ItemsBuffer.Instance.GetChosenItem().GetComponent<Mail>().TypeMatches(_type))
            {
                ItemsBuffer.Instance.PullFromBuffer(ItemsBuffer.Instance.GetChosenItem(), GeneratePos());
                putInRightBox();
            }
            else
            {
                putInWrongBox();
            }
        }
        catch
        {
            Debug.LogWarning("Clicked with an empty hand");
        }
    }
    [SerializeField] private Bounds _spaceForItems;
    public Vector3 GeneratePos()
    {
        Vector3 from = transform.position + Vector3.Scale(_spaceForItems.center - _spaceForItems.extents, transform.localScale);
        Vector3 to = transform.position + Vector3.Scale(_spaceForItems.center + _spaceForItems.extents, transform.localScale);
        return new Vector3(UnityEngine.Random.Range(from.x, to.x), UnityEngine.Random.Range(from.y, to.y), UnityEngine.Random.Range(from.z, to.z));
    }
    private void Start()
    {
        putInWrongBox = () => {
            Debug.Log("Wrong");
        };
        putInRightBox = () => {
            Debug.Log("Right");
        };
    }
}
