using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlutoAmount : MonoBehaviour {
	//variable que guarda el tamaño maximo de pluton
	public float maxScale = 1f;
	//tiempo de cooldown de crecimiento de pluton
	public float growTimeCoolDown = 5f;
	//masa que recibe pluton despues de pasar el cooldown
	public float plusGrow = 0.05f;
	//variable para guardar el tiempo
	private float timeGrow;
	// Use this for initialization
	void Start () {
		//reseteamos el tiempo
		timeGrow = growTimeCoolDown;
	}

	// Update is called once per frame
	void Update () {
		//restamos el tiempo
		timeGrow -= Time.deltaTime;
		//si el tiempo llega a 0
		if (timeGrow <= 0f) {
			//damos masa a pluton
			TakeAmount (plusGrow);
			//reseteamos el tiempo
			timeGrow = growTimeCoolDown;
		}
		//si el tamaño de pluton se pasa del maximo
		if (transform.localScale.x > maxScale) {
			//hacemos que no pase del maximo
			transform.localScale = new Vector3 (maxScale,maxScale,maxScale);
		}
	}

	/// <summary>
	/// Metodo que da masa a pluton
	/// </summary>
	public void TakeAmount(float amount){
		transform.localScale += new Vector3 (amount,amount,amount);
	}
	/// <summary>
	///Metodo que quita masa  a pluton
	/// </summary>
	public void RemoveAmount(float amount){
		transform.localScale -= new Vector3 (amount,amount,amount);
	}
}
