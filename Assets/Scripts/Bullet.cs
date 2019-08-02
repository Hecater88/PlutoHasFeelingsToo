using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
	//radio de daño de la explosión
	public float explosionRadius = 3f;
	//daño que hace la explosión
	public int damage = 100;
	//para mostrar el gizmo en debug
	public bool drawGizmo = true;
	//referencia al sistema de particulas
	private ParticleSystem explosionParticles;
	//para asegurarnos que solo se activa una vez
	private bool activated = false;
	// Use this for initialization
	GameObject player;
	PlayerShooting playerShooting;
	void Start () {
		explosionParticles = GetComponent<ParticleSystem> ();
		player = GameObject.FindGameObjectWithTag ("Player");
		playerShooting = player.GetComponentInChildren<PlayerShooting> ();
	}
	void Update(){
		Destroy (gameObject,7f);
	}

	void OnTriggerEnter (Collider other)
	{
		//verificamos si el objeto colisionado, tiene un layer ground, shootable o wall
		//y además la explosión no ha sido activada aún
		if (other.gameObject.layer == LayerMask.NameToLayer ("Shootable") && !activated) {

			if (playerShooting.powerUpActivated == false) {
				playerShooting.TakePointsforPowerUp (10);
			}
			//indicamos que ya ha sido activado
			activated = true;
			//realizamos la explosión de partículas
			explosionParticles.Play ();

			//detectamos las colisiones dentro del radio pasado como parámetro
			Collider[] colls = Physics.OverlapSphere (transform.position, explosionRadius);
			foreach (Collider col in colls) {
				//verificamos si el objeto impactado dispone de EnemyHealth
				EnemyHealth enemyHealth = col.GetComponent<EnemyHealth> ();
				EnemyHealthJupiter enemyHealthJupiter = col.GetComponent<EnemyHealthJupiter> ();
				EnemyHealthEarth enemyHealEarth = col.GetComponent<EnemyHealthEarth> ();

				//si dispone del componente, le hacemos daño
				if (enemyHealth != null) {
					enemyHealth.TakeDamage (damage, col.transform.position);
				}

				if (enemyHealthJupiter != null) {
					enemyHealthJupiter.TakeDamage (damage, col.transform.position);
				}
				if (enemyHealEarth != null) {
					enemyHealEarth.TakeDamage (damage, col.transform.position);
				}
			}

			//recuperamos todos los meshrenderer de la bala y los desactivamos para hacerla invisible
			MeshRenderer[] children = GetComponentsInChildren<MeshRenderer> ();
			foreach (MeshRenderer child in children) {
				child.enabled = false;
			}
			//temporizador la destruccion del objeto pasados 3 segundos
			Destroy (gameObject,0.5f);
		}
		if (other.CompareTag ("Asteroid")) {
			explosionParticles.Play ();
			//recuperamos todos los meshrenderer de la bala y los desactivamos para hacerla invisible
			MeshRenderer[] children = GetComponentsInChildren<MeshRenderer> ();
			foreach (MeshRenderer child in children) {
				child.enabled = false;
			}
			Destroy (gameObject,0.1f);
		}
	}
}
