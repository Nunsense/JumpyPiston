using UnityEngine;
using System.Collections;

public class WorldManager : MonoBehaviour {

	public GameObject plataformPrefab;
	public FollowBall cam;
	public float moveUpDistace = 25f;
	public Ball ball;
	public TextAnimation plataformCountText;
	public TextAnimation cointCountText;
	public TimeBar time;

	public GameObject inGameMenu;
	public GameObject gameOverMenu;

	public float initialTime = 5;
	public float maxTime = 5;
	public int timeComboLimit = 3;
	float timeComboCount;

	public float timeMultiplyerAddPerJump = 0.01f;
	float timeMultiplyer = 1f;

	int coinCount;

	bool addTime;

	Plataform currentPlataform;
	Plataform nextPlataform;

	bool canJump = false;

	int plataformCount;

	float timeCount;

	public bool gameInPause;

	void Start() {
		NewGame();
	}

	public void NewGame() {
		gameInPause = false;
		addTime = false;
		coinCount = 0;
		timeCount = initialTime;
		timeComboCount = 0;
		plataformCount = 0;
		canJump = true;

		GameObject newPlat = GameObject.Instantiate(plataformPrefab);
		newPlat.transform.parent = transform;
		newPlat.transform.position = Vector3.zero;

		currentPlataform = newPlat.GetComponent<Plataform>();
		currentPlataform.NewSize();

		newPlat = GameObject.Instantiate(plataformPrefab);
		newPlat.transform.parent = transform;
		newPlat.transform.position = new Vector3(0, moveUpDistace, 0);

		nextPlataform = newPlat.GetComponent<Plataform>();
		nextPlataform.NewSize();

		cam.MoveTo(currentPlataform.transform.position);

		inGameMenu.SetActive(true);
		gameOverMenu.SetActive(false);
	}

	void Update() {
		if (gameInPause)
			return;

		if (canJump && Input.GetAxis("Vertical") > 0) {
			currentPlataform.Fire();
		}

		if (addTime) {
			ChangeTime(Time.deltaTime * timeMultiplyer);
		} else {
			if (timeComboCount < timeComboLimit) {
				ChangeTime(-Time.deltaTime);

				if (timeCount <= 0) {
					inGameMenu.SetActive(false);
					gameOverMenu.SetActive(true);
					gameInPause = true;
				}
			}

			if (timeComboCount <= 0) {
				time.SetCombo(false);
			}
		}
	}

	void NextPlataform() {
		Plataform temp = currentPlataform;
		currentPlataform = nextPlataform;
		nextPlataform = temp;

		nextPlataform.transform.position = new Vector3(0, currentPlataform.transform.position.y + moveUpDistace, 0);
	}

	public void BallJumped() {
		bool goLeft = Random.value <= 0.5f;
		ball.ChangeDirection(goLeft);
		nextPlataform.NewSize();
		nextPlataform.PositionAt(ball.transform.position, goLeft);

		canJump = false;
		cam.MoveTo(nextPlataform.transform.position);

		plataformCount += 1;
		plataformCountText.SetValue(plataformCount);

		timeComboCount += 1;
		if (timeComboCount >= timeComboLimit) {
			ChangeTime(maxTime);
			time.SetCombo(true);
		}
		addTime = true;

		timeMultiplyer += timeMultiplyerAddPerJump;
	}

	public void BallArrivedNextPlataform() {
		NextPlataform();
		canJump = true;
		addTime = false;
	}

	void ChangeTime(float val) {
		timeCount = Mathf.Clamp(timeCount + val, 0, maxTime);
		time.SetTime(timeCount);
	}

	public void ResetTimeBonus() {
		timeComboCount = 0;
	}

	public void AddCoin(int value) {
		coinCount += value;
		cointCountText.SetValue(coinCount);		
	}

	public void ResumeGame() {
		ChangeTime(maxTime);
		inGameMenu.SetActive(true);
		gameOverMenu.SetActive(false);
		gameInPause = false;
	}
}
