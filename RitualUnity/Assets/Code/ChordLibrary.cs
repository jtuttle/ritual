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
		List<AudioClip> notes = new List<AudioClip>();
		notes.Add(Resources.Load("Chanting/AbMajadd6/1_Ab3") as AudioClip);
		notes.Add(Resources.Load("Chanting/AbMajadd6/2_F4") as AudioClip);
		notes.Add(Resources.Load("Chanting/AbMajadd6/3_Ab4") as AudioClip);
		notes.Add(Resources.Load("Chanting/AbMajadd6/4_C5") as AudioClip);
		notes.Add(Resources.Load("Chanting/AbMajadd6/5_Eb5") as AudioClip);
		notes.Add(Resources.Load("Chanting/AbMajadd6/6_F5") as AudioClip);
		_chords.Add(notes);

		notes = new List<AudioClip>();
		notes.Add(Resources.Load("Chanting/Aminadd4/1_D4") as AudioClip);
		notes.Add(Resources.Load("Chanting/Aminadd4/2_A4") as AudioClip);
		notes.Add(Resources.Load("Chanting/Aminadd4/3_C5") as AudioClip);
		notes.Add(Resources.Load("Chanting/Aminadd4/4_D5") as AudioClip);
		notes.Add(Resources.Load("Chanting/Aminadd4/5_E5") as AudioClip);
		notes.Add(Resources.Load("Chanting/Aminadd4/6_A5") as AudioClip);
		_chords.Add(notes);

		notes = new List<AudioClip>();
		notes.Add(Resources.Load("Chanting/BbMaj7/1_Bb3") as AudioClip);
		notes.Add(Resources.Load("Chanting/BbMaj7/2_D4") as AudioClip);
		notes.Add(Resources.Load("Chanting/BbMaj7/3_Bb4") as AudioClip);
		notes.Add(Resources.Load("Chanting/BbMaj7/4_D5") as AudioClip);
		notes.Add(Resources.Load("Chanting/BbMaj7/5_A5") as AudioClip);
		notes.Add(Resources.Load("Chanting/BbMaj7/6_F6") as AudioClip);
		_chords.Add(notes);

		notes = new List<AudioClip>();
		notes.Add(Resources.Load("Chanting/Cdom7/1_E4") as AudioClip);
		notes.Add(Resources.Load("Chanting/Cdom7/2_G4") as AudioClip);
		notes.Add(Resources.Load("Chanting/Cdom7/3_C5") as AudioClip);
		notes.Add(Resources.Load("Chanting/Cdom7/4_G5") as AudioClip);
		notes.Add(Resources.Load("Chanting/Cdom7/5_Bb5") as AudioClip);
		notes.Add(Resources.Load("Chanting/Cdom7/6_C6") as AudioClip);
		_chords.Add(notes);

		notes = new List<AudioClip>();
		notes.Add(Resources.Load("Chanting/CMaj/1_C4") as AudioClip);
		notes.Add(Resources.Load("Chanting/CMaj/2_E4") as AudioClip);
		notes.Add(Resources.Load("Chanting/CMaj/3_G4") as AudioClip);
		notes.Add(Resources.Load("Chanting/CMaj/4_C5") as AudioClip);
		notes.Add(Resources.Load("Chanting/CMaj/5_E5") as AudioClip);
		notes.Add(Resources.Load("Chanting/CMaj/6_G5") as AudioClip);
		_chords.Add(notes);

		notes = new List<AudioClip>();
		notes.Add(Resources.Load("Chanting/Dmin7/1_C4") as AudioClip);
		notes.Add(Resources.Load("Chanting/Dmin7/2_D4") as AudioClip);
		notes.Add(Resources.Load("Chanting/Dmin7/3_A4") as AudioClip);
		notes.Add(Resources.Load("Chanting/Dmin7/4_F5") as AudioClip);
		notes.Add(Resources.Load("Chanting/Dmin7/5_A5") as AudioClip);
		notes.Add(Resources.Load("Chanting/Dmin7/6_D6") as AudioClip);
		_chords.Add(notes);
	}
}