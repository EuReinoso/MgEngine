using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace MgEngine.Component
{
    public class Animator
    {
        private ContentManager _content;
        private AnimationManager _animations;
        private Dictionary<object, Texture2D> _textures;
        private Dictionary<object, Vector2?> _offSets;

        public Animator(ContentManager content) 
        { 
            _content = content;
            _animations = new();
            _textures = new();
            _offSets = new();
        }

        public void Update(float dt)
        {
            _animations.Update(dt);
        }

        public void Add(string path, object actionKey, int frameWidth, int frameHeight, List<int> frameTimeList, int row = 1, Action? onReset = null, bool reverse = false, Vector2? offSet = null)
        {
            Texture2D texture = _content.Load<Texture2D>(path);

            _textures.Add(actionKey, texture);
            _offSets.Add(actionKey, offSet);
            _animations.AddAnimation(actionKey, frameWidth, frameHeight, frameTimeList, row, onReset, reverse);
        }

        public void Add(string path, object actionKey, int frameWidth, int frameHeight, int frameDuration, int row = 1, Action? onReset = null, bool reverse = false, Vector2? offSet = null)
        {
            Texture2D texture = _content.Load<Texture2D>(path);

            var framesList = new List<int>();
            for (int i = 0; i < (int)(texture.Width / frameWidth); i++)
            {
                framesList.Add(frameDuration);
            }

            _textures.Add(actionKey, texture);
            _offSets.Add(actionKey, offSet);
            _animations.AddAnimation(actionKey, frameWidth, frameHeight, framesList, row, onReset, reverse);
        }

        public void SetAction(object actionKey)
        {
            _animations.SetAnimation(actionKey);
        }

        public object CurrentAction { get { return _animations.CurrentAction; } }

        public Texture2D GetTexture(object actionKey)
        {
            return _textures[actionKey];
        }

        public Vector2? GetOffSet(object actionKey)
        {
            return _offSets[actionKey];
        }

        public Rectangle GetCurrentFrame()
        {
            return _animations.CurrentAnimation.CurrentFrame;
        }

    }
}
