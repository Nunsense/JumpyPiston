using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

	public float speed = 10f;
	public float currentSpeed;

	public float moveUpDistace = 25f;
	public WorldManager world;

	Transform trans;
	Vector3 pos;
	bool movingUp;
	Rigidbody body;

	Vector3 originPos;
	Vector3 targetPos;
	float movingTime;
	public float moveTime = 2f;

	Animator anim;

	void Awake() {
		anim = GetComponent<Animator>();
		body = GetComponent<Rigidbody>();
		trans = transform;
		movingUp = false;
		currentSpeed = speed;
	}

	void Update() {
		if (world.gameInPause)
			return;

		if (movingUp) { 
			movingTime += Time.deltaTime;

			pos = Vector3.Lerp(originPos, targetPos, movingTime / moveTime);

			if (movingTime >= moveTime) {
				movingUp = false;
				pos = targetPos;
				body.isKinematic = false;
				world.BallArrivedNextPlataform();
			}
		} else {
			pos = trans.position;
			pos.x += currentSpeed * Time.deltaTime;
		}
		trans.position = pos;
	}

	void OnCollisionEnter(Collision col) {
		if (col.gameObject.tag == "Wall") {
			currentSpeed *= -1;
			world.ResetTimeBonus();
		}
		if (col.gameObject.tag == "Piston") {
			body.isKinematic = true;
			originPos = trans.position;
			targetPos = trans.position;
			targetPos.y += moveUpDistace;
			originPos.y += 1;
			trans.position = originPos;
			movingUp = true;
			movingTime = 0;
			world.BallJumped();
			anim.SetTrigger("jump");
		}
	}

	void OnTriggerEnter(Collider col) {
		if (col.tag == "Coin") {
			Coin coin = col.gameObject.GetComponent<Coin>();
			if (coin.isAlive()) {
				world.AddCoin(coin.value);
				coin.Hide(true);
			}
		}
	}

	public bool IsMovingLeft() {
		return speed < 0;
	}

	public void ChangeDirection(bool left) {
		currentSpeed = left ? -speed : speed;
	}
}
