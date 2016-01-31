using System;
using System.Collections.Generic;

using UnityEngine;

public class GameData {
	public static GameObject Player { get; set; }
	public static List<GameObject> Monks { get; set; }
	public static List<AudioClip> Notes { get; set; }
	public static int PlayerNote { get; set; }

	public static List<GameObject> MonkAnimPrototypes;

	public static void LoadMonkAnimPrototypes() {
		MonkAnimPrototypes = new List<GameObject>();

		for(int i = 0; i < 4; i++) {
			string monkAnimPath = "Prefabs/MonkAnims/MonkAnim" + (i + 1);
			MonkAnimPrototypes.Add(Resources.Load(monkAnimPath) as GameObject);
		}
	}
}
