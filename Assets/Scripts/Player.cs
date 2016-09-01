using UnityEngine;
using System.Collections;
using InControl;
using SonicBloom.Koreo;

public class Player : MonoBehaviour {

	private delegate void stateUpdate();
	private stateUpdate StateUpdate;

	public PlayerStates currentState {get; private set;}

	public int speed;

	float maxYrot = 25;
	float maxXrot = 25;
	float maxZrot = 25;

	float rotSpeed = 240;


	public int currentColor {get; private set;}
	public Gradient trailGrad;
	public Material trailMat;
	float trailcount;
	ParticleSystem myTrail;

	public Renderer triangleL;
	public Renderer triangleR;

	float stickThreshold = .2f;

	bool isBouncing;

	ParticleSystem colorChangeEffect;

	void Awake(){
		colorChangeEffect = transform.GetChild(3).gameObject.GetComponent<ParticleSystem>();
		myTrail = transform.GetChild(0).GetComponent<ParticleSystem>();
		myTrail.gameObject.SetActive(false);
		EventManager.instance.AddListener<BeatEvent>(bounceToBeat);
	}

	void OnDestroy(){
		EventManager.instance.RemoveListener<BeatEvent>(bounceToBeat);
	}

	// Use this for initialization
	void Start () {
		setState(PlayerStates.CanMove);

	}
	
