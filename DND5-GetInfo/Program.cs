using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace DND5_GetInfo
{
  class Program
  {
    static void Main(string[] args)
    {
      string baseURI = "https://donjon.bin.sh/5e/";

      // Build Item list
      string itemJson = File.ReadAllText("item_data.json");
      string spellJson = File.ReadAllText("spell_data.json");
      string monsterJson = File.ReadAllText("monster_data.json");

      var allItems = Items.Item.FromJson(itemJson);
      var allMonsters = Monsters.Item.FromJson(monsterJson);
      var allSpells = Spells.Spell.FromJson(spellJson);

      int i = 0;
      //// Retrieve all items
      //foreach (var joi in joItems)
      //{
      //  string name = joi["name"];
      //  WebClient wc = WebRequest.CreateHttp(baseURI + "magic_items/rpc.cgi?name=" + WebUtility.HtmlEncode(name));
      //}
    }
  }

  namespace Items
  {
    public partial class Item
    {
      [JsonProperty("rarity", NullValueHandling = NullValueHandling.Ignore)]
      public Rarity? Rarity { get; set; }

      [JsonProperty("page", NullValueHandling = NullValueHandling.Ignore)]
      public string Page { get; set; }

      [JsonProperty("limits", NullValueHandling = NullValueHandling.Ignore)]
      public string Limits { get; set; }

      [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
      public string Name { get; set; }

      [JsonProperty("attunement", NullValueHandling = NullValueHandling.Ignore)]
      public Attunement? Attunement { get; set; }

      [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
      public TypeEnum? Type { get; set; }

      [JsonProperty("srd_name", NullValueHandling = NullValueHandling.Ignore)]
      public string SrdName { get; set; }
    }

    public enum Attunement { No, Yes };

    public enum Rarity { Common, Legendary, Rare, Uncommon, VeryRare };

    public enum TypeEnum { Armor, Potion, Ring, Rod, Scroll, Staff, Wand, Weapon, WondrousItem };

    public partial class Item
    {
      public static Dictionary<string, Item> FromJson(string json) => JsonConvert.DeserializeObject<Dictionary<string, Item>>(json, Converter.Settings);
    }

    public static class Serialize
    {
      public static string ToJson(this Dictionary<string, Item> self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }

    internal static class Converter
    {
      public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
      {
        MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
        DateParseHandling = DateParseHandling.None,
        Converters =
            {
                AttunementConverter.Singleton,
                RarityConverter.Singleton,
                TypeEnumConverter.Singleton,
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
      };
    }

    internal class AttunementConverter : JsonConverter
    {
      public override bool CanConvert(Type t) => t == typeof(Attunement) || t == typeof(Attunement?);

      public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
      {
        if (reader.TokenType == JsonToken.Null) return null;
        var value = serializer.Deserialize<string>(reader);
        switch (value)
        {
          case "no":
            return Attunement.No;
          case "yes":
            return Attunement.Yes;
        }
        throw new Exception("Cannot unmarshal type Attunement");
      }

      public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
      {
        if (untypedValue == null)
        {
          serializer.Serialize(writer, null);
          return;
        }
        var value = (Attunement)untypedValue;
        switch (value)
        {
          case Attunement.No:
            serializer.Serialize(writer, "no");
            return;
          case Attunement.Yes:
            serializer.Serialize(writer, "yes");
            return;
        }
        throw new Exception("Cannot marshal type Attunement");
      }

      public static readonly AttunementConverter Singleton = new AttunementConverter();
    }

    internal class RarityConverter : JsonConverter
    {
      public override bool CanConvert(Type t) => t == typeof(Rarity) || t == typeof(Rarity?);

      public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
      {
        if (reader.TokenType == JsonToken.Null) return null;
        var value = serializer.Deserialize<string>(reader);
        switch (value)
        {
          case "Common":
            return Rarity.Common;
          case "Legendary":
            return Rarity.Legendary;
          case "Rare":
            return Rarity.Rare;
          case "Uncommon":
            return Rarity.Uncommon;
          case "Very Rare":
            return Rarity.VeryRare;
        }
        throw new Exception("Cannot unmarshal type Rarity");
      }

      public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
      {
        if (untypedValue == null)
        {
          serializer.Serialize(writer, null);
          return;
        }
        var value = (Rarity)untypedValue;
        switch (value)
        {
          case Rarity.Common:
            serializer.Serialize(writer, "Common");
            return;
          case Rarity.Legendary:
            serializer.Serialize(writer, "Legendary");
            return;
          case Rarity.Rare:
            serializer.Serialize(writer, "Rare");
            return;
          case Rarity.Uncommon:
            serializer.Serialize(writer, "Uncommon");
            return;
          case Rarity.VeryRare:
            serializer.Serialize(writer, "Very Rare");
            return;
        }
        throw new Exception("Cannot marshal type Rarity");
      }

      public static readonly RarityConverter Singleton = new RarityConverter();
    }

    internal class TypeEnumConverter : JsonConverter
    {
      public override bool CanConvert(Type t) => t == typeof(TypeEnum) || t == typeof(TypeEnum?);

      public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
      {
        if (reader.TokenType == JsonToken.Null) return null;
        var value = serializer.Deserialize<string>(reader);
        switch (value)
        {
          case "Armor":
            return TypeEnum.Armor;
          case "Potion":
            return TypeEnum.Potion;
          case "Ring":
            return TypeEnum.Ring;
          case "Rod":
            return TypeEnum.Rod;
          case "Scroll":
            return TypeEnum.Scroll;
          case "Staff":
            return TypeEnum.Staff;
          case "Wand":
            return TypeEnum.Wand;
          case "Weapon":
            return TypeEnum.Weapon;
          case "Wondrous Item":
            return TypeEnum.WondrousItem;
        }
        throw new Exception("Cannot unmarshal type TypeEnum");
      }

      public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
      {
        if (untypedValue == null)
        {
          serializer.Serialize(writer, null);
          return;
        }
        var value = (TypeEnum)untypedValue;
        switch (value)
        {
          case TypeEnum.Armor:
            serializer.Serialize(writer, "Armor");
            return;
          case TypeEnum.Potion:
            serializer.Serialize(writer, "Potion");
            return;
          case TypeEnum.Ring:
            serializer.Serialize(writer, "Ring");
            return;
          case TypeEnum.Rod:
            serializer.Serialize(writer, "Rod");
            return;
          case TypeEnum.Scroll:
            serializer.Serialize(writer, "Scroll");
            return;
          case TypeEnum.Staff:
            serializer.Serialize(writer, "Staff");
            return;
          case TypeEnum.Wand:
            serializer.Serialize(writer, "Wand");
            return;
          case TypeEnum.Weapon:
            serializer.Serialize(writer, "Weapon");
            return;
          case TypeEnum.WondrousItem:
            serializer.Serialize(writer, "Wondrous Item");
            return;
        }
        throw new Exception("Cannot marshal type TypeEnum");
      }

      public static readonly TypeEnumConverter Singleton = new TypeEnumConverter();
    }
  }

  namespace Monsters
  {
    public partial class Item
    {
      [JsonProperty("alignment", NullValueHandling = NullValueHandling.Ignore)]
      public Alignment? Alignment { get; set; }

      [JsonProperty("challenge", NullValueHandling = NullValueHandling.Ignore)]
      public ChallengeUnion? Challenge { get; set; }

      [JsonProperty("size", NullValueHandling = NullValueHandling.Ignore)]
      public Size? Size { get; set; }

      [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
      public string Name { get; set; }

      [JsonProperty("xp", NullValueHandling = NullValueHandling.Ignore)]
      [JsonConverter(typeof(DecodingChoiceConverter))]
      public long? Xp { get; set; }

      [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
      public MonsterType? Type { get; set; }

      [JsonProperty("page", NullValueHandling = NullValueHandling.Ignore)]
      public string Page { get; set; }

      [JsonProperty("environment", NullValueHandling = NullValueHandling.Ignore)]
      public Environment Environment { get; set; }

      [JsonProperty("tags", NullValueHandling = NullValueHandling.Ignore)]
      public string Tags { get; set; }
    }

    public partial class Environment
    {
      [JsonProperty("Forest", NullValueHandling = NullValueHandling.Ignore)]
      public Arctic? Forest { get; set; }

      [JsonProperty("Hill", NullValueHandling = NullValueHandling.Ignore)]
      public Arctic? Hill { get; set; }

      [JsonProperty("Desert", NullValueHandling = NullValueHandling.Ignore)]
      public Arctic? Desert { get; set; }

      [JsonProperty("Coastal", NullValueHandling = NullValueHandling.Ignore)]
      public Arctic? Coastal { get; set; }

      [JsonProperty("Underdark", NullValueHandling = NullValueHandling.Ignore)]
      public Arctic? Underdark { get; set; }

      [JsonProperty("Swamp", NullValueHandling = NullValueHandling.Ignore)]
      public Arctic? Swamp { get; set; }

      [JsonProperty("Urban", NullValueHandling = NullValueHandling.Ignore)]
      public Arctic? Urban { get; set; }

      [JsonProperty("Arctic", NullValueHandling = NullValueHandling.Ignore)]
      public Arctic? Arctic { get; set; }

      [JsonProperty("Mountain", NullValueHandling = NullValueHandling.Ignore)]
      public Arctic? Mountain { get; set; }

      [JsonProperty("Underwater", NullValueHandling = NullValueHandling.Ignore)]
      public Arctic? Underwater { get; set; }

      [JsonProperty("Grassland", NullValueHandling = NullValueHandling.Ignore)]
      public Arctic? Grassland { get; set; }
    }

    public enum Alignment { Any, AnyChaotic, AnyEvil, Ce, Cg, CgOrNe, Cn, Le, Lg, Ln, N, Ne, Ng, NgOrNe, Unaligned };

    public enum ChallengeEnum { The12, The14, The18 };

    public enum Arctic { Yes };

    public enum Size { Gargantuan, Huge, Large, Medium, Small, Tiny };

    public enum MonsterType { Aberration, Beast, Celestial, Construct, Dragon, Elemental, Fey, Fiend, Giant, Humanoid, Monstrosity, Ooze, Plant, Undead };

    public partial struct ChallengeUnion
    {
      public ChallengeEnum? Enum;
      public long? Integer;

      public static implicit operator ChallengeUnion(ChallengeEnum Enum) => new ChallengeUnion { Enum = Enum };
      public static implicit operator ChallengeUnion(long Integer) => new ChallengeUnion { Integer = Integer };
    }

    public partial class Item
    {
      public static Dictionary<string, Item> FromJson(string json) => JsonConvert.DeserializeObject<Dictionary<string, Item>>(json, Monsters.Converter.Settings);
    }

    public static class Serialize
    {
      public static string ToJson(this Dictionary<string, Item> self) => JsonConvert.SerializeObject(self, Monsters.Converter.Settings);
    }

    internal static class Converter
    {
      public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
      {
        MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
        DateParseHandling = DateParseHandling.None,
        Converters =
            {
                AlignmentConverter.Singleton,
                ChallengeUnionConverter.Singleton,
                ChallengeEnumConverter.Singleton,
                ArcticConverter.Singleton,
                SizeConverter.Singleton,
                TypeEnumConverter.Singleton,
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
      };
    }

    internal class AlignmentConverter : JsonConverter
    {
      public override bool CanConvert(Type t) => t == typeof(Alignment) || t == typeof(Alignment?);

      public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
      {
        if (reader.TokenType == JsonToken.Null) return null;
        var value = serializer.Deserialize<string>(reader);
        switch (value)
        {
          case "Any":
            return Alignment.Any;
          case "Any chaotic":
            return Alignment.AnyChaotic;
          case "Any evil":
            return Alignment.AnyEvil;
          case "CE":
            return Alignment.Ce;
          case "CG":
            return Alignment.Cg;
          case "CG or NE":
            return Alignment.CgOrNe;
          case "CN":
            return Alignment.Cn;
          case "LE":
            return Alignment.Le;
          case "LG":
            return Alignment.Lg;
          case "LN":
            return Alignment.Ln;
          case "N":
            return Alignment.N;
          case "NE":
            return Alignment.Ne;
          case "NG":
            return Alignment.Ng;
          case "NG or NE":
            return Alignment.NgOrNe;
          case "Unaligned":
            return Alignment.Unaligned;
        }
        throw new Exception("Cannot unmarshal type Alignment");
      }

      public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
      {
        if (untypedValue == null)
        {
          serializer.Serialize(writer, null);
          return;
        }
        var value = (Alignment)untypedValue;
        switch (value)
        {
          case Alignment.Any:
            serializer.Serialize(writer, "Any");
            return;
          case Alignment.AnyChaotic:
            serializer.Serialize(writer, "Any chaotic");
            return;
          case Alignment.AnyEvil:
            serializer.Serialize(writer, "Any evil");
            return;
          case Alignment.Ce:
            serializer.Serialize(writer, "CE");
            return;
          case Alignment.Cg:
            serializer.Serialize(writer, "CG");
            return;
          case Alignment.CgOrNe:
            serializer.Serialize(writer, "CG or NE");
            return;
          case Alignment.Cn:
            serializer.Serialize(writer, "CN");
            return;
          case Alignment.Le:
            serializer.Serialize(writer, "LE");
            return;
          case Alignment.Lg:
            serializer.Serialize(writer, "LG");
            return;
          case Alignment.Ln:
            serializer.Serialize(writer, "LN");
            return;
          case Alignment.N:
            serializer.Serialize(writer, "N");
            return;
          case Alignment.Ne:
            serializer.Serialize(writer, "NE");
            return;
          case Alignment.Ng:
            serializer.Serialize(writer, "NG");
            return;
          case Alignment.NgOrNe:
            serializer.Serialize(writer, "NG or NE");
            return;
          case Alignment.Unaligned:
            serializer.Serialize(writer, "Unaligned");
            return;
        }
        throw new Exception("Cannot marshal type Alignment");
      }

      public static readonly AlignmentConverter Singleton = new AlignmentConverter();
    }

    internal class ChallengeUnionConverter : JsonConverter
    {
      public override bool CanConvert(Type t) => t == typeof(ChallengeUnion) || t == typeof(ChallengeUnion?);

      public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
      {
        switch (reader.TokenType)
        {
          case JsonToken.Integer:
            var integerValue = serializer.Deserialize<long>(reader);
            return new ChallengeUnion { Integer = integerValue };
          case JsonToken.String:
          case JsonToken.Date:
            var stringValue = serializer.Deserialize<string>(reader);
            switch (stringValue)
            {
              case "1/2":
                return new ChallengeUnion { Enum = ChallengeEnum.The12 };
              case "1/4":
                return new ChallengeUnion { Enum = ChallengeEnum.The14 };
              case "1/8":
                return new ChallengeUnion { Enum = ChallengeEnum.The18 };
            }
            long l;
            if (Int64.TryParse(stringValue, out l))
            {
              return new ChallengeUnion { Integer = l };
            }
            break;
        }
        throw new Exception("Cannot unmarshal type ChallengeUnion");
      }

      public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
      {
        var value = (ChallengeUnion)untypedValue;
        if (value.Integer != null)
        {
          serializer.Serialize(writer, value.Integer.Value);
          return;
        }
        if (value.Enum != null)
        {
          switch (value.Enum)
          {
            case ChallengeEnum.The12:
              serializer.Serialize(writer, "1/2");
              return;
            case ChallengeEnum.The14:
              serializer.Serialize(writer, "1/4");
              return;
            case ChallengeEnum.The18:
              serializer.Serialize(writer, "1/8");
              return;
          }
        }
        throw new Exception("Cannot marshal type ChallengeUnion");
      }

      public static readonly ChallengeUnionConverter Singleton = new ChallengeUnionConverter();
    }

    internal class ChallengeEnumConverter : JsonConverter
    {
      public override bool CanConvert(Type t) => t == typeof(ChallengeEnum) || t == typeof(ChallengeEnum?);

      public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
      {
        if (reader.TokenType == JsonToken.Null) return null;
        var value = serializer.Deserialize<string>(reader);
        switch (value)
        {
          case "1/2":
            return ChallengeEnum.The12;
          case "1/4":
            return ChallengeEnum.The14;
          case "1/8":
            return ChallengeEnum.The18;
        }
        throw new Exception("Cannot unmarshal type ChallengeEnum");
      }

      public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
      {
        if (untypedValue == null)
        {
          serializer.Serialize(writer, null);
          return;
        }
        var value = (ChallengeEnum)untypedValue;
        switch (value)
        {
          case ChallengeEnum.The12:
            serializer.Serialize(writer, "1/2");
            return;
          case ChallengeEnum.The14:
            serializer.Serialize(writer, "1/4");
            return;
          case ChallengeEnum.The18:
            serializer.Serialize(writer, "1/8");
            return;
        }
        throw new Exception("Cannot marshal type ChallengeEnum");
      }

      public static readonly ChallengeEnumConverter Singleton = new ChallengeEnumConverter();
    }

    internal class ArcticConverter : JsonConverter
    {
      public override bool CanConvert(Type t) => t == typeof(Arctic) || t == typeof(Arctic?);

      public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
      {
        if (reader.TokenType == JsonToken.Null) return null;
        var value = serializer.Deserialize<string>(reader);
        if (value == "yes")
        {
          return Arctic.Yes;
        }
        throw new Exception("Cannot unmarshal type Arctic");
      }

      public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
      {
        if (untypedValue == null)
        {
          serializer.Serialize(writer, null);
          return;
        }
        var value = (Arctic)untypedValue;
        if (value == Arctic.Yes)
        {
          serializer.Serialize(writer, "yes");
          return;
        }
        throw new Exception("Cannot marshal type Arctic");
      }

      public static readonly ArcticConverter Singleton = new ArcticConverter();
    }

    internal class SizeConverter : JsonConverter
    {
      public override bool CanConvert(Type t) => t == typeof(Size) || t == typeof(Size?);

      public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
      {
        if (reader.TokenType == JsonToken.Null) return null;
        var value = serializer.Deserialize<string>(reader);
        switch (value)
        {
          case "Gargantuan":
            return Size.Gargantuan;
          case "Huge":
            return Size.Huge;
          case "Large":
            return Size.Large;
          case "Medium":
            return Size.Medium;
          case "Small":
            return Size.Small;
          case "Tiny":
            return Size.Tiny;
        }
        throw new Exception("Cannot unmarshal type Size");
      }

      public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
      {
        if (untypedValue == null)
        {
          serializer.Serialize(writer, null);
          return;
        }
        var value = (Size)untypedValue;
        switch (value)
        {
          case Size.Gargantuan:
            serializer.Serialize(writer, "Gargantuan");
            return;
          case Size.Huge:
            serializer.Serialize(writer, "Huge");
            return;
          case Size.Large:
            serializer.Serialize(writer, "Large");
            return;
          case Size.Medium:
            serializer.Serialize(writer, "Medium");
            return;
          case Size.Small:
            serializer.Serialize(writer, "Small");
            return;
          case Size.Tiny:
            serializer.Serialize(writer, "Tiny");
            return;
        }
        throw new Exception("Cannot marshal type Size");
      }

      public static readonly SizeConverter Singleton = new SizeConverter();
    }

    internal class TypeEnumConverter : JsonConverter
    {
      public override bool CanConvert(Type t) => t == typeof(MonsterType) || t == typeof(MonsterType?);

      public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
      {
        if (reader.TokenType == JsonToken.Null) return null;
        var value = serializer.Deserialize<string>(reader);
        switch (value)
        {
          case "Aberration":
            return MonsterType.Aberration;
          case "Beast":
            return MonsterType.Beast;
          case "Celestial":
            return MonsterType.Celestial;
          case "Construct":
            return MonsterType.Construct;
          case "Dragon":
            return MonsterType.Dragon;
          case "Elemental":
            return MonsterType.Elemental;
          case "Fey":
            return MonsterType.Fey;
          case "Fiend":
            return MonsterType.Fiend;
          case "Giant":
            return MonsterType.Giant;
          case "Humanoid":
            return MonsterType.Humanoid;
          case "Monstrosity":
            return MonsterType.Monstrosity;
          case "Ooze":
            return MonsterType.Ooze;
          case "Plant":
            return MonsterType.Plant;
          case "Undead":
            return MonsterType.Undead;
        }
        throw new Exception("Cannot unmarshal type TypeEnum");
      }

      public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
      {
        if (untypedValue == null)
        {
          serializer.Serialize(writer, null);
          return;
        }
        var value = (MonsterType)untypedValue;
        switch (value)
        {
          case MonsterType.Aberration:
            serializer.Serialize(writer, "Aberration");
            return;
          case MonsterType.Beast:
            serializer.Serialize(writer, "Beast");
            return;
          case MonsterType.Celestial:
            serializer.Serialize(writer, "Celestial");
            return;
          case MonsterType.Construct:
            serializer.Serialize(writer, "Construct");
            return;
          case MonsterType.Dragon:
            serializer.Serialize(writer, "Dragon");
            return;
          case MonsterType.Elemental:
            serializer.Serialize(writer, "Elemental");
            return;
          case MonsterType.Fey:
            serializer.Serialize(writer, "Fey");
            return;
          case MonsterType.Fiend:
            serializer.Serialize(writer, "Fiend");
            return;
          case MonsterType.Giant:
            serializer.Serialize(writer, "Giant");
            return;
          case MonsterType.Humanoid:
            serializer.Serialize(writer, "Humanoid");
            return;
          case MonsterType.Monstrosity:
            serializer.Serialize(writer, "Monstrosity");
            return;
          case MonsterType.Ooze:
            serializer.Serialize(writer, "Ooze");
            return;
          case MonsterType.Plant:
            serializer.Serialize(writer, "Plant");
            return;
          case MonsterType.Undead:
            serializer.Serialize(writer, "Undead");
            return;
        }
        throw new Exception("Cannot marshal type TypeEnum");
      }

      public static readonly TypeEnumConverter Singleton = new TypeEnumConverter();
    }

    internal class DecodingChoiceConverter : JsonConverter
    {
      public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);

      public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
      {
        if (reader.TokenType == JsonToken.Null) return null;
        switch (reader.TokenType)
        {
          case JsonToken.Integer:
            var integerValue = serializer.Deserialize<long>(reader);
            return integerValue;
          case JsonToken.String:
          case JsonToken.Date:
            var stringValue = serializer.Deserialize<string>(reader);
            long l;
            if (Int64.TryParse(stringValue, out l))
            {
              return l;
            }
            break;
        }
        throw new Exception("Cannot unmarshal type long");
      }

      public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
      {
        if (untypedValue == null)
        {
          serializer.Serialize(writer, null);
          return;
        }
        var value = (long)untypedValue;
        serializer.Serialize(writer, value);
        return;
      }

      public static readonly DecodingChoiceConverter Singleton = new DecodingChoiceConverter();
    }
  }

  namespace Spells
  {
    public partial class Spell
    {
      [JsonProperty("level", NullValueHandling = NullValueHandling.Ignore)]
      public Level Level { get; set; }

      [JsonProperty("duration", NullValueHandling = NullValueHandling.Ignore)]
      public Duration Duration { get; set; }

      [JsonProperty("school", NullValueHandling = NullValueHandling.Ignore)]
      public School School { get; set; }

      [JsonProperty("components", NullValueHandling = NullValueHandling.Ignore)]
      public Components Components { get; set; }

      [JsonProperty("range", NullValueHandling = NullValueHandling.Ignore)]
      public Range Range { get; set; }

      [JsonProperty("page", NullValueHandling = NullValueHandling.Ignore)]
      public string Page { get; set; }

      [JsonProperty("concentration", NullValueHandling = NullValueHandling.Ignore)]
      public bool Concentration { get; set; }

      [JsonProperty("ritual", NullValueHandling = NullValueHandling.Ignore)]
      public bool Ritual { get; set; }

      [JsonProperty("char_class", NullValueHandling = NullValueHandling.Ignore)]
      public CharClass CharClass { get; set; }

      [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
      public string Name { get; set; }

      [JsonProperty("casting_time", NullValueHandling = NullValueHandling.Ignore)]
      public string CastingTime { get; set; }
    }

    public partial class CharClass
    {
      //Warlock
      //Paladin
      //Cleric
      //Druid

      [JsonProperty("Sorcerer", NullValueHandling = NullValueHandling.Ignore)]
      public bool Sorcerer { get; set; }

      [JsonProperty("Wizard", NullValueHandling = NullValueHandling.Ignore)]
      public bool Wizard { get; set; }

      [JsonProperty("Ranger", NullValueHandling = NullValueHandling.Ignore)]
      public bool Ranger { get; set; }

      [JsonProperty("Druid", NullValueHandling = NullValueHandling.Ignore)]
      public bool Druid { get; set; }

      [JsonProperty("Bard", NullValueHandling = NullValueHandling.Ignore)]
      public bool Bard { get; set; }
    }

    public enum Level { The1st, The2nd, The3rd, The4th, The5th, The6th, The7th, The8th, The9th, Cantrip };
    public enum Components { S, Sm, SMgp, V, Vm, Vs, VsMgp, Vsm, VMgp };
    public enum School { Abjuration, Conjuration, Divination, Enchantment, Evocation, Illusion, Necromancy, Transmutation };
    public enum Range { Sight, Unlimited, The1Mile, Self, Special, The100Feet, The10Feet, The120Feet, The150Feet, The15Feet, The300Feet, The30Feet, The500Feet, The500Miles, The5Feet, The60Feet, The90Feet, Touch };
    public enum CastingTime { Instantaneous, The1Action, The1BonusAction, The1Reaction, The1Hour, The1Minute, The10Minutes, The12Hours, The8Hours, The24Hours };
    public enum Duration { The1Hour, The1MinuteThe1Round, The10DaysThe10MinutesThe24Hours, The30Days, The7Days, The8Hours, Instantaneous, Special, UntilDispelled, UpTo1Hour, UpTo1Minute, UpTo1Round, UpTo10Minutes, UpTo2Hours, UpTo24Hours, UpTo6Rounds, UpTo8Hours };


    public partial class Spell
    {
      public static Dictionary<string, Spell> FromJson(string json) => JsonConvert.DeserializeObject<Dictionary<string, Spell>>(json, Spells.Converter.Settings);
    }

    public static class Serialize
    {
      public static string ToJson(this Dictionary<string, Spell> self) => JsonConvert.SerializeObject(self, Spells.Converter.Settings);
    }

    internal static class Converter
    {
      public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
      {
        MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
        DateParseHandling = DateParseHandling.None,
        Converters =
            {
          CastingTimeConverter.Singleton,
                YesNoBoolConverter.Singleton,
                ComponentsConverter.Singleton,
                SchoolConverter.Singleton,
                RangeConverter.Singleton,
                DurationEnumConverter.Singleton,
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
      };
    }


    internal class CastingTimeConverter : JsonConverter
    {
      public override bool CanConvert(Type t) => t == typeof(CastingTime) || t == typeof(CastingTime?);

      public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
      {
        if (reader.TokenType == JsonToken.Null) return null;
        var value = serializer.Deserialize<string>(reader);
        switch (value)
        {
          case "1 action":
            return CastingTime.The1Action;
          case "1 bonus action":
            return CastingTime.The1BonusAction;
          case "1 minute":
            return CastingTime.The1Minute;
          case "1 reaction":
            return CastingTime.The1Reaction;
        }
        throw new Exception("Cannot unmarshal type PurpleCastingTime");
      }

      public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
      {
        if (untypedValue == null)
        {
          serializer.Serialize(writer, null);
          return;
        }
        var value = (CastingTime)untypedValue;
        switch (value)
        {
          case CastingTime.The1Action:
            serializer.Serialize(writer, "1 action");
            return;
          case CastingTime.The1BonusAction:
            serializer.Serialize(writer, "1 bonus action");
            return;
          case CastingTime.The1Minute:
            serializer.Serialize(writer, "1 minute");
            return;
          case CastingTime.The1Reaction:
            serializer.Serialize(writer, "1 reaction");
            return;
        }
        throw new Exception("Cannot marshal type PurpleCastingTime");
      }

      public static readonly CastingTimeConverter Singleton = new CastingTimeConverter();
    }

    internal class YesNoBoolConverter : JsonConverter
    {
      public override bool CanConvert(Type t) => t == typeof(bool) || t == typeof(bool?);

      public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
      {
        if (reader.TokenType == JsonToken.Null) return null;
        var value = serializer.Deserialize<string>(reader);
        switch (value)
        {
          case "no":
            return false;
          case "yes":
            return true;
        }
        throw new Exception("Cannot unmarshal type Concentration");
      }

      public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
      {
        if (untypedValue == null)
        {
          serializer.Serialize(writer, null);
          return;
        }
        var value = (bool)untypedValue;
        switch (value)
        {
          case false:
            serializer.Serialize(writer, "no");
            return;
          case true:
            serializer.Serialize(writer, "yes");
            return;
        }
        throw new Exception("Cannot marshal type Concentration");
      }

      public static readonly YesNoBoolConverter Singleton = new YesNoBoolConverter();
    }

    internal class ComponentsConverter : JsonConverter
    {
      public override bool CanConvert(Type t) => t == typeof(Components) || t == typeof(Components?);

      public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
      {
        if (reader.TokenType == JsonToken.Null) return null;
        var value = serializer.Deserialize<string>(reader);
        switch (value)
        {
          case "S":
            return Components.S;
          case "SM":
            return Components.Sm;
          case "V":
            return Components.V;
          case "VM":
            return Components.Vm;
          case "VS":
            return Components.Vs;
          case "VSM":
            return Components.Vsm;
          case "VSMgp":
            return Components.VsMgp;
        }
        throw new Exception("Cannot unmarshal type Components");
      }

      public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
      {
        if (untypedValue == null)
        {
          serializer.Serialize(writer, null);
          return;
        }
        var value = (Components)untypedValue;
        switch (value)
        {
          case Components.S:
            serializer.Serialize(writer, "S");
            return;
          case Components.Sm:
            serializer.Serialize(writer, "SM");
            return;
          case Components.V:
            serializer.Serialize(writer, "V");
            return;
          case Components.Vm:
            serializer.Serialize(writer, "VM");
            return;
          case Components.Vs:
            serializer.Serialize(writer, "VS");
            return;
          case Components.Vsm:
            serializer.Serialize(writer, "VSM");
            return;
          case Components.VsMgp:
            serializer.Serialize(writer, "VSMgp");
            return;
        }
        throw new Exception("Cannot marshal type Components");
      }

      public static readonly ComponentsConverter Singleton = new ComponentsConverter();
    }

    internal class SchoolConverter : JsonConverter
    {
      public override bool CanConvert(Type t) => t == typeof(School) || t == typeof(School?);

      public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
      {
        if (reader.TokenType == JsonToken.Null) return null;
        var value = serializer.Deserialize<string>(reader);
        switch (value)
        {
          case "Abjuration":
            return School.Abjuration;
          case "Conjuration":
            return School.Conjuration;
          case "Divination":
            return School.Divination;
          case "Enchantment":
            return School.Enchantment;
          case "Evocation":
            return School.Evocation;
          case "Illusion":
            return School.Illusion;
          case "Necromancy":
            return School.Necromancy;
          case "Transmutation":
            return School.Transmutation;
        }
        throw new Exception("Cannot unmarshal type School");
      }

      public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
      {
        if (untypedValue == null)
        {
          serializer.Serialize(writer, null);
          return;
        }
        var value = (School)untypedValue;
        switch (value)
        {
          case School.Abjuration:
            serializer.Serialize(writer, "Abjuration");
            return;
          case School.Conjuration:
            serializer.Serialize(writer, "Conjuration");
            return;
          case School.Divination:
            serializer.Serialize(writer, "Divination");
            return;
          case School.Enchantment:
            serializer.Serialize(writer, "Enchantment");
            return;
          case School.Evocation:
            serializer.Serialize(writer, "Evocation");
            return;
          case School.Illusion:
            serializer.Serialize(writer, "Illusion");
            return;
          case School.Necromancy:
            serializer.Serialize(writer, "Necromancy");
            return;
          case School.Transmutation:
            serializer.Serialize(writer, "Transmutation");
            return;
        }
        throw new Exception("Cannot marshal type School");
      }

      public static readonly SchoolConverter Singleton = new SchoolConverter();
    }

    internal class RangeConverter : JsonConverter
    {
      public override bool CanConvert(Type t) => t == typeof(Range) || t == typeof(Range?);

      public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
      {
        if (reader.TokenType == JsonToken.Null) return null;
        var value = serializer.Deserialize<string>(reader);
        switch (value)
        {
          case "10 feet":
            return Range.The10Feet;
          case "100 feet":
            return Range.The100Feet;
          case "120 feet":
            return Range.The120Feet;
          case "150 feet":
            return Range.The150Feet;
          case "30 feet":
            return Range.The30Feet;
          case "300 feet":
            return Range.The300Feet;
          case "5 feet":
            return Range.The5Feet;
          case "500 feet":
            return Range.The500Feet;
          case "500 miles":
            return Range.The500Miles;
          case "60 feet":
            return Range.The60Feet;
          case "90 feet":
            return Range.The90Feet;
          case "Self":
            return Range.Self;
          case "Special":
            return Range.Special;
          case "Touch":
            return Range.Touch;
        }
        throw new Exception("Cannot unmarshal type Range");
      }

      public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
      {
        if (untypedValue == null)
        {
          serializer.Serialize(writer, null);
          return;
        }
        var value = (Range)untypedValue;
        switch (value)
        {
          case Range.The10Feet:
            serializer.Serialize(writer, "10 feet");
            return;
          case Range.The100Feet:
            serializer.Serialize(writer, "100 feet");
            return;
          case Range.The120Feet:
            serializer.Serialize(writer, "120 feet");
            return;
          case Range.The150Feet:
            serializer.Serialize(writer, "150 feet");
            return;
          case Range.The30Feet:
            serializer.Serialize(writer, "30 feet");
            return;
          case Range.The300Feet:
            serializer.Serialize(writer, "300 feet");
            return;
          case Range.The5Feet:
            serializer.Serialize(writer, "5 feet");
            return;
          case Range.The500Feet:
            serializer.Serialize(writer, "500 feet");
            return;
          case Range.The500Miles:
            serializer.Serialize(writer, "500 miles");
            return;
          case Range.The60Feet:
            serializer.Serialize(writer, "60 feet");
            return;
          case Range.The90Feet:
            serializer.Serialize(writer, "90 feet");
            return;
          case Range.Self:
            serializer.Serialize(writer, "Self");
            return;
          case Range.Special:
            serializer.Serialize(writer, "Special");
            return;
          case Range.Touch:
            serializer.Serialize(writer, "Touch");
            return;
        }
        throw new Exception("Cannot marshal type Range");
      }

      public static readonly RangeConverter Singleton = new RangeConverter();
    }


    internal class DurationEnumConverter : JsonConverter
    {
      public override bool CanConvert(Type t) => t == typeof(Duration) || t == typeof(Duration?);

      public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
      {
        if (reader.TokenType == JsonToken.Null) return null;
        var value = serializer.Deserialize<string>(reader);
        switch (value)
        {
          case "1 action":
            return Duration.The1Action;
          case "1 bonus action":
            return Duration.The1BonusAction;
          case "1 hour":
            return Duration.The1Hour;
          case "1 minute":
            return Duration.The1Minute;
          case "10 minutes":
            return Duration.The10Minutes;
          case "12 hours":
            return Duration.The12Hours;
          case "24 hours":
            return Duration.The24Hours;
        }
        throw new Exception("Cannot unmarshal type DurationEnum");
      }

      public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
      {
        if (untypedValue == null)
        {
          serializer.Serialize(writer, null);
          return;
        }
        var value = (Duration)untypedValue;
        switch (value)
        {
          case Duration.The1Action:
            serializer.Serialize(writer, "1 action");
            return;
          case Duration.The1BonusAction:
            serializer.Serialize(writer, "1 bonus action");
            return;
          case Duration.The1Hour:
            serializer.Serialize(writer, "1 hour");
            return;
          case Duration.The1Minute:
            serializer.Serialize(writer, "1 minute");
            return;
          case Duration.The10Minutes:
            serializer.Serialize(writer, "10 minutes");
            return;
          case Duration.The12Hours:
            serializer.Serialize(writer, "12 hours");
            return;
          case Duration.The24Hours:
            serializer.Serialize(writer, "24 hours");
            return;
        }
        throw new Exception("Cannot marshal type DurationEnum");
      }

      public static readonly DurationEnumConverter Singleton = new DurationEnumConverter();
    }

  }

}
