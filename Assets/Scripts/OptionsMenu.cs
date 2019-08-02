using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class OptionsMenu : MonoBehaviour {
	//referencia al audiomixer
	public AudioMixer audioMixer;
	//listado de opciones
	public Dropdown resolutionDropDown;
	//array de resoluciones
	Resolution[] resolutions;

	void Start(){
		//guardamos en el array todas las resoluciones posibles
		resolutions = Screen.resolutions;
		//Limpia las opciones del dropdown 
		resolutionDropDown.ClearOptions ();
		//Crea una lista de strings, que van a ser las resoluciones de pantalla
		List<string> options = new List<string> ();
	
		//inicializamos la variable a 0
		int currentResulotionIndex = 0;
		for (int i = 0; i < resolutions.Length; i++) {
			//a cada una de las resoluciones que tenemos dependiendo del monitor que tengamos la guardamos en una variable temporal "option"
			string option = resolutions [i].width + "x" + resolutions [i].height;
			//y lo añadimos a la lista de strings
			options.Add (option);

			//si la resolucion es compatible con el monitor aumentamos el numero de resoluciones
			if (resolutions [i].width == Screen.currentResolution.width && resolutions [i].height == Screen.currentResolution.height) {
				currentResulotionIndex = i;
			}
		}

		//añadimos la lista en el dropdown
		resolutionDropDown.AddOptions (options);
		//actualizamos el numero de resoluciones en la lista
		resolutionDropDown.value = currentResulotionIndex;
		//actualizamos la lista
		resolutionDropDown.RefreshShownValue ();
	}

	/// <summary>
	/// Funcion para poner en pantalla la resolucion seleccionada.	
	/// </summary>
	public void SetResolution(int resolutionIndex){
		Resolution resolution = resolutions [resolutionIndex];
		Screen.SetResolution (resolution.width, resolution.height, Screen.fullScreen);
	}

	/// <summary>
	/// Funcion para modificar en el juego el sonido
	/// </summary>
	public void SetVolume(float volume){
		audioMixer.SetFloat ("volume", volume);
	}

	/// <summary>
	/// Funcion para modificar la calidad gráfica del juego
	/// </summary>
	public void SetQuality(int qualityIndex){
		QualitySettings.SetQualityLevel (qualityIndex); 
	}

	/// <summary>
	/// Funcion para poner el juego en modo ventana o modo pantalla completa
	/// </summary>
	public void SetFullscreen(bool isFullscreen){
		Screen.fullScreen = isFullscreen;
	}
}
