using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractComp : MonoBehaviour
{
    List<Interactable> interactablesInRange = new List<Interactable>();


    private void OnTriggerEnter(Collider other)
    {
        Interactable otherInteractable = other.GetComponent<Interactable>();
        if(otherInteractable)
        {
            if(!interactablesInRange.Contains(otherInteractable))
            {
                interactablesInRange.Add(otherInteractable);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        Interactable otherInteractable = other.GetComponent<Interactable>();
        if (otherInteractable)
        {
            if (interactablesInRange.Contains(otherInteractable))
            {
                interactablesInRange.Remove(otherInteractable);
            }
        }
    }

    public void RemoveInteractable(Interactable interactableToRemove)
    {
        if (interactablesInRange.Contains(interactableToRemove))
        {
            interactablesInRange.Remove(interactableToRemove);
        }
    }
    public void InteractWithInteractable()
    {
        Interactable closestInteractable = GetClosestInteractable();
        if(closestInteractable != null)
        {
            closestInteractable.Interacting();
        }
    }

    Interactable GetClosestInteractable()
    {
        Interactable closestInteractable = null;
        if(interactablesInRange.Count == 0)
        {
            return closestInteractable;
        }

        float closestDist = float.MaxValue;
        foreach(var interactablesItem in interactablesInRange)
        {
            float Dist = Vector3.Distance(transform.position, interactablesItem.transform.position);
            if(Dist < closestDist)
            {
                closestInteractable = interactablesItem;
                closestDist = Dist;
            }
        }
        return closestInteractable;
    }
}
