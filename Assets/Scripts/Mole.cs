using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mole : MonoBehaviour {

	float speed;
	float emerging;
	float emergeMinTime = 0.5f;
	float emergeMaxTime = 1.5f;
	float emergeFinalTime = 2f;
	float emergeMaxDistance = 2f;
	float stopTime = 0.2f;
	float minHitY = 1.75f;
	float delay = 1f;
	static int activeMoles;
	static int totalMoleCount;
	static ArrayList moleList;
	AudioSource sound;
	bool stopped;
	int maxMoles = 20;

	Vector3 startPos;

	void Awake()
	{
		sound = GetComponent<AudioSource>();
		if(moleList==null){
			moleList = new ArrayList();
		}
		moleList.Add(this);
		startPos = transform.position + Vector3.down*emergeMaxDistance;
	}
	
	// Update is called once per frame
	void Update () {
		if(stopped){
			transform.position = startPos + Vector3.down*emergeMaxDistance;
			return;
		}

		if(GameManager2.IsGameStarted()){
			emerging -= Time.deltaTime;
		}
		if(emerging>emergeMaxTime){
			transform.position = startPos + Vector3.down*emergeMaxDistance;
		}else{
			float trueTime = emerging;
			if(emerging<=-emergeMaxTime){
				if(emerging>=-emergeMaxTime-stopTime){
					//stand
					trueTime = -emergeMaxTime;
				}else{
					//continue
					trueTime += stopTime;
				}
			}

			transform.position = startPos + Vector3.down*Mathf.Sin(Mathf.PI/2f*trueTime/emergeMaxTime)*emergeMaxDistance;
		}

		if(emerging<-emergeFinalTime-stopTime){
			RestartTime();
		}
	}

	void RestartTime(){
		if(totalMoleCount<maxMoles){
			emerging = Random.Range(emergeMinTime, emergeMaxTime);
			if(totalMoleCount<6){
				//add delay to first moles
				emerging += delay*totalMoleCount;
			}
			totalMoleCount++;
		}else{
			stopped = true;
			
			//if everyone stopped, finish
			bool finished = true;
			foreach(Mole m in moleList){
				if(!m.stopped) finished = false;
			}
			if(finished) GameManager2.FinishGame();
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.tag=="Hammer" && transform.localPosition.y>minHitY && !sound.isPlaying){ //isPlaying is a gamb to prevent double hit
			sound.Play();
			GameManager2.IncrementScore();
			RestartTime();
		}
	}

	public static void RestartMoles(){
		totalMoleCount = 0;
		foreach(Mole m in moleList){
			m.stopped = false;
			m.RestartTime();
		}
	}
}
