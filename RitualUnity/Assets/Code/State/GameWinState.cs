using System.Collections;

using UnityEngine;

public class GameWinState : BaseGameEndState {
	private AudioClip _winMusic;

	private float FLY_SPEED = 0.03f;

	public GameWinState()
		: base(GameState.GameWin) {

	}

	public override void InitState(FSMTransition transition) {
		base.InitState(transition);

		_winMusic = Resources.Load("Music/Victory Music") as AudioClip;
	}

	public override void EnterState(FSMTransition transition) {
		base.EnterState(transition);

		Camera.main.GetComponent<FollowCamera>().enabled = false;

		PlayEndMusic(_winMusic);
	}

	public override void ExitState(FSMTransition transition) {
		Camera.main.GetComponent<FollowCamera>().enabled = true;

		foreach(GameObject monk in GameData.Monks) {
			Vector3 monkPos = monk.transform.position;
			monk.transform.position = new Vector3(monkPos.x, 0, monkPos.z);
		}

		base.ExitState(transition);
	}

	public override void Update() {
		base.Update();

		foreach(GameObject monk in GameData.Monks) {
			Vector3 monkPos = monk.transform.position;
			monk.transform.position = new Vector3(monkPos.x, monkPos.y + FLY_SPEED, monkPos.z);
		}
	}
}
