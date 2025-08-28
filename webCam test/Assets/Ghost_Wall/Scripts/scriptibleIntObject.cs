using UnityEngine;

[CreateAssetMenu(fileName = "NewIntObject", menuName = "ScriptableObjects/IntObject", order = 1)]
public class scriptibleIntObject : ScriptableObject
{
    public int value; // The integer value stored in this ScriptableObject

    // Method to set the value
    public void SetValue(int newValue)
    {
        value = newValue;
    }

    // Method to get the value
    public int GetValue()
    {
        return value;
    }
    
    
}