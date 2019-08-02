using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour {

	//para control interno del tiempo de enfriamiento
	private float timer;
	//referencia al player
	private GameObject player;
	//referencia al rigidbody del player
	private Rigidbody playerRB;
	//prefab de la bala instanciada
	public GameObject bulletPrefab;
	//referencia a la esfera pluto
	private GameObject pluto;
	//referencia del script PlutoAmount
	PlutoAmount plutoAmount;
	//variable publica que indica cuanta masa quita a PLutón
	public float removeAmount = 0.05f;
	//referencia al particle system del disparo
	ParticleSystem shootParticle;
	//tiempo de enfriamiento de la habilidad especial
	public float coolDown = 20f;
	//fuerza de retroceso aplicada con el disparo
	public float recoilForce = 200f;
	//fuerza con la que será lanzada la bala
	public float bulletForce = 700f;
	//cooldown para el ultimate
	private float coolDownShootModifier = 0f;
	//variable que indica cuanta masa quita a pluton con la ulti
	private float removeAmountModifier = 0f;
	//variable que guarda los puntos necesarios para activar el ultimate
	public float pointsForPowerUP = 100;
	//variable que guarda los puntos para activar el ultimate
	public int actualPointsForPowerUp = 0;


	[Header("PowerUp")]
	//indica si está activo el powerup
	[Tooltip("Indica si está activo el powerup")]
	//variable booleana para diferenciar si está activa o no el power up
	public bool powerUp = false;
	public bool powerUpActivated;
	//referencia del particle system para el power up
	public GameObject powerUpAura;
	//duracion del powerup
	public float powerUpDuration = 10f;
	//variable que hace de cuenta atrás para el powerup
	private float powerUpCountDown = 0f;
	//variable que resta el cooldown de disparo
	public float coolDownShoot = 0.5f;
	//variable que anula el removeAmount de Pluto, para poder disparar sin perder masa
	public float removeAmountPowerUp=0.05f;
	//referencia al UI del power UP
	public Animator powerUpUI;


	// Use this for initialization
	void Start () {
		//localizamos el objeto
		player = GameObject.FindGameObjectWithTag ("Player");
		//localizamos el objeto
		pluto = GameObject.FindGameObjectWithTag ("Pluto");
		plutoAmount = pluto.GetComponent<PlutoAmount> ();
		//recuperamos la referencia al componente Rigidbody del player
		playerRB = player.GetComponentInParent<Rigidbody> ();
		//recuperamos la referencia al particle system con el efecto del disparo
		shootParticle = GetComponent<ParticleSystem> ();


	}
	
	// Update is called once per frame
	void Update () {
		
		//decrementamos el contador de tiempo
		timer -= Time.deltaTime;
		//si se pulsa el boton de disparo secundario y el temporizador de enfriamiento ha terminado
		if (Input.GetButtonDown ("Fire1") && timer <= 0 && pluto.transform.localScale.x>0.3f) {
			//podemos disparar
			Shoot();
		}
		//si los puntos para la ulti supera el maximo
		if (actualPointsForPowerUp > pointsForPowerUP) {
			//activamos la ulti
			powerUp = true;
			//activamos el hudd que indica de que tiene disponible la ultimate
			powerUpUI.SetBool ("PowerUp", true);
		}
		//si se pulsa el click derecho y powerup es true
		if (Input.GetButtonDown ("Fire2") && powerUp) {
			AchievementManager.AM.AchievementIncreaseAmount("powerUp",1);
			//activamos ultimate
			PowerUpOn ();
		}

		if (powerUpCountDown > 0) {
			powerUpCountDown -= Time.deltaTime;
		} else {
			
			PowerUpOff ();
		}

	}
	/// <summary>
	/// Shoot this instance.
	/// </summary>
	void Shoot(){
		//inicializamos
		timer = (coolDown-coolDownShootModifier);
		Debug.Log (coolDownShootModifier);

		//muestro los efectos del disparo
		shootParticle.Play ();


		GameObject tempBullet = Instantiate (bulletPrefab, 
												transform.position, 
												Quaternion.Euler(0f,transform.rotation.eulerAngles.y,0f));
		//aplicamos la fuerza de lanzamiento a la bala del cañon
		tempBullet.GetComponent<Rigidbody> ().AddForce (transform.forward * bulletForce);

		//aplicamos la fuerza de retroceso al tanque
		playerRB.AddForce (-transform.forward * recoilForce);


		//pluto.transform.localScale -= new Vector3 (0.05f,0.05f,0.05f);
		plutoAmount.RemoveAmount(removeAmount-removeAmountModifier);


	}
		/// <summary>
		// Metodo que activa el ultimate
		/// </summary>
		public void PowerUpOn(){
		powerUpUI.SetBool ("PowerUp", false);
		//ponemos la boleana de ultimate a false para resetearlo
		powerUp = false;
		//ponemos la booleana a true
		powerUpActivated = true;
		//resetaeamos los puntos de ultimate
		actualPointsForPowerUp = 0;
		//activamos el sistema de particulas
		powerUpAura.SetActive(true);
		//contador del tiempo durante la ulti
		powerUpCountDown = powerUpDuration;
		//quitamos cooldown a los disparos
		coolDownShootModifier = coolDownShoot;
		//quitamos el numero de masa que le quita de pluto
		removeAmountModifier = removeAmount;
	}

	/// <summary>
	// Metodo que desactiva el ultimate
	/// </summary>
	public void PowerUpOff(){
		powerUpActivated = false;
		powerUpAura.SetActive(false);
		coolDownShootModifier = 0f;
		removeAmountModifier = 0f;
		powerUpCountDown = 0f;
	}
	
	/// <summary>
	// Metodo que lleva a la scena de MainMenu
	/// </summary>
	public void TakePointsforPowerUp(int points){
		
		actualPointsForPowerUp += points;

	}
}
