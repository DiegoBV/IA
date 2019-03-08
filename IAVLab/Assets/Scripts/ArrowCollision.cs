using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowCollision : MonoBehaviour {
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.GetComponent<TankBehaviour> () != null) { //es el tanque
			Destroy(this.gameObject);
		}
	}

}
