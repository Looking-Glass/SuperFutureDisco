using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using InControl;

public class SongCarousel : MonoBehaviour {

	Vector3 [] startPositions;
	Vector3 [] startScales;
	List<SongSelectionUI> songUIs;
	bool canScroll;

	public float scrollPauseTime;
	public float stickThreshold;


	void Awake(){
		canScroll = true;
	}

	// Use this for initialization
	void Start () {
		songUIs = new List<SongSelectionUI>();
		foreach(SongSelectionUI song in transform.GetComponentsInChildren<SongSelectionUI>()){
			songUIs.Add(song);
		}
		setupPositionsAndScales();
		initialPopulation();
	}
	
	// Update is called once per frame
	void Update () {
		if(canScroll){
			if(GameManager.instance.inputDevice.LeftStickX.Value < -1 * stickThreshold || Input.GetKey(KeyCode.LeftArrow) || GameManager.instance.inputDevice.DPadLeft.WasPressed){
				moveLeft();
				canScroll = false;
				StartCoroutine("resetScroll");
			} else if(GameManager.instance.inputDevice.LeftStickX.Value > 1 * stickThreshold || Input.GetKey(KeyCode.RightArrow) || GameManager.instance.inputDevice.DPadRight.WasPressed){
				moveRight();
				canScroll = false;
				StartCoroutine("resetScroll");
			}
		}
	}

	IEnumerator resetScroll(){
		yield return new WaitForSeconds(scrollPauseTime);
		canScroll = true;
		yield return null;
	}

	void setupPositionsAndScales(){
		startPositions = new Vector3[7];
		startScales = new Vector3[7];
		int counter =0;
		foreach(SongSelectionUI song in songUIs){
			startPositions[counter] = song.transform.localPosition;
			startScales[counter] = song.transform.localScale;
			Debug.Log("POS"+ counter + " "+ startPositions[counter]);


			startScales[counter] = song.transform.parent.localScale;

			counter++;
		}
	}

	void initialPopulation(){
		//set center to gamemanager.songs[0], go back and forward from there
		int songListCounter=0;
		int startUIpos = Mathf.RoundToInt(songUIs.Count/2);
		if(GameManager.instance.songs.Length >0){
			songUIs[startUIpos].setSong(GameManager.instance.songs[0]);
			songUIs[startUIpos].currentCarouselPosition = startUIpos;

		} else {
			Debug.Log("no songs set in GameManager!");
			return;
		}
		//populate right of center
		for(int i=startUIpos +1; i< songUIs.Count; i++){
			songListCounter ++;
			songListCounter = checkForSongListCounterOutOfRange(songListCounter);
			songUIs[i].positionInSongList = songListCounter;
			songUIs[i].setSong(GameManager.instance.songs[songListCounter]);
			songUIs[i].currentCarouselPosition = i;
		}
		//reset to center
		songListCounter=0;

		//populate left of center
		for(int i=startUIpos - 1; i>=0; i--){
			songListCounter --;
			songListCounter = checkForSongListCounterOutOfRange(songListCounter);
			songUIs[i].positionInSongList = songListCounter;
			songUIs[i].setSong(GameManager.instance.songs[songListCounter]);
			songUIs[i].currentCarouselPosition = i;
		}


	}

	int checkForSongListCounterOutOfRange(int songListCounter){
		if(songListCounter > GameManager.instance.songs.Length -1){
			songListCounter = 0;
		} else if(songListCounter <0){
			songListCounter = GameManager.instance.songs.Length -1;
		}
		return songListCounter;
	}

	void moveLeft(){
		
		int lastSong=0;
		//loop through every one and move to appropriate target pos, resetting the first one
		for(int i =1; i< songUIs.Count; i++){
//			Debug.Log(i);


			songUIs[i].startMoving(
				
				startPositions[songUIs[i].currentCarouselPosition],
				startPositions[songUIs[i].currentCarouselPosition-1],
				startScales[songUIs[i].currentCarouselPosition],
				startScales[songUIs[i].currentCarouselPosition-1]
			);

			songUIs[i].currentCarouselPosition-=1;


			if(songUIs[i].currentCarouselPosition == songUIs.Count-2){
				lastSong = songUIs[i].positionInSongList;
				Debug.Log(lastSong);
			}

		}

		SongSelectionUI temp = songUIs[0];
		songUIs.RemoveAt(0);
		songUIs.Add(temp);
		songUIs[songUIs.Count-1].transform.position = startPositions[startPositions.Length-1];
		songUIs[songUIs.Count-1].currentCarouselPosition = startPositions.Length -1;

		//do special for first pos

		Debug.Log("HO:"+songUIs[songUIs.Count-1]+" "+checkForSongListCounterOutOfRange(lastSong+1) );
		songUIs[songUIs.Count-1].positionInSongList = checkForSongListCounterOutOfRange(lastSong+1);
		songUIs[songUIs.Count-1].setSong(GameManager.instance.songs[checkForSongListCounterOutOfRange(lastSong+1)]);



	}

	void moveRight(){

	}

	void playPreview(){

	}
}
