using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementManager : MonoBehaviour {

	//referencia al objeto achievementUI que realizará al mostrado del logro
	public AchievementUI achievementUI;

	public static AchievementManager AM;
	// Use this for initialization
	void Start () {
		if (AM == null) {
			AM = GetComponent<AchievementManager> ();
		}
	}

	/// <summary>
	/// Incrementamos el contador del logro identificado con el code
	/// </summary>
	/// <param name="code">Code.</param>
	/// <param name="amount">Amount.</param>
	public void AchievementIncreaseAmount (string code, int amount){
		//para controlar si un logro ha sido desbloqueado en este incremento
		bool unlocked = false;

		//recorremos la lista de los logros disponibles
		foreach (Achievement ach in DataManager.DM.achievements) {
			if (ach.achievementCode == code) {
				//hacemos el incremento
				ach.achievementActualAmount += amount;

				//tras el incremento verificamos si se ha cumplido el logro
				//y su no estaba ya desbloqueado previamente
				if(ach.achievementActualAmount >= ach.achievementTargetAmount && !ach.achievementUnlocked){
					//marcamos el logro como conseguido
					ach.achievementUnlocked = true;

					//configuramos el logro por pantalla
					achievementUI.SetAchievement (ach.achievementName, ach.achievementImageName);

					//mostramos el logro por pantalla
					achievementUI.ShowAchievement ();

					//indicamos que en este ciclo se ha desbloqueado un logro
					unlocked = true;

				}
			}
		}

		//si se ha desbloqeuado un logro en esta partida, guardamos las estadísticas
		if(unlocked){
			DataManager.DM.Save ();
		}
	}
}
