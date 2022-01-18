using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna;
using static Terraria.ModLoader.ModContent;



namespace dmod.NPCS
{
    public class MalfunctioningDrone : ModNPC
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Malfuntioning Drone");//Test enemy
            Main.npcFrameCount[npc.type] = 10;
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {

            return SpawnCondition.OverworldDay.Chance * 0.3f;

        }

        public override void SetDefaults()
        {
            npc.width = 14;
            npc.height = 8;
            npc.aiStyle = 14;
            npc.damage = 30;
            npc.defense = 15;
            npc.lifeMax = 100;
            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCDeath14;
            npc.knockBackResist = 0.2f;
            npc.value = 112f;
            aiType = NPCID.Hellbat;
            animationType = ProjectileID.Typhoon;
            

        }

      
    }

    
}