using System.Collections;

using UnityEngine;

public class GameWinState : BaseGameEndState {
	private AudioClip _winMusic;

	public GameWinState()
		: base(GameState.GameWin) {

	}

	public override void InitState(FSMTransition transition) {
		base.InitState(transition);

		_winMusic = Resources.Load("Music/Victory Music") as AudioClip;
	}

	public override void EnterState(FSMTransition transition) {
		base.EnterState(transition);

		PlayEndMusic(_winMusic);
	}
}
