using UnityEngine;
using UnityEngine.UI; // Make sure to include the UI namespace for InputField

public class main_menu_Settings : MonoBehaviour
{
    public Light_Spawn lightSpawn; // Reference to the Light_Spawn script
    public InputField inputField; // Reference to the InputField

    void Start()
    {
        // Add a listener to the InputField to update the number of lights
        inputField.onEndEdit.AddListener(UpdateNumberOfLights);
    }

    void UpdateNumberOfLights(string input)
    {
        // Parse the input and update the numberOfLights variable
        if (int.TryParse(input, out int newNumberOfLights))
        {
            lightSpawn.numberOfLights = newNumberOfLights;
        }
        else
        {
            Debug.LogWarning("Invalid input. Please enter a valid number.");
        }
    }
}
