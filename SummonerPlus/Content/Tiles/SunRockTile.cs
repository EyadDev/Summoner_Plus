//using TerrariaPlus.Content.Buffs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SummonerPlus.Content.Items;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace SummonerPlus.Content.Tiles
{
	public class SunRockTile : ModTile
	{
		public override void SetStaticDefaults()
		{
			TileID.Sets.Ore[Type] = true;

			Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileShine[Type] = 1000;
            Main.tileShine2[Type] = false;
            Main.tileSpelunker[Type] = true;
            Main.tileOreFinderPriority[Type] = 750;

            AddMapEntry(new Color(25, 100, 100));

            DustType = DustID.SolarFlare;
            ItemDrop = ModContent.ItemType<SunRockOre>();
            HitSound = SoundID.Tink;
            MineResist = 4;
            MinPick = 210;
            
        }

        public override void FloorVisuals(Player player)
        {
            player.AddBuff(BuffID.Burning, 2);
        }
    }
}
