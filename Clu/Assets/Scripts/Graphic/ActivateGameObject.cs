using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Simple activacion o desactivacion de un GameObject
 */
public class ActivateGameObject : MonoBehaviour {
    public GameObject go;
    
    public void Activate()
    {
        go.SetActive(!go.activeSelf);
    }
}
