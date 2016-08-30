using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SonicBloom.Koreo;
using InControl;

public class GameManager : MonoBehaviour {

    private static GameManager _instance;
    public static GameManager instance {
        get {
            if (_instance == null) {
                _instance = GameObject.FindObjectOfType<GameManager>();
                //				DontDestroyOnLoad(_instance.gameObject);
            }
            return _instance;
        }
    }

	private delegate void stateUpdate();
	private stateUpdate StateUpdate;



	public Transform cubeCamera;
	public ObstacleManager om {get; private set;}
	public float universalOffset {get; private set;}

	public List<Material> colors;
	private List<Material> originalColors;
	public Material partyMaterial;
	public Material whiteMaterial;

	public GameObject playerTrail;

	public GameObject superTitleText;
	public GameObject superTitleCover;

	private AudioSource mainAudio;
	public AudioSource sfxAudio;

	public Koreographer myKoreor;
	public GameObject Player;


	public GameObject [] titleObjects;
	public GameObject [] menuObjects;
	public GameObject [] playingObjects;
	public GameObject [] resultsObjects;

	public GameObject moviePlane;

	public SongData [] songs;

	public InputDevice inputDevice {get; private set;}

	public float sphereTimeToShipFromBackOfScreen;


	public GameState currentState {get; private set;}
	public void setState(GameState newState){

		sfxAudio.Stop();
		mainAudio.Stop();
		StopCoroutine("playTitleAnimation");
		StopCoroutine("setUpKoreoStats");

		switch (newState) {

		case GameState.Title:
			
			turnStuffOnOff(titleObjects,true);
			turnStuffOnOff(menuObjects,false);
			turnStuffOnOff(playingObjects,false);
			turnStuffOnOff(resultsObjects,false);
			StartCoroutine("playTitleAnimation");
			StateUpdate = titleUpdate;
			break;
		case GameState.Menu:
			turnStuffOnOff(titleObjects,false);
			turnStuffOnOff(menuObjects,true);
			turnStuffOnOff(playingObjects,false);
			turnStuffOnOff(resultsObjects,false);
			StateUpdate = menuUpdate;
			break;
		case GameState.Playing:
			
			turnStuffOnOff(titleObjects,false);
			turnStuffOnOff(menuObjects,false);
			turnStuffOnOff(playingObjects,true);
			turnStuffOnOff(resultsObjects,false);
			StartCoroutine("setUpKoreoStats");
			if(moviePlane!=null){
				moviePlane.GetComponent<Renderer>().enabled = false;
			}
			myKoreor.gameObject.GetComponent<AudioSource>().Play();
			GetComponent<AudioSource>().clip = myKoreor.gameObject.GetComponent<AudioSource>().clip;
			StateUpdate = playingUpdate;

			break;
		case GameState.Result:
			turnStuffOnOff(titleObjects,false);
			turnStuffOnOff(menuObjects,false);
			turnStuffOnOff(playingObjects,false);
			turnStuffOnOff(resultsObjects,true);
			StateUpdate = resultUpdate;
			break;
		default:
			throw new System.ArgumentOutOfRangeException ();
		}
		currentState = newState;
	}

	void Update(){
		inputDevice = InputManager.ActiveDevice;
		if(StateUpdate != null){
			StateUpdate();
		}
	}

	void turnStuffOnOff(GameObject [] stuff, bool onOff){
		if(stuff.Length >0){
			foreach(GameObject g in stuff){
				if(g!=null){
					g.SetActive(onOff);
				}
			}
		}

	}

	void titleUpdate(){
		
		if(Input.GetKeyDown(KeyCode.Space) || inputDevice.Action1.WasPressed || inputDevice.Command.WasPressed){
			GameObject.Find("BottomPart").transform.SetX(-15);
			setState(GameState.Menu);

		}
	}

	void menuUpdate(){
		if(Input.GetKeyDown(KeyCode.Space) || inputDevice.Action1.WasPressed || inputDevice.Command.WasPressed){
			setState(GameState.Playing);
		}
	}

	void playingUpdate(){
		if(Input.GetKeyDown(KeyCode.R) || inputDevice.Command.WasPressed){
			endParty();
			GetComponent<ScoreManager>().reset();
			om.clearAllObstacles();
			setState(GameState.Title);


		}
	}

