using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerHealth : MonoBehaviour {

	//Vida máxima del jugador
	public int maxHealth = 100;
	//vida actual del jugador
	public int currentHealth;
	//referencia al slider
	public Slider healthSlider;
	//referencia a la imagen con la que flashearemos la pantalla al recibir daño
	public Image damageImage;
	//clip de audio con la destrucción del tanque
	//public AudioClip deathClip;
	//tiempo que durará el fade-out de la imagen de daño
	public float flashSpeed = 5f;
	//color de l aimagen de daño
	public Color flashColor = new Color (0.5f, 0.5f,0.5f,1f);

	//sistema de partículas mostrado cuando el jugador muere
	public GameObject destructionParticles;


	//referencia al componenete playerController
	PlayerController playerController;
	//referencia al componenete playerShooting
	PlayerShooting playerShooting;



	//para controlar cuando el jugador está muerto
	bool isDead;
	//para controlar cuando el jugador recube daño
	public bool damaged;
	public bool damagedArchievement = false;

	// Use this for initialization
	void Start () {
		
		playerController = GetComponent<PlayerController> ();
		playerShooting = GetComponentInChildren<PlayerShooting> ();


		//inicializamos la vida actual para que sea igual a la vida máxima
		currentHealth = maxHealth;

	}

	// Update is called once per frame
	void Update () {
		if (damaged) {
			//Si se ha recibido daño, hacemos que el color de la imagen de daño sea igual al predefinido
			damageImage.color = flashColor;
		} else {
			damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
		}
		//desactivamos el estado de daño recibido una vez coloreada la imagen de daño
		damaged = false;


	}
	/// <summary>
	/// Realiza las acciones de control de daño del jugador, aplicando el daño recibido como parámetro	
	/// </summary>
	/// <param name="amount">Cantidad de daño recibido</param>
	public void TakeDamage (int amount){
		damagedArchievement = true;
		//indicamos que el jugador ha recibido daño
		damaged = true;

		//reducimos la vida del jugador
		currentHealth -= amount;
		//actualizamos la barra de vida
		healthSlider.value = currentHealth;
		//si la vida es menor o igual que 0, consideramos que el jugador ha muerto
		if (currentHealth <= 0 && !isDead) {
			Death ();
		}
	}

		public void TakeHealth(int amount){
			currentHealth += amount;
			healthSlider.value = currentHealth;
			if (currentHealth > maxHealth) {
			currentHealth = maxHealth;
			}
		}


	/// <summary>
	/// Realiza las acciones de la muerte del jugador
	/// </summary>
	void  Death (){
		//indicamos que el ejugador está muerto
		isDead = true;



		//activamos el sistema de paticulas de destruccion del player
		destructionParticles.SetActive (true);

		//desactivamos los controles de movimiento del tanque, desactivamos el script entero
		playerController.enabled = false;
		//desactivamos el control de disparo 
		playerShooting.enabled = false;
		GameManager.GM.EndGame ();

	}
}
