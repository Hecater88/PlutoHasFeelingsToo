using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackEarth : MonoBehaviour {
	//para control interno del tiempo de enfriamiento
	private float timer;
	//referencia al enemigo Earth
	//private GameObject earth;
	//referencia al rigidbody de Earth
	//private Rigidbody earthRB;
	//prefab de la bala instanciada
	public GameObject bulletPrefab;
	//tiempo de enfriamiento de la habilidad especial
	public float coolDown = 20f;
	//fuerza de retroceso aplicada con el disparo
	public float recoilForce = 200f;
	//fuerza con la que será lanzada la bala
	public float bulletForce = 700f;

	// Use this for initialization
	void Start () {
		//localizamos el objeto Earth
		//earth = GameObject.FindGameObjectWithTag ("Earth");
		//earthRB = earth.GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime;
		if (timer <= 0) {

			Shoot ();
		}
	}

	void Shoot(){
		//inicializamos
		timer = coolDown;

		//muestro los efectos del disparo
		//shootParticle.Play ();


		GameObject tempBullet = Instantiate (bulletPrefab, 
											transform.position, 
											Quaternion.Euler(0f,transform.rotation.eulerAngles.y,0f));
		//aplicamos la fuerza de lanzamiento a la bala del cañon
		tempBullet.GetComponent<Rigidbody> ().AddForce (transform.forward * bulletForce);

		//aplicamos la fuerza de retroceso al tanque
		//earthRB.AddForce (-transform.forward * recoilForce);



	}
}
