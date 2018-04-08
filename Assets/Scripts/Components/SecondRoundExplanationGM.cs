using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SecondRoundExplanationGM : MonoBehaviour {

	public void GoToSecondRoundWait(){
		SceneManager.LoadScene("second-round-wait");
	}
}
