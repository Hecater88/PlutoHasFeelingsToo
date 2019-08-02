using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementUI : MonoBehaviour {
	//imagen del logro
	public Image imageUI;
	//nombre del logro
	private Text nameUI;
	//referencia al animator
	private Animator anim;


	// Use this for initialization
	void Start () {
		//recuperamos la referencia al nombre del logro, buscando un text entre los hijos
		nameUI = GetComponentInChildren<Text> ();
		//recuperamos la referencia al componente animator
		anim = GetComponent<Animator> ();
	}

	/// <summary>
	/// Modifica los vlaores de imagen y texto del logro
	/// </summary>
	/// <param name="name">Name.</param>
	/// <param name="imageName">Image name.</param>
	public void SetAchievement (string name, string imageName){
		//recuperamos el sprite de la carpeta Resources y lo asignamos al objeto image del canvas
		imageUI.sprite = Resources.Load<Sprite>(imageName);
		//cambiamos el texto del logro
		nameUI.text = name;
	}

	/// <summary>
	///Muestra el logro por pantalla
	/// </summary>
	public void ShowAchievement (){
		anim.SetTrigger ("Show");
	}
}
