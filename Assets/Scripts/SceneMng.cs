using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMng : MonoBehaviour {

	/// <summary>
	/// Cambia a la escena solicitada
	/// </summary>
	public void SceneLoad (string name){
			SceneManager.LoadScene (name);
	}
	/// <summary>
	/// Metodo que reinicia el juego
	/// </summary>
	public void Retry(){
		SceneLoad ("Game");
	}

	/// <summary>
	/// Metodo que sale del juego
	/// </summary>
	public void QuitGame(){
		Application.Quit ();
	}
}
