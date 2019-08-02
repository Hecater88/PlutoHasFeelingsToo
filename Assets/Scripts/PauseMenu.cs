using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

	public static bool GameIsPaused = false;
	public GameObject pauseMenuUI;
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			if (GameIsPaused) {
				Resume ();
			} else {
				Pause();
			}
		}
	}

	/// <summary>
	// Metodo para volver al juego, des pues de haber pulsado pausa
	/// </summary>
	public void Resume(){
		pauseMenuUI.SetActive (false);
		Time.timeScale = 1f;
		GameIsPaused = false;
	}
	/// <summary>
	// Metodo para pausar el juego
	/// </summary>
	void Pause(){
		pauseMenuUI.SetActive (true);
		Time.timeScale = 0f;
		GameIsPaused = true;
	}
	/// <summary>
	// Metodo que lleva a la scena de MainMenu
	/// </summary>
	public  void LoadMenu(){
		Time.timeScale = 1f;
		SceneManager.LoadScene ("MainMenu");
	}
	/// <summary>
	// Metodo para salir del juego
	/// </summary>
	public void QuitGame(){
		Application.Quit ();
	}
}