	void resultUpdate(){
		if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Space)){
			setState(GameState.Title);
		}
	}

	void Awake(){
		//moviePlane = GameObject.Find("MoviePlane");
		originalColors = new List<Material>();
		for(int i=0; i<colors.Count;i++){
			originalColors.Add(colors[i]);
		}

		//universalOffset = 2.5f;
		universalOffset = sphereTimeToShipFromBackOfScreen;
		if(cubeCamera == null){
			cubeCamera = GameObject.Find("HYPERCUBE").transform;
		}
		om = GetComponent<ObstacleManager>();
		//START THE MUSIC HERE!!!
		//Invoke("startMusic",universalOffset);

		mainAudio = GetComponent<AudioSource>();
		setState(GameState.Title);
		//GameObject.Find("Super1").GetComponent<TitleText>().startAnimation(Time.time, 2);
	//	playTitleAnimation();
	}

	IEnumerator playTitleAnimation(){
		superTitleCover.transform.localPosition = new Vector3(superTitleCover.GetComponent<TitleText>().positionXCurve.Evaluate(0), superTitleCover.GetComponent<TitleText>().positionYCurve.Evaluate(0),0.1f);
		superTitleCover.SetActive(false);
		superTitleText.SetActive(false);
		GameObject.Find("Press Space").transform.eulerAngles = new Vector3(90,0,0);
		//Invoke("revealSuper",3.2f);
		//Invoke("showSpace",3.75f);
		mainAudio.clip = Resources.Load("Audio/Music/shortSting") as AudioClip;
//		Debug.Log("weplay1");
		mainAudio.Play();
		foreach(TitleText t in GameObject.Find("BottomPart").GetComponentsInChildren<TitleText>()){
			t.startAnimation(Time.time,2.8f);
		}
		transform.GetChild(0).gameObject.SetActive(false);
		yield return new WaitForSeconds(2.7f);
		revealSuper();
		yield return new WaitForSeconds(.55f);
		showSpace();
		yield return null;

	}
	void revealSuper(){
		superTitleCover.SetActive(true);
		superTitleText.SetActive(true);
		sfxAudio.clip = Resources.Load("Audio/chimes2") as AudioClip;
		sfxAudio.Play();
		GameObject.Find("SuperCover").GetComponent<TitleText>().startAnimation(Time.time,.25f);
	}

	void showSpace(){
		GameObject.Find("Press Space").GetComponent<TitleText>().startAnimation(Time.time,.5f);
	}
	void startMusic(){
//		Debug.Log("weplay2");
		mainAudio.Play();
	}

	IEnumerator setUpKoreoStats(){
		int maxY=0;
		int minY=0;
		bool useFloat = false;
		Koreography myKoreo = myKoreor.GetKoreographyAtIndex(1);

		int totalNotes = myKoreo.Tracks[0].GetAllEvents().Count;

		if(myKoreo.Tracks[1].GetAllEvents()[0].GetFloatValue() !=0){
			useFloat = true;
		}
//		Debug.Log(useFloat);
		foreach(KoreographyEvent ke in myKoreo.Tracks[1].GetAllEvents()){
			if(useFloat){
				if(minY==0){
					maxY = Mathf.RoundToInt(ke.GetFloatValue());
					minY = Mathf.RoundToInt(ke.GetFloatValue());
				}
				if(ke.GetFloatValue() < minY){
					minY = Mathf.RoundToInt(ke.GetFloatValue());
				}

				if(ke.GetFloatValue() > maxY){
					maxY = Mathf.RoundToInt(ke.GetFloatValue());
				}
			} else {
				if(minY==0){
					maxY = ke.GetIntValue();
					minY = ke.GetIntValue();
				}
				if(ke.GetIntValue() < minY){
					minY = ke.GetIntValue();
				}

				if(ke.GetIntValue() > maxY){
					maxY = ke.GetIntValue();
				}
			}
		}
		
		om.maxNote = maxY;
		om.minNote = minY;
//		Debug.Log("OH "+minY + " "+maxY);
		GetComponent<ScoreManager>().totalNotes = totalNotes;
		om.setYRange();
		//mainAudio.clip = Resources.Load("Audio/Music/Overcast") as AudioClip;
		yield return new WaitForSeconds(universalOffset);

		startMusic();
		yield return new WaitForSeconds(universalOffset);
		EventManager.instance.Raise(new StartSongEvent());
		yield return null;

	}

	public void startParty(){
		om.startParty();
		transform.GetComponent<AudioSource>().volume = 1;
		for(int i=0; i<colors.Count; i++){
			colors[i] = partyMaterial;
		}
		playerTrail.SetActive(true);
		om.resetColors();
		//moviePlane.GetComponent<MeshRenderer>().enabled = true;
	}

	public void endParty(){
		om.endParty();
		transform.GetComponent<AudioSource>().volume = .75f;
		for(int i=0; i<colors.Count;i++){
			colors[i] = originalColors[i];
		}
		playerTrail.SetActive(false);
		om.resetColors();
		//moviePlane.GetComponent<MeshRenderer>().enabled = false;
	}

}

public enum GameState {Title,Menu,Playing,Result}
