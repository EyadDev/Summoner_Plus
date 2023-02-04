using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using SummonerPlus.Content.Projectiles;
using IL.Terraria.GameContent.UI.Elements;

namespace SummonerPlus.Content.Buffs
{
	public class StarStrikerDebuff : ModBuff
	{
		public override void SetStaticDefaults() {
			// This allows the debuff to be inflicted on NPCs that would otherwise be immune to all debuffs.
			BuffID.Sets.IsAnNPCWhipDebuff[Type] = true;
		}

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<StarStrikerDebuffNPC>().markedByStarStriker = true;
            npc.GetGlobalNPC<StarStrikerDebuffNPC>().SetBuffIndex(buffIndex);
        }
	}

	public class StarStrikerDebuffNPC : GlobalNPC
	{
		// This is required to store information on entities that isn't shared between them.
		public override bool InstancePerEntity => true;

		public bool markedByStarStriker;
		int buffIndex;

		public void SetBuffIndex(int buffIndexToBeSet)
		{
			buffIndex = buffIndexToBeSet;
		}

        public override void ResetEffects(NPC npc) {
            markedByStarStriker = false;
        }
        public override void OnHitByProjectile(NPC npc, Projectile projectile, int damage, float knockback, bool crit)
        {

            if (markedByStarStriker && npc.type != NPCID.TargetDummy && (projectile.minion || ProjectileID.Sets.MinionShot[projectile.type]))
            {
                Projectile spawnedProj = new Projectile();

                Random rand = new Random();

				for (int i = 0; i < 2; i++)
				{
					float PosX = npc.position.X + rand.Next(-50, 50);

					spawnedProj = Main.projectile[Projectile.NewProjectile(projectile.GetSource_FromThis(), new Vector2(PosX, Main.player[projectile.owner].position.Y - 600), new Vector2((float)(rand.NextDouble() * MakeNegOrPos() * 3f), 10f), ProjectileID.HallowStar, 10, 1, projectile.owner)];
					spawnedProj.friendly = true;
					spawnedProj.ArmorPenetration = 10;
                }

                npc.DelBuff(buffIndex);
            }
        }
        int MakeNegOrPos()
        {
            Random rand = new Random();

            if (rand.Next(0, 2) == 0)
            {
                return 1;
            }
            else
            {
                return -1;
            }
        }
    }
}
