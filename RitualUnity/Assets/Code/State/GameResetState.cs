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

		GameData.Player.transform.position = Vector3.zero;
		SetPlayerAnim();

		GameData.Notes = GetNotes();
		GameData.PlayerNote = UnityEngine.Random.Range(1, GameData.Notes.Count - 2);

		SetPlayerNote();
		SetMonkNotes(GameData.Monks, GameData.Notes, GameData.PlayerNote);

		ExitState(new FSMTransition(GameState.GamePlay));
	}

	private List<AudioClip> GetNotes() {
		return ChordLibrary.Instance.GetRandomChord();
	}

	private void SetPlayerAnim() {
		GameObject player = GameData.Player;

		if(player.transform.childCount > 0) {
			GameObject.Destroy(player.transform.GetChild(0).gameObject);
		}

		
		GameObject monk = GameObject.Instantiate(GameData.GetRandomMonkAnim());
		monk.transform.parent = player.transform;
	}

	private void SetPlayerNote() {
		AudioSource playerSource = GameData.Player.GetComponent<AudioSource>();
		playerSource.clip = GameData.Notes[GameData.PlayerNote];
		playerSource.volume = 0;
	}

	private void SetMonkNotes(List<GameObject> monks, List<AudioClip> notes, int playerNote) {
		int monkIndex = 0;

		for(int i = 0; i < notes.Count; i++) {
			if(i == playerNote)
				continue;

			AudioClip note = notes[i];
			GameObject monk = monks[monkIndex];

			AudioSource source = monk.GetComponent<AudioSource>();
			source.clip = note;
			source.Play();

			monk.name = "Monk (" + note.name + ")";

			monkIndex++;
		}
	}
}
