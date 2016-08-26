using UnityEngine;
using System.Collections;

public class HitParticle : MonoBehaviour {
	public float dieTime;

	public AnimationCurve sizeCurve;
	float startTime;
	// Use this for initialization
	void Start () {
		startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.time - startTime > dieTime){
			Destroy(this.gameObject);
		}
		float size =.6f*sizeCurve.Evaluate((Time.time-startTime)/dieTime);
		transform.localScale = new Vector3(size,size,transform.localScale.z);
//		Debug.Log(name+" "+(Time.time-startTime/dieTime));
	}
}
