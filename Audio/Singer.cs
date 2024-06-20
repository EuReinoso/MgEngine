using MgEngine.Util;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;

#pragma warning disable CS8618
namespace MgEngine.Audio
{
    public static class Singer
    {
        private static Dictionary<object, SoundEffect> _sounds;
        private static Dictionary<object, Song> _musics;
        private static ContentManager _content;

        private static float _masterVolume { get; set; }
        private static float _musicVolume { get; set; }
        private static float _soundEffectsVolume { get; set; }

        public static void Initialize(ContentManager content)
        {
            _masterVolume = 1;
            _musicVolume = 1;
            _soundEffectsVolume = 1;
            _content = content;
            _sounds = new();
            _musics = new();
        }

        public static float MasterVolume 
        { 
            get { return _masterVolume; } 

            set 
            {  
                _masterVolume = MgMath.Clamp(value, 0, 1);
                MediaPlayer.Volume = _musicVolume * _masterVolume;
            } 
        }

        public static float MusicVolume
        {
            get { return _musicVolume; }

            set
            {
                _musicVolume = MgMath.Clamp(value, 0, 1);
                MediaPlayer.Volume = _musicVolume * _masterVolume;
            }
        }

        public static float SoundEffectsVolume
        {
            get { return _soundEffectsVolume; }

            set
            {
                _soundEffectsVolume = MgMath.Clamp(value, 0, 1);
            }
        }

        public static void AddSound(string path, object key)
        {
            var sound = _content.Load<SoundEffect>(path);
            _sounds.Add(key, sound);
        }

        public static void AddMusic(string path, object key)
        {
            var music = _content.Load<Song>(path);
            _musics.Add(key, music);
        }

        public static void PlaySound(object soundKey, float volume = 1, bool validateKey = false)
        {
            if (validateKey && !_sounds.ContainsKey(soundKey))
                return;

            var sound = _sounds[soundKey].CreateInstance();
            sound.Volume = MgMath.Clamp(volume * _soundEffectsVolume * _masterVolume, 0, 1);
            sound.Play();
        }

        public static void PlayMusic(object musicKey, bool repeat = true)
        {
            MediaPlayer.Volume = _musicVolume * _masterVolume;
            MediaPlayer.Play(_musics[musicKey]);
            MediaPlayer.IsRepeating = repeat;
        }
    }
}
