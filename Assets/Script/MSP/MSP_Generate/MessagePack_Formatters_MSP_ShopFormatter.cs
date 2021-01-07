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

    public sealed class MSP_ShopFormatter : global::MessagePack.Formatters.IMessagePackFormatter<global::MSP_Shop>
    {

        public void Serialize(ref MessagePackWriter writer, global::MSP_Shop value, global::MessagePack.MessagePackSerializerOptions options)
        {
            if (value == null)
            {
                writer.WriteNil();
                return;
            }

            IFormatterResolver formatterResolver = options.Resolver;
            writer.WriteArrayHeader(9);
            formatterResolver.GetFormatterWithVerify<string>().Serialize(ref writer, value.DataKey, options);
            formatterResolver.GetFormatterWithVerify<string>().Serialize(ref writer, value.Goods, options);
            writer.Write(value.GoodsAmount);
            writer.Write(value.Shop_Kinds);
            formatterResolver.GetFormatterWithVerify<string>().Serialize(ref writer, value.Icon, options);
            formatterResolver.GetFormatterWithVerify<string>().Serialize(ref writer, value.Description, options);
            formatterResolver.GetFormatterWithVerify<string>().Serialize(ref writer, value.Name, options);
            formatterResolver.GetFormatterWithVerify<string>().Serialize(ref writer, value.Reward_Item, options);
            writer.Write(value.Reward_Count);
        }

        public global::MSP_Shop Deserialize(ref MessagePackReader reader, global::MessagePack.MessagePackSerializerOptions options)
        {
            if (reader.TryReadNil())
            {
                return null;
            }

            options.Security.DepthStep(ref reader);
            IFormatterResolver formatterResolver = options.Resolver;
            var length = reader.ReadArrayHeader();
            var __DataKey__ = default(string);
            var __Goods__ = default(string);
            var __GoodsAmount__ = default(int);
            var __Shop_Kinds__ = default(int);
            var __Icon__ = default(string);
            var __Description__ = default(string);
            var __Name__ = default(string);
            var __Reward_Item__ = default(string);
            var __Reward_Count__ = default(int);

            for (int i = 0; i < length; i++)
            {
                switch (i)
                {
                    case 0:
                        __DataKey__ = formatterResolver.GetFormatterWithVerify<string>().Deserialize(ref reader, options);
                        break;
                    case 1:
                        __Goods__ = formatterResolver.GetFormatterWithVerify<string>().Deserialize(ref reader, options);
                        break;
                    case 2:
                        __GoodsAmount__ = reader.ReadInt32();
                        break;
                    case 3:
                        __Shop_Kinds__ = reader.ReadInt32();
                        break;
                    case 4:
                        __Icon__ = formatterResolver.GetFormatterWithVerify<string>().Deserialize(ref reader, options);
                        break;
                    case 5:
                        __Description__ = formatterResolver.GetFormatterWithVerify<string>().Deserialize(ref reader, options);
                        break;
                    case 6:
                        __Name__ = formatterResolver.GetFormatterWithVerify<string>().Deserialize(ref reader, options);
                        break;
                    case 7:
                        __Reward_Item__ = formatterResolver.GetFormatterWithVerify<string>().Deserialize(ref reader, options);
                        break;
                    case 8:
                        __Reward_Count__ = reader.ReadInt32();
                        break;
                    default:
                        reader.Skip();
                        break;
                }
            }

            var ____result = new global::MSP_Shop();
            ____result.DataKey = __DataKey__;
            ____result.Goods = __Goods__;
            ____result.GoodsAmount = __GoodsAmount__;
            ____result.Shop_Kinds = __Shop_Kinds__;
            ____result.Icon = __Icon__;
            ____result.Description = __Description__;
            ____result.Name = __Name__;
            ____result.Reward_Item = __Reward_Item__;
            ____result.Reward_Count = __Reward_Count__;
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
