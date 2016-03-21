using UnityEngine;
using System.Collections;

public class Piston : MonoBehaviour {

	enum MovingTo { none, up, down };

	public Transform top;
	public Transform walls;

	Vector3 firedTargetPos = new Vector3(0, 1, 0);
	Vector3 restTargetPos = new Vector3(0, 0.45f, 0);
	Vector3 currentTargetPos;
	MovingTo moving;

	float movingTime;
	public float moveTime = 0.5f;

	void Start () {
		moving = MovingTo.none;
		top.localPosition = restTargetPos;
		walls.localPosition = top.localPosition;
	}

	void Update () {
		if (moving == MovingTo.up) {
			movingTime += Time.deltaTime;
			top.localPosition = Vector3.Lerp(restTargetPos, firedTargetPos, movingTime / moveTime);

			if (movingTime >= moveTime) {
				moving = MovingTo.down;
				top.localPosition = firedTargetPos;
			}
			walls.localPosition = top.localPosition;
		} else if (moving == MovingTo.down) {
			movingTime += Time.deltaTime;
			top.localPosition = Vector3.Lerp(firedTargetPos, restTargetPos, movingTime / moveTime);

			if (movingTime >= moveTime) {
				moving = MovingTo.none;
				top.localPosition = restTargetPos;
			}
			walls.localPosition = top.localPosition;
		}
	}

	public void Fire() {
		if (moving == MovingTo.none) {
			moving = MovingTo.up;
			movingTime = 0;
		}
	}
}
