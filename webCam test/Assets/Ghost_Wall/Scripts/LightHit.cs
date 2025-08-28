using UnityEngine;
using System.Collections;

public class LightHit : MonoBehaviour
{
    public GameObject lightPrefab; // Prefab of the light object to spawn
    public float maxDistance = 11f; // Maximum distance for the raycast
    

    private void FixedUpdate()
    {
        if (lightPrefab != null)
        {
            // Perform a raycast from the light's position in the direction it is facing
            Ray ray = new Ray(lightPrefab.transform.position, lightPrefab.transform.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, maxDistance))
            {
                Debug.Log("Spotlight is pointing at: " + hit.collider.gameObject.name);
                StartCoroutine(DisableObjectTemporarily(hit.collider.gameObject));
            }
            else
            {
                Debug.Log("Spotlight is not pointing at any object.");
            }
        }
        
    }

    private IEnumerator DisableObjectTemporarily(GameObject obj)
    {
        obj.GetComponent<MeshRenderer>().enabled = false; // Disable the object
        yield return new WaitForSeconds(3); // Wait for 1 second
        obj.GetComponent<MeshRenderer>().enabled = true; // Re-enable the object
    }
}