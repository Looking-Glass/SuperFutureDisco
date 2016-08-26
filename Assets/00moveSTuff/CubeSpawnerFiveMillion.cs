using UnityEngine;
using System.Collections;
using Reaktion;

public class CubeSpawnerFiveMillion : MonoBehaviour {

	// Use this for initialization
	public GameObject cube;
	public Vector3 bounds;
	public Vector3 scaleBounds;
	public int numCubes;
	public float spawnTime=2;
	void Start () {
		StartCoroutine("Spawn");
	
	}

	IEnumerator Spawn(){
		while(true){
			Vector3 pos= new Vector3(Random.Range(-bounds.x,bounds.x),0f,Random.Range(-bounds.z,bounds.z));
			GameObject _cube=Instantiate(cube,pos+transform.position,Quaternion.identity) as GameObject;
			//_cube.GetComponent<TransformGear>().scale.arbitraryVector= new Vector3(Random.Range(scaleBounds.x/2f,scaleBounds.x),Random.Range(scaleBounds.y/2f,scaleBounds.y),Random.Range(scaleBounds.z/2f,scaleBounds.z));
			//_cube.transform.parent=transform.parent;
			yield return new WaitForSeconds(spawnTime);

		}

	}
	// Update is called once per frame
	void Update () {
	
	}
}
