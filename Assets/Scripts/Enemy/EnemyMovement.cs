using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyMovement : MonoBehaviour {

	//Posición del objeto player.
	Transform player;
	//referencia al navmeshagent del enemigo.
	NavMeshAgent nav;
	//referencia al componente player health del player
	PlayerHealth playerHealth;
	// Use this for initialization
	void Start () {
		//Recuperamos la referencia al transform del player.
		player = GameObject.FindGameObjectWithTag ("Player").transform;

		//recuperamos la componente PlayerHealth del Player
		playerHealth = player.GetComponent<PlayerHealth> ();

		//Recuperamos el componente NacMeshAgent del enemy.
		nav = GetComponent<NavMeshAgent>();
	}

	// Update is called once per frame
	void Update () {
		//Si el navḿeshagent está activo,
		if (nav.enabled) {
			//fijamos como objetivo de desplazamiento la posición del jugador.
			nav.SetDestination(player.position);
			//si el jugador ha muerto y el nav está activo

			if (playerHealth.currentHealth <= 0 && !nav.isStopped) {
				//paramos el seguimiento del enemigo

				nav.isStopped = true;

			}
		}



	}
}
