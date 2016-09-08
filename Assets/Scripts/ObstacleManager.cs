using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObstacleManager : MonoBehaviour {

	private List<Obstacle> obstacles;
	bool canAdd;
	float border = 1;

	public float lastSpawnTime {get; private set;}
	int lastNote;
	int lastColor;
	Vector3 lastSpawnPos;
	float minTimeDifferenceForNewColor = .4f;

	bool inParty;
	Transform cube;

	public int maxNote;
	public int minNote;


	float cubeYRange;
	float noteYRange;
	float yBorder;


	// Use this for initialization
	void Start () {
		cube = GameManager.instance.cubeCamera;
		inParty = false;
		obstacles = new List<Obstacle>();
		EventManager.instance.AddListener<PlayerHitObstacleEvent>(hitObs);
		EventManager.instance.AddListener<ObstacleMissedEvent>(missedObs);
		lastNote = 60;

		yBorder = 1f;
		cubeYRange = cube.localScale.y - 2 * yBorder;



	}

	public void setYRange(){
		noteYRange = maxNote - minNote;
	}

	void OnDestroy(){
		EventManager.instance.RemoveListener<PlayerHitObstacleEvent>(hitObs);
		EventManager.instance.RemoveListener<ObstacleMissedEvent>(missedObs);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void AddObstacle(float note = -1)
	{

//		Debug.Log("NOTE IS"+note);
		GameObject b = Spawner.Spawn("Prefabs/Obstacle",getRandomSpawnPos(note)) as GameObject;
		b.transform.parent = transform;
		Obstacle obs = b.GetComponent<Obstacle>();


		obstacles.Add(obs);
		canAdd = true;
		if(Time.time - lastSpawnTime > minTimeDifferenceForNewColor){
			int newColor = getNewColor();
			obs.setColor(newColor);
			lastColor = newColor;
		} else {
			obs.setColor(lastColor);
		}
		lastSpawnTime = Time.time;
		lastNote = Mathf.RoundToInt(note);

		if(inParty){
			float rotDir = Random.Range(0,2);
			if(rotDir==0){
				rotDir = -1;
			}
			//GameObject l = Spawner.Spawn("Prefabs/LightCylinder",getRandomSpawnPos()) as GameObject;
			//l.transform.parent = transform;
			//l.transform.eulerAngles = new Vector3(0,0,rotDir * 30);
		}
	}

	int getNewColor(){
		int dir = Random.Range(0,2);
		if(dir==0)
			dir-=1;

		int newColor = lastColor + dir;
		if(newColor < 0){
			newColor = GameManager.instance.colors.Count - 1;
		} else if (newColor >= GameManager.instance.colors.Count){
			newColor = 0;
		}

		return newColor;
	}


	//properly manage removal of butterflies
	public void DestroyObstacle(Obstacle o) {

		//EventManager.instance.Raise(new EnemyDieEvent());
		if (obstacles !=null) { 
			obstacles.Remove(o);
		}
		Destroy(o.gameObject);
	}

	public void clearAllObstacles() {
		if (obstacles != null) {
			for (int i = obstacles.Count - 1; i >= 0; i--) {
				DestroyObstacle(obstacles[i]);
			}
		}

	}

	public void resetColors(){
		if(obstacles.Count >0){
			foreach(Obstacle o in obstacles){
				o.setColor(o.currentColor);
			}
		}
	}

	void hitObs(PlayerHitObstacleEvent e){
		DestroyObstacle(e.obstacle);
	}

	void missedObs(ObstacleMissedEvent e){
		DestroyObstacle(e.obstacle);
	}

	Vector3 getRandomSpawnPos(float note = -1){
//		Debug.Log(minNote+" "+maxNote);
		//full random
		float x = Random.Range(-1 * cube.localScale.x/2 + border, cube.localScale.x/2 - border);
		float y = Random.Range(-1 * cube.localScale.y/2 + border, cube.localScale.y/2 - border);
		float z = cube.localScale.z/2;

		if(!inParty){
			//
			// GET X
			//

			float maxX = .8f * cube.localScale.x;
			float xRandomizer = .2f * cube.localScale.x;
			//NOTE: decided against Y randomization so that levels could be more clearly designed with the feeling of going up/down with the music
			//float yRandomizer = .1f * cube.localScale.y;

			//The number at the end of the equation below determines how wide apart X spawns will be based on spawn time from previous target
			//Lower means wider apart, most testing/dev was done with value of 2
			float genX = (maxX * (Time.time - lastSpawnTime))/1.5f;


			if(lastSpawnPos.x > 0){
				genX *= -1;
			}

			//add randomness to x pos
			genX += Random.Range(-1*xRandomizer,xRandomizer);

			//always go towards the center
			if(Time.time - lastSpawnTime <.1){
				genX = .1f;
			}
				
			x = lastSpawnPos.x + genX;

			//check if out of bounds
			if(Mathf.Abs(x) > cube.localScale.x/2-border){
				x = Random.Range(-1 * cube.localScale.x/2 + border, cube.localScale.x/2 - border);
			}
				
			// SET Y
			note -= minNote;
			y =  (-1 * cube.localScale.y/2) + (note/noteYRange * cubeYRange + .5f*border);// + Random.Range(-1*yRandomizer,yRandomizer);

			if(Mathf.Abs(y) > cube.localScale.y/2){
				if(y>0){
					y=cube.localScale.y/2 - border;
				} else {
					y = (-1 *cube.localScale.y/2) + border;
				}
			}
		}
		lastSpawnPos = new Vector3(x,y,z);
		return lastSpawnPos;





	}

//	public void test(){
//		foreach(Obstacle o in obstacles){
//			o.setColor2(1);
//		}
//	}

	public void startParty(){
		inParty = true;
	}

	public void endParty(){
		inParty = false;
	}
}


