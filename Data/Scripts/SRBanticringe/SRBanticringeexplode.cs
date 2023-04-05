using Sandbox.Common.ObjectBuilders;
using Sandbox.Game;
using Sandbox.Game.Entities;
using Sandbox.Game.Entities.Blocks;
using Sandbox.ModAPI;
using System;
using VRage.Game.Components;
using VRage.ModAPI;
using VRage.ObjectBuilders;
using VRageMath;


namespace SRBanticringe
{
    [MyEntityComponentDescriptor(typeof(MyObjectBuilder_ConveyorSorter), false, "SC_SRB")]
    public class SRBDetector : MyGameLogicComponent
    {
        private IMyConveyorSorter SRB;
        private int updateCounter = 0;
        private const int UPDATE_INTERVAL = 60; // Check once per second

        public override void Init(MyObjectBuilder_EntityBase objectBuilder)
        {
            NeedsUpdate = MyEntityUpdateEnum.BEFORE_NEXT_FRAME;
        }

        public override void UpdateOnceBeforeFrame()
        {
            SRB = (IMyConveyorSorter)Entity;
            NeedsUpdate = MyEntityUpdateEnum.EACH_10TH_FRAME;
        }

        public override void UpdateBeforeSimulation10()
        {
            updateCounter++;

            if (updateCounter < UPDATE_INTERVAL)
            {
                return;
            }

            updateCounter = 0;

            if (!SRB.Enabled)
            {
                // Turn on the block

                //SRB.Enabled = true; 
                //MyAPIGateway.Utilities.ShowNotification("The solid fuel is still burning.", 10000, "Green");

                //Or it could just fucking explode

                //MyAPIGateway.Utilities.ShowNotification("An SRB has detonated from overpressure.", 10000, "Red");

                /*  Vector3D myPos = Entity.GetPosition();
                  float dmg = 10000f;
                  float radius = 100f;

                
                  //Explosion Damage!
                  BoundingSphereD sphere = new BoundingSphereD(myPos, radius);
                  MyExplosionInfo bomb = new MyExplosionInfo(dmg, dmg, sphere, MyExplosionTypeEnum.MISSILE_EXPLOSION, true, true);
                  bomb.CreateParticleEffect = false;
                  bomb.LifespanMiliseconds = 150 + (int)radius * 45;
                  MyExplosions.AddExplosion(ref bomb, true);
                */


                Vector3D myPos = Entity.GetPosition();
                double radius = 100;
                BoundingSphereD explosionSphere = new BoundingSphereD(myPos, radius);
                MyExplosionInfo explosionInfo = new MyExplosionInfo()
                {
                    PlayerDamage = 0f,
                    Damage = 10f,
                    ExplosionType = MyExplosionTypeEnum.GRID_DESTRUCTION,
                    ExplosionSphere = explosionSphere,
                    LifespanMiliseconds = 700,
                    ParticleScale = 1f,
                    OwnerEntity = null, 
                    ExplosionFlags = MyExplosionFlags.AFFECT_VOXELS | MyExplosionFlags.APPLY_FORCE_AND_DAMAGE | MyExplosionFlags.CREATE_DECALS | MyExplosionFlags.CREATE_PARTICLE_EFFECT | MyExplosionFlags.CREATE_SHRAPNELS | MyExplosionFlags.APPLY_DEFORMATION | MyExplosionFlags.CREATE_PARTICLE_DEBRIS,
                    VoxelCutoutScale = 1f,
                    PlaySound = true,
                    ApplyForceAndDamage = true,
                    KeepAffectedBlocks = false,
                    StrengthImpulse = 0.0f
                };

                MyExplosions.AddExplosion(ref explosionInfo);




            }
        }
    }
}
