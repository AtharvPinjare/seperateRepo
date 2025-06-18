using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractionManager : MonoBehaviour
{
    [SerializeField] private float interactionDistance = 3f;
    [SerializeField] private LayerMask interactableLayer;
    [SerializeField] private InputActionReference triggerButton; // Assign trigger input in Inspector
    [SerializeField] private GameObject interactionPrompt; // UI Prompt (optional)

    private IInteractable currentInteractable;

    private void Update()
    {
        CheckForInteractable();

        if (currentInteractable != null && triggerButton.action.WasPressedThisFrame())
        {
            currentInteractable.Interact();
            interactionPrompt.SetActive(false);
        }
    }

    private void CheckForInteractable() 
    {
        Ray ray = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, interactionDistance, interactableLayer))
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();

            if (interactable != null)
            {
                //Debug.Log(hit.collider.gameObject.name);
                currentInteractable = interactable;
                if (interactionPrompt) interactionPrompt.SetActive(true);
                return;
            }
            
        }

        // No interactable found, hide prompt
        currentInteractable = null;
        if (interactionPrompt) interactionPrompt.SetActive(false);
    }


}
