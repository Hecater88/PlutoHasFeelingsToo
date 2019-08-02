using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidsforOptions : MonoBehaviour {
	//velocidad a la que se moverá el asteroide
	public float speed = 10f;
	//limite del asteroide
	public float destinationX = 30f;
	public float tumble;
	private Rigidbody rb;


	void Start () {
		rb = GetComponent<Rigidbody> ();
		rb.angularVelocity = Random.insideUnitSphere * tumble;
	}
	// Update is called once per frame
	void Update () {
		//calculamos la nueva posicion en funcion de la velocidad.
		Vector3 newPos = new Vector3(transform.position.x + 
			(speed * Time.deltaTime), transform.position.y, 
			transform.position.z);

		//aplicamos la nueva posicion.
		transform.position = newPos;

		//si la nube llega a la posición de destino, la destruyo.
		if (transform.position.x >= destinationX) {
			Destroy (gameObject);

		}


	}




}
