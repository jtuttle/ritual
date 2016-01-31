using System.Collections;

using UnityEngine;

public class GameWinState : BaseGameEndState {
	private AudioClip _winMusic;

	private GameObject _camTarget;

	private float FLY_SPEED = 0.08f;

	public GameWinState()
		: base(GameState.GameWin) {

	}

	public override void InitState(FSMTransition transition) {
		base.InitState(transition);

		_winMusic = Resources.Load("Music/Victory Music") as AudioClip;
	}

	public override void EnterState(FSMTransition transition) {
		base.EnterState(transition);

		//Camera.main.GetComponent<FollowCamera>().enabled = false;

		_camTarget = new GameObject();
		_camTarget.transform.position = GameData.Player.transform.position;

		Camera.main.GetComponent<FollowCamera>().Target = _camTarget.transform;
		//_followCam.Distance = 12.0f;

		PlayEndMusic(_winMusic);
	}

	public override void ExitState(FSMTransition transition) {
		//Camera.main.GetComponent<FollowCamera>().enabled = true;
		Camera.main.GetComponent<FollowCamera>().Target = GameData.Player.transform;

		GameObject.Destroy(_camTarget);

		foreach(GameObject monk in GameData.Monks) {
			Vector3 monkPos = monk.transform.position;
			monk.transform.position = new Vector3(monkPos.x, 0, monkPos.z);
		}

		base.ExitState(transition);
	}

	public override void Update() {
		base.Update();

		Vector3 playerPos = GameData.Player.transform.position;
		GameData.Player.transform.position = new Vector3(playerPos.x, playerPos.y + FLY_SPEED, playerPos.z);

		foreach(GameObject monk in GameData.Monks) {
			Vector3 monkPos = monk.transform.position;
			monk.transform.position = new Vector3(monkPos.x, monkPos.y + FLY_SPEED, monkPos.z);
		}
	}
}
