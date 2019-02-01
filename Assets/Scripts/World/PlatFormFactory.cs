
using UnityEngine;

public class PlatFormFactory : MonoBehaviour {

	[SerializeField] private GameObject stoneyPlatform;
	[SerializeField] private GameObject grassyPlatform;
	[SerializeField] private GameObject bouncyPlatform;

	private void Awake(){
		CleanSlate();
	}

	void Start () {
		if (transform.position.x != 0){
			Vector3[,] possiblePositions = new Vector3[5, 3];

			float Yposition = -2f;
			float XPosition = -3.6f;

			int maxRows = 5;
			int maxColumns = 3;

			for (int i = 0; i < maxRows; i++)
			{
				for (int n = 0; n < maxColumns; n++)
				{
					possiblePositions[i, n] = new Vector3(transform.position.x + XPosition, Yposition, 1);
					XPosition += (XPosition == 3.6f) ? -7.2f : 3.6f;
				}
				Yposition += 1.5f;
			}

			GameObject[] randomPlatforms = new GameObject[3] { stoneyPlatform, grassyPlatform, bouncyPlatform };

			int patternOrRandom = Random.Range(1, 4);

			if (patternOrRandom < 3)
				CreatePatterned(possiblePositions, randomPlatforms);
			else
				CreateChaotic(possiblePositions, randomPlatforms);
		}
	}

	private void CreateChaotic(Vector3[,] possiblePositions, GameObject[] randomPlatforms) {
		int percentChance = 75;

		foreach (Vector3 actualPositions in possiblePositions) {
			int diceRoll = Random.Range(1, 100);

			if (diceRoll < percentChance) {
				GameObject createdPlatform = (GameObject)GameObject.Instantiate(randomPlatforms[Random.Range(0, 3)], actualPositions, transform.rotation);
				createdPlatform.transform.parent = this.gameObject.transform;
				percentChance -= 5;
			}
			percentChance -= 5;
		}
	}

	private void CreatePatterned(Vector3[,] possiblePositions, GameObject[] randomPlatforms) {
		int patternCounter = 0;
		int evenOrOdd = Random.Range(0, 2);

		foreach (Vector3 actualPosition in possiblePositions) {
			if (patternCounter % 2 == evenOrOdd && patternCounter < Random.Range(5, 15)) {
				GameObject createdPlatform = (GameObject)GameObject.Instantiate(randomPlatforms[Random.Range(0, 3)], actualPosition, transform.rotation);
				createdPlatform.transform.parent = this.gameObject.transform;
			}
			patternCounter ++;
		}
	}

	private void CleanSlate() {
		foreach (Transform child in transform){
			Destroy(child.gameObject);
		}
	}
}
