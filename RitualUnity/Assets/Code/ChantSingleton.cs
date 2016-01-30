using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.IO;

namespace Assets.Code
{
    public class ChantClips
    {
        public enum Chant { AbM6, Am4, BbM7, Cd7, CM, Dm7 };

        private List<List<AudioClip>> _chants =
            new List<List<AudioClip>>();

        private static ChantClips _instance = new ChantClips();

        private ChantClips()
        {
            DirectoryInfo topDirInfo = new DirectoryInfo("Resources/Chanting");
            FileInfo[] chordDirs = topDirInfo.GetFiles();
            foreach (FileInfo chordDir in chordDirs)
            {
                DirectoryInfo chordDirInfo = new DirectoryInfo(chordDir.FullName);
                FileInfo[] noteFiles = chordDirInfo.GetFiles();
                List<AudioClip> chordNotes = new List<AudioClip>();
                foreach (FileInfo noteFileInfo in noteFiles)
                { 
                    chordNotes.Add(Resources.Load(noteFileInfo.FullName) as AudioClip);
                }
                _chants.Add(chordNotes);
            }
        }

        public static ChantClips Instance { get { return _instance; } }

        public List<List<AudioClip>> Get()
        {
            return _chants;
        }
    }
}