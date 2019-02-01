
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStartManager : MonoBehaviour {

	private float LoopTimer;

	void Update () {  
		LoopTimer += Time.deltaTime * 60;

			if (LoopTimer > 30){
				SpriteRenderer pressEnterRenderer = GetComponent<SpriteRenderer>();
				pressEnterRenderer.enabled = !pressEnterRenderer.enabled;
				LoopTimer = 0;
			}

			if (Input.GetButtonDown("Start Button")){
				WorldManager.score = 0;
				SceneManager.LoadScene("Cheese Land");
			}
	}
}
