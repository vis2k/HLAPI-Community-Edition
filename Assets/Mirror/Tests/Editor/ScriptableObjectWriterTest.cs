using NUnit.Framework;
using UnityEngine;

namespace Mirror.Tests
{
    internal class MyScriptableObject : ScriptableObject
    {
        public int someData;
    }

    [TestFixture]
    public class ScriptableObjectWriterTest
    {

        // ArraySegment<byte> is a special case,  optimized for no copy and no allocation
        // other types are generated by the weaver


        struct ScriptableObjectMessage : IMessageBase
        {
            public MyScriptableObject scriptableObject;

            // Weaver auto generates serialization
            public void Deserialize(NetworkReader reader) {}
            public void Serialize(NetworkWriter writer) {}
        }

        [Test]
        public void TestWriteScriptableObject()
        {
            ScriptableObjectMessage message = new ScriptableObjectMessage
            {
                scriptableObject = ScriptableObject.CreateInstance<MyScriptableObject>()
            };

            message.scriptableObject.someData = 10;

            byte[] data = MessagePackerTest.PackToByteArray(message);

            ScriptableObjectMessage unpacked = MessagePacker.Unpack<ScriptableObjectMessage>(data);

            Assert.That(unpacked.scriptableObject, Is.Not.Null);
            Assert.That(unpacked.scriptableObject.someData, Is.EqualTo(10));
        }

    }
}
