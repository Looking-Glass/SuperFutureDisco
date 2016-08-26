using UnityEngine;
using System.Collections;
using Reaktion;
public class ReaktBit : MonoBehaviour {

	// Use this for initialization
	public float scale = 10f;
	public float lifeTime=10f;
	void Start () {
	
		GetComponent<TransformGear>().scale.max=Mathf.Abs(transform.position.x)*scale;
		Invoke("Kill",lifeTime);
	}

	void Kill(){
		Destroy(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
