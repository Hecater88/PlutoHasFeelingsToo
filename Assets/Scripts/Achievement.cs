using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Achievement{
		//nombre del logro
		public string achievementName;
		//código del logro para localizarlo
		public string achievementCode;
		//nombre del sprite del logro
		public string achievementImageName;
		//descripcion del logro
		public string achievementDescription;
		//cantidad a conseguir para obtener el logro
		public int achievementTargetAmount;
		//cantidad actual obtenida
		public int achievementActualAmount;
		//bool para controlar si el logro ya ha sido desbloqueado
		public bool achievementUnlocked = false;
}
