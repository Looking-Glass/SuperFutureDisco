using UnityEngine;
using System.Collections;

public class TextFeedback : MonoBehaviour {

	float spawnTime;
	Vector3 origScale;
	public bool goUp;

	// Use this for initialization
	void Start () {
		//goUp = true;
		spawnTime = Time.time;
		origScale = transform.localScale;
	}

	public AnimationCurve sizeCurve;
	// Update is called once per frame
	void Update () {
		if(goUp){
			transform.ShiftY(1*Time.deltaTime);
		} else {
			transform.ShiftY(-1*Time.deltaTime);
		}
		//		Debug.Log(Time.time - spawnTime);
		if(Time.time - spawnTime > 1f){
			Destroy(this.gameObject);
		}
		transform.localScale = origScale * sizeCurve.Evaluate(Time.time - spawnTime);
	}
}
