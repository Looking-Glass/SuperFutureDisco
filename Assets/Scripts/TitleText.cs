using UnityEngine;
using System.Collections;

public class TitleText : MonoBehaviour {

	float startTime;
	float duration;

	bool running;


	public AnimationCurve positionXCurve;
	public AnimationCurve positionYCurve;
	public AnimationCurve rotationXCurve;
	public AnimationCurve rotationZCurve;
	public AnimationCurve rotationYCurve;

	public bool animatePostion;
	public bool animateRotationX;
	public bool animateRotationY;
	public bool animateRotationZ;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(running){
			runAnimation();
		}
	}
			
	public void startAnimation(float s, float dur){
		startTime = s;
		duration = dur;
		running =true;
	}

	void runAnimation(){
		if(Time.time - startTime <=duration){
			if(animatePostion){
				transform.SetX(positionXCurve.Evaluate((Time.time - startTime)/duration));
				transform.SetY(positionYCurve.Evaluate((Time.time - startTime)/duration));
			}
			if(animateRotationX){
				transform.eulerAngles = new Vector3(rotationXCurve.Evaluate((Time.time - startTime)/duration),transform.eulerAngles.y,transform.eulerAngles.z);
			}
			if(animateRotationY){
				transform.eulerAngles = new Vector3(transform.eulerAngles.x,rotationYCurve.Evaluate((Time.time - startTime)/duration),transform.eulerAngles.z);
			}
			if(animateRotationZ){
				transform.eulerAngles = new Vector3(transform.eulerAngles.x,transform.eulerAngles.y,rotationZCurve.Evaluate((Time.time - startTime)/duration));

			}
		} else {
			running=false;
		}
	}
}
