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
		List<GameObject> monkAnims = new List<GameObject>();

		GameObject monkPrototype = Resources.Load("Prefabs/Monk") as GameObject;

		for(int i = 0; i < 4; i++) {
			string monkAnimPath = "Prefabs/MonkAnims/MonkAnim" + (i + 1);
			monkAnims.Add(Resources.Load(monkAnimPath) as GameObject);
		}

		List<GameObject> monks = new List<GameObject>();

		float monkX = -10;
		float xStep = 5;
		float z = 12;

		for(int i = 0; i < 5; i++) {
			GameObject monk = (GameObject)GameObject.Instantiate(monkPrototype);
			GameObject monkAnim = (GameObject)GameObject.Instantiate(monkAnims[i % 4]);
			monkAnim.transform.parent = monk.transform;

			monk.transform.position = new Vector3(monkX, 0, z);
			monk.name = "Monk";

			monks.Add(monk);

			monkX += xStep;
		}

		return monks;
	}
}