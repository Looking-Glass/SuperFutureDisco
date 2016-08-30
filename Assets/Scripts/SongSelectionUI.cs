using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using SonicBloom.Koreo;

public class SongSelectionUI : MonoBehaviour {

	public SongData currentSong;
	public Text titleUI;
	public Text authorUI;
	public Image imageUI;
	public Text percentUI;
	public Text scoreUI;
	public AudioSource myAudio;

	public int positionInSongList;

	public float moveTime;
	public AnimationCurve xCurve;
	public AnimationCurve yCurve;
	public AnimationCurve zCurve;


	public int currentCarouselPosition;
	Vector3 moveStartPosition;
	Vector3 targetPosition;
	Vector3 moveStartScale;
	Vector3 moveTargetScale;

	bool isMoving;
	float timeCount=0;

	public bool debugMe;




	// Use this for initialization
	void Awake () {
		titleUI = transform.GetChild(0).GetComponent<Text>();
		authorUI = transform.GetChild(1).GetComponent<Text>();
		imageUI = transform.GetChild(2).GetComponent<Image>();
		scoreUI = transform.GetChild(3).GetComponent<Text>();
		percentUI = transform.GetChild(4).GetComponent<Text>();
		myAudio = GetComponent<AudioSource>();
	}

	// Update is called once per frame
	void Update () {
		if(isMoving){
			if(debugMe){
				
				Debug.Log(transform.localPosition);
				//Debug.Log(transform.GetComponent<RectTransform>().localScale);
			}
			timeCount+=Time.deltaTime;
			transform.localPosition = new Vector3(
				moveStartPosition.x + (xCurve.Evaluate(timeCount/moveTime) * (targetPosition.x - moveStartPosition.x)),
				moveStartPosition.y + (yCurve.Evaluate(timeCount/moveTime) * (targetPosition.y - moveStartPosition.y)),
				moveStartPosition.z + (zCurve.Evaluate(timeCount/moveTime) * (targetPosition.z - moveStartPosition.z))
			);

//			transform.parent.localScale = new Vector3(
//				moveStartScale.x + (xCurve.Evaluate(timeCount/moveTime) * (moveTargetScale.x - moveStartScale.x)),
//				moveStartScale.y + (yCurve.Evaluate(timeCount/moveTime) * (moveTargetScale.y - moveStartScale.y)),
//				moveStartScale.z
//			);


			if(Mathf.Abs(targetPosition.x - transform.localPosition.x) < .05f){
				if(debugMe){Debug.Log(name+"STOP"+transform.localPosition);}
				stopMoving();
			}
		}


	}

	public void startMoving(Vector3 start, Vector3 target, Vector3 startScale, Vector3 targetScale){
		
		if(debugMe){
			Debug.Log("I am "+name+ " my pos is "+currentCarouselPosition+" move from "+start+" to "+target);
		}
		moveStartPosition = start;
		targetPosition = target;
		isMoving = true;
	}

	void stopMoving(){
		isMoving = false;
		timeCount = 0;
	}

	public void setSong(SongData newData){
		currentSong = newData;
		titleUI.text = currentSong.songName;
		authorUI.text = currentSong.songAuthor;
		imageUI.sprite = currentSong.image;
		percentUI.text = currentSong.highHitPercent+"%";
		scoreUI.text = currentSong.highScore.ToString();
		myAudio.clip = currentSong.audioClip;
	}
}

[System.Serializable]
public struct SongData{

	public Koreography koreography;
	public AudioClip audioClip;
	public MovieTexture movie;
	public Sprite image;
	public string songName;
	public string songAuthor;
	public int highScore;
	public int highHitPercent;
	public float previewStartSeconds;
}