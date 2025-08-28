using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI; // Make sure to include the UI namespace for InputField

public class MenuUpdate : MonoBehaviour
{
    public UnityEvent menuButtonEvent;
    public InputActionReference actionReference;

    private void OnEnable()
    {
        if (actionReference != null && actionReference.action != null)
        {
            Debug.Log("Subscribing to action: " + actionReference.action.name);
            actionReference.action.Enable();
            actionReference.action.performed += OnActionPerformed;
        }
        else
        {
            Debug.LogError("ActionReference or action is null!");
        }
    }

    private void OnDisable()
    {
        // Unsubscribe from the action's performed event
        actionReference.action.performed -= OnActionPerformed;
    }

    private void OnActionPerformed(InputAction.CallbackContext context)
    {
        Debug.Log("action performed: " + context.action.name);
        // The action has been performed, invoke the event
        menuButtonEvent.Invoke();
    }
}