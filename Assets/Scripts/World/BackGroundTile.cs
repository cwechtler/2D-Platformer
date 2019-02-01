
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class BackGroundTile : MonoBehaviour {

	private const int LookAheadOffset = 2;
	public bool hasRightCopy = false;
	public bool hasLeftCopy = false;

	private float camWidthX;
	private float spriteWidthX;

	Transform copiedTo;
	Transform copiedFrom;

	void Start () {
		spriteWidthX = GetComponent<SpriteRenderer>().bounds.size.x;
		camWidthX = Camera.main.orthographicSize * Camera.main.aspect;
	}
	
	void Update () {
		float spriteRightEdge = transform.position.x + spriteWidthX / 2;
		float spriteLeftEdge = transform.position.x - spriteWidthX / 2;

		float camRightEdge = Camera.main.transform.position.x + camWidthX;
		float camLeftEdge = Camera.main.transform.position.x - camWidthX;

		if (camRightEdge + LookAheadOffset > spriteRightEdge) {
			if (!hasRightCopy) {
				MakeCopy(CopyTo.right);
			}
		}
		if (camLeftEdge - LookAheadOffset < spriteLeftEdge){
			if (!hasLeftCopy){
				MakeCopy(CopyTo.left);             
			}
		}
		DestroyIfInvisible(camRightEdge, camLeftEdge, spriteRightEdge, spriteLeftEdge);

	}

	private void MakeCopy(CopyTo side) {
		Vector3 copyPosition = new Vector3(transform.position.x + spriteWidthX * (int)side, transform.position.y, transform.position.z);
		copiedTo = (Transform)Instantiate(transform, copyPosition, transform.rotation);
		copiedTo.GetComponent<BackGroundTile>().copiedFrom = this.transform;
		copiedTo.parent = this.transform.parent;

		if (side == CopyTo.right){
			this.hasRightCopy = copiedTo.GetComponent<BackGroundTile>().hasLeftCopy = true;
		}
		else if (side == CopyTo.left) {
			this.hasLeftCopy = copiedTo.GetComponent<BackGroundTile>().hasRightCopy = true;
		}
	}

	private void DestroyIfInvisible(float camRightEdge, float camLeftEdge, float spriteRightEdge, float sprightLeftEdge){
		bool isSprightInvisibleToRightOfCamera = (sprightLeftEdge - camRightEdge > spriteWidthX);
		bool isSprightInvisibleToLeftOfCamera = (camLeftEdge - spriteRightEdge > spriteWidthX);

		if (isSprightInvisibleToRightOfCamera){
			if (copiedFrom != null){
				copiedFrom.GetComponent<BackGroundTile>().hasRightCopy = false;
			}
			if (copiedTo != null){
				copiedTo.GetComponent<BackGroundTile>().hasRightCopy = false;
			}
			GameObject.Destroy(gameObject);
		}
		else if (isSprightInvisibleToLeftOfCamera) {
			if (copiedFrom != null){
				copiedFrom.GetComponent<BackGroundTile>().hasLeftCopy = false;
			}
			if (copiedTo != null){
				copiedTo.GetComponent<BackGroundTile>().hasLeftCopy = false;
			}
			GameObject.Destroy(gameObject);
		}
	}

	private enum CopyTo {
		right = 1,
		left = -1
	}
}
