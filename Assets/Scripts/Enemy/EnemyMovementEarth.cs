using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyMovementEarth : MonoBehaviour {
	//referencia al navmeshagent del enemigo.
	NavMeshAgent nav;
	//variable que guarda la distancia de offset entre el NPC y el chekpoint
	//public float offset = 1f;
	//Posición del objeto player.
	Transform player;
	// Use this for initialization
	void Start () {
		//Recuperamos el componente NacMeshAgent del enemy.
		nav = GetComponent<NavMeshAgent>();
		//Recuperamos la referencia al transform del player.
		player = GameObject.FindGameObjectWithTag ("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
		if (nav.enabled) {
			nav.SetDestination (player.position);
		}
	}
}
