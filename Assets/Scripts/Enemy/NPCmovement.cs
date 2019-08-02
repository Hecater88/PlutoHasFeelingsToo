using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class NPCmovement : MonoBehaviour {
	//referencia al navmeshagent del enemigo.
	NavMeshAgent nav;
	//variable que guarda la distancia de offset entre el NPC y el chekpoint
	public float offset = 1f;
	//variable que guarda la position en la que tiene que ir jupiter
	Vector3 actualPosition;
	//variable booleana que determina si se ha encontrado o no un enemigo
	public bool localizedEnemy = false;


	public float maxX;
	public float minX;
	public float maxZ;
	public float minZ;

	//Posición del objeto player.
	Transform player;
	//Posicion del objetivo
	Vector3 objetive;
	// Use this for initialization
	void Start () {
		//Recuperamos el componente NacMeshAgent del enemy.
		nav = GetComponent<NavMeshAgent>();
		//Recuperamos la referencia al transform del player.
		player = GameObject.FindGameObjectWithTag ("Player").transform;


		actualPosition = new Vector3 (Random.Range(minX,maxX),0f,Random.Range(minZ,maxZ));

		objetive = actualPosition;
	}

	// Update is called once per frame
	void Update () {
		
		if(nav.enabled){
				nav.SetDestination (objetive);
				if (!localizedEnemy) {
					Vector3 distance;
					distance = (objetive - transform.position);
					if (distance.magnitude <= offset) {
						actualPosition = new Vector3 (Random.Range(minX,maxX),0f,Random.Range(minZ,maxZ));
						objetive = actualPosition;

					}
				} else {
					objetive = player.position;
					
				}
		}

	}
	/// <summary>
	/// Asigna un enemigo que perseguir
	/// </summary>
	/// <param name="Player">Player.</param>
	public void AssignEnemy(Transform Player){
		//Igualamos las referencias
		objetive = Player.position;
		//Si localizamos el enemigo, ponemos la booleana a true
		localizedEnemy = true;
	}
}
