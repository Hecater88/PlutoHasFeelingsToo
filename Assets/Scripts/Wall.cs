using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour {
	//booleana para controlar si el jugador esta tocando el collider
	bool playerInRange;

	// Update is called once per frame
	void Update () {
		if (playerInRange) {
			//activamos el meshrenderer de wall
			GetComponent<MeshRenderer> ().enabled = true;
		} else {
			//desactivamos el meshrenderer de wall
			GetComponent<MeshRenderer> ().enabled = false;
		}
	}
	void OnTriggerEnter(Collider other){
		//si el objeto colisionado es el player
		if (other.CompareTag ("Player")) {
			//indicamos que el jugador se encuentra dentro del rango de ataque
			playerInRange = true;
		}
	}

	void OnTriggerExit(Collider other){
		if (other.CompareTag ("Player")) {
			//indicamos que el jugador ya nos se encuentra en rango
			playerInRange = false;
		}
	}
}
