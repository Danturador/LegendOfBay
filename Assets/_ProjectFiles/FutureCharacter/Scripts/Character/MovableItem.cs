using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableItem : MonoBehaviour
{
    private bool isChild = false; 

    void Update()
    {
       
    }

    public void ToggleParent(Transform player)
    {
        if (!isChild)
        {
            
            transform.SetParent(player);
            transform.localPosition = new Vector2(2f, 0.2f);
            isChild = true;
        }
    }

    public void DropItem()
    {
        transform.SetParent(null);
        isChild = false;
    }

}

