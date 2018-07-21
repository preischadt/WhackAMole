using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager2 : MonoBehaviour {

	static bool started;
	static int score;
	public Text scoreText;
	
	void Update () {
		scoreText.text = (started || score>0)? score + "" : "";
	}

	public static void IncrementScore(){
		score++;
	}

	public static void StartGame(){
		Mole.RestartMoles();
		started = true;
		score = 0;
	}

	public static void FinishGame(){
		started = false;
	}

	public static bool IsGameStarted(){
		return started;
	}
}
