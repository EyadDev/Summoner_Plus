using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using SummonerPlus.Content.Projectiles;
using System.Net.Http.Headers;
using Terraria.Audio;
using Terraria.Chat;
using Terraria.Chat.Commands;

namespace SummonerPlus.Content.Buffs
{
    public class LesserSpectralFlame : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // This allows the debuff to be inflicted on NPCs that would otherwise be immune to all debuffs.
            BuffID.Sets.IsAnNPCWhipDebuff[Type] = true;
            DisplayName.SetDefault("Lesser Spectral flame"); // Buff display name
            Description.SetDefault("the heat of lost souls"); // Buff description
            Main.debuff[Type] = true;  // Is it a debuff?
            Main.pvpBuff[Type] = true; // Players can give other players buffs, which are listed as pvpBuff
        }

        public override void Update(NPC npc, ref int buffIndex)
        {

            npc.GetGlobalNPC<LesserSpectralFlameNPC>().Lesserspectralburn = true;

            Dust.NewDust(npc.position, npc.width, npc.height, DustID.DungeonSpirit, 0, 0, 0, default, 1.2f);
        }
    }

    public class LesserSpectralFlameNPC : GlobalNPC
    {
        public override void UpdateLifeRegen(NPC npc, ref int damage)
        {
            if (Lesserspectralburn)
            {
                npc.lifeRegen -= 80;
                damage = 40;
            }
        }

        // This is required to store information on entities that isn't shared between them.
        public override bool InstancePerEntity => true;


        public bool Lesserspectralburn;

        public override void ResetEffects(NPC npc)
        {
            Lesserspectralburn = false;
        }
    }
}
