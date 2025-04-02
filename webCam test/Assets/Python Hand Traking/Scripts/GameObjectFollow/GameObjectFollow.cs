using UnityEngine;

public class GameObjectFollow : MonoBehaviour
{
    public Transform objectToFollow;
    
    void Update()
    {
        if(objectToFollow != null)
        {
            transform.LookAt(objectToFollow);
        }
    }
}