	// Update is called once per frame
	void Update () {
		if(StateUpdate != null){
			StateUpdate();
		}
//		trailMat.color = trailGrad.Evaluate(trailcount);
//		trailcount += Time.deltaTime/2;
//		if(trailcount >=1){
//			trailcount = 0;
//		}
		myTrail.startRotation3D = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z)*Mathf.Deg2Rad;
//		Debug.Log("1: "+transform.eulerAngles);
	}

	public void setState(PlayerStates newState){


		switch (newState) {

			case PlayerStates.CanMove:
				StateUpdate = checkForOB;
				StateUpdate += doMovement;
				StateUpdate += colorControls;
			break;
			default:
				throw new System.ArgumentOutOfRangeException ();
		}

		currentState = newState;
	}

	void checkForOB(){
		if (transform.position.x > GameManager.instance.cubeCamera.localScale.x / 2) {
			transform.SetX(GameManager.instance.cubeCamera.localScale.x / 2);
		} else if (transform.position.x < GameManager.instance.cubeCamera.localScale.x / -2) {
			transform.SetX(GameManager.instance.cubeCamera.localScale.x / -2);
		}

		if (transform.position.y > GameManager.instance.cubeCamera.localScale.y / 2) {
			transform.SetY(GameManager.instance.cubeCamera.localScale.y / 2);
		} else if (transform.position.y < GameManager.instance.cubeCamera.localScale.y / -2) {
			transform.SetY((GameManager.instance.cubeCamera.localScale.y)/ -2);
		}
	}

	void doMovement(){
//		if(Input.GetKey(KeyCode.LeftArrow)){
//			transform.ShiftX(-1 * speed * Time.deltaTime);
//			aimLeft();
//		} else if (Input.GetKey(KeyCode.RightArrow)){
//			transform.ShiftX(speed * Time.deltaTime);
//			aimRight();
//		} else {
//			aimHorizCenter();
//		}
//
//		if(Input.GetKey(KeyCode.UpArrow)){
//			transform.ShiftY(speed * Time.deltaTime);
//			aimUp();
//		} else if (Input.GetKey(KeyCode.DownArrow)){
//			transform.ShiftY(-1 * speed * Time.deltaTime);
//			aimDown();
//		} else {
//			aimVertCenter();
//		}
//

		// Use last device which provided input.


		// Rotate target object with left stick.
		if(GameManager.instance.inputDevice.LeftStickX.Value < -1 * stickThreshold || Input.GetKey(KeyCode.LeftArrow) || GameManager.instance.inputDevice.DPadLeft.WasPressed){
			if(GameManager.instance.inputDevice.LeftStickX.Value==0){
				transform.ShiftX(-1 * speed * Time.deltaTime);
			} else {
				transform.ShiftX(GameManager.instance.inputDevice.LeftStickX.Value * speed * Time.deltaTime);
			}
			aimLeft();
		} else if(GameManager.instance.inputDevice.LeftStickX.Value > stickThreshold || Input.GetKey(KeyCode.RightArrow) || GameManager.instance.inputDevice.DPadRight.WasPressed){
			if(GameManager.instance.inputDevice.LeftStickX.Value==0){
				transform.ShiftX(1 * speed * Time.deltaTime);
			} else {
				transform.ShiftX(GameManager.instance.inputDevice.LeftStickX.Value * speed * Time.deltaTime);
			}
			aimRight();
		} else {
			aimHorizCenter();
		}

		if(GameManager.instance.inputDevice.LeftStickY.Value > stickThreshold || Input.GetKey(KeyCode.UpArrow) || GameManager.instance.inputDevice.DPadUp.WasPressed){
			if(GameManager.instance.inputDevice.LeftStickY.Value==0){
				transform.ShiftY(1 * speed * Time.deltaTime);
			} else {
				transform.ShiftY(GameManager.instance.inputDevice.LeftStickY.Value * speed * Time.deltaTime);
			}
			aimUp();
		} else if (GameManager.instance.inputDevice.LeftStickY.Value < -1 * stickThreshold || Input.GetKey(KeyCode.DownArrow)  || GameManager.instance.inputDevice.DPadDown.WasPressed){
			if(GameManager.instance.inputDevice.LeftStickY.Value==0){
				transform.ShiftY(-1 * speed * Time.deltaTime);
			} else {
				transform.ShiftY(GameManager.instance.inputDevice.LeftStickY.Value * speed * Time.deltaTime);
			}
			aimDown();
		} else {
			if(!isBouncing){
				aimVertCenter();
			}
		}

		// Get two colors based on two action buttons.
//		var color1 = GameManager.instance.inputDevice.Action1.IsPressed ? Color.red : Color.white;
//		var color2 = GameManager.instance.inputDevice.Action2.IsPressed ? Color.green : Color.white;
//
//		// Blend the two colors together to color the object.
//		GetComponent<Renderer>().material.color = Color.Lerp( color1, color2, 0.5f );






	}

	void bounceToBeat(BeatEvent be){
//		Debug.Log("BOUNCE GET");
		//if(GameManager.instance.inputDevice.LeftStickX > -1 * stickThreshold)
		StartCoroutine("bounce");
	}

	IEnumerator bounce(){
		yield return new WaitForSeconds(GameManager.instance.sphereTimeToShipFromBackOfScreen);
		isBouncing = true;
		int frames = 3;
		for(int i=0; i<frames;i++){
			aimUp();
			yield return new WaitForSeconds(Time.deltaTime);
		}
		isBouncing = false;
		yield return null;

	}

	void aimLeft(){
//		Debug.Log("aimplz");
//		Debug.Log(transform.eulerAngles.y);
		if(transform.eulerAngles.y > 180 - maxYrot){
			transform.RotateY(-1 * rotSpeed * Time.deltaTime);
	//		Debug.Log("aturnin");
		}

//		Debug.Log(transform.eulerAngles.z);
		if(transform.eulerAngles.z > 180 - maxZrot){
			transform.RotateZ(-1 * rotSpeed * Time.deltaTime);
		}
	}

	void aimRight(){
		if(transform.eulerAngles.y < 180 + maxYrot){
			transform.RotateY(rotSpeed * Time.deltaTime);
			//		Debug.Log("aturnin");
		}

//		Debug.Log(transform.eulerAngles.z);
		if(transform.eulerAngles.z < 180 + maxZrot){
			transform.RotateZ(rotSpeed * Time.deltaTime);
		}
	}

	void aimUp(){
		if(transform.eulerAngles.x < 180){
			if(transform.eulerAngles.x < maxXrot){
				transform.RotateX(rotSpeed * Time.deltaTime);
			}
		} else {
			if(transform.eulerAngles.x < 360+ maxXrot){
				transform.RotateX(rotSpeed * Time.deltaTime);
			}
		}
		
	}

	void aimDown(){
		if(transform.eulerAngles.x < 180){
			if(transform.eulerAngles.x > 0 - maxXrot){
				transform.RotateX(-1 * rotSpeed * Time.deltaTime);
			}
		} else {
			if(transform.eulerAngles.x > 360 - maxXrot){
				transform.RotateX(-1 * rotSpeed * Time.deltaTime);
			}
		}

	}

	void aimHorizCenter(){
		if(transform.eulerAngles.y < 178){
				transform.RotateY(rotSpeed/3 * Time.deltaTime);
		} else if(transform.eulerAngles.y > 182){
			transform.RotateY(-1 * rotSpeed/3 * Time.deltaTime);
		}

		if(transform.eulerAngles.z < 178){
			transform.RotateZ(rotSpeed/3 * Time.deltaTime);
		} else if (transform.eulerAngles.z > 182){
			transform.RotateZ(-1 * rotSpeed/3 * Time.deltaTime);
		}
	}

	void aimVertCenter(){
		if(transform.eulerAngles.x < 180 && transform.eulerAngles.x >1){
			transform.RotateX(-1 * rotSpeed * Time.deltaTime);
		} else if (transform.eulerAngles.x >270 && transform.eulerAngles.x < 359){
			transform.RotateX(rotSpeed * Time.deltaTime);
		}
	}

	void setColor(int c){
		GetComponent<AudioSource>().Play();
		GetComponent<Renderer>().material = GameManager.instance.colors[c];

		colorChangeEffect.gameObject.GetComponent<Renderer>().material = GameManager.instance.colors[c];
		colorChangeEffect.startRotation3D = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z)*Mathf.Deg2Rad;

		currentColor = c;
		if(c==0){
			triangleL.material = GameManager.instance.colors[GameManager.instance.colors.Count-1];
			triangleR.material = GameManager.instance.colors[c+1];
		} else if(c==GameManager.instance.colors.Count-1){
			triangleL.material = GameManager.instance.colors[c-1];
			triangleR.material = GameManager.instance.colors[0];
		} else {
			triangleL.material = GameManager.instance.colors[c-1];
			triangleR.material = GameManager.instance.colors[c+1];
		}

		colorChangeEffect.Emit(1);

	}

	void colorControls(){
		if(Input.GetKeyDown(KeyCode.Q) || GameManager.instance.inputDevice.Action2.WasPressed){
			setColor(0);
		}

		if(Input.GetKeyDown(KeyCode.W) || GameManager.instance.inputDevice.Action4.WasPressed){
			setColor(1);
		}

		if(Input.GetKeyDown(KeyCode.E) || GameManager.instance.inputDevice.Action3.WasPressed){
			setColor(2);
		}

		if(Input.GetKeyDown(KeyCode.A) || GameManager.instance.inputDevice.LeftBumper.WasPressed || GameManager.instance.inputDevice.LeftTrigger.WasPressed){
			Debug.Log("bumper?");
			if(currentColor==0){
				setColor(GameManager.instance.colors.Count-1);
			} else {
				setColor(currentColor - 1);
			}
		} 

		if(Input.GetKeyDown(KeyCode.D) || GameManager.instance.inputDevice.RightBumper.WasPressed || GameManager.instance.inputDevice.RightTrigger.WasPressed){
			if(currentColor==GameManager.instance.colors.Count-1){
				setColor(0);
			} else {
				setColor(currentColor + 1);
			}
		}

		if(Input.GetKeyDown(KeyCode.Space) || GameManager.instance.inputDevice.Action1.WasPressed){
			EventManager.instance.Raise(new TryStartPartyEvent());
		}


			
	}

	void OnTriggerEnter(Collider other){
		if(other.GetComponent<Obstacle>().isHittable){
			EventManager.instance.Raise(
				new PlayerHitObstacleEvent(
					currentColor, 
					other.transform.GetComponent<Obstacle>().currentColor,
					other.transform.position.x,
					other.transform.position.y,
					other.GetComponent<Obstacle>()
				));
		}
	}
}


		


public enum PlayerStates {CanMove}
