using UnityEngine;
using UnityEngine.UI;

public class TimeBar : MonoBehaviour {
	Image image;
	float max = 700f;
	Vector3 origin;

	bool isInCombo;

	public float maxTime = 5f;

	void Awake() {
		isInCombo = false;
		image = GetComponent<Image>();
	}

	void Start() {
		origin = transform.localPosition;
	}

	void Update() {
		if (isInCombo) {
			Shake(10f);
		}
	}

	void Shake(float amount) {
		transform.localPosition = origin - Random.insideUnitSphere * amount;
	}

	public void SetCombo(bool combo) {
		isInCombo = combo;
		if (isInCombo) {
			image.color = Color.yellow;
		} else {
			image.color = Color.red;
			transform.localPosition = origin;
		}
	}

	public void SetTime(float time) {
		Vector2 rect = image.rectTransform.sizeDelta;
		rect.x = (time * max) / maxTime;
		image.rectTransform.sizeDelta = rect;
	}
}
