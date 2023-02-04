using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using SummonerPlus.Content.Projectiles;

namespace SummonerPlus.Content.Buffs
{
	public class ChlorophyteSlashDebuff : ModBuff
	{
		public override void SetStaticDefaults() {
			// This allows the debuff to be inflicted on NPCs that would otherwise be immune to all debuffs.
			BuffID.Sets.IsAnNPCWhipDebuff[Type] = true;
		}

		public override void Update(NPC npc, ref int buffIndex) 
		{
			npc.GetGlobalNPC<ChlorophyteSlashDebuffNPC>().markedByChlorophyteSlash = true;
			npc.GetGlobalNPC<ChlorophyteSlashDebuffNPC>().SetBuffIndex(buffIndex);
        }
	}

	public class ChlorophyteSlashDebuffNPC : GlobalNPC
	{
		// This is required to store information on entities that isn't shared between them.
		public override bool InstancePerEntity => true;

		public bool markedByChlorophyteSlash;
		int buffIndex;

		public void SetBuffIndex(int buffIndexToBeSet)
		{
			buffIndex = buffIndexToBeSet;
		}

        public override void ResetEffects(NPC npc) {
            markedByChlorophyteSlash = false;
        }
        public override void OnHitByProjectile(NPC npc, Projectile projectile, int damage, float knockback, bool crit)
        {

            if (markedByChlorophyteSlash && npc.type != NPCID.TargetDummy && (projectile.minion || ProjectileID.Sets.MinionShot[projectile.type]))
            {
                Projectile spawnedProj = new Projectile();

                Random rand = new Random();

				for (int i = 0; i < rand.Next(1, 4); i++)
				{
					spawnedProj = Main.projectile[Projectile.NewProjectile(projectile.GetSource_FromThis(), npc.position, new Vector2((float)rand.NextDouble() * MakeNegOrPos() * 5, (float)rand.NextDouble() * MakeNegOrPos() * 5), ProjectileID.SporeCloud, 40, 0.5f, projectile.owner)];
					spawnedProj.friendly = true;
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
