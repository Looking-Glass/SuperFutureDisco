using UnityEngine;
using System.Collections;

public class VideoTexture : MonoBehaviour {

	// Use this for initialization
	void Start () {
		//Invoke("go",2*GameManager.instance.universalOffset);
		EventManager.instance.AddListener<StartSongEvent>(go);
	}

	void OnDestroy(){
		EventManager.instance.AddListener<StartSongEvent>(go);
	}

	void go(StartSongEvent se){
		((MovieTexture)GetComponent<Renderer>().material.mainTexture).Stop();
		((MovieTexture)GetComponent<Renderer>().material.mainTexture).Play();
	}


}
