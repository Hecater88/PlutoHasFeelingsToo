using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyHealthJupiter : MonoBehaviour {
	//vida inicial del enemigo
	public int startingHealth = 100;
	//vida actual del enemigo
	public int currentHealth;
	//velocidad de hundimiento
	//public float sinkSpeed = 1f;
	//puntos recibidos al aliminar al enemigo
	public int points = 10;
	//clip de sonido que será reproducido al recibir un impacto
	//public AudioClip hitSound;
	//referencia al audiosource
	AudioSource audioSource;

	//sistema de particulas activado cuando el enemigo muere
	ParticleSystem explosionPlanet;
	//referencia al capsule collider, necesaria para desactivarla cuando el enemigo muera
	CapsuleCollider capsuleCollider;
	//referencia al animator
	//Animator anim;

	//referencia al componente enemyAttack, para desactivarlo cuando muera
	EnemyAttackJupiter enemyAttack;

	//para controlar si el enemigo está muerto
	bool isDead;
	//para controlar si el enemigo se encuentra hundiendose
	bool isSinking;


	// Use this for initialization
	void Start () {
		//recuperamos la refencia al audiosource
		audioSource = GetComponent<AudioSource> ();
		//cuidado, que coge el primer particle system de la jerarquia
		//recuperamos la referencia al sistema de partículas entre los hijos
		explosionPlanet = GetComponentInChildren<ParticleSystem>();
		//recuperamos la referencia al capsule colider
		capsuleCollider = GetComponent<CapsuleCollider> ();
		//recuperamos la referencia al Animator
		//anim = GetComponent<Animator> ();
		//recuperamos la referencia al EnemyAttack
		enemyAttack = GetComponentInChildren<EnemyAttackJupiter> ();

		//asignamos la vida inicial
		currentHealth = startingHealth;
	}


	/// <summary>
	/// Método que aplica el daño al enemigo
	/// Aplica el daño recibido como parámetro y posiciona el sistema de partículas en el punto de impacto
	/// </summary>
	/// <param name="amount">Amount.</param>
	/// <param name="hitPoint">Hit point.</param>
	public void TakeDamage(int amount, Vector3 hitPoint){
		if (isDead) {
			//si está muerto no necesitamos hacer que reciba daño
			return;
		}
		//aplicamos el daño
		currentHealth -= amount;
		//posicionamos y ejecutamos el sistema de partículas de recibir impacto
		//hitParticles.transform.position = hitPoint;
		//hitParticles.Play ();

		//reproducimos el sonido del impacto
		//audioSource.PlayOneShot (hitSound);

		//verificamos si el enemigo ha muerto tras recibir el daño
		if(currentHealth <= 0){
			Death ();
		}

	}

	/// <summary>
	/// Realiza las acciones para la gestion de la muerte del enemigo	
	/// </summary>
	void Death (){
		//indicamos que el enemigo está muerto
		isDead = true;

		//destruimos al componente capsule collider, para evitar comportamientos no deseados tras la muerte del enemigo
		Destroy (capsuleCollider);

		//destruimos el sphere collider responsable de hacer daño al enemigo
		Destroy (GetComponentInChildren<SphereCollider> ());

		//desactivamos el componente EnemyAttack, para que no siga haciendo daño al jugador
		enemyAttack.enabled = false;

		//desactivamos el navmeshAgent
		GetComponent<NavMeshAgent> ().enabled = false;

		//activamos el modo kinematic del rigidbody, para evitar que atraviese el suelo de golpe debido a la gravedad
		GetComponent<Rigidbody> ().isKinematic = true;
		//desactivmaos todos los mesh renderer
		MeshRenderer[] children = GetComponentsInChildren<MeshRenderer> ();
		foreach (MeshRenderer child in children) {
			child.enabled = false;
		}
		explosionPlanet.Play ();
		audioSource.Play ();
		Destroy (gameObject, 2f);

		//activamos el trigger que activará la animación de muerte
		//anim.SetTrigger ("Dead");

		//le indico al gamemanager que un enemigo de la oleada ha muerto
		GameManager.GM.EnemyDead ();

		//actualizo el contador para los logros de enemigos muertos
		//AchievementManager.AM.AchievementIncreaseAmount ("killer",1);
		AchievementManager.AM.AchievementIncreaseAmount("Jupiter",1);
		Debug.Log("Enemy dead");
	}

}
