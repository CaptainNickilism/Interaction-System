using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum InteractableType
{
    PickUp,
    Interact,
    Connection
}

public abstract class Interactable : MonoBehaviour // This will become abstract
{
    [Header("General properties")]
    [SerializeField] protected List<GameObject> itemsToEnable;
    [SerializeField] protected List<GameObject> itemsToDisable;
    [Tooltip("Should the object be active when laoding the location?")]
    public bool activeOnStart;
    

    // Start is called before the first frame update
    void Awake()
    {
        _ = gameObject.AddComponent(typeof(PolygonCollider2D)) as PolygonCollider2D; // This automatically draws a pixel-perfect collider based on the 2D sprite shape
    }

    private void Start()
    {
        if (activeOnStart) gameObject.SetActive(true);
        else gameObject.SetActive(false);
    }

    public virtual void Interact()                          // This is for simple interactions
    {
        Debug.LogError("No Interact() override found.");
    }

    public virtual void Interact(GameObject otherObject)    // This is for interactions with other objects
    {
        Debug.LogError("No Interact() override found.");
    }

    public void UpdateWorldItems()
    {
        foreach (GameObject toDisable in itemsToDisable)
        {
            toDisable.SetActive(false);
        }
        foreach (GameObject toEnable in itemsToEnable)
        {
            toEnable.SetActive(true);
        }
    }


}
