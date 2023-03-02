using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Usable : Interactable
{
    [Header("Usable properties")]
    [Tooltip("Should the required items be consumed on use?")]
    [SerializeField] protected bool consumeItems;
    [SerializeField] protected List<Interactable> requiredItems;

    public override void Interact()
    {
        if (CheckRequiredItems()) 
        {
            Debug.Log("Interacted with " + this.name);
            UpdateWorldItems();
        }
    }

    public bool CheckRequiredItems()
    {
        bool result = true;
        if (Player.instance.enableDebugMode) Debug.Log("Starting required items check");
        if (requiredItems.Count > 0)
        {
            foreach (Interactable item in requiredItems)
            {
                if (!Player.instance.inventory.Contains(item))
                {
                    result = false;
                    if (Player.instance.enableDebugMode) Debug.Log(item.name + " is missing from inventory. Check failed.");
                }
                //else if (removeRequiredItems) // This is if we want to remove the required items even if the player still doesn't have them all
                //{
                //    Player.instance.inventory.Remove(item);
                //    interactable.requiredItems.Remove(item);
                //}
            }
        }
        if (result && consumeItems) // This is if we want to remove all required items at once, only if the player has them all
        {
            if (Player.instance.enableDebugMode) Debug.Log("Removing required items from the inventory");
            foreach (Interactable toRemove in requiredItems)
            {
                Player.instance.inventory.Remove(toRemove);
            }
        }
        return result;
    }
}