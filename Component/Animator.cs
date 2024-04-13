using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MgEngine.Component
{
    public class Animator
    {
        private ContentManager _content;
        private AnimationManager _animations;
        private Dictionary<object, Texture2D> _textures;

        public Animator(ContentManager content) 
        { 
            _content = content;
            _animations = new();
            _textures = new();
        }

        public void Update(float dt)
        {
            _animations.Update(dt);
        }

        public void Add(string path, object actionKey, int frameWidth, int frameHeight, List<int> frameTimeList, int row = 1)
        {
            Texture2D texture = _content.Load<Texture2D>(path);

            _textures.Add(actionKey, texture);
            _animations.AddAnimation(actionKey, frameWidth, frameHeight, frameTimeList, row);
        }

        public void SetAction(object actionKey)
        {
            _animations.SetAnimation(actionKey);
        }

        public Texture2D GetTexture(object actionKey)
        {
            return _textures[actionKey];
        }

        public Rectangle GetCurrentFrame()
        {
            return _animations.CurrentAnimation.CurrentFrame;
        }

    }
}
