using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstRoundExplanationGM : MonoBehaviour {

	public void GoToFirstRoundWait(){
		SceneManager.LoadScene("first-round-wait");
	}
}
