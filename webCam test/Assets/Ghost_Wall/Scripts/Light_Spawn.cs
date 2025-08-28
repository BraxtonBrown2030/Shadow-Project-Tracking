using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Light_Spawn : MonoBehaviour
{
    public scriptibleIntObject numberofhandsSOBJ; // Reference to the ScriptableObject that holds the number of lights
    public int numberOfLights; // how many lights to spawn for each hand for the tracking script
    public GameObject lightPrefab; // Prefab of the light object to spawn
    public Vector3 spawnPosition; // Position where the lights will be spawned
    public Multy_Hand_Testin multyHandTestin;
    
    private List<GameObject> spawnedLights = new List<GameObject>(); // List to store al
    
    void Start()
    {
            for (int i = 0; i < numberOfLights; i++)
            {
                GameObject light = Instantiate(lightPrefab, spawnPosition, Quaternion.identity);
                spawnedLights.Add(light); // Add the clone to the list
            }

            // Assign the spawned lights to the playerLights array in Multy_Hand_Testin
            if (multyHandTestin != null)
            {
                multyHandTestin.playerLights = spawnedLights.ToArray();
            }
        
    }
    
    public void updateScriptableObject()
    {
        
        numberOfLights = numberofhandsSOBJ.value;
        
    }

    public void UpdateLightNumber()
    {
        
        for (int i = 0; i < numberOfLights; i++)
        {
            
            GameObject light = Instantiate(lightPrefab, spawnPosition, Quaternion.identity);
            spawnedLights.Add(light); // Add the clone to the list
            
        }
        // Assign the spawned lights to the playerLights array in Multy_Hand_Testin
        if (multyHandTestin != null)
        {
            multyHandTestin.playerLights = spawnedLights.ToArray();
        }

    }

}
