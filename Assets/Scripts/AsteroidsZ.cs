using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidsZ : MonoBehaviour {
	//velocidad a la que se moverá el asteroide
	public float speed = 10f;
	//limite del asteroide
	public float destinationX = 30f;
	//referencia al sistema de particulas
	private ParticleSystem explosionParticles;
	//prefab de la moneda
	//public GameObject coinPrefab;
	public int asteroidDamage = 15;
	GameObject player;
	PlayerHealth playerHealth;
	BoxCollider boxCollider;
	private bool activated = false;
	public float tumble;
	private Rigidbody rb;

	public AudioSource audioSource;

	void Start () {
		rb = GetComponent<Rigidbody> ();
		rb.angularVelocity = Random.insideUnitSphere * tumble;
		explosionParticles = GetComponentInChildren<ParticleSystem> ();
		player = GameObject.FindGameObjectWithTag ("Player");
		playerHealth = player.GetComponent<PlayerHealth> ();
		boxCollider = GetComponent<BoxCollider> ();
	}
	// Update is called once per frame
	void Update () {
		//calculamos la nueva posicion en funcion de la velocidad.
		Vector3 newPos = new Vector3(transform.position.x , 
									 transform.position.y, 
									 transform.position.z-(speed * Time.deltaTime));

		//aplicamos la nueva posicion.
		transform.position = newPos;

		//si la nube llega a la posición de destino, la destruyo.
		if (transform.position.z <= (-destinationX)) {
			Destroy (gameObject);

		}


	}

	void OnTriggerEnter(Collider other){
		if (other.CompareTag ("Bullet") && !activated) {
			//desactivmaos todos los mesh renderer
			MeshRenderer[] children = GetComponentsInChildren<MeshRenderer> ();
			foreach (MeshRenderer child in children) {
				child.enabled = false;
			}
			activated = true;
			audioSource.Play();
			explosionParticles.Play();
			Destroy (boxCollider);
//			int coinflip = Random.Range (0,15);
			/*if (coinflip <=3) {
				Instantiate (coinPrefab, 
							transform.position, 
							Quaternion.Euler (0f, transform.rotation.eulerAngles.y, 0f));
			}*/
			Destroy (gameObject,2f);
		}

		if (other.CompareTag ("Player") && !activated) {
			activated = true;
			audioSource.Play ();
			explosionParticles.Play ();
			MeshRenderer[] children = GetComponentsInChildren<MeshRenderer> ();
			foreach (MeshRenderer child in children) {
				child.enabled = false;
			}
			Destroy (boxCollider);
			playerHealth.TakeDamage (asteroidDamage);
			Destroy (gameObject,2f);
		}



	}
}
