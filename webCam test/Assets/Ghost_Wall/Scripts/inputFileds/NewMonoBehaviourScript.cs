using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public scriptibleIntObject intObject; // Reference to the ScriptableObject
    public TMP_InputField inputField; // Reference to the InputField

    void Start()
    {
        // Initialize the InputField with the value from the ScriptableObject
        inputField.text = intObject.value.ToString();

        // Add a listener to update the ScriptableObject when the InputField value changes
        inputField.onEndEdit.AddListener(UpdateScriptableObject);
    }

    void UpdateScriptableObject(string input)
    {
        // Parse the input and update the ScriptableObject value
        if (int.TryParse(input, out int newValue))
        {
            intObject.SetValue(newValue);
            Debug.Log("ScriptableObject value updated to: " + newValue);
        }
        else
        {
            Debug.LogWarning("Invalid input. Please enter a valid number.");
        }
    }
}