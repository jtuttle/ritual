using System;
using System.Collections.Generic;

using UnityEngine;

public class GameCreateState : FSMState {
	private List<GameObject> _monks;

	public GameCreateState()
		: base(GameState.GameCreate) {

	}

	public override void InitState(FSMTransition transition) {
		base.InitState(transition);

	}

	public override void EnterState(FSMTransition transition) {
		base.EnterState(transition);

		// TODO: retrieve this from resources manager
		List<AudioClip> notes = new List<AudioClip>();

		PlaceMonks(notes);
		Debug.Log("WOO");

		ExitState(new FSMTransition(GameState.GamePlay));
	}

	// TODO: change this to actually use the notes.
	private void PlaceMonks(List<AudioClip> notes) {
		_monks = new List<GameObject>();

		GameObject prototype = Resources.Load("Prefabs/Monk") as GameObject;

		float startX = -6;
		float xStep = 6;
		float z = 1;

		for(int i = 0; i < 3; i++) {
			GameObject monk = (GameObject)GameObject.Instantiate(prototype);

			monk.transform.position = new Vector3(startX + (xStep * i), 1.5f, z);
			// TODO: change GO name to Monk(note) or something useful

			_monks.Add(monk);
		}
	}
}
