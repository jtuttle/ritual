﻿using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class GamePlayState : FSMState {
	private CharacterController _player;
	private AudioSource _playerSource;

	private Rect _bounds;
	private bool _moved;

	private float SPEED = 0.1f;

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

		_moved = false;

		_bounds = GetMovementBounds();
    }

	public override void ExitState(FSMTransition transition) {
		_player = null;
		_playerSource = null;

		base.ExitState(transition);
	}

	public override void Update() {
		base.Update();

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

	public override void OnGUI() {
		base.OnGUI();


		if(!_moved) {
			GUIStyle centeredStyle = GUI.skin.GetStyle("Label");
			centeredStyle.alignment = TextAnchor.UpperCenter;
			centeredStyle.normal.textColor = Color.black;
			centeredStyle.fontSize = 36;

			string message = "Find your place in the\nritual by listening\nto your fellow monks\n\nPress space to sing!";

			GUI.Label(new Rect(0, (Screen.height / 2) - 100, Screen.width, Screen.height), message, centeredStyle);
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

		if(h != 0 || v != 0) {
			_player.Move(new Vector3(h, 0, v));
			_moved = true;
		}
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
		return true;

		GameObject leftMonk = GameData.Monks[GameData.PlayerNote - 1];
		GameObject rightMonk = GameData.Monks[GameData.PlayerNote];

		float xMin = leftMonk.transform.position.x;
		float xMax = rightMonk.transform.position.x;
		float playerX = _player.transform.position.x;

		return playerX > xMin && playerX < xMax;
	}
}
