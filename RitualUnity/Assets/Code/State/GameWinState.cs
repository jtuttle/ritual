using System.Collections;

using UnityEngine;

public class GameWinState : FSMState {
	public GameWinState()
		: base(GameState.GameWin) {

	}

	public override void InitState(FSMTransition transition) {
		base.InitState(transition);


	}

	public override void EnterState(FSMTransition transition) {
		base.EnterState(transition);

		ExitState(new FSMTransition(GameState.GameReset));
	}
}
