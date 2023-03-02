using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Holdable : Interactable
{
    public override void Interact()
    {
        Debug.LogWarning("You should be holding this, not clicking."); // This can become an animated helper overlay
    }
}
