
using UnityEngine;

public class GameOverManager : MonoBehaviour {

	public static bool isGameOver;

	private SpriteRenderer gameOverRenderer;

	void Start () {
		gameOverRenderer = GetComponent<SpriteRenderer>();
		isGameOver = false;
	}
	
	void Update () {
		if (isGameOver) {
			gameOverRenderer.color = Color.Lerp(gameOverRenderer.color, new Color(1, 1, 1, 0.8f), 0.5f * Time.deltaTime);

			GameStartManager reStart = GetComponentInChildren<GameStartManager>();
			reStart.enabled = true;
		}
	}
}
