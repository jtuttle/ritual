using System;

using UnityEngine;

public enum GameState {
	GameCreate
}

public class GameController : MonoBehaviour {
	private FiniteStateMachine _fsm;

	public void Awake() {
		_fsm = new FiniteStateMachine();



	}
}
