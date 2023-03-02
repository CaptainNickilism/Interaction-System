using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Connection : Interactable
{
    [Header("Connection properties")]
    [SerializeField] protected Location targetLocation;
    private bool firstUse = true;

    public override void Interact()
    {
        LocationManager.instance.SwitchLocation(targetLocation);
        if(firstUse)
        {
            UpdateWorldItems();
            firstUse = false;
        }
        
    }
}
