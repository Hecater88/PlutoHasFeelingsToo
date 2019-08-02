using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AchievementMenu : MonoBehaviour {
	//nombre del logro
	public string achievementName;
	//referencia al contenedor de la imagen
	public Image imageUI;
	//referencia al texto del nombre
	public Text nameUI;
	//referencia al texto de descripcion
	public Text descriptionUI;
	//referencia al objeto shadow
	public GameObject shadow;
	// Use this for initialization
	void Start () {
		//recorremos el array de logros en busca del que coincida con el nombre buscado
		foreach(Achievement ach in DataManager.DM.achievements){
			if (ach.achievementName == achievementName) {
				//mostramos la imagen del logro
				imageUI.sprite = Resources.Load<Sprite> (ach.achievementImageName);
				//mostramos nombre del logro
				nameUI.text = ach.achievementName;
				//mostramos descripcion del logro
				descriptionUI.text = ach.achievementDescription;
				//si el logro no esta desbloqueado, mostramos la imagen de shadow que lo oscurezca
				shadow.SetActive (!ach.achievementUnlocked);
			}
		}
	}
}
