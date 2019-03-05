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
	Position current;
	Stack<UCM.IAV.Puzzles.Model.SlidingPuzzle.Node> stack = null;
	Position targetPosition = null;

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
		}while(block_.getType() == MovableBlock.TipoCasilla.R || block_.getType() == MovableBlock.TipoCasilla.F);

		current = block_.position;
		print ("hellooo: " + current);
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

	public void setCurrent(Position p){
		this.current = p;
	}

	public Position getCurrent(){
		return this.current;	
	}

	public void setStack(Stack<UCM.IAV.Puzzles.Model.SlidingPuzzle.Node> stack_){
		this.stack = stack_;
		InvokeRepeating("movement", 2f, 1f);
	}

	void movement(){
		if (stack != null && stack.Count > 0) {
			//me muevo
			Tuple<uint, uint> pos = stack.Pop ().Position;
			targetPosition = new Position (pos.Item1, pos.Item2);
			MovableBlock block_ = tablero.GetBlock (new Position (targetPosition.GetRow (), targetPosition.GetColumn ()));
			this.transform.position = new Vector3 (block_.transform.position.x, this.transform.position.y, block_.transform.position.z);
			current = targetPosition;

			/*if (targetPosition == null) {
				Tuple<uint, uint> pos = stack.Pop ().Position;
				targetPosition = new Position(pos.Item1, pos.Item2);
			} else if (targetPosition != getCurrent ()) {
				MovableBlock block_ = tablero.GetBlock (new Position (targetPosition.GetRow (), targetPosition.GetColumn ()));
				this.transform.position = new Vector3 (block_.transform.position.x, this.transform.position.y, block_.transform.position.z);
				current = targetPosition;
			} else {
				targetPosition = null;
			}*/
		} 
		else {
			CancelInvoke ();
		}
	}
}
