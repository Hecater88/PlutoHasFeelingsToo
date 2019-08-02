using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {
	private int numberCoins;
	PlayerHealth playerHealth;
	GameObject player;
	public float tumble;
	private Rigidbody rb;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		//recuperamos el componente PlayerHealth del player
		playerHealth = player.GetComponent<PlayerHealth> ();
		numberCoins = Random.Range (1, 20);
		rb = GetComponent<Rigidbody> ();
		rb.angularVelocity = Random.insideUnitSphere * tumble;
	}
	
	// Update is called once per frame
	void Update () {
		Destroy (gameObject,10f);
	}
	void OnTriggerEnter(Collider other){
		if(other.CompareTag("Player")){
			AchievementManager.AM.AchievementIncreaseAmount("suerte",1);
			playerHealth.TakeHealth (numberCoins);
			Destroy (gameObject);
		}
	}
}
