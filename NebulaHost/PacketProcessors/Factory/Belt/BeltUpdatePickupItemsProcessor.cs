﻿using NebulaModel.Attributes;
using NebulaModel.Networking;
using NebulaModel.Packets.Belt;
using NebulaModel.Packets.Processors;

namespace NebulaHost.PacketProcessors.Factory.Belt
{
    [RegisterPacketProcessor]
    class BeltUpdatePickupItemsProcessor : IPacketProcessor<BeltUpdatePickupItemsPacket>
    {
        public void ProcessPacket(BeltUpdatePickupItemsPacket packet, NebulaConnection conn)
        {
            CargoTraffic traffic = GameMain.data.factories[packet.FactoryIndex].cargoTraffic;
            //Iterate though belt updates and remove target items
            for (int i = 0; i < packet.BeltUpdates.Length; i++)
            {
                CargoPath cargoPath = traffic.GetCargoPath(traffic.beltPool[packet.BeltUpdates[i].BeltId].segPathId);
                //Check if belt exists
                if (cargoPath != null)
                {
                    cargoPath.TryPickItem(packet.BeltUpdates[i].SegId - 4 - 1, 12);
                }
            }
        }
    }
}