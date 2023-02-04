//using TerrariaPlus.Content.Buffs;
using Terraria.ObjectData;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SummonerPlus.Content.Items;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace SummonerPlus.Content.Tiles
{
	public class SunBarTile : ModTile
	{
		public override void SetStaticDefaults()
		{
			Main.tileSolid[Type] = true;
            Main.tileSolidTop[Type] = true;
            Main.tileShine[Type] = 1100;
            Main.tileFrameImportant[Type] = true;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
            TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.newTile.LavaDeath = false;
            TileObjectData.addTile(Type);

            AddMapEntry(new Color(25, 100, 90));

            //DustType = DustID.SolarFlare;
            //ItemDrop = ModContent.ItemType<SunRockOre>();
            //HitSound = SoundID.Tink;
            //MineResist = 4;
            //MinPick = 210;
        }

        public override bool Drop(int x, int y)
        {
            Tile t = Main.tile[x, y];
            int style = t.TileFrameX / 18;

            switch (style)
            {
                case 0:
                    Item.NewItem(new EntitySource_TileBreak(x, y), x * 16, y * 16, 16, 16, ModContent.ItemType<SunBar>());
                    break;
            }

            return base.Drop(x, y);
        }
    }
}
