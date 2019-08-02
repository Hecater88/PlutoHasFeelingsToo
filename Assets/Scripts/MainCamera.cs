using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour {
	//objetivo de eguimiento d ela cámara
	public GameObject target;
	//distancia en el eje z a la que se posicionará la cámara respecto al objetivo
	public float zMargin = 2f;
	//suavizado del desplazamiento de la camara
	public float smoothTime = 0.2f;
	//variable que recibirá el valor de uno de los métodos
	private Vector3 velocity = Vector3.zero;
	//capas con las que colisionará el linecast
	public LayerMask wallLayer;

	// Update is called once per frame
	void FixedUpdate () {
		//calculamos cual será la posicion a la que desplazaremos la cámara
		Vector3 tempPosition = new Vector3 (target.transform.position.x,
			transform.position.y, 
			target.transform.position.z - zMargin);

		CheckOcclusion (ref tempPosition);

		//desplazamos la cámara con un movimiento suavizado
		transform.position = Vector3.SmoothDamp(transform.position,  
			tempPosition,
			ref velocity,
			smoothTime);

		//suavizado de la rotación de la camara hacia el objetivo
		transform.rotation = Quaternion.Lerp (transform.rotation, 
			Quaternion.LookRotation (target.transform.position - transform.position),
			Time.deltaTime);
	}
	/// <summary>
	/// Verificamos si la visión de la cámara está siendo obstaculizada por algún objeto
	/// </summary>
	/// <param name="pos">Position.</param>
	void CheckOcclusion (ref Vector3 pos)
	{
		//linea de debug desde el objetivo hasta la cámara
		Debug.DrawLine (target.transform.position, pos, Color.blue);

		//variable donde almacenaré la información de la colisión del linecast
		RaycastHit wallHit = new RaycastHit ();

		//realizamos el linecast y recueramos la informacion de la colisión
		if (Physics.Linecast (target.transform.position, 
			pos,
			out wallHit, 
			wallLayer)) {
			//si se detecta una colisión trazaremos un debug line vertical para identificar el punto
			Debug.DrawRay (wallHit.point, Vector3.up, Color.green);

			//posicionamos
			pos = new Vector3 (wallHit.point.x, 
				pos.y, 
				wallHit.point.z);

		}
	}
}
