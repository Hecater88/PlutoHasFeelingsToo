using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidManagerX : MonoBehaviour {
	//margen de generacion en el eje y
	public float marginY = 10f;
	//margen total de generacion en el eje Z
	public float marginZ = 5f;
	//prefabs de asteroides
	public GameObject[] asteroidPrefabs;
	//contador regresivo para realizar el spawn de nuevas asteroides
	public float asteroidDelay = 7f;
	//tiempo máximo de separación entre genración de asteroides
	private float spawnCounter = 0f;
	void Update () {
		spawnCounter -= Time.deltaTime;
		if(spawnCounter <= 0){
			spawnCounter = asteroidDelay;
			SpawnAsteroid();
		}
	}

	/// <summary>
	/// Genera un asteroide en una posición aleatoria dentro de un rango Z, ponemos el tamaño total del objeto
	///	y la dividimos por dos para que los asteroides aparezcan tanto en el eje positivo y el negativo del orgien del objeto
	/// </summary>
	void SpawnAsteroid (){
		//elegimos una posición aletaoria del eje z del objeto
		Vector3 asteroidNewPosition = new Vector3 (transform.position.x,
													transform.position.y ,
													transform.position.z + Random.Range (-(marginZ/2), (marginZ/2)));
		//Instanciamos asteroide
		Instantiate (asteroidPrefabs [Random.Range (0, asteroidPrefabs.Length)], asteroidNewPosition, Quaternion.identity);

	}

	void OnDrawGizmos(){
		//represento con un gizmo de cubo, el area de genracion de asteroides
		Gizmos.DrawCube (transform.position, new Vector3 (1, marginY, marginZ));
	}
}
