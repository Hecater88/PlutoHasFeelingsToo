using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceManager : MonoBehaviour {
	//tamaño maximo del objeto en el eje x
	public float maxX;
	//tamaño minimo del objeto en el eje x
	public float minX;
	//tamaño maximo del objeto en el eje z
	public float maxZ;
	//tamaño minimo del objeto en el eje z
	public float minZ;
	
	//tiempo de cooldown para el respawn
	public float coolDownRespawnIce = 10f;
	//variable para el tiempo
	private float timer;
	public GameObject icePrefab;
	// Use this for initialization
	void Start () {
		//localizamos el objeto
		//pluto = GameObject.FindGameObjectWithTag ("Pluto");


	}
	
	// Update is called once per frame
	void Update () {
		//cada cierto tiempo generamos un hielo
		timer -= Time.deltaTime;
		if (timer <= 0) {
			IceGenerator ();
		}
	}

	/// <summary>
	/// Funcion para posicion de forma aleatoria el respawn de lo shielos
	/// </summary>
	public void IceGenerator(){
			timer = coolDownRespawnIce;
			Vector3 icePosition = new Vector3 (Random.Range(minX,maxX),
												1f,
												Random.Range(minZ,maxZ));

			Instantiate(icePrefab,icePosition,Quaternion.identity);
	}
}
