using System.Collections;

using UnityEngine;

public class GameWinState : BaseGameEndState {
	private AudioClip _victoryMusic;

	private AudioSource _source;

	public GameWinState()
		: base(GameState.GameWin) {

	}

	public override void InitState(FSMTransition transition) {
		base.InitState(transition);

		_victoryMusic = Resources.Load("Music/Victory Music") as AudioClip;
	}

	public override void EnterState(FSMTransition transition) {
		base.EnterState(transition);

		PlayVictoryMusic();
	}

	public override void Update() {
		if(!_source.isPlaying) {
			ExitState(new FSMTransition(GameState.GameReset));	
		}
	}

	private void PlayVictoryMusic() {
		_source = GameData.Player.GetComponent<AudioSource>();
		_source.clip = _victoryMusic;
		_source.volume = 1;
		_source.loop = false;
		_source.Play();
	}
}
