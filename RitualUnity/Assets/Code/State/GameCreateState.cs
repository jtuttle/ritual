using System;

using UnityEngine;

public class GameCreateState : FSMState {
	public GameCreateState()
		: base(GameState.GameCreate) {

	}

	public override void InitState(FSMTransition transition) {
		base.InitState(transition);

	}

	public override void EnterState(FSMTransition transition) {
		base.EnterState(transition);

		ExitState(new FSMTransition(GameState.GamePlay));
	}
}
