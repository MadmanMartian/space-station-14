﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SS3D_shared.HelperClasses;
using Lidgren.Network;

namespace SGO
{
    class BasicMoverComponent : GameObjectComponent
    {
        public BasicMoverComponent()
        {
            family = SS3D_shared.GO.ComponentFamily.Mover;
        }

        public void Translate(double x, double y)
        {
            Owner.position = new Vector2(x, y);
            Owner.Moved();
            SendPositionUpdate();
        }

        public void SendPositionUpdate()
        {
            Owner.SendComponentNetworkMessage(this, Lidgren.Network.NetDeliveryMethod.ReliableUnordered, null, Owner.position.X, Owner.position.Y);
        }

        public void SendPositionUpdate(NetConnection client)
        {
            Owner.SendComponentNetworkMessage(this, Lidgren.Network.NetDeliveryMethod.ReliableUnordered, client, Owner.position.X, Owner.position.Y);
        }

        public override void HandleInstantiationMessage(NetConnection netConnection)
        {
            SendPositionUpdate(netConnection);
        }
    }
}
