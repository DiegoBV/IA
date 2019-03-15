using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public Board tablero;
	public Color[] colors;
    //lo del instance....

	// Use this for initialization
	void Awake () {
        this.Initialize();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void Initialize()
    {
        tablero.Initialize(this);
    }
}
