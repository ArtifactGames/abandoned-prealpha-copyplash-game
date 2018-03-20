using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsGM : MonoBehaviour {

	public Toggle fullscreen;
	public Dropdown gameLanguage;

	// Use this for initialization
	void Start () {
		fullscreen.isOn = GameConfig.fullscreen;
		gameLanguage.value = GameConfig.locale;
	}
	
	public void onSave(){
		GameConfig.fullscreen = fullscreen.isOn;
		GameConfig.locale = gameLanguage.value;
        SceneManager.LoadScene("main-menu");
    }

	public void onCancel(){
		SceneManager.LoadScene("main-menu");
	}
}
