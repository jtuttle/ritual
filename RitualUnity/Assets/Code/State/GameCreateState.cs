using System;
using System.Collections.Generic;

using UnityEngine;

public class GameCreateState : FSMState {
	private List<GameObject> _monks;
	private int _playerNote;

	public GameCreateState()
		: base(GameState.GameCreate) {

	}

	public override void InitState(FSMTransition transition) {
		base.InitState(transition);

	}

	public override void EnterState(FSMTransition transition) {
		base.EnterState(transition);

		List<AudioClip> notes = GetNotes();

		_playerNote = UnityEngine.Random.Range(1, notes.Count - 2);

		AudioSource playerSource = GameObject.Find("Player").GetComponent<AudioSource>();
		playerSource.clip = notes[_playerNote];
		playerSource.volume = 0;

		PlaceMonks(notes, _playerNote);

		GameData data = new GameData(notes, _playerNote, _monks);

		ExitState(new GameDataTransition(GameState.GamePlay, data));
	}

	public override void ExitState(FSMTransition transition) {
		_monks = null;
		_playerNote = -1;

		base.ExitState(transition);
	}

	// TODO: change this to actually use the notes.
	private void PlaceMonks(List<AudioClip> notes, int playerNote) {
		_monks = new List<GameObject>();

		GameObject prototype = Resources.Load("Prefabs/Monk") as GameObject;

		float monkX = -6;
		float xStep = 3;
		float z = 1;

		for(int i = 0; i < notes.Count; i++) {
			if(i == playerNote) continue;

			GameObject monk = (GameObject)GameObject.Instantiate(prototype);

			AudioSource source = monk.GetComponent<AudioSource>();
			source.clip = notes[i];
			source.Play();

			monk.transform.position = new Vector3(monkX, 1.5f, z);
			// TODO: change GO name to Monk(note) or something useful
			monk.name = "Monk (" + notes[i].name + ")";

			_monks.Add(monk);

			monkX += xStep;
		}
	}

	// TODO: hook this up to Jasper's resource manager thing
	// TEMPORARY while Jasper is coding up his thing
	private List<AudioClip> GetNotes() {
		List<AudioClip> notes = new List<AudioClip>();

		notes.Add(Resources.Load("Chanting/CMaj/1_C4") as AudioClip);
		notes.Add(Resources.Load("Chanting/CMaj/2_E4") as AudioClip);
		notes.Add(Resources.Load("Chanting/CMaj/3_G4") as AudioClip);
		notes.Add(Resources.Load("Chanting/CMaj/4_C5") as AudioClip);
		notes.Add(Resources.Load("Chanting/CMaj/5_E5") as AudioClip);
		notes.Add(Resources.Load("Chanting/CMaj/6_G5") as AudioClip);

		return notes;
	}
}
