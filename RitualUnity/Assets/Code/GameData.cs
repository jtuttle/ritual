using System;
using System.Collections.Generic;

using UnityEngine;

public class GameData {
	public static GameObject Player { get; private set; }
	public static List<GameObject> Monks { get; private set; }
	public static List<AudioClip> Notes { get; private set; }
	public static int PlayerNote { get; private set; }
}
