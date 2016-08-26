using UnityEngine;
using System.Collections;
using SonicBloom.Koreo;

public class MusicReceiver : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Koreographer.Instance.RegisterForEvents("Melody", FireMelody);
		Koreographer.Instance.RegisterForEvents("Beat", FireBeat);
		//Koreographer.Instance.RegisterForEvents("Bump", test);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FireMelody(KoreographyEvent koreoEvent){
		
//		Debug.Log("BUMP "+Time.time);
		if(koreoEvent.GetFloatValue() != 0){
			
			GameManager.instance.om.AddObstacle(Mathf.RoundToInt(koreoEvent.GetFloatValue()));
		} else {

			GameManager.instance.om.AddObstacle(koreoEvent.GetIntValue());
		}
	//	GameManager.instance.om.test();
	}

	void FireBeat(KoreographyEvent koreoEvent){
		EventManager.instance.Raise(new BeatEvent());

	}


}
