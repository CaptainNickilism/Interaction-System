using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickable : Interactable
{
    public override void Interact()
    {
        Player.instance.AddToInventory(GetComponent<Interactable>());
        UpdateWorldItems();
    }
}
