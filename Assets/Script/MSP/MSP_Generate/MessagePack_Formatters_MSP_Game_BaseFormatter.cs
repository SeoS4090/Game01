// <auto-generated>
// THIS (.cs) FILE IS GENERATED BY MPC(MessagePack-CSharp). DO NOT CHANGE IT.
// </auto-generated>

#pragma warning disable 618
#pragma warning disable 612
#pragma warning disable 414
#pragma warning disable 168

#pragma warning disable SA1129 // Do not use default value type constructor
#pragma warning disable SA1200 // Using directives should be placed correctly
#pragma warning disable SA1309 // Field names should not begin with underscore
#pragma warning disable SA1312 // Variable names should begin with lower-case letter
#pragma warning disable SA1403 // File may only contain a single namespace
#pragma warning disable SA1649 // File name should match first type name

namespace MessagePack.Formatters
{
    using System;
    using System.Buffers;
    using MessagePack;

    public sealed class MSP_Game_BaseFormatter : global::MessagePack.Formatters.IMessagePackFormatter<global::MSP_Game_Base>
    {

        public void Serialize(ref MessagePackWriter writer, global::MSP_Game_Base value, global::MessagePack.MessagePackSerializerOptions options)
        {
            if (value == null)
            {
                writer.WriteNil();
                return;
            }

            IFormatterResolver formatterResolver = options.Resolver;
            writer.WriteArrayHeader(7);
            formatterResolver.GetFormatterWithVerify<string>().Serialize(ref writer, value.DataKey, options);
            writer.Write(value.Var_int);
            writer.Write(value.Var_float);
            formatterResolver.GetFormatterWithVerify<string>().Serialize(ref writer, value.Var_string, options);
            formatterResolver.GetFormatterWithVerify<global::System.Collections.Generic.List<int>>().Serialize(ref writer, value.Var_list_int, options);
            formatterResolver.GetFormatterWithVerify<global::System.Collections.Generic.List<float>>().Serialize(ref writer, value.Var_list_float, options);
            formatterResolver.GetFormatterWithVerify<global::System.Collections.Generic.List<string>>().Serialize(ref writer, value.Var_list_string, options);
        }

        public global::MSP_Game_Base Deserialize(ref MessagePackReader reader, global::MessagePack.MessagePackSerializerOptions options)
        {
            if (reader.TryReadNil())
            {
                return null;
            }

            options.Security.DepthStep(ref reader);
            IFormatterResolver formatterResolver = options.Resolver;
            var length = reader.ReadArrayHeader();
            var __DataKey__ = default(string);
            var __Var_int__ = default(int);
            var __Var_float__ = default(float);
            var __Var_string__ = default(string);
            var __Var_list_int__ = default(global::System.Collections.Generic.List<int>);
            var __Var_list_float__ = default(global::System.Collections.Generic.List<float>);
            var __Var_list_string__ = default(global::System.Collections.Generic.List<string>);

            for (int i = 0; i < length; i++)
            {
                switch (i)
                {
                    case 0:
                        __DataKey__ = formatterResolver.GetFormatterWithVerify<string>().Deserialize(ref reader, options);
                        break;
                    case 1:
                        __Var_int__ = reader.ReadInt32();
                        break;
                    case 2:
                        __Var_float__ = reader.ReadSingle();
                        break;
                    case 3:
                        __Var_string__ = formatterResolver.GetFormatterWithVerify<string>().Deserialize(ref reader, options);
                        break;
                    case 4:
                        __Var_list_int__ = formatterResolver.GetFormatterWithVerify<global::System.Collections.Generic.List<int>>().Deserialize(ref reader, options);
                        break;
                    case 5:
                        __Var_list_float__ = formatterResolver.GetFormatterWithVerify<global::System.Collections.Generic.List<float>>().Deserialize(ref reader, options);
                        break;
                    case 6:
                        __Var_list_string__ = formatterResolver.GetFormatterWithVerify<global::System.Collections.Generic.List<string>>().Deserialize(ref reader, options);
                        break;
                    default:
                        reader.Skip();
                        break;
                }
            }

            var ____result = new global::MSP_Game_Base();
            ____result.DataKey = __DataKey__;
            ____result.Var_int = __Var_int__;
            ____result.Var_float = __Var_float__;
            ____result.Var_string = __Var_string__;
            ____result.Var_list_int = __Var_list_int__;
            ____result.Var_list_float = __Var_list_float__;
            ____result.Var_list_string = __Var_list_string__;
            reader.Depth--;
            return ____result;
        }
    }
}

#pragma warning restore 168
#pragma warning restore 414
#pragma warning restore 618
#pragma warning restore 612

#pragma warning restore SA1129 // Do not use default value type constructor
#pragma warning restore SA1200 // Using directives should be placed correctly
#pragma warning restore SA1309 // Field names should not begin with underscore
#pragma warning restore SA1312 // Variable names should begin with lower-case letter
#pragma warning restore SA1403 // File may only contain a single namespace
#pragma warning restore SA1649 // File name should match first type name
