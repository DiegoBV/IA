using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateGameObject : MonoBehaviour {
    public GameObject go;
    
    public void Activate()
    {
        go.SetActive(!go.activeSelf);
    }
}
