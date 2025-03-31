using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Authrizer : MonoBehaviour
{
    public GameObject suspentedGameObject;


    private void Start()
    {
        suspentedGameObject = new GameObject();
        suspentedGameObject.SetActive(true);
        
    }
}
