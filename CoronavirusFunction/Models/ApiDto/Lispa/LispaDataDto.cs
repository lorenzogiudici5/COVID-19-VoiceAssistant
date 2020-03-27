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

    public enum ProvinceLombardia { Bergamo, Brescia, Como, Cremona, Ignota, Lecco, Lodi, Mantova, Milano, MonzaEDellaBrianza, Pavia, Sondrio, Varese };

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
        public override bool CanConvert(Type t) => t == typeof(ProvinceLombardia) || t == typeof(ProvinceLombardia?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "BERGAMO":
                    return ProvinceLombardia.Bergamo;
                case "BRESCIA":
                    return ProvinceLombardia.Brescia;
                case "COMO":
                    return ProvinceLombardia.Como;
                case "CREMONA":
                    return ProvinceLombardia.Cremona;
                case "IGNOTA":
                    return ProvinceLombardia.Ignota;
                case "LECCO":
                    return ProvinceLombardia.Lecco;
                case "LODI":
                    return ProvinceLombardia.Lodi;
                case "MANTOVA":
                    return ProvinceLombardia.Mantova;
                case "MILANO":
                    return ProvinceLombardia.Milano;
                case "MONZA E DELLA BRIANZA":
                    return ProvinceLombardia.MonzaEDellaBrianza;
                case "PAVIA":
                    return ProvinceLombardia.Pavia;
                case "SONDRIO":
                    return ProvinceLombardia.Sondrio;
                case "VARESE":
                    return ProvinceLombardia.Varese;
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
            var value = (ProvinceLombardia)untypedValue;
            switch (value)
            {
                case ProvinceLombardia.Bergamo:
                    serializer.Serialize(writer, "BERGAMO");
                    return;
                case ProvinceLombardia.Brescia:
                    serializer.Serialize(writer, "BRESCIA");
                    return;
                case ProvinceLombardia.Como:
                    serializer.Serialize(writer, "COMO");
                    return;
                case ProvinceLombardia.Cremona:
                    serializer.Serialize(writer, "CREMONA");
                    return;
                case ProvinceLombardia.Ignota:
                    serializer.Serialize(writer, "IGNOTA");
                    return;
                case ProvinceLombardia.Lecco:
                    serializer.Serialize(writer, "LECCO");
                    return;
                case ProvinceLombardia.Lodi:
                    serializer.Serialize(writer, "LODI");
                    return;
                case ProvinceLombardia.Mantova:
                    serializer.Serialize(writer, "MANTOVA");
                    return;
                case ProvinceLombardia.Milano:
                    serializer.Serialize(writer, "MILANO");
                    return;
                case ProvinceLombardia.MonzaEDellaBrianza:
                    serializer.Serialize(writer, "MONZA E DELLA BRIANZA");
                    return;
                case ProvinceLombardia.Pavia:
                    serializer.Serialize(writer, "PAVIA");
                    return;
                case ProvinceLombardia.Sondrio:
                    serializer.Serialize(writer, "SONDRIO");
                    return;
                case ProvinceLombardia.Varese:
                    serializer.Serialize(writer, "VARESE");
                    return;
            }
            throw new Exception("Cannot marshal type NomePro");
        }

        public static readonly NomeProConverter Singleton = new NomeProConverter();
    }
}
