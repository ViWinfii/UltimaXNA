﻿using Microsoft.Xna.Framework;
using UltimaXNA.Core.Rendering;
using UltimaXNA.UltimaWorld;
using UltimaXNA.UltimaWorld.Maps;
using UltimaXNA.UltimaWorld.Controllers;

namespace UltimaXNA.UltimaEntities.EntityViews
{
    class ItemView : AEntityView
    {
        new Item Entity
        {
            get { return (Item)base.Entity; }
        }

        public ItemView(Item item)
            : base(item)
        {
            if (Entity.ItemData.IsWet)
                SortZ += 1;
        }

        private int m_DisplayItemID = -1;

        public override bool Draw(SpriteBatch3D spriteBatch, Vector3 drawPosition, MouseOverList mouseOverList, Map map)
        {
            if (Entity.NoDraw)
                return false;

            // Update Display texture, if necessary.
            if (Entity.DisplayItemID != m_DisplayItemID)
            {
                m_DisplayItemID = Entity.DisplayItemID;
                DrawTexture = UltimaData.ArtData.GetStaticTexture(m_DisplayItemID);
                DrawArea = new Rectangle(DrawTexture.Width / 2 - 22, DrawTexture.Height - 44 + (Entity.Z * 4), DrawTexture.Width, DrawTexture.Height);
                PickType = PickType.PickObjects;
                DrawFlip = false;
            }

            // Update hue vector.
            HueVector = Utility.GetHueVector(Entity.Hue);

            bool drawn = base.Draw(spriteBatch, drawPosition, mouseOverList, map);

            DrawOverheads(spriteBatch, drawPosition, mouseOverList, map, (int)DrawArea.Y - 22);

            return drawn;
        }
    }
}
