using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Sospechoso))]

public class RandomAnimationPicker : MonoBehaviour {

	Animator anim;
	int AnimChoose;
	
	// Update is called once per frame
	void Start () {
		
		anim = GetComponent<Animator> ();
		anim.speed = 1;

		AnimChoose = Random.Range(1,3);
		anim.SetInteger("Idle", AnimChoose);
	}
}
