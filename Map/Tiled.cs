using MgEngine.Component;
using MgEngine.Util;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

#pragma warning disable CS8618
namespace MgEngine.Map
{
    public class Tiled
    {
        Map _map;
        Dictionary<string, List<Entity>> _layers;
        SpriteSheet _spritesheet;

        #region Classes
        public class Map
        {
            [JsonPropertyName("compressionlevel")]
            public int CompressionLevel { get; set; }

            [JsonPropertyName("editorsettings")]
            public EditorSettings EditorSettings { get; set; }

            [JsonPropertyName("height")]
            public int Height { get; set; }

            [JsonPropertyName("infinite")]
            public bool Infinite { get; set; }

            [JsonPropertyName("layers")]
            public List<Layer> Layers { get; set; }

            [JsonPropertyName("nextlayerid")]
            public int NextLayerId { get; set; }

            [JsonPropertyName("nextobjectid")]
            public int NextObjectId { get; set; }

            [JsonPropertyName("orientation")]
            public string Orientation { get; set; }

            [JsonPropertyName("renderorder")]
            public string RenderOrder { get; set; }

            [JsonPropertyName("tiledversion")]
            public string TiledVersion { get; set; }

            [JsonPropertyName("tileheight")]
            public int TileHeight { get; set; }

            [JsonPropertyName("tilesets")]
            public List<Tileset> Tilesets { get; set; }

            [JsonPropertyName("tilewidth")]
            public int TileWidth { get; set; }

            [JsonPropertyName("type")]
            public string Type { get; set; }

            [JsonPropertyName("version")]
            public string Version { get; set; }

            [JsonPropertyName("width")]
            public int Width { get; set; }
        }

        public class EditorSettings
        {
            [JsonPropertyName("chunksize")]
            public ChunkSize ChunkSize { get; set; }
        }

        public class ChunkSize
        {
            [JsonPropertyName("height")]
            public int Height { get; set; }

            [JsonPropertyName("width")]
            public int Width { get; set; }
        }

        public class Layer
        {
            [JsonPropertyName("class")]
            public string Class { get; set; }

            [JsonPropertyName("data")]
            public List<int> Data { get; set; }

            [JsonPropertyName("height")]
            public int Height { get; set; }

            [JsonPropertyName("id")]
            public int Id { get; set; }

            [JsonPropertyName("name")]
            public string Name { get; set; }

            [JsonPropertyName("opacity")]
            public float Opacity { get; set; }

            [JsonPropertyName("type")]
            public string Type { get; set; }

            [JsonPropertyName("visible")]
            public bool Visible { get; set; }

            [JsonPropertyName("width")]
            public int Width { get; set; }

            [JsonPropertyName("x")]
            public int X { get; set; }

            [JsonPropertyName("y")]
            public int Y { get; set; }
        }

        public class Tileset
        {
            [JsonPropertyName("firstgid")]
            public int FirstGid { get; set; }

            [JsonPropertyName("source")]
            public string Source { get; set; }
        }
        #endregion

        public Tiled(SpriteSheet spritesheet)
        {
            _spritesheet = spritesheet;
        }

        public List<Entity> GetLayer(string layerKey)
        {
            return _layers[layerKey];
        }

        public void Draw(SpriteBatch spriteBatch, float scrollX = 0, float scrollY = 0)
        {
            foreach (var layer in _layers.Values)
            {
                foreach (var entity in layer)
                {
                    entity.Draw(spriteBatch, scrollX, scrollY);
                }
            }
        }

        public void ReadMap(string path)
        {
            _map = JsonSerializer.Deserialize<Map>(File.ReadAllText(path));

            if (_map is null)
                throw new Exception("Invalid Map!");

            _layers = new();

            foreach (var layer in _map.Layers)
            {
                var entities = new List<Entity>();

                int y = 0;
                int x = 0;
                foreach (int tile in layer.Data)
                {
                    if (tile - 1 >= 0)
                    {
                        var ent = (Entity)Activator.CreateInstance(typeof(Entity), _spritesheet.Sprites[tile - 1]);

                        ent.X = x * _map.TileWidth * MgDefault.Scale;
                        ent.Y = y * _map.TileHeight * MgDefault.Scale;

                        entities.Add(ent);
                    }

                    x++;

                    if (x >= layer.Width)
                    {
                        x = 0;
                        y++;
                    }
                }

                _layers.Add(layer.Name, entities);
            }
        }

    }
}