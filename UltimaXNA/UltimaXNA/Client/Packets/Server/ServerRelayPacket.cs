﻿/***************************************************************************
 *   ServerRelayPacket.cs
 *   Part of UltimaXNA: http://code.google.com/p/ultimaxna
 *   
 *   begin                : May 31, 2009
 *   email                : poplicola@ultimaxna.com
 *
 ***************************************************************************/

/***************************************************************************
 *
 *   This program is free software; you can redistribute it and/or modify
 *   it under the terms of the GNU General Public License as published by
 *   the Free Software Foundation; either version 3 of the License, or
 *   (at your option) any later version.
 *
 ***************************************************************************/
#region usings
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
#endregion

namespace UltimaXNA.Client.Packets.Server
{
    public class ServerRelayPacket : RecvPacket
    {
        readonly int _ipAddress;
        readonly int _port;
        readonly int _accountId;

        public int IpAddress
        {
            get { return _ipAddress; }
        }

        public int Port
        {
            get { return _port; }
        }

        public int AccountId
        {
            get { return _accountId; }
        }

        public ServerRelayPacket(PacketReader reader)
            : base(0x8C, "Server Relay")
        {
            _ipAddress = reader.ReadInt32();
            _port = reader.ReadInt16();
            _accountId = reader.ReadInt32();
        }
    }
}
