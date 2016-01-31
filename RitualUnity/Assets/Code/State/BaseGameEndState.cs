using UnityEngine;
using System.Collections;

public class BaseGameEndState : FSMState {
	private AudioSource _source;

	public BaseGameEndState(GameState gameState)
		: base(gameState) {

	}

	public override void EnterState(FSMTransition transition) {
		base.EnterState(transition);

		SilenceMonks();
		SilenceMusic();
	}

	public override void ExitState(FSMTransition transition) {
		_source = null;

		base.ExitState(transition);
	}

	public override void Update() {
		if(!_source.isPlaying) {
			ExitState(new FSMTransition(GameState.GameReset));	
		}
	}

	private void SilenceMonks() {
		foreach(GameObject monk in GameData.Monks) {
			monk.GetComponent<AudioSource>().Stop();
		}
	}

	private void SilenceMusic() {
		Camera.main.GetComponent<AudioSource>().volume = 0;
	}

	protected void PlayEndMusic(AudioClip music) {
		_source = GameData.Player.GetComponent<AudioSource>();
		_source.clip = music;
		_source.volume = 1;
		_source.loop = false;
		_source.Play();
	}
}
