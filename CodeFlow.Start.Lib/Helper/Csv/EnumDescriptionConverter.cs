using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using System;
using System.ComponentModel;

namespace CodeFlow.Start.Lib.Helper.Csv;
public class EnumDescriptionConverterr<T> : DefaultTypeConverter where T : struct, Enum
{
    public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
    {
        foreach (var field in typeof(T).GetFields())
        {
            if (Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
            {
                if (attribute.Description == text)
                {
                    return (T)field.GetValue(null);
                }
            }
            else
            {
                if (field.Name == text)
                {
                    return (T)field.GetValue(null);
                }
            }
        }
        throw new ArgumentException($"Unknown description '{text}' for enum '{typeof(T).Name}'");
    }

    public override string? ConvertToString(object value, IWriterRow row, MemberMapData memberMapData)
    {
        if (value is T enumValue)
        {
            var field = typeof(T).GetField(enumValue.ToString());
            if (Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
            {
                return attribute.Description;
            }
            return enumValue.ToString();
        }
        return base.ConvertToString(value, row, memberMapData);
    }
}