using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour {

	public int value;
	public GameObject coin;
	Animator anim;
	bool dead;

	void Awake() {
		dead = true;
		anim = GetComponent<Animator>();
	}

	public void Show() {
		dead = false;
		gameObject.SetActive(true);
	}

	public void Hide(bool animate) {
		dead = true;
		if (animate) {
			anim.SetTrigger("hide");
		} else {
			gameObject.SetActive(false);
		}
	}

	public bool isAlive() {
		return !dead;
	}

	void Disable() {
		gameObject.SetActive(false);
	}
}
