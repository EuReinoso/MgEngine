using MgEngine.Util;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.IO;

#pragma warning disable CS8618
namespace MgEngine.Audio
{
    public static class Singer
    {
        private static Dictionary<object, SoundEffect> _sounds;
        private static Dictionary<object, Song> _musics;
        private static Dictionary<object, SoundEffect> _musicEffects;
        private static SoundEffectInstance _musicEffect;
        private static ContentManager _content;
        private static List<SoundEffectInstance> _soundTrack;

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
            _soundTrack = new();
            _musicEffects = new();
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

        public static void Update()
        {
            for (int i = _soundTrack.Count - 1; i >= 0; i--)
            {
                var sound = _soundTrack[i];

                if (sound.State == SoundState.Stopped)
                    _soundTrack.RemoveAt(i);
            }
        }

        public static void SetSpeed(float speed)
        {
            foreach(var sound in _soundTrack)
            {
                sound.Pitch = sound.Pitch * speed;
            }

            if (_musicEffect is not null)
                _musicEffect.Pitch = speed;
        }

        public static void VolumeFlush()
        {
            foreach (var sound in _soundTrack)
            {
                sound.Volume = _soundEffectsVolume * _masterVolume;
            }

            if (_musicEffect is not null)
                _musicEffect.Volume = _musicVolume * _masterVolume;

            MediaPlayer.Volume = _musicVolume * _masterVolume;
        }

        public static void AddAllSound(string path)
        {
            string finalPath = Path.Combine(_content.RootDirectory, path);

            foreach(string file in Directory.GetFiles(finalPath))
            {
                string fileName = Path.GetFileName(file).Replace(".xnb", "");
                string filePath = file.Replace("Content\\", "").Replace(".xnb", "");

                AddSound(filePath, fileName);
            }
        }

        public static void AddAllMusic(string path)
        {
            string finalPath = Path.Combine(_content.RootDirectory, path);

            foreach (string file in Directory.GetFiles(finalPath,"*.ogg"))
            {
                string fileName = Path.GetFileName(file).Replace(".ogg", "");
                string filePath = file.Replace("Content\\", "").Replace(".ogg", "");

                AddMusic(filePath, fileName);
            }

            foreach (string file in Directory.GetFiles(finalPath, "*.xnb"))
            {
                string fileName = Path.GetFileName(file).Replace(".xnb", "");
                string filePath = file.Replace("Content\\", "").Replace(".xnb", "");

                AddMusicEffect(filePath, fileName);
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

        public static void AddMusicEffect(string path, object key)
        {
            var music = _content.Load<SoundEffect>(path);
            _musicEffects.Add(key, music);
        }

        public static void PlaySound(object soundKey, float volume = 1, bool validateKey = false, bool isLooped = false)
        {
            if (validateKey && !_sounds.ContainsKey(soundKey))
                return;

            var sound = _sounds[soundKey].CreateInstance();
            sound.Volume = MgMath.Clamp(volume * _soundEffectsVolume * _masterVolume, 0, 1);
            sound.IsLooped = isLooped;
            sound.Play();
            _soundTrack.Add(sound);
        }

        public static void PlayMusicEffect(object musicKey, float volume = 1, bool isLooped = false)
        {
            if (_musicEffect is not null)
                _musicEffect.Stop();

            _musicEffect = _musicEffects[musicKey].CreateInstance();
            _musicEffect.Volume = MgMath.Clamp(volume * _musicVolume * _masterVolume, 0, 1);
            _musicEffect.IsLooped = isLooped;
            _musicEffect.Play();
        }

        public static void PlayMusic(object musicKey, bool repeat = true)
        {
            MediaPlayer.Volume = _musicVolume * _masterVolume;
            MediaPlayer.Play(_musics[musicKey]);
            MediaPlayer.IsRepeating = repeat;
        }
    }
}
