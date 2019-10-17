using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mail : PickableItem
{
    protected override void OnInteract()
    {
        ItemsBuffer.Instance.TryInsertBuffer(gameObject);
    }
}
