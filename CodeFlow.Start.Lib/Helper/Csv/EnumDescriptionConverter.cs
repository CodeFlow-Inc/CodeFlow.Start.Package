using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using System;
using System.ComponentModel;

namespace CodeFlow.Start.Lib.Helper.Csv;

/// <summary>
/// 
/// </summary>
/// <typeparam name="T"></typeparam>
public class EnumDescriptionConverterr<T> : DefaultTypeConverter where T : struct, Enum
{

    /// <summary>
    /// Converts a string to an enum value based on the description attribute or name.
    /// </summary>
    /// <param name="text">The string to convert.</param>
    /// <param name="row">The CSV reader row.</param>
    /// <param name="memberMapData">The member map data.</param>
    /// <returns>The corresponding enum value.</returns>
    /// <exception cref="ArgumentException">Thrown when the description does not match any enum value.</exception>
    public override object? ConvertFromString(string? text, IReaderRow row, MemberMapData memberMapData)
    {
        ArgumentNullException.ThrowIfNull(text);

        foreach (var field in typeof(T).GetFields())
        {
            if (field != null && Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
            {
                if (attribute.Description == text)
                {
                    return (T)field.GetValue(null)!;
                }
            }
            else
            {
                if (field != null && field.Name == text)
                {
                    return (T)field.GetValue(null)!;
                }
            }
        }
        throw new ArgumentException($"Descrição inválida '{text}' para o enum '{typeof(T).Name}'");
    }

    /// <summary>
    /// Converts an enum value to its description attribute or name for CSV output.
    /// </summary>
    /// <param name="value">The enum value to convert.</param>
    /// <param name="row">The CSV writer row.</param>
    /// <param name="memberMapData">The member map data.</param>
    /// <returns>The corresponding description or name as a string.</returns>
    public override string? ConvertToString(object? value, IWriterRow row, MemberMapData memberMapData)
    {
        if (value is T enumValue)
        {
            var field = typeof(T).GetField(enumValue.ToString());
            if (field != null && Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
            {
                return attribute.Description;
            }
            return enumValue.ToString();
        }
        return base.ConvertToString(value, row, memberMapData);
    }
}
