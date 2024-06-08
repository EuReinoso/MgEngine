using MgEngine.Input;
using MgEngine.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MgEngine.UI.UITypes;

#pragma warning disable CS8618
namespace MgEngine.UI
{
    public class Panel : Widget
    {
        private List<Widget> _widgets;
        public HorizontalAlign HorizontalAlign { get; set; }
        public VerticalAlign VerticalAlign { get; set; }
        public Orientation Orientation { get; set; }
        public SizeMode SizeMode { get; set; }

        public Color BackGroundColor { get; set; }

        public Panel() : base(MgDefault.PanelTexture)
        {
            Initialize();
        }

        public Panel(Texture2D texture) : base(texture)
        {
            Initialize();
        }

        private void Initialize()
        {
            _widgets = new();

            HorizontalAlign = HorizontalAlign.Left;
            VerticalAlign = VerticalAlign.Top;
            Orientation = Orientation.Vertical;
            SizeMode = SizeMode.Auto;
            BackGroundColor = Color.Transparent;
        }

        public void AddWidget(Widget widget)
        {
            _widgets.Add(widget);
        }

        public void RemoveWidget(Widget widget)
        {
            _widgets.Remove(widget);
        }

        public override void Update(Inputter inputter)
        {
            CalculatePositions();

            foreach(var widget in _widgets)
            {
                widget.IsEnabled = IsEnabled;

                widget.Update(inputter);
            }
        }

        public override void Draw(SpriteBatch spriteBatch, float scrollX = 0, float scrollY = 0)
        {
            ColorEffect = BackGroundColor;

            base.Draw(spriteBatch, scrollX, scrollY);

            foreach (var widget in _widgets)
            {
                widget.Draw(spriteBatch, scrollX, scrollY);
            }
        }

        private void CalculatePositions()
        {
            float xAbs = Rect.Left;
            float yAbs = Rect.Top;

            int widthAbs = 0;
            int heightAbs = 0;

            foreach(var widget in _widgets)
            {
                widget.X = xAbs + widget.Margin.Left + widget.Width / 2;
                widget.Y = yAbs + widget.Margin.Top + widget.Height / 2;

                if (Orientation == Orientation.Horizontal)
                {
                    xAbs += widget.Width + widget.Margin.Left;

                    widthAbs += widget.Width + widget.Margin.Width;

                    if (widget.Height + widget.Margin.Height > heightAbs)
                        heightAbs = widget.Height + widget.Margin.Height;
                }
                else
                {
                    yAbs += widget.Height + widget.Margin.Bottom;

                    heightAbs += widget.Height + widget.Margin.Height;

                    if (widget.Width + widget.Margin.Width > widthAbs)
                        widthAbs = widget.Width + widget.Margin.Width;
                }

            }

            if (SizeMode == SizeMode.Auto)
            {
                Width = widthAbs;
                Height = heightAbs;
            }

        }
    }
}
