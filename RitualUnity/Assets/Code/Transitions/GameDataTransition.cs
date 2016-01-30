using System;
using System.Collections;

using UnityEngine;

public class GameDataTransition : FSMTransition {
	public GameData GameData { get; private set; }

	public GameDataTransition(Enum nextStateId, GameData gameData)
		: base(nextStateId, false) {
			
		GameData = gameData;
	}
}
