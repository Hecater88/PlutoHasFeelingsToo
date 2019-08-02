using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//para poder serializar a binario añadimo el siguiente namespace
using System.Runtime.Serialization.Formatters.Binary;
//para grabar a fichero
using System.IO;
public class DataManager : MonoBehaviour {
	//array de logors
	public Achievement[] achievements;
	//nombre del fichero guardado
	public string fileName = "data.dat";
	//
	public static DataManager DM;

	// Use this for initialization
	void Awake () {
		if (DM == null) {
			//le indicamos que esta instancia, no será destruida al cargar nuevas escenas
			DontDestroyOnLoad (gameObject);
			//creacion de la instancia estática
			DM = GetComponent <DataManager> ();
		} else if (DM != GetComponent<DataManager>()) {
			Destroy (gameObject);
		}
		//recuperamos la informacion del disco
		Load();
	}
	/// <summary>
	/// Guarda la informacion al disco
	/// </summary>
	public void Save(){
		//objeto utilizado para serializar / deserializar
		BinaryFormatter bf = new BinaryFormatter ();

		//creamos / sobrescribimos el fichero con los datos
		FileStream file = File.Create (Application.persistentDataPath + "/" + fileName);

		//serializamos el contenido de logros al fichero
		bf.Serialize (file,achievements);

		//una vez finalizado cerramos el fichero
		file.Close();
	}

	/// <summary>
	/// Recupera la informacion guardada en el disco
	/// </summary>
	public void Load(){
		//para localizar el fichero fácilmente, sacamos por consola, la ruta del fichero
		Debug.Log(Application.persistentDataPath + "/"+ fileName);

		//antes de intentar abrir el fichero, verificamos si existe
		if (File.Exists (Application.persistentDataPath + "/" + fileName)) {

			//objeto utilizado para serializar / deserializar
			BinaryFormatter bf = new BinaryFormatter ();
			//abrimos el fichero para su lectura
			FileStream file = File.Open (Application.persistentDataPath + "/" + fileName, FileMode.Open);

			//deserializamos el contenido del fichero y lo volcamos en el array de logros
			achievements = (Achievement[])bf.Deserialize (file);

			//cerramos el fichero una vez hemos terminado
			file.Close();
		}
	}

}
