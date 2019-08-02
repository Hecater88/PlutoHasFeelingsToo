using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAttack : MonoBehaviour {


	//duracion de tiempo entre ataques
	public float timeBetweenAttacks = 0.5f;
	//daño que realiza el enemigo por ataque
	public int attackDamage = 10;


	//para controlar si el jugador se encuentra en rango para recibir daño
	bool playerInRange;
	//temporizador para la gestion del daño
	float timer;

	//referencia al jugador
	GameObject player;
	//referencia al componente playerHealth del jugador
	PlayerHealth playerHealth;
	//referencia al componente navmeshagent
	NavMeshAgent nav;


	// Use this for initialization
	void Start () {
		//localizamos el gameObject del player
		player = GameObject.FindGameObjectWithTag ("Player");
		//recuperamos el componente PlayerHealth del player
		playerHealth = player.GetComponent<PlayerHealth> ();
		//recuperamos la referencia al navmeshagent
		nav = GetComponent<NavMeshAgent> ();
	}

	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		//si ha pasado el tiempo entre ataques y el jugador se encuentra en rango para recibir daño
		if (timer >= timeBetweenAttacks && playerInRange) {
			//atacamos al jugador
			Attack ();
		}
	}

	void OnTriggerEnter(Collider other){
		//si el objeto colisionado es el player
		if (other.CompareTag ("Player")) {
			//indicamos que el jugador se encuentra dentro del rango de ataque
			playerInRange = true;
			if (nav.enabled) {
				//detenemos el movimiento del enemigo
				nav.isStopped = true;
			}
		}
	}

	void OnTriggerExit(Collider other){
		if (other.CompareTag ("Player")) {
			//indicamos que el jugador ya NO se encuentra en rango
			playerInRange = false;

			if (nav.enabled) {
				//reanudamos el movimiento del enemigo
				nav.isStopped = false;
			}
		}
	}


	/// <summary>
	/// Realiza las acciones de ataque para hacer daño al jugador
	/// </summary>
	void Attack(){
		//una vez se consiga atacar, reseteamos el contador
		timer = 0f;
		if (playerHealth.currentHealth > 0) {
			//le hago daño
			playerHealth.TakeDamage (attackDamage);

		}
	}
}
