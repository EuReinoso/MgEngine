using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#pragma warning disable CS8618
namespace MgEngine.Map
{
    public class SpriteSheet
    {
        private Texture2D _texture;
        private Texture2D[] _sprites;
        private int _cutWidth;
        private int _cutHeight;

        public SpriteSheet(ContentManager content, string path, int cutWidth, int cutHeight)
        {
            _texture = content.Load<Texture2D>(path);
            _cutWidth = cutWidth;
            _cutHeight = cutHeight;

            CutSprites();
        }

        public Texture2D[] Sprites { get { return _sprites; } }

        public Texture2D Texture { get { return _texture; } }

        private void CutSprites()
        {
            int sizeX = _texture.Width / _cutWidth;
            int sizeY = _texture.Height / _cutHeight;

            _sprites = new Texture2D[sizeX * sizeY];

            int i = 0;

                for (int y = 0; y < sizeY; y++)
            {
            for (int x = 0; x < sizeX; x++)
                {
                    var crop = new Rectangle(x * _cutWidth, y * _cutHeight, _cutWidth, _cutHeight);

                    _sprites[i] = CropTexture(_texture, crop);

                    i++;
                }
            }
        }

        private Texture2D CropTexture(Texture2D original, Rectangle crop)
        {
            var croppedTexture = new Texture2D(original.GraphicsDevice, crop.Width, crop.Height, false, original.Format);

            var data = new Color[crop.Width * crop.Height];

            var originalData = new Color[original.Width * original.Height];
            original.GetData(originalData);

            for (int x = 0; x < crop.Width; x++)
            {
                for (int y = 0; y < crop.Height; y++)
                {
                    int croppedIndex = y * crop.Width + x;
                    int originalIndex = (y + crop.Y) * original.Width + (x + crop.X);
                    data[croppedIndex] = originalData[originalIndex];
                }
            }

            croppedTexture.SetData(data);

            return croppedTexture;
        }


    }
}
