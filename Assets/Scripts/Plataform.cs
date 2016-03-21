using UnityEngine;
using System.Collections;

public class Plataform : MonoBehaviour {

	public Transform leftWall;
	public Transform left;
	public Transform rigth;
	public Transform rigthWall;

	public Coin yellowCoin;
	public Coin blueCoin;
	public Coin purpleCoin;

	public Piston piston;

	int minLength = 0;
	int maxLength = 5;

	int pos;

	public GameObject[] blocks;

	void Awake() {
		minLength = 1;
		maxLength = blocks.Length - 1;
	}

	void Start() {
		yellowCoin.Hide(false);
		blueCoin.Hide(false);
		purpleCoin.Hide(false);
	}

	public void NewSize() {
		pos = Random.Range(minLength, maxLength);		
		Vector3 temp = piston.transform.position;
		piston.transform.position = blocks[pos].transform.position;
		blocks[pos].transform.position = temp;

		float coinChance = Random.value;
		Coin coin = null;
		if (coinChance < 0.1f) {
			coin = purpleCoin;
			yellowCoin.Hide(false);
			blueCoin.Hide(false);
		} else if (coinChance < 0.4f) {
			coin = blueCoin;
			yellowCoin.Hide(false);
			purpleCoin.Hide(false);
		} else if (coinChance < 0.9f) {
			coin = yellowCoin;
			purpleCoin.Hide(false);
			blueCoin.Hide(false);
		}
		if (coin != null) {
			Vector3 coinPos = coin.transform.position;
			coinPos.x = Random.Range(-4, 4);
			coin.transform.position = coinPos;
			coin.Show();
		}
	}

	public void Fire() {
		piston.Fire();
	}

	public void PositionAt(Vector3 posTarget, bool left) {
		Vector3 pos = transform.position;
		if (left) {
			pos.x = posTarget.x - 4f;
		} else {
			pos.x = posTarget.x + 4f;
		}
		transform.position = pos;
	}
}
