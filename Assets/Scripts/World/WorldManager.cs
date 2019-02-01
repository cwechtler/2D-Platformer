
using UnityEngine;

public class WorldManager : MonoBehaviour {

	public static int score;
	public static int level;

	private static int _difficulty;
	public static int Difficulty {get {return level / 3;}}

	[SerializeField] private GameObject cheeseHead;
	[SerializeField] private GameObject cheeseFist;

	private bool pauseSwitch;

	private void Awake(){
		level = 3;
	}

	private void Start (){
		Application.targetFrameRate = 60;
		QualitySettings.vSyncCount = 1; //Set Vsync to 1 for build to avoid jitter, set to 0 in editor for debugging FPS
	}

	private void Update(){
		GamePause();
	}

	private void GamePause(){

		if (Input.GetButtonDown("Start Button")) {
			pauseSwitch = !pauseSwitch;
		}
		if (pauseSwitch && !GameOverManager.isGameOver) {
			Time.timeScale = 0;
			cheeseHead.GetComponent<PlayerController>().enabled = false;
			cheeseFist.GetComponent<AttackController>().enabled = false;
		}
		else if(!pauseSwitch && !GameOverManager.isGameOver){
			Time.timeScale = 1;
			cheeseHead.GetComponent<PlayerController>().enabled = true;
			cheeseFist.GetComponent<AttackController>().enabled = true;
		}
	}
}
