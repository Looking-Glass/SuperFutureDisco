using UnityEngine;
using System.Collections;
using SonicBloom.Koreo;

public class Obstacle : MonoBehaviour {

	private delegate void stateUpdate();
	private stateUpdate StateUpdate;
	public ObstacleState currentState {get; private set;}

	private float currentScaler = 1;
	private float baseScaleRate = .95f;
	private int lastBump;

	public float speed;
	public Vector3 startPos;
	public int currentColor {get; private set;}

	public AnimationCurve xyScaleCurve;

	public bool isHittable;


	// Use this for initialization
	protected void Start () {
		startPos = transform.position;

		setState(ObstacleState.Moving);

	}
	
	// Update is called once per frame
	protected void Update () {
		if(StateUpdate != null){
			StateUpdate();
		}

//		
	}

	void OnDestroy(){
		
	}

	public void setState(ObstacleState newState){

		switch (newState) {
		case ObstacleState.Stopped:
			StateUpdate = stoppedUpdate;
			break;
		case ObstacleState.Moving:
			StateUpdate = doMovement;
			break;
		default:
			throw new System.ArgumentOutOfRangeException ();
		}

		currentState = newState;


	}

	float getScale(){
		return -1 * xyScaleCurve.Evaluate(transform.position.z);
	}

	protected virtual void doMovement(){
		
		transform.ShiftZ(-1 * speed * Time.deltaTime);
//		Debug.Log((transform.position.z - startPos.z) + " " + Time.time);
		if(transform.position.z < -1 * GameManager.instance.cubeCamera.localScale.z /2){
			EventManager.instance.Raise(new ObstacleMissedEvent(this));
		}
		transform.localScale = new Vector3(getScale(),getScale(), transform.localScale.z);

		//transform.localScale *= currentScaler;

	}

	protected virtual void stoppedUpdate(){

	}

	public void setColor(int c){
		GetComponent<Renderer>().material = GameManager.instance.colors[c];
		currentColor =c;
	}

	public void setColor2(int val){
		Debug.Log("change" + Time.time);
		if(Time.time > GameManager.instance.universalOffset){
			//int val = Mathf.RoundToInt(ke.GetFloatValue());
			if(currentScaler ==1){
				currentScaler = baseScaleRate;
			} else if(currentScaler <1){
				currentScaler = 1.05f;
			} else {
				currentScaler = baseScaleRate;
			}
		}

	}

}


public enum ObstacleState { Stopped, Moving }
