using System.Collections;

using UnityEngine;

public class GameLoseState : BaseGameEndState {
	private AudioClip _loseMusic;
	private GameObject _smokePrototype;
	private Color _flashColor;
	private Texture2D _flashPixel;

	private GameObject _smoke;

	private int _flashFrameDelay;
	private int _flashFrameDuration;
	private bool _flashStarted;

	private float LIGHT_INTENSITY_FADE = 0.02f;

	public GameLoseState()
		: base(GameState.GameLose) {

	}

	public override void InitState(FSMTransition transition) {
		base.InitState(transition);

		_loseMusic = Resources.Load("Music/Death Bolt") as AudioClip;
		_smokePrototype = Resources.Load("Prefabs/Smoke") as GameObject;

		_flashColor = Color.white;

		_flashPixel = new Texture2D(1,1);
		_flashPixel.SetPixel(0, 0, _flashColor);
		_flashPixel.Apply();
	}

	public override void EnterState(FSMTransition transition) {
		base.EnterState(transition);

		SetPixelAlpha(0);

		PlayEndMusic(_loseMusic);

		_flashFrameDelay = 60;
		_flashFrameDuration = 5;
		_flashStarted = false;
	}

	public override void ExitState(FSMTransition transition) {
		SetPlayerVisible(true);

		GameObject.Destroy(_smoke);
		_smoke = null;

		base.ExitState(transition);
	}

	public override void Update() {
		base.Update();

		if(_light.intensity > 0) {
			_light.intensity -= LIGHT_INTENSITY_FADE;
		}
	}

	public override void OnGUI() {
		base.OnGUI();

		if(_flashFrameDelay > 0) _flashFrameDelay--;

		if(_flashFrameDelay == 0) {
			SetPixelAlpha(1);

			if(!_flashStarted) {	
				_flashStarted = true;
				SetPlayerVisible(false);

				_smoke = GameObject.Instantiate(_smokePrototype);
				_smoke.transform.position = GameData.Player.transform.position;
			}

			if(_flashFrameDuration > 0) _flashFrameDuration--;

			if(_flashFrameDuration == 0) {
				SetPixelAlpha(0);
			}
		}

		GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), _flashPixel);
	}

	private void SetPixelAlpha(float a){
		_flashColor.a = a;
		_flashPixel.SetPixel(0, 0, _flashColor);
		_flashPixel.Apply();
	}

	private void SetPlayerVisible(bool visible) {
		GameObject player = GameData.Player;
		int childCount = player.transform.childCount;
		Transform child;

		for(int i = 0; i < childCount; i++) {
			child = player.transform.GetChild(i);

			if(child.name == "rig") continue;

			child.gameObject.SetActive(visible);
		}
	}
}
