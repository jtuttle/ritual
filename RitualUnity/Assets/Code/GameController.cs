using System;

using UnityEngine;

public enum GameState {
	GameCreate, GamePlay, GameWin, GameLose
}

public class GameController : MonoBehaviour {
	private FiniteStateMachine _fsm;

	protected void Awake() {
		_fsm = new FiniteStateMachine();

		_fsm.AddState(new GameCreateState());
		_fsm.AddState(new GamePlayState());
		_fsm.AddState(new GameWinState());
		_fsm.AddState(new GameLoseState());

		_fsm.ChangeState(new FSMTransition(GameState.GameCreate));
	}

	protected void Update() {
		if(_fsm != null)
			_fsm.Update();
	}

	protected void OnGUI() {
		if(_fsm != null)
			_fsm.OnGUI();
	}
}
