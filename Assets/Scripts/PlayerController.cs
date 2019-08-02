using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	


	//almacena el vector direcion del desplazamiento del player
	Vector3 movement;
	//velocidad máxima del player
	public float speed = 1.5f;
	//aceleración del player
	public float acceleration = 1000f;

	//referencia del rigidbosy del player
	Rigidbody rB;
	//distancia máxima del trazado del rayo desde el ratón contra el suelo
	private float camRayLength = 100f;

	//layer del colisión con el suelo, los layers se pueden recuperar de forma int o por nombre
	int groundMask;


	void Start () {
		////recuperamos la referencia al componente Rigidbody del player
		rB = GetComponent<Rigidbody> ();
		//recupera el índice de la mascara correspondiente
		groundMask = LayerMask.GetMask ("Ground");
	}
	
	void FixedUpdate(){
		//almacenamos en una variable el valor del eje de desplazamiento vertical
		float v = Input.GetAxis ("Vertical");
		//almacenamos en una variable el valor del eje de desplazamiento horizontal
		float h = Input.GetAxis ("Horizontal");

		Move (h, v);
		TurnAround ();
	}



	/// <summary>
	/// Desplazamiento del player
	/// </summary>
	/// <param name="h">Direccion horizontal</param>
	/// <param name="v">Direcccion vertical</param>
	void Move (float h, float v){
		//asignamos el vector de movimiento del tanque
		movement.Set (h, 0, v);
		//normallizamos el desplazamiento, para quue en las diagonales vaya a la misma velocidad
		movement = movement.normalized;
		//aplicamos la aceleración en la dirección del vector, suavizándola multiplizándola por el deltatime
		movement = movement * acceleration * Time.deltaTime;


		//si la velocidad absoluta del rigidbody, es inferior a la velocidad definida
		if (rB.velocity.magnitude < speed) {
			//aplicamos fuerza al rigidbody
			rB.AddForce (movement);
		}

		/*//si el tanque sealed está desplazando enum alguna dirección
		if (Mathf.Abs(h) > 0 || Mathf.Abs(v) > 0 ) {
			//recuperamos el input en un vector
			Vector3 temp = new Vector3 (h, 0f, v);
		}*/
	}
	/// <summary>
	/// Giro del jugador
	/// </summary>
	void TurnAround(){
		//trazamos un rayo desde la camara utilizando la posicion del raton en la pantalla
		Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);
		//alamacena el punto de colision del raycast con el suelo
		RaycastHit groundHit;
		//si el rayo choca con el groud
		if (Physics.Raycast (camRay, out groundHit, camRayLength, groundMask)) {
			//calculamos la dirección a la que apuntará la torreta.
			Vector3 playerToMouse = groundHit.point - transform.position;
			//eliminamos la componente vertical
			playerToMouse.y = 0f;
			//dibujo de una linea de la direccion donde apunta el player
			Debug.DrawLine (transform.position, groundHit.point, Color.green);
			//Obtenemos la rotación a la que debemos girar la torreta para que mire a la posición en la que se encuentra el cursor.
			Quaternion newRotation = Quaternion.LookRotation (playerToMouse);
			//Giramos la torreta en la dirección calculada.
			transform.rotation = newRotation;
		}
	}
		
}
