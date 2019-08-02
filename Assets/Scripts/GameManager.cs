using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
public class GameManager : MonoBehaviour {
	//referencia estatica del gamemanager
	public static GameManager GM;
	//referencia del hud
	public GameObject hud;
	//referencia al menu de muerte
	public GameObject deadMenu;
	//variable que indica el record de puntos
	private float record = 120f;
	//referencia al script
	PlayerHealth playerHealth;
	//referencia al texto que indica el record de puntos
	public Text recordText;
	//referencia al texto donde indica el numero de oleadas superadas
	public Text superedWaveText;
	//variable que guarda el record de numero de oleadas
	public int recordWave = 0;
	//variable que guarda el numero de oleadas superadas
	public int superedWaves = 0;
	//referencia al player
	GameObject player;


	[Header("Wave UI")]
	//referencia al UI de oleadas
	public GameObject waveUI;
	//referencia al número de oleada en el HUD
	public Text waveText;
	public Text waveTextInGame;

	[Header ("Enemies")]
	//listado de puntos de spawn
	//public GameObject[] spawnPoints;
	//listado de prefabs de enemigos
	public GameObject[] enemies;
	//prefab del boss
	public GameObject enemyBoss;

	[Header ("waves")]
	//delay entre spawn de los enemigos
	public float spawnDelay = 0.2f;
	//número por el que multiplicaremos el número de la oleada, para obtener el número de enemigos
	//que serán generados
	public int waveEnemyNumberMultiplier = 20;
	//cada cuantas oleadas se genera un boss
	public int waveBoss = 5;
	//enemigos generados en la oleada actual
	private int waveEnemies;
	//enemigos restantes de la oleada (generados o no)
	private int remainingEnemies;
	//numero de oleada actual
	public int actualWave = 0;
	//temporizador para la generacion de enemigos
	private float spawnTimer = 0f;
	//número máximo de enemigos en escena
	public int maxEnemiesOnScene = 50;
	//contador de enemigos en escena
	public int enemiesOnScene;

	[Header ("Map")]
	//tamaño maximo del map en el eje x
	public float maxX;
	//tamaño maximo del map en el eje -x
	public float minX;
	//tamaño maximo del map en el eje z
	public float maxZ;
	//tamaño maximo del map en el eje -z
	public float minZ; 


