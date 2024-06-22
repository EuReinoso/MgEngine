using MgEngine.Component;
using MgEngine.Input;
using MgEngine.Interface;
using MgEngine.Screen;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace MgEngine.Util
{
    public static class MgUtil
    {
        public static Texture2D GetTexture2D(ContentManager content, string path)
        {
            return content.Load<Texture2D>(path);
        }

        public static Color RandomColor()
        {
            return new Color(new Random().Next(0, 255), new Random().Next(0, 255), new Random().Next(0, 255));
        }

        public static Vector2 RandomWindowPos(Window window, int margin = 0)
        {
            return new Vector2(new Random().Next(0 + margin, window.Width - margin), new Random().Next(0, window.Height - margin));
        }

        public static Vector2 RandomCanvasPos(Canvas canvas, int margin = 0)
        {
            return new Vector2(new Random().Next(0 + margin, canvas.Width - margin), new Random().Next(0, canvas.Height - margin));
        }

        public static Vector2 RandomPos(int minX, int maxX, int minY, int maxY)
        {
            return new Vector2(new Random().Next(minX, maxX), new Random().Next(minY, maxY));
        }

        public static Color ColorLight(Color color, float factor)
        {
            factor = MgMath.Clamp(factor, 0f, 1f);

            int red = (int)(color.R * factor);
            int green = (int)(color.G * factor);
            int blue = (int)(color.B * factor);

            return new Color(red, green, blue, color.A);
        }

        public static Color ColorAlpha(Color color, float factor)
        {
            factor = MgMath.Clamp(factor, 0f, 1f);

            byte newAlpha = (byte)(color.A * factor);

            return new Color(color.R, color.G, color.B, newAlpha);
        }

        public static List<T> Choice<T>(this List<T> originalList, int quant, bool removeFromOriginal = false)
        {
            var rand = new Random();

            var randList = new List<T>();

            for (int i = 0; i < quant; i++)
            {
                int index = rand.Next(originalList.Count);

                randList.Add(originalList[index]);

                if (removeFromOriginal)
                    originalList.RemoveAt(index);
            }

            return randList;
        }

        public static void UpdateList<T>(this List<T> list, Inputter inputter, float dt) where T : IUpdate
        {
            foreach(var item in list)
            {
                item.Update(inputter, dt);
            }
        }

#pragma warning disable CS8602
#pragma warning disable CS8600
        public static T DeepCopy<T>(this T originalObject) where T : new()
        {
            if (originalObject == null)
                return new T();

            var type = originalObject.GetType();

            if (type == null)
                return new T();

            var properties = type.GetProperties();

            T clonedObj = (T)Activator.CreateInstance(type);

            foreach (var property in properties)
            {
                if (property.CanWrite)
                {
                    object value = property.GetValue(originalObject);
                    if (value != null && value.GetType().IsClass && !value.GetType().FullName.StartsWith("System."))
                        property.SetValue(clonedObj, DeepCopy(value));
                    else
                        property.SetValue(clonedObj, value);
                }
            }

            return clonedObj ?? new T();
        }
#pragma warning restore CS8602
#pragma warning restore CS8600

    }
}
