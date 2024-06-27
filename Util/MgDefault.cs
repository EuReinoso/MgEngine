using MgEngine.Font;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System;
using MgEngine.UI;


#pragma warning disable CS8618
namespace MgEngine.Util
{
    public class MgDefault
    {
        public static void Initialize(ContentManager content)
        {
            Font = new(content, "Font/monogram", new() { 8, 9, 10, 11, 12, 13, 14, 15 });
            FontSize = 11;
            ButtonTexture = content.Load<Texture2D>("UI/Default/Button");
            PanelTexture = content.Load<Texture2D>("UI/Default/Panel");
            CircleTexture = content.Load<Texture2D>("Effect/Default/Circle");
            RectTexture = content.Load<Texture2D>("Effect/Default/Rect");
            TriangleTexture = content.Load<Texture2D>("Effect/Default/Triangle");
            Scale = 1;
            Margin = new(3, 3, 3, 3);
        }

        public static float Scale;

        public static FontGroup Font;
        public static int FontSize;

        public static Texture2D ButtonTexture;
        public static Texture2D PanelTexture;
        public static Margin Margin;

        public static Texture2D CircleTexture;
        public static Texture2D RectTexture;
        public static Texture2D TriangleTexture;
    }
}
