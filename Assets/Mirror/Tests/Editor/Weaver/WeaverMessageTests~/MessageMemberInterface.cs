using System;
using Mirror;
using UnityEngine;

namespace WeaverMessageTests.MessageMemberInterface
{
    interface SuperCoolInterface { }

    struct MessageMemberInterface : IMessageBase
    {
        public uint netId;
        public Guid assetId;
        public Vector3 position;
        public Quaternion rotation;
        public SuperCoolInterface invalidField;
        public byte[] payload;

        // Weaver will generate serialization
        public void Serialize(NetworkWriter writer) {}
        public void Deserialize(NetworkReader reader) {}
    }
}
