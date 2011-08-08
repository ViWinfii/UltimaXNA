﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace UltimaXNA.UILegacy.Gumplings
{
    class TextLabelAscii : Control
    {
        public int Hue = 0;
        public int FontID = 0;
        public string Text = string.Empty;
        Texture2D _texture = null;

        public TextLabelAscii(Control owner, int page)
            : base(owner, page)
        {

        }

        public TextLabelAscii(Control owner, int page, int x, int y, int hue, int fontid, string text)
            : this(owner, page)
        {
            buildGumpling(x, y, hue, fontid, text);
        }

        void buildGumpling(int x, int y, int hue, int fontid, string text)
        {
            Position = new Point2D(x, y);
            Hue = hue;
            FontID = fontid;
            Text = text;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(ExtendedSpriteBatch spriteBatch)
        {
            _texture = Data.ASCIIText.GetTextTexture(Text, FontID);
            spriteBatch.Draw2D(_texture, Position, Hue, true, false);
            base.Draw(spriteBatch);
        }
    }
}
