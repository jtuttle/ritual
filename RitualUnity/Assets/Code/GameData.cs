using System;
using System.Collections.Generic;

using UnityEngine;

public class GameData {
	public List<AudioClip> Notes { get; private set; }
	public int PlayerNote { get; private set; }
	public List<GameObject> Monks { get; private set; }

	public GameData(List<AudioClip> notes, int playerNote, List<GameObject> monks) {
		Monks = monks;
		Notes = notes;
		PlayerNote = playerNote;
	}
}
