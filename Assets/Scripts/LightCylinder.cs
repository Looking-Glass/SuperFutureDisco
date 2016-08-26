using UnityEngine;
using System.Collections;

public class LightCylinder : Obstacle {

	// Use this for initialization
	void Start () {
		isHittable = false;
		base.Start();
	
	}
	
	// Update is called once per frame
	void Update () {
		base.Update();
	}

	protected override void doMovement(){
		//transform.SetX();
		transform.ShiftZ(-.75f * speed * Time.deltaTime);
		transform.RotateY(40 * speed * Time.deltaTime);
		//		Debug.Log((transform.position.z - startPos.z) + " " + Time.time);
		if(transform.position.z < -1 * (GameManager.instance.cubeCamera.localScale.z/2 + 5)){
			Destroy(this.gameObject);
		}
	}
}
