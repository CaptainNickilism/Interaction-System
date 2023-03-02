using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Draggable : Interactable
{
    public GameObject targetObject;
    [Tooltip("Should the item go back to its original position if it is dropped in the wrong place?")]
    public bool bindedToPosition = true;
    [Range(0.001f, 1f)]public float returnSpeed = 1f;
    private Vector2 defaultPos, currentPos;

    private void Start()
    {
        defaultPos = gameObject.transform.position;
        currentPos = defaultPos;
    }

    private void Update()
    {
        transform.position = Vector2.Lerp(transform.position, currentPos, returnSpeed);
    }


    public override void Interact()
    {
        if (InteractionManager.instance.enableDebugMode) Debug.LogWarning("You should be dragging this, not clicking."); // This can become an animated helper overlay      
    }

    public override void Interact(GameObject otherObject)
    {   
        if (otherObject == targetObject)
        {
            if(InteractionManager.instance.enableDebugMode) Debug.Log("Dropped " + gameObject + " on " + otherObject); // This can become an animated helper overlay
            // Do stuff on drop
            UpdateWorldItems();
        }
        else
        {
            if (InteractionManager.instance.enableDebugMode) Debug.LogError("Dropped " + gameObject + " on the WRONG object (" + otherObject+")"); // This can become an animated helper overlay
            if (bindedToPosition) RestorePosition();
        }
    }

    private void RestorePosition()
    {
        currentPos = defaultPos;
    }

}
