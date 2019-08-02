using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : MonoBehaviour {
	//daño que hace el misil
	public int damage = 20;
	//referencia al sistema de particulas
	private ParticleSystem explosionParticles;
	//para asegurarnos que solo se activa una vez
	private bool activated = false;
	private GameObject player;


	public float tumble;
	// Use this for initialization
	void Start () {
		
		explosionParticles = GetComponent<ParticleSystem> ();
		player = GameObject.FindGameObjectWithTag ("Player");

	}
	
	// Update is called once per frame
	void Update () {
		Destroy (gameObject,7f);
	}

	void OnTriggerEnter (Collider other){
		if (other.CompareTag ("Player") && !activated) {
			activated = true;
			PlayerHealth playerHealth = player.GetComponent<PlayerHealth> ();
			if (playerHealth != null) {
				int critics = Random.Range(0,10);
				if (critics == 1) {
					playerHealth.TakeDamage (80);
					AchievementManager.AM.AchievementIncreaseAmount("criticos",1);
				}

				playerHealth.TakeDamage (damage);
			}
			explosionParticles.Play ();
			Destroy (gameObject);
		}


	}
}
