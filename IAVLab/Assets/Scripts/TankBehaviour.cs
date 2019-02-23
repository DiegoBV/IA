using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UCM.IAV.Puzzles;
using System;
using UCM.IAV.Puzzles.Model;

public class TankBehaviour : MonoBehaviour {

	bool clicked_ = false;
	private BlockBoard tablero;
	private System.Random rnd = new System.Random(Guid.NewGuid().GetHashCode());

	public void Initialize(BlockBoard board){
		tablero = board;
		clicked_ = false;
		changeColor();

		int w = tablero.getWidth();
		int h = tablero.getHeight();

		MovableBlock block_ = null;
		do{
			uint x = (uint)rnd.Next(0, w);
			uint y = (uint)rnd.Next(0, h);

			block_ = tablero.GetBlock(new Position(x, y));
		}while(block_.getType() == MovableBlock.TipoCasilla.R);
			
		this.transform.position = new Vector3 (block_.transform.position.x, this.transform.position.y, block_.transform.position.z);
	}

	// Update is called once per frame
	/*void Update () {
		
	}*/

	public void OnMouseUpAsButton() {
		clicked_ = !clicked_;
		changeColor();
	}

	public bool isClicked(){
		return clicked_;
	}

	private void changeColor(){
		Renderer rend = this.GetComponent<Renderer> ();

		if (rend != null) {
			if (clicked_)
				rend.material.SetColor("_Color", Color.green);
			else
				rend.material.SetColor("_Color", Color.red);
		}
	}
}
