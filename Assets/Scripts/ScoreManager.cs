using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreManager : MonoBehaviour {

	int playerScore;
	int comboMultiplier = 1;
	int baseScoreForHit = 10;

	int [] colorLevels;
	public Slider [] levelSliders;
	public Text scoreText;
	public Material [] textMaterials;
	int colorLevelMax = 20;
	Transform canvas;

	float partyCountdown;
	int currentPartyLevel;

	int penalty = 2;

	public int totalNotes;



	private delegate void stateUpdate();
	private stateUpdate StateUpdate;
	private ScoreState currentState;

	public AnimationCurve partyLevelToTime;

	float secondsForCurrentParty;

	void setState(ScoreState newState){
		switch (newState) {
		case ScoreState.Normal:
			StateUpdate = normalUpdate;
			break;
		case ScoreState.Party:
			partyCountdown = secondsForCurrentParty;
			StateUpdate = partyUpdate;
			break;
		default:
			throw new System.ArgumentOutOfRangeException ();
		}
		currentState = newState;
	}



	// Use this for initialization
	void Start () {
		EventManager.instance.AddListener<PlayerHitObstacleEvent>(hitObs);
		EventManager.instance.AddListener<ObstacleMissedEvent>(missedObs);
		EventManager.instance.AddListener<TryStartPartyEvent>(tryStartParty);
		canvas = GameManager.instance.transform.FindChild("Canvas");
	}

	void OnDestroy(){
		EventManager.instance.RemoveListener<PlayerHitObstacleEvent>(hitObs);
		EventManager.instance.AddListener<ObstacleMissedEvent>(missedObs);
		EventManager.instance.RemoveListener<TryStartPartyEvent>(tryStartParty);
	}
	
	// Update is called once per frame
	void Update () {
		if(StateUpdate != null){
			StateUpdate();
		}
	}

	void normalUpdate(){
		
	}

	void partyUpdate(){
		if(partyCountdown <=0){
			setState(ScoreState.Normal);
			GameManager.instance.endParty();
		}
		foreach(Slider s in levelSliders){
			s.value = currentPartyLevel * (partyCountdown/secondsForCurrentParty);
		}
		partyCountdown -= Time.deltaTime;
	}

	public void reset(){
		playerScore = 0;
		comboMultiplier = 1;
		foreach (Slider s in levelSliders){
			s.value =0;
		}
	}

	int getPartyLevel(){


//		bool readyToParty = true;

//		foreach(Slider s in levelSliders){
//			if(s.value < s.maxValue){
//				readyToParty = false;
//			}
//		}

		int partyLevel = Mathf.RoundToInt(levelSliders[0].maxValue);
		foreach (Slider s in levelSliders){
			if(s.value <partyLevel){
				partyLevel = Mathf.RoundToInt(s.value);
			}
		}
		return partyLevel;

	}

	void hitObs(PlayerHitObstacleEvent e){
		GameObject f;
		//Instantiate a text notification object
		//Debug.Log(
		if(e.obstacleY > 0){
			f = Instantiate(Resources.Load("Prefabs/TextFeedback"),new Vector3(e.obstacleX, e.obstacleY-1,-4.68f),Quaternion.identity) as GameObject;
			f.GetComponent<TextFeedback>().goUp = false;
		} else {
			f = Instantiate(Resources.Load("Prefabs/TextFeedback"),new Vector3(e.obstacleX, e.obstacleY+1,-4.68f),Quaternion.identity) as GameObject;
			f.GetComponent<TextFeedback>().goUp = true;
		}

		f.transform.SetParent(canvas);
		if(currentState == ScoreState.Normal){
			
	//		Debug.Log(e.obstacleX+" "+ e.obstacleY);

			if(e.playerColor == e.obstacleColor){
				GameObject p;
				p = Instantiate(Resources.Load("Prefabs/HitParticleCircle"),new Vector3(e.obstacleX, e.obstacleY,e.obstacle.transform.position.z),Quaternion.identity) as GameObject;
				p.GetComponent<Renderer>().material = textMaterials[e.obstacleColor];
				int scoreToAdd = baseScoreForHit * comboMultiplier;
				updateScore(scoreToAdd);
				f.GetComponent<Text>().material = textMaterials[e.obstacleColor];
				f.GetComponent<Text>().text = "+"+scoreToAdd.ToString();
				levelSliders[e.obstacleColor].value += 1;
				comboMultiplier +=1;



			} else {
				f.GetComponent<Text>().material = Resources.Load("Materials/text_red") as Material;
				f.GetComponent<Text>().text = "X";
				f.GetComponent<Text>().fontStyle = FontStyle.Bold;
				levelSliders[e.obstacleColor].value -= penalty;
				comboMultiplier = 1;
			}
		} else {
			GameObject p;
			p = Instantiate(Resources.Load("Prefabs/HitParticleCircle"),new Vector3(e.obstacleX, e.obstacleY,e.obstacle.transform.position.z),Quaternion.identity) as GameObject;
			p.GetComponent<Renderer>().material = textMaterials[e.obstacleColor];
			int scoreToAdd = baseScoreForHit * comboMultiplier;
			updateScore(scoreToAdd);
			f.GetComponent<Text>().material = textMaterials[e.obstacleColor];
			f.GetComponent<Text>().text = "+"+scoreToAdd.ToString();

			comboMultiplier +=1;
		}


	}

	void updateScore(int scoreToAdd){
		playerScore += scoreToAdd;
		scoreText.text = playerScore.ToString();

	}

	void missedObs(ObstacleMissedEvent e){
		if(currentState == ScoreState.Normal){
			GameObject f = Instantiate(Resources.Load("Prefabs/TextFeedback"),new Vector3(e.obstacleX,e.obstacleY+1,-4.68f),Quaternion.identity) as GameObject;
			f.transform.SetParent(canvas);
			f.GetComponent<Text>().material = Resources.Load("Materials/text_red") as Material;
			f.GetComponent<Text>().text = "X";
			f.GetComponent<Text>().fontStyle = FontStyle.Bold;
			comboMultiplier = 1;
			levelSliders[e.obstacle.currentColor].value -= penalty;
		}
	}

	void tryStartParty(TryStartPartyEvent e){
		int partyLevel = getPartyLevel();
		if(partyLevel >=5){
			currentPartyLevel = partyLevel;
			secondsForCurrentParty = Mathf.RoundToInt(partyLevelToTime.Evaluate(partyLevel));
			setState(ScoreState.Party);
			GameManager.instance.startParty();
			foreach(Slider s in levelSliders){
				s.value = partyLevel;
			}
		}
	}




}

public enum ScoreState { Normal, Party }
