#pragma warning disable 618
#pragma warning disable 612
#pragma warning disable 414
#pragma warning disable 168

namespace MessagePack.Resolvers
{
    using System;
    using MessagePack;

    public class GeneratedResolver : global::MessagePack.IFormatterResolver
    {
        public static readonly global::MessagePack.IFormatterResolver Instance = new GeneratedResolver();

        GeneratedResolver()
        {

        }

        public global::MessagePack.Formatters.IMessagePackFormatter<T> GetFormatter<T>()
        {
            return FormatterCache<T>.formatter;
        }

        static class FormatterCache<T>
        {
            public static readonly global::MessagePack.Formatters.IMessagePackFormatter<T> formatter;

            static FormatterCache()
            {
                var f = GeneratedResolverGetFormatterHelper.GetFormatter(typeof(T));
                if (f != null)
                {
                    formatter = (global::MessagePack.Formatters.IMessagePackFormatter<T>)f;
                }
            }
        }
    }

    internal static class GeneratedResolverGetFormatterHelper
    {
        static readonly global::System.Collections.Generic.Dictionary<Type, int> lookup;

        static GeneratedResolverGetFormatterHelper()
        {
            lookup = new global::System.Collections.Generic.Dictionary<Type, int>(2)
            {
                {typeof(global::ManagerStatus), 0 },
                {typeof(global::MessengerMode), 1 },
            };
        }

        internal static object GetFormatter(Type t)
        {
            int key;
            if (!lookup.TryGetValue(t, out key)) return null;

            switch (key)
            {
                case 0: return new MessagePack.Formatters.ManagerStatusFormatter();
                case 1: return new MessagePack.Formatters.MessengerModeFormatter();
                default: return null;
            }
        }
    }
}

#pragma warning disable 168
#pragma warning restore 414
#pragma warning restore 618
#pragma warning restore 612

#pragma warning disable 618
#pragma warning disable 612
#pragma warning disable 414
#pragma warning disable 168

namespace MessagePack.Formatters
{
    using System;
    using MessagePack;

    public sealed class ManagerStatusFormatter : global::MessagePack.Formatters.IMessagePackFormatter<global::ManagerStatus>
    {
        public int Serialize(ref byte[] bytes, int offset, global::ManagerStatus value, global::MessagePack.IFormatterResolver formatterResolver)
        {
            return MessagePackBinary.WriteInt32(ref bytes, offset, (Int32)value);
        }
        
        public global::ManagerStatus Deserialize(byte[] bytes, int offset, global::MessagePack.IFormatterResolver formatterResolver, out int readSize)
        {
            return (global::ManagerStatus)MessagePackBinary.ReadInt32(bytes, offset, out readSize);
        }
    }

    public sealed class MessengerModeFormatter : global::MessagePack.Formatters.IMessagePackFormatter<global::MessengerMode>
    {
        public int Serialize(ref byte[] bytes, int offset, global::MessengerMode value, global::MessagePack.IFormatterResolver formatterResolver)
        {
            return MessagePackBinary.WriteInt32(ref bytes, offset, (Int32)value);
        }
        
        public global::MessengerMode Deserialize(byte[] bytes, int offset, global::MessagePack.IFormatterResolver formatterResolver, out int readSize)
        {
            return (global::MessengerMode)MessagePackBinary.ReadInt32(bytes, offset, out readSize);
        }
    }


}

#pragma warning disable 168
#pragma warning restore 414
#pragma warning restore 618
#pragma warning restore 612


