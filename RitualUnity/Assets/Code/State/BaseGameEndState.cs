using UnityEngine;
using System.Collections;

public class BaseGameEndState : FSMState {

	public BaseGameEndState(GameState gameState)
		: base(gameState) {

	}

	public override void EnterState(FSMTransition transition) {
		base.EnterState(transition);

		SilenceMonks();
		SilenceMusic();
	}


	private void SilenceMonks() {
		foreach(GameObject monk in GameData.Monks) {
			monk.GetComponent<AudioSource>().Stop();
		}
	}

	private void SilenceMusic() {
		Camera.main.GetComponent<AudioSource>().volume = 0;
	}
}
