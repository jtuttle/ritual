using System.Collections.Generic;

using UnityEngine;

public class GameResetState : FSMState {
	public GameResetState()
		: base(GameState.GameReset) {

	}

	public override void InitState(FSMTransition transition) {
		base.InitState(transition);


	}

	public override void EnterState(FSMTransition transition) {
		base.EnterState(transition);

		List<AudioClip> notes = GetNotes();

		GameData.Notes = GetNotes();

		int playerNote = UnityEngine.Random.Range(1, notes.Count - 2);
		GameData.PlayerNote = playerNote;

		SetPlayerNote();
		SetMonkNotes();

		ExitState(new FSMTransition(GameState.GamePlay));
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

	private void SetPlayerNote() {
		AudioSource playerSource = GameData.Player.GetComponent<AudioSource>();
		playerSource.clip = GameData.Notes[GameData.PlayerNote];
		playerSource.volume = 0;
	}

	private void SetMonkNotes() {
		int monkIndex = 0;

		for(int i = 0; i < GameData.Notes.Count; i++) {
			if(i == GameData.PlayerNote)
				continue;

			AudioClip note = GameData.Notes[i];
			GameObject monk = GameData.Monks[monkIndex];

			AudioSource source = monk.GetComponent<AudioSource>();
			source.clip = note;
			source.Play();

			monk.name = "Monk (" + note.name + ")";

			monkIndex++;
		}
	}
}
