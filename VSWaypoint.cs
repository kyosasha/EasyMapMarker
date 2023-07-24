using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.API.Common.Entities;
using Vintagestory.API.Config;

namespace VSWaypoint
{
    public class VSWaypoint : ModSystem
    {
        public override void Start(ICoreAPI api)
        {
            base.Start(api);

            // Get the client API
            ICoreClientAPI clientApi = api as ICoreClientAPI;
            if (clientApi == null) return;

            // Get the player
            EntityPlayer player = clientApi.World.Player.Entity;

            // Get the block targeted by the player
            BlockSelection blockSelection = player.CurrentBlockSelection;
            if (blockSelection == null) return;

            Block block = clientApi.World.BlockAccessor.GetBlock(blockSelection.Position);

            // Create a waypoint
            Waypoint waypoint = new Waypoint();
            waypoint.Name = block.Code.ToString(); // You might want to use a more descriptive name
            waypoint.Position = blockSelection.Position;

            // Add the waypoint to the player's waypoint list
            player.GetBehavior<EntityBehaviorWaypoints>().AddWaypoint(waypoint);

            // Send the /waypoint command to the player
            clientApi.SendChatMessage("/waypoint " + waypoint.Name);
        }
    }
}
