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

		List<AudioClip> notes = GetNotes();

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

		for(int i = 0; i < 1; i++) {//notes.Count; i++) {
			GameObject monk = (GameObject)GameObject.Instantiate(prototype);

			AudioSource source = monk.GetComponent<AudioSource>();
			source.clip = notes[i];
			source.Play();

			monk.transform.position = new Vector3(startX + (xStep * i), 1.5f, z);
			// TODO: change GO name to Monk(note) or something useful

			_monks.Add(monk);
		}
	}

	// TODO: hook this up to Jasper's resource manager thing
	// TEMPORARY while Jasper is coding up his thing
	private List<AudioClip> GetNotes() {
		List<AudioClip> notes = new List<AudioClip>();

		notes.Add(Resources.Load("Chanting/CMaj/C5") as AudioClip);
		notes.Add(Resources.Load("Chanting/CMaj/E5") as AudioClip);
		notes.Add(Resources.Load("Chanting/CMaj/G4") as AudioClip);
		notes.Add(Resources.Load("Chanting/CMaj/G5") as AudioClip);

		return notes;
	}
}
