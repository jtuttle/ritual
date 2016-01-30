using UnityEngine;
using System.Collections;

public class GamePlayState : FSMState {
	private CharacterController _player;

	private float SPEED = 0.1f;

    public GamePlayState()
        : base(GameState.GamePlay) {

    }

    public override void InitState(FSMTransition transition) {
        base.InitState(transition);

		_player = GameObject.Find("Player").GetComponent<CharacterController>();
    }

    public override void EnterState(FSMTransition transition) {
        base.EnterState(transition);

        
    }

	public override void Update() {
		MoveChar();
	}

	private void MoveChar() {
		int h = 0;
		int v = 0;

		if(Input.GetKey(KeyCode.LeftArrow)) {
			h -= 1;
		}

		if(Input.GetKey(KeyCode.RightArrow)) {
			h += 1;
		}

		if(Input.GetKey(KeyCode.UpArrow)) {
			v += 1;
		}

		if(Input.GetKey(KeyCode.DownArrow)) {
			v -= 1;
		}

		_player.Move(new Vector3(h * SPEED, 0, v * SPEED));

	}
}
