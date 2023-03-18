using System;
using Sandbox.Common.ObjectBuilders;
using Sandbox.Game;
using Sandbox.Game.Entities;
using Sandbox.ModAPI;
using VRage.Game;
using VRage.Game.Components;
using VRage.Game.ModAPI;
using VRage.Game.VisualScripting;
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
                Vector3D position = Entity.GetPosition();
                Sandbox.Game.MyVisualScriptLogicProvider.CreateExplosion(position, 50f, 100000);



            }
		}
    }
}
