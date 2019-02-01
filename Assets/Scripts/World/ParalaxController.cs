
using UnityEngine;

public class ParalaxController : MonoBehaviour {

	[SerializeField] private float speed;
	[SerializeField] private float autoScroll;

	private Camera mainCamera;
	private Vector3 paralaxFollowCamera;

	private float scroll;
	private float offSet;
	
	void Start () {
		mainCamera = Camera.main;
		paralaxFollowCamera = transform.position;
		offSet = transform.position.x;
	}

	void LateUpdate () {
		scroll += autoScroll * Time.deltaTime * 60;
		paralaxFollowCamera.x = mainCamera.transform.position.x * speed + scroll + offSet;
		transform.position = paralaxFollowCamera;
	}
}
