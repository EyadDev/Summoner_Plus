using IL.Terraria.ID;
using Steamworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SummonerPlus.Common.Players
{
    internal class FrostBurnInflictionPlayer : ModPlayer
    {
        public override void ResetEffects()
        {
            HasFrostburnInflictionItem = false;
        }

        public bool HasFrostburnInflictionItem;
    }
}
