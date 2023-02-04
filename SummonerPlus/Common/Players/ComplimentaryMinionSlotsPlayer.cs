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
    internal class ComplimentaryMinionSlotsPlayer : ModPlayer
    {
        public float MaxCompSlots = 1;
        public float FilledCompSlots;

        public bool HasBeeSet = false;
        public bool HasBandOfFriendship = false;


        public override void PreUpdate()  
        {
            MaxCompSlots = 1;

            if (HasBeeSet)
            {
                MaxCompSlots += 1;
            }

            if (HasBandOfFriendship)
            {
                MaxCompSlots += 1;
            }
        }

        public override void ResetEffects()
        {
            HasBeeSet = false;
            HasBandOfFriendship = false;
        }
    }
}
