using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.IO;

public class ChordLibrary {
	private List<List<AudioClip>> _chords = new List<List<AudioClip>>();

	private static ChordLibrary _instance = new ChordLibrary();
	public static ChordLibrary Instance { get { return _instance; } }

	private ChordLibrary() {
		LoadChords();
    }

    public List<AudioClip> GetRandomChord() {
		int randomIndex = UnityEngine.Random.Range(0, _chords.Count - 1);
		return _chords[randomIndex];
    }

	private void LoadChords() {
		DirectoryInfo topDirInfo = new DirectoryInfo("Assets/Resources/Chanting");

		foreach(DirectoryInfo chordDirInfo in topDirInfo.GetDirectories()) {
			List<AudioClip> notes = new List<AudioClip>();

			foreach(FileInfo noteFileInfo in chordDirInfo.GetFiles("*.wav")) {
				String resourceName = "Chanting/" + chordDirInfo.Name + "/" + noteFileInfo.Name.Split('.')[0];
				notes.Add(Resources.Load(resourceName) as AudioClip);
			}

			_chords.Add(notes);
		}
	}
}