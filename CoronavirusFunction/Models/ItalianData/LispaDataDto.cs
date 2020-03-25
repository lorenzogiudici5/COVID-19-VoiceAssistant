using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CoronavirusFunction.Models
{
    public partial class LispaDataDto
    {
        [JsonProperty("objectIdFieldName")]
        public string ObjectIdFieldName { get; set; }

        [JsonProperty("uniqueIdField")]
        public UniqueIdField UniqueIdField { get; set; }

        [JsonProperty("globalIdFieldName")]
        public string GlobalIdFieldName { get; set; }

        [JsonProperty("geometryType")]
        public string GeometryType { get; set; }

        [JsonProperty("spatialReference")]
        public SpatialReference SpatialReference { get; set; }

        [JsonProperty("fields")]
        public List<Field> Fields { get; set; }

        [JsonProperty("features")]
        public List<Feature> Features { get; set; }
    }

    public partial class Feature
    {
        [JsonProperty("attributes")]
        public LispaDataCity Attributes { get; set; }
    }

    public partial class LispaDataCity
    {
        [JsonProperty("FID")]
        public long Fid { get; set; }

        [JsonProperty("COD_ISTATN")]
        public string CodIstatn { get; set; }

        [JsonProperty("NOME_COM")]
        public string NomeCom { get; set; }

        [JsonProperty("NOME_PRO")]
        public string NomePro { get; set; }

        [JsonProperty("POSITIVI")]
        public long Positivi { get; set; }

        [JsonProperty("DECEDUTI")]
        public long Deceduti { get; set; }

        [JsonProperty("TOTALE")]
        public long Totale { get; set; }
    }

    public partial class Field
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("alias")]
        public string Alias { get; set; }

        [JsonProperty("sqlType")]
        public string SqlType { get; set; }

        [JsonProperty("domain")]
        public object Domain { get; set; }

        [JsonProperty("defaultValue")]
        public object DefaultValue { get; set; }

        [JsonProperty("length", NullValueHandling = NullValueHandling.Ignore)]
        public long? Length { get; set; }
    }

    public partial class SpatialReference
    {
        [JsonProperty("wkid")]
        public long Wkid { get; set; }

        [JsonProperty("latestWkid")]
        public long LatestWkid { get; set; }
    }

    public partial class UniqueIdField
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("isSystemMaintained")]
        public bool IsSystemMaintained { get; set; }
    }

    public enum NomePro { Bergamo, Brescia, Como, Cremona, Ignota, Lecco, Lodi, Mantova, Milano, MonzaEDellaBrianza, Pavia, Sondrio, Varese };

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
        {
            NomeProConverter.Singleton,
            new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
        },
        };
    }

    internal class NomeProConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(NomePro) || t == typeof(NomePro?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "BERGAMO":
                    return NomePro.Bergamo;
                case "BRESCIA":
                    return NomePro.Brescia;
                case "COMO":
                    return NomePro.Como;
                case "CREMONA":
                    return NomePro.Cremona;
                case "IGNOTA":
                    return NomePro.Ignota;
                case "LECCO":
                    return NomePro.Lecco;
                case "LODI":
                    return NomePro.Lodi;
                case "MANTOVA":
                    return NomePro.Mantova;
                case "MILANO":
                    return NomePro.Milano;
                case "MONZA E DELLA BRIANZA":
                    return NomePro.MonzaEDellaBrianza;
                case "PAVIA":
                    return NomePro.Pavia;
                case "SONDRIO":
                    return NomePro.Sondrio;
                case "VARESE":
                    return NomePro.Varese;
            }
            throw new Exception("Cannot unmarshal type NomePro");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (NomePro)untypedValue;
            switch (value)
            {
                case NomePro.Bergamo:
                    serializer.Serialize(writer, "BERGAMO");
                    return;
                case NomePro.Brescia:
                    serializer.Serialize(writer, "BRESCIA");
                    return;
                case NomePro.Como:
                    serializer.Serialize(writer, "COMO");
                    return;
                case NomePro.Cremona:
                    serializer.Serialize(writer, "CREMONA");
                    return;
                case NomePro.Ignota:
                    serializer.Serialize(writer, "IGNOTA");
                    return;
                case NomePro.Lecco:
                    serializer.Serialize(writer, "LECCO");
                    return;
                case NomePro.Lodi:
                    serializer.Serialize(writer, "LODI");
                    return;
                case NomePro.Mantova:
                    serializer.Serialize(writer, "MANTOVA");
                    return;
                case NomePro.Milano:
                    serializer.Serialize(writer, "MILANO");
                    return;
                case NomePro.MonzaEDellaBrianza:
                    serializer.Serialize(writer, "MONZA E DELLA BRIANZA");
                    return;
                case NomePro.Pavia:
                    serializer.Serialize(writer, "PAVIA");
                    return;
                case NomePro.Sondrio:
                    serializer.Serialize(writer, "SONDRIO");
                    return;
                case NomePro.Varese:
                    serializer.Serialize(writer, "VARESE");
                    return;
            }
            throw new Exception("Cannot marshal type NomePro");
        }

        public static readonly NomeProConverter Singleton = new NomeProConverter();
    }
}
