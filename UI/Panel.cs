﻿using MgEngine.Input;
using MgEngine.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
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

        private HorizontalAlign _horizontalAlign;
        private VerticalAlign _verticalAlign;
        private Orientation _orientation;
        private SizeMode _sizeMode;

        public Color BackGroundColor { get; set; }

        public Panel() : base(MgDefault.PanelTexture)
        {
            Initialize();
        }

        public Panel(Texture2D texture) : base(texture)
        {
            Initialize();
        }

        #region Properties
        public HorizontalAlign HorizontalAlign
        {
            get { return _horizontalAlign; }
            set { _horizontalAlign = value; AlocateWidgets(); }
        }

        public VerticalAlign VerticalAlign
        {
            get { return _verticalAlign; }
            set { _verticalAlign = value; AlocateWidgets(); }
        }

        public Orientation Orientation
        {
            get { return _orientation; }
            set { _orientation = value; AlocateWidgets(); }
        }

        public SizeMode SizeMode
        {
            get { return _sizeMode; }
            set { _sizeMode = value; AlocateWidgets(); }
        }
        #endregion

        private void Initialize()
        {
            _widgets = new();

            _horizontalAlign = HorizontalAlign.Left;
            _verticalAlign = VerticalAlign.Top;
            _orientation = Orientation.Vertical;
            _sizeMode = SizeMode.Auto;
            BackGroundColor = Color.Transparent;

            OnResize += () => { AlocateWidgets(); };
            OnMove += () => { AlocateWidgets(); };
        }

        public void AddWidget(Widget widget)
        {
            widget.OnResize += () => { AlocateWidgets(); };

            _widgets.Add(widget);

            AlocateWidgets();
        }

        public void RemoveWidget(Widget widget)
        {
            _widgets.Remove(widget);
        }

        public override void Update(Inputter inputter)
        {
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
        
        private void AlocateWidgets()
        {
            bool disableCenter = (HorizontalAlign == HorizontalAlign.Center && Orientation == Orientation.Horizontal);

            float x = 0;
            if (HorizontalAlign == HorizontalAlign.Left || disableCenter)
                x = Rect.Left;
            else if (HorizontalAlign == HorizontalAlign.Right)
                x = Rect.Right;
            else if (HorizontalAlign == HorizontalAlign.Center)
                x = Rect.X;

            float y = 0;
            if (VerticalAlign == VerticalAlign.Top)
                y = Rect.Top;
            else if (VerticalAlign == VerticalAlign.Bottom)
                y = Rect.Bottom;

            int width = 0;
            int height = 0;

            List<Widget> widgetList = ReverseByOrientation(_widgets, disableCenter);

            foreach (var widget in widgetList)
            {
                CalculateWidgetPosition(disableCenter, x, y, widget);

                if (Orientation == Orientation.Horizontal)
                {
                    if (HorizontalAlign == HorizontalAlign.Left || disableCenter)
                        x += widget.Width + widget.Margin.Left;
                    else if (HorizontalAlign == HorizontalAlign.Right)
                        x -= widget.Width + widget.Margin.Left;

                    width += widget.Width + widget.Margin.Width;

                    if (widget.Height + widget.Margin.Height > height)
                        height = widget.Height + widget.Margin.Height;
                }
                else
                {

                    if (VerticalAlign == VerticalAlign.Top)
                        y += widget.Height + widget.Margin.Bottom;
                    else if (VerticalAlign == VerticalAlign.Bottom)
                        y -= widget.Height + widget.Margin.Top;

                    height += widget.Height + widget.Margin.Height;

                    if (widget.Width + widget.Margin.Width > width)
                        width = widget.Width + widget.Margin.Width;
                }
            }

            if (SizeMode == SizeMode.Auto)
            {
                Width = width;
                Height = height;
            }
        }

        private List<Widget> ReverseByOrientation(List<Widget> widgetList, bool disableCenter)
        {
            if (Orientation == Orientation.Horizontal && HorizontalAlign == HorizontalAlign.Left || disableCenter)
                return widgetList;

            if (VerticalAlign == VerticalAlign.Top && Orientation != Orientation.Vertical)
                return widgetList.Reverse<Widget>().ToList();

            if (VerticalAlign == VerticalAlign.Bottom)
                return widgetList.Reverse<Widget>().ToList();

            return widgetList;
        }

        private void CalculateWidgetPosition(bool disableCenter, float x, float y, Widget widget)
        {
            if (HorizontalAlign == HorizontalAlign.Left || disableCenter)
                widget.X = x + widget.Margin.Left + widget.Width / 2;
            else if (HorizontalAlign == HorizontalAlign.Right)
                widget.X = x - widget.Margin.Right - widget.Width / 2;
            else if (HorizontalAlign == HorizontalAlign.Center)
                widget.X = x;

            if (VerticalAlign == VerticalAlign.Top)
                widget.Y = y + widget.Margin.Top + widget.Height / 2;
            else if (VerticalAlign == VerticalAlign.Bottom)
                widget.Y = y - widget.Margin.Bottom - widget.Height / 2;
        }
    }
}