	void Awake () {
		if (GM == null) {
			GM = GetComponent<GameManager> ();
		}
	}



	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		playerHealth = player.GetComponent<PlayerHealth> ();
		//scoreText.text = actualScore.ToString ();
		NewWave ();
		recordWave = LoadMaxWaves ();
	}

	// Update is called once per frame
	void Update () {
		
		//incrementamos el contador de tiempo
		spawnTimer += Time.deltaTime;
		record -= Time.deltaTime;

		//si quedan enemigos por generar y ha pasado el tiempo de delay
		if (waveEnemies > 0 && spawnTimer > spawnDelay && enemiesOnScene < maxEnemiesOnScene) {
			//generamos un nuevo enemigo
			GenerateEnemy();
			//reseteamos el temporizador de generacion
			spawnTimer = 0f;
		}

		//si se ha acabado con todos los enemigos de la oleada, generamos la siguiente oleada
		if (remainingEnemies <= 0) {
			if (record > 0f && actualWave>0) {
				AchievementManager.AM.AchievementIncreaseAmount("imparable",1);
			}
			if (playerHealth.damagedArchievement == false && actualWave > 5) {
				AchievementManager.AM.AchievementIncreaseAmount("tocar",1);
			}

			playerHealth.damagedArchievement = false;
			AchievementManager.AM.AchievementIncreaseAmount("ola",1);
			NewWave ();

		}
		superedWaveText.text = (actualWave-1).ToString ();
		waveTextInGame.text = actualWave.ToString ();
	}

	/// <summary>
	/// Realiza las acciones necesaria para la generacion de una nueva oleada.
	/// </summary>
	public void NewWave(){
		

		//en primer lugar incrementamos el número de la oleada
		record = 120f;
		actualWave++;
		//actualizamos el UI de oleadas
		waveText.text = actualWave.ToString ();



		//mostramos el UI de oleada
		waveUI.GetComponent<Animator> ().SetTrigger ("Show");

		//calculamos el  numero de enemigos que tendra la oleada
		waveEnemies = actualWave * waveEnemyNumberMultiplier;

		//el número de enemigos restantes de la oleada será inicialmente el número calculado para la oleada
		remainingEnemies = waveEnemies;

		//inicializado de enemigos en escena en cada oleada
		enemiesOnScene = 0;

		//verificamos si en esta oleada corresponde generar un boss
		if (actualWave % waveBoss == 0) {
			//generamos un boss en funcion de cuantas veces sea divisible la oleada entre waveboss
			for (int i = 0; i < (actualWave / waveBoss); i++) {
				GenerateBoss ();
			}
		}

		if (actualWave > recordWave) {
			SaveMaxWaves ();
			recordWave = actualWave;
			recordText.text = recordWave.ToString ();
		}

	}
	/// <summary>
	/// Genera un enemigo en una posicion de spawn aleatoria	
	/// </summary>
	public void GenerateEnemy(){
		//elijo una posicion de spawn al azar
		//int randomSpawn = Random.Range (0, spawnPoints.Length);
		//elijo un enemigo al azar
		int randomEnemy = Random.Range (0,enemies.Length);
		Vector3 randomSpawn = new Vector3 (Random.Range(minX,maxX),
											0.5f,
											Random.Range(minZ,maxZ));
		//instanciamos el enemigo
		if ((player.transform.position.x-5)< randomSpawn.x || (player.transform.position.x+5) >randomSpawn.x &&
			(player.transform.position.z-5)< randomSpawn.z || (player.transform.position.z+5) >randomSpawn.z) {
			GameObject tempEnemy = Instantiate (enemies [randomEnemy], randomSpawn, Quaternion.identity);
			//activamos el navemeshagent del enemigo tras ser instanciado
			//para evitar el error de reposicionamiento
			tempEnemy.GetComponent<NavMeshAgent>().enabled = true;
		}

	

		//decrementamos el número de enemigos restantes por generar
		waveEnemies--;

		//incrementamos el contador de enemigos en la escena
		enemiesOnScene++;

	}

	public void EnemyDead(){
		//decrementamos el contador de enemigos restantes
		remainingEnemies--;

		//decrementamos el contador de enemigos en la escena
		enemiesOnScene--;
	}

	/// <summary>
	/// Genera un boss
	/// </summary>
	public void GenerateBoss(){
		//elijo una posicion de spawn al azar
		//int randomSpawn = Random.Range (0, spawnPoints.Length);
		Vector3 randomSpawn = new Vector3 (Random.Range(minX,maxX),
			0.5f,
			Random.Range(minZ,maxZ));

		//instanciamos el enemigo
		GameObject tempBoss = Instantiate (enemyBoss, randomSpawn, Quaternion.identity);

		//activamos el navemeshagent del enemigo tras ser instanciado
		//para evitar el error de reposicionamiento
		tempBoss.GetComponent<NavMeshAgent>().enabled = true;

		//decrementamos el número de enemigos restantes por generar
		waveEnemies--;

		//incrementamos el contador de enemigos en la escena
		enemiesOnScene++;


	}
	
	/// <summary>
	/// Funcion que activa el gameover
	/// </summary>
	public void EndGame(){
		hud.SetActive (false);
		deadMenu.SetActive (true);
	}
	
	/// <summary>
	// metodo para guargar la oleada maxima con playerprefs
	/// </summary>
	 public void SaveMaxWaves(){
		PlayerPrefs.SetInt ("MaxWaves", recordWave);
		
	}

	
	/// <summary>
	// metodo para cargar la oleada maxima con playerprefs
	/// </summary>
 	 public int LoadMaxWaves(){
			//verificamos el valor a recuperar
		if (PlayerPrefs.HasKey ("MaxWaves")) {
			recordWave = PlayerPrefs.GetInt ("MaxWaves", recordWave);
	}
		//recupera el valor
		return recordWave;
	}
}
