using System.Collections;

using UnityEngine;

public class GamePlayState : FSMState {
	private CharacterController _player;
	private AudioSource _playerSource;

	private float SPEED = 0.1f;

    public GamePlayState()
        : base(GameState.GamePlay) {

    }

    public override void InitState(FSMTransition transition) {
        base.InitState(transition);

		_player = GameObject.Find("Player").GetComponent<CharacterController>();
		_playerSource = _player.GetComponent<AudioSource>();
    }

    public override void EnterState(FSMTransition transition) {
        base.EnterState(transition);

		GameData gameData = (transition as GameDataTransition).GameData;
    }

	public override void Update() {
		MovePlayer();
		PlayPlayerNote();
	}

	private void MovePlayer() {
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
}
