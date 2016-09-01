using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using InControl;


public class SongCarousel : MonoBehaviour {

	Vector3 [] startPositions;
	Vector3 [] startScales;
	List<SongSelectionUI> songUIs;
	AudioSource myAudio;
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
		StartCoroutine("playPreview");
		myAudio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		if(canScroll){
			if(GameManager.instance.inputDevice.LeftStickX.Value < -1 * stickThreshold || Input.GetKey(KeyCode.LeftArrow) || GameManager.instance.inputDevice.DPadLeft.WasPressed){
				moveRight();
				canScroll = false;
				StartCoroutine("resetScroll");
			} else if(GameManager.instance.inputDevice.LeftStickX.Value > 1 * stickThreshold || Input.GetKey(KeyCode.RightArrow) || GameManager.instance.inputDevice.DPadRight.WasPressed){
				moveLeft();
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
		


			startScales[counter] = song.transform.localScale;

			counter++;
		}
	}

	public SongData getCurrentSelection(){
		foreach(SongSelectionUI songUI in songUIs){
			if(songUI.currentCarouselPosition==3){
				Debug.Log(songUI.currentSong.koreography.name);
				return songUI.currentSong;
			}
		}
		return new SongData();

	}

	public void initialPopulation(){
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

	public void refreshCarousel(){
		initialPopulation();
		foreach(SongSelectionUI songUI in songUIs){
			songUI.refreshUI();
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
		StopCoroutine("playPreview");
		stopPreview();
		myAudio.Play();
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
			}

		}
		//warp the leftmost one to the right, off screen
		SongSelectionUI temp = songUIs[0];
		songUIs.RemoveAt(0);
		songUIs.Add(temp);
		songUIs[songUIs.Count-1].transform.position = startPositions[startPositions.Length-1];
		songUIs[songUIs.Count-1].currentCarouselPosition = startPositions.Length -1;

		//set song for last pos
		songUIs[songUIs.Count-1].positionInSongList = checkForSongListCounterOutOfRange(lastSong+1);
		songUIs[songUIs.Count-1].setSong(GameManager.instance.songs[checkForSongListCounterOutOfRange(lastSong+1)]);

		StartCoroutine("playPreview");

	}

	void moveRight(){
		
		StopCoroutine("playPreview");

		stopPreview();
		myAudio.Play();
		int lastSong=0;
		//loop through every one and move to appropriate target pos, resetting the first one
		for(int i =0; i< songUIs.Count-1; i++){
			//			Debug.Log(i);

			songUIs[i].startMoving(

				startPositions[songUIs[i].currentCarouselPosition],
				startPositions[songUIs[i].currentCarouselPosition+1],
				startScales[songUIs[i].currentCarouselPosition],
				startScales[songUIs[i].currentCarouselPosition+1]
			);

			songUIs[i].currentCarouselPosition+=1;


			if(songUIs[i].currentCarouselPosition == 1){
				lastSong = songUIs[i].positionInSongList;
			}

		}
		//warp the leftmost one to the right, off screen
		SongSelectionUI temp = songUIs[songUIs.Count-1];
		songUIs.RemoveAt(songUIs.Count-1);
		songUIs.Insert(0,temp);
		songUIs[0].transform.position = startPositions[0];
		songUIs[0].currentCarouselPosition = 0;

		//set song for last pos
		songUIs[0].positionInSongList = checkForSongListCounterOutOfRange(lastSong-1);
		songUIs[0].setSong(GameManager.instance.songs[checkForSongListCounterOutOfRange(lastSong-1)]);
		StartCoroutine("playPreview");


	}

	IEnumerator playPreview(){
		
		foreach(SongSelectionUI songUI in songUIs){
			if(songUI.currentCarouselPosition==3){
				songUI.myAudio.volume = 1;
				yield return new WaitForSeconds(songUI.moveTime + .1f);
				songUI.myAudio.time = songUI.currentSong.previewStartSeconds;
				songUI.myAudio.Play();
				yield return new WaitForSeconds(10);
				float i=1;
				while(i>=0){
					i-=Time.deltaTime/3;
					songUI.myAudio.volume = i;
					yield return new WaitForSeconds(Time.deltaTime);
				}
			}
		}

		yield return null;
	}

	void stopPreview(){
		foreach(SongSelectionUI songUI in songUIs){
			songUI.myAudio.Stop();
		}
	}
}
