using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidsManagerZ : MonoBehaviour {
	//margen de generacion en el eje y
	public float marginY = 10f;
	//margen de generacion en el eje X
	public float marginX = 5f;
	//prefabs d enubes disponibles
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
	/// Genera un asteroide en una posición aleatoria dentro de un rango X, ponemos el tamaño total del objeto
	///	y la dividimos por dos para que los asteroides aparezcan tanto en el eje positivo y el negativo del orgien del objeto
	/// </summary>
	void SpawnAsteroid (){
		//elegimos una posición aletaoria del eje x del objeto
		Vector3 cloudNewPosition = new Vector3 (transform.position.x+ Random.Range (-(marginX/2), (marginX/2)),
			transform.position.y ,
			transform.position.z );
		//Instanciamos asteroide
		Instantiate (asteroidPrefabs [Random.Range (0,asteroidPrefabs.Length)], cloudNewPosition, Quaternion.identity);

	}

	void OnDrawGizmos(){
		//represento con un gizmo de cubo, el area de genracion de nubes
		Gizmos.DrawCube (transform.position, new Vector3 (marginX, marginY, 1));
	}
}
