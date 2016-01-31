using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class GamePlayState : FSMState {
	private Rect _tutorialRect;
	private Texture2D _tutorialTexture;

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

		_tutorialRect = new Rect(Screen.width * 0.05f, 0, Screen.width * 0.9f, Screen.height);
		_tutorialTexture = Resources.Load("Textures/UI") as Texture2D;
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
			GUI.DrawTexture(_tutorialRect, _tutorialTexture, ScaleMode.ScaleToFit, true);
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

		if(Input.GetKey(KeyCode.UpArrow)) {
			v += SPEED;
		}

		if(Input.GetKey(KeyCode.DownArrow) && playerPos.z - SPEED > _bounds.yMin) {
			v -= SPEED;
		}

		if(h != 0 || v != 0) {
			Vector3 move = new Vector3(h, 0, v);

			_player.Move(move);
			_player.transform.GetChild(0).rotation = Quaternion.LookRotation(move, Vector3.up);

			_moved = true;
		}
	}

	private void PlayPlayerNote() {
		if(Input.GetKey(KeyCode.Space)) {
			if(!_playerSource.isPlaying) {
				_playerSource.volume = 1;
				_playerSource.Play();

				SetMonkVolume(0.3f);
			}
		} else {
			_playerSource.volume = 0;
			_playerSource.Stop();

			SetMonkVolume(1);
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

	private void SetMonkVolume(float volume) {
		foreach(GameObject monk in GameData.Monks) {
			monk.GetComponent<AudioSource>().volume = volume;
		}
	}
}
