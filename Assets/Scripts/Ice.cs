using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ice : MonoBehaviour {
	//referencia a la esfera pluto
	private GameObject pluto;
	PlutoAmount plutoAmount;

	//Variable que guarda la cantidad de crecimiento para pluton
	float amountIce;
	public float minIce= 0.05f;
	public float maxIce = 0.2f;
	public float timeDestruct = 100f;
	// Use this for initialization
	void Start () {
		//localizamos el objeto
		pluto = GameObject.FindGameObjectWithTag ("Pluto");
		plutoAmount = pluto.GetComponent<PlutoAmount> ();
		amountIce = Random.Range (minIce, maxIce);
	
	}
	
	// Update is called once per frame
	void Update () {
		Destroy (gameObject,timeDestruct);
	}
	void OnTriggerEnter(Collider other){
		if (other.CompareTag ("Player")) {
			/*if (pluto.transform.localScale.x < 1f) {
				pluto.transform.localScale += new Vector3 (amountIce, amountIce, amountIce);
			*/
			AchievementManager.AM.AchievementIncreaseAmount("hielo",1);
			plutoAmount.TakeAmount (amountIce);
			Destroy (gameObject);
			}
			
		}
	}

