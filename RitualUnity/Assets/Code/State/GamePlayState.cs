using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class GamePlayState : FSMState {
	private CharacterController _player;
	private AudioSource _playerSource;

	private Rect _bounds;

	private float SPEED = 0.2f;

    public GamePlayState()
        : base(GameState.GamePlay) {

    }

    public override void InitState(FSMTransition transition) {
        base.InitState(transition);

    }

    public override void EnterState(FSMTransition transition) {
        base.EnterState(transition);

		_player = GameData.Player.GetComponent<CharacterController>();
		_playerSource = _player.GetComponent<AudioSource>();

		_bounds = GetMovementBounds();
    }

	public override void ExitState(FSMTransition transition) {
		_player = null;
		_playerSource = null;

		base.ExitState(transition);
	}

	public override void Update() {
		MovePlayer();
		PlayPlayerNote();

		if(HasJoinedChorus()) {
			if(HasCorrectPlacement()) {
				ExitState(new FSMTransition(GameState.GameWin));
			} else {
				ExitState(new FSMTransition(GameState.GameLose));
			}
		}
	}

	private Rect GetMovementBounds() {
		List<GameObject> monks = GameData.Monks;

		Vector3 topLeft = monks[0].transform.position;
		Vector3 topRight = monks[monks.Count - 1].transform.position;

		return new Rect(topLeft.x, 0, topRight.x - topLeft.x, topLeft.z);
	}

	private void MovePlayer() {
		float h = 0;
		float v = 0;

		Vector3 playerPos = _player.transform.position;

		if(Input.GetKey(KeyCode.LeftArrow) && playerPos.x - SPEED > _bounds.xMin) {
			h -= SPEED;
		}

		if(Input.GetKey(KeyCode.RightArrow) && playerPos.x + SPEED < _bounds.xMax) {
			h += SPEED;
		}

		if(Input.GetKey(KeyCode.UpArrow)/* && playerPos.z + SPEED < _bounds.yMax*/) {
			v += SPEED;
		}

		if(Input.GetKey(KeyCode.DownArrow) && playerPos.z - SPEED > _bounds.yMin) {
			v -= SPEED;
		}

		_player.Move(new Vector3(h, 0, v));

		// Hack to stop player from jumping into the air on occasion.
		//Vector3 playerPos = _player.transform.position;
		//playerPos = new Vector3(playerPos.x, 0, playerPos.z);
	}

	private void PlayPlayerNote() {
		if(Input.GetKey(KeyCode.Space)) {
			if(!_playerSource.isPlaying) {
				_playerSource.volume = 1;
				_playerSource.Play();
			}
		} else {
			_playerSource.volume = 0;
			_playerSource.Stop();
		}
	}

	private bool HasJoinedChorus() {
		return _player.transform.position.z >= _bounds.yMax;
	}

	private bool HasCorrectPlacement() {
		GameObject leftMonk = GameData.Monks[GameData.PlayerNote - 1];
		GameObject rightMonk = GameData.Monks[GameData.PlayerNote];

		float xMin = leftMonk.transform.position.x;
		float xMax = rightMonk.transform.position.x;
		float playerX = _player.transform.position.x;

		return playerX > xMin && playerX < xMax;
	}
}
