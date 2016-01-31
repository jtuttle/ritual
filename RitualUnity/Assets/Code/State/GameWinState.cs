using System.Collections;

using UnityEngine;

public class GameWinState : BaseGameEndState {
	private AudioClip _winMusic;

	private GameObject _camTarget;

	private GameObject _idol;
	private Vector3 _originalIdolPosition;
	private bool _shake;

	private float SHAKE_AMOUNT = 0.2f;
	private float IDOL_RISE_SPEED = 0.04f;
	private float MONK_FLY_SPEED = 0.08f;
	private float LIGHT_INTENSITY_GROW = 0.02f;

	public GameWinState()
		: base(GameState.GameWin) {

	}

	public override void InitState(FSMTransition transition) {
		base.InitState(transition);

		_winMusic = Resources.Load("Music/Victory Music") as AudioClip;
	}

	public override void EnterState(FSMTransition transition) {
		base.EnterState(transition);

		_camTarget = new GameObject();
		_camTarget.transform.position = GameData.Player.transform.position;

		_idol = GameObject.Find("Idol");
		_originalIdolPosition = _idol.transform.position;
		_idol.GetComponent<AudioSource>().Play();

		Camera.main.GetComponent<FollowCamera>().Target = _camTarget.transform;

		PlayEndMusic(_winMusic);
	}

	public override void ExitState(FSMTransition transition) {
		Camera.main.GetComponent<FollowCamera>().Target = GameData.Player.transform;

		GameObject.Destroy(_camTarget);

		_idol.transform.position = _originalIdolPosition;

		foreach(GameObject monk in GameData.Monks) {
			Vector3 monkPos = monk.transform.position;
			monk.transform.position = new Vector3(monkPos.x, 0, monkPos.z);
		}

		base.ExitState(transition);
	}

	public override void Update() {
		base.Update();

		Vector3 playerPos = GameData.Player.transform.position;
		GameData.Player.transform.position = new Vector3(playerPos.x, playerPos.y + MONK_FLY_SPEED, playerPos.z);

		foreach(GameObject monk in GameData.Monks) {
			Vector3 monkPos = monk.transform.position;
			monk.transform.position = new Vector3(monkPos.x, monkPos.y + MONK_FLY_SPEED, monkPos.z);
		}

		Vector3 idolPos = _idol.transform.position;

		if(idolPos.y < 0) {
			_idol.transform.position = new Vector3(idolPos.x + SHAKE_AMOUNT * (_shake ? -1 : 1), 
												   idolPos.y + IDOL_RISE_SPEED, 
				                                   idolPos.z);

			_shake = !_shake;
		} else {
			_idol.GetComponent<AudioSource>().Stop();
		}

		if(_light && _light.intensity < 2) {
			_light.intensity += LIGHT_INTENSITY_GROW;
		}
	}
}
