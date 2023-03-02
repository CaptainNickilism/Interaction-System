using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class InteractionManager : MonoBehaviour
{
    static public InteractionManager instance = null;
    public bool enableDebugMode;

    public Holdable heldObject;
    public Draggable draggedObject;
    public Interactable interactedObject;
    void Awake()
    {

        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void ClickAction(GameObject clickedObject)
    {
        if (clickedObject && clickedObject.TryGetComponent(out Interactable interactable)) //if the hit object has the Interactable component
        {
            interactedObject = interactable;
            interactedObject.Interact();
        }
        else if (enableDebugMode) Debug.Log("Clicked on a non interactable surface");
    }

    public void StartHoldAction(GameObject loggedObject) // Actions to perform when holding starts
    {
        if (loggedObject && loggedObject.TryGetComponent(out Holdable holdable)) //if the hit object has the Draggable component
        {
            heldObject = holdable;
        }
        else if (enableDebugMode) Debug.LogWarning("No Holdable component found.");
    }

    public void HoldAction(GameObject loggedObject)
    {
        // Things to do while holding
    }

    public void StopHoldAction(GameObject loggedObject) // Actions to perform when holding stops
    {
        if (heldObject)
        {
            // Things to do when stopping hold           
            heldObject = null;
        }
        else if (enableDebugMode) Debug.LogError("Attempting to stop hold, but no Holdable object found");
    }

    public void StartDragAction(GameObject loggedObject) // Actions to perform when dragging starts
    {  
        if (loggedObject && loggedObject.TryGetComponent(out Draggable draggable)) //if the hit object has the Draggable component
        {
            draggedObject = draggable;
            draggedObject.gameObject.GetComponent<PolygonCollider2D>().enabled = false; // Disables collision to be able to raycast behind the currently dragged object
        }
        else if (enableDebugMode) Debug.LogWarning("No Draggable component found.");
    }

    public void DragAction(GameObject loggedObject)
    {
        if (draggedObject)
        {
            draggedObject.gameObject.transform.localPosition = InputManager.instance.cursorPos;
        }
        else if (enableDebugMode) Debug.LogWarning("No item is being dragged!");
    }

    public void StopDragAction(GameObject hoveredObject) // Actions to perform when dragging stops
    {
        if (draggedObject)
        {
            draggedObject.Interact(hoveredObject);
            draggedObject.gameObject.GetComponent<PolygonCollider2D>().enabled = true; // Enables collision again
            draggedObject = null;
        }
        else if (enableDebugMode) Debug.LogError("Attempting to stop drag, but no Draggable object is logged");
    }

}
