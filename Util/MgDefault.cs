using MgEngine.Font;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Metadata;

namespace MgEngine.Util
{
    public class MgDefault
    {
        public static void Initialize(ContentManager content)
        {
            Font = new(content, "Font/monogram", new() { 8, 9, 10, 11, 12, 13, 14, 15 });
            ButtonTexture = content.Load<Texture2D>("UI/Default/Button");
        }

        public static FontGroup Font;

        public static Texture2D ButtonTexture; 
    }
}
