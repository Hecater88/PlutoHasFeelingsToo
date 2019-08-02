using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVision : MonoBehaviour {
	//Posición del objeto player.
	GameObject player;
	NPCmovement vision;
	void Start(){
		//player = GameObject.FindGameObjectWithTag ("Player");
		vision = GetComponentInParent<NPCmovement>();

	}
	void OnTriggerEnter (Collider other){
		if (other.CompareTag ("Player")) {
			vision.AssignEnemy (other.transform);
		}
	}
}
