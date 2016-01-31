using System;
using System.Collections.Generic;

using UnityEngine;

public class GameCreateState : FSMState {
	public GameCreateState()
		: base(GameState.GameCreate) {

	}

	public override void InitState(FSMTransition transition) {
		base.InitState(transition);

	}

	public override void EnterState(FSMTransition transition) {
		base.EnterState(transition);

		GameData.Player = PlacePlayer();
		GameData.Monks = PlaceMonks();

		ExitState(new FSMTransition(GameState.GameReset));
	}

	public override void ExitState(FSMTransition transition) {
		
		base.ExitState(transition);
	}

	private GameObject PlacePlayer() {
		GameObject player = GameObject.Instantiate(Resources.Load("Prefabs/Player") as GameObject);

		Camera.main.GetComponent<FollowCamera>().Target = player.transform;

		return player;
	}

	private List<GameObject> PlaceMonks() {
		List<GameObject> monks = new List<GameObject>();

		GameObject prototype = Resources.Load("Prefabs/Monk") as GameObject;

		float monkX = -8;
		float xStep = 4;
		float z = 8;

		for(int i = 0; i < 5; i++) { //notes.Count; i++) {
			GameObject monk = (GameObject)GameObject.Instantiate(prototype);

			monk.transform.position = new Vector3(monkX, 0, z);
			monk.name = "Monk";

			monks.Add(monk);

			monkX += xStep;
		}

		return monks;
	}
}
