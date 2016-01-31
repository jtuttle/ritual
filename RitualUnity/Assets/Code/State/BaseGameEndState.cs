using UnityEngine;
using System.Collections;

public class BaseGameEndState : FSMState {
	private AudioSource _source;

	protected FollowCamera _followCam;
	private float _originalDistance;

	protected Light _light;
	private float _originalLightIntensity;

	private float CAMERA_ZOOM_SPEED = 0.02f;

	public BaseGameEndState(GameState gameState)
		: base(gameState) {

	}

	public override void EnterState(FSMTransition transition) {
		base.EnterState(transition);

		_followCam = Camera.main.GetComponent<FollowCamera>();
		_originalDistance = _followCam.Distance;

		_light = (Light)GameObject.Find("Directional Light").GetComponent<Light>();
		_originalLightIntensity = _light.intensity;

		SilenceMonks();
		SilenceMusic();
	}

	public override void ExitState(FSMTransition transition) {
		_followCam.Distance = _originalDistance;

		_source = null;

		_light.intensity = _originalLightIntensity;
		_light = null;

		base.ExitState(transition);
	}

	public override void Update() {
		if(_source && !_source.isPlaying)
			ExitState(new FSMTransition(GameState.GameReset));

		if(_followCam)
			_followCam.Distance += CAMERA_ZOOM_SPEED;
	}

	private void SilenceMonks() {
		foreach(GameObject monk in GameData.Monks) {
			monk.GetComponent<AudioSource>().Stop();
		}
	}

	private void SilenceMusic() {
		Camera.main.GetComponent<AudioSource>().volume = 0;
	}

	protected void PlayEndMusic(AudioClip music) {
		_source = GameData.Player.GetComponent<AudioSource>();
		_source.clip = music;
		_source.volume = 1;
		_source.loop = false;
		_source.Play();
	}
}
