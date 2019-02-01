
using UnityEngine;

public class HUD : MonoBehaviour {

	[SerializeField] private GUIStyle fontStyle = new GUIStyle();

	private int fontSize;

	void Start () {
		fontStyle.font = Resources.Load<Font>("Fonts/trebucbd");
	}

	private void OnGUI(){
		ScaleFontSize();

		GUI.Label(new Rect(Screen.width / 85f, Screen.height / 58, Screen.width, Screen.height),
			string.Format("Score: {0}", WorldManager.score.ToString()), fontStyle);

		GUI.Label(new Rect(Screen.width / 85f, Screen.height / 20, Screen.width, Screen.height),
			string.Format("Level: {0}", (WorldManager.level - 3).ToString()), fontStyle);
	}

	private void ScaleFontSize() {
		fontSize = Screen.width / 64;
		fontStyle.fontSize = fontSize;
	}
}
