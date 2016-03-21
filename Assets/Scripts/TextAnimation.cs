using UnityEngine;
using UnityEngine.UI;

public class TextAnimation : MonoBehaviour {

	Text text;
	Vector3 target = Vector3.one * 1.1f;

	bool backToNormal;
	float movingTime;
	public float moveTime = 0.2f;

	void Awake() {
		text = GetComponent<Text>();
		backToNormal = false;
	}

	void Start() {
	}

	void Update() {
		if (backToNormal) {
			movingTime += Time.deltaTime;

			transform.localScale = Vector3.Lerp(target, Vector3.one, movingTime / moveTime);

			if (movingTime >= moveTime) {
				transform.localScale = Vector3.one;
				backToNormal = false;
			}
		}
	}

	public void SetValue(int val) {
		transform.localScale = target;
		backToNormal = true;
		movingTime = 0;
		text.text = val.ToString();
	}
}
