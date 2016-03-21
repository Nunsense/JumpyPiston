using UnityEngine;
using System.Collections;

public class FollowBall : MonoBehaviour {

	public Transform ball;
	public float speed = 8f;
	Vector3 pos;
	float yOffset;
	Camera cam;

	bool changing = false;
	Vector3 originPos;
	Vector3 targetPos;
	float movingTime;
	public float moveTime = 2f;

	Vector3 tempPos;

	void Awake() {
		cam = GetComponent<Camera>();
		changing = false;
	}

	void Start() {
		yOffset = transform.position.y - ball.position.y;
	}

	void Update() {
		if (changing) {
			movingTime += Time.deltaTime;

			tempPos = transform.position;
			targetPos.y = ball.position.y + yOffset;
			tempPos = Vector3.Lerp(originPos, targetPos, movingTime / moveTime);

			if (movingTime >= moveTime) {
				changing = false;
				tempPos = targetPos;
			}
			transform.position = tempPos;
		}
	}

	public void MoveTo(Vector3 center) {
		originPos = transform.position;
		targetPos = center;
		targetPos.z = -16;
		movingTime = 0;
		changing = true;
	}
}
