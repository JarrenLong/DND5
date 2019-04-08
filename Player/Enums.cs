using System;

namespace DND5.Player
{
  [Flags]
  public enum ItemClass : uint
  {

  }

  [Flags]
  public enum SizeClass : uint
  {
    Unknown = 0,
    /// <summary>
    /// Takes up 2.5x2.5 feet of space
    /// </summary>
    Tiny,
    /// <summary>
    /// Takes up 5x5 feet of space
    /// </summary>
    Small,
    /// <summary>
    /// Takes up 5x5 feet of space
    /// </summary>
    Medium,
    /// <summary>
    /// Takes up 10x10 feet of space
    /// </summary>
    Large,
    /// <summary>
    /// Takes up 15x15 feet of space
    /// </summary>
    Huge,
    /// <summary>
    /// Takes up 20x20 feet or more of space
    /// </summary>
    Gargantuan
  }

  [Flags]
  public enum Race : uint
  {
    Unknown = 0,
    Dwarf,
    // V4
    Eladrin,
    Elf,
    Halfling,
    Human,
    Dragonborn,
    Gnome,
    HalfElf,
    HalfOrc,
    Tiefling
  }

  [Flags]
  public enum Class : uint
  {
    Unknown = 0,
    Barbarian,
    Bard,
    Cleric,
    Druid,
    Fighter,
    Monk,
    Paladin,
    Ranger,
    Rogue,
    // v3.0
    Sorcerer,
    //v4
    Warlock,
    // v4
    Warlord,
    Wizard
  }

  [Flags]
  public enum Language : uint
  {
    None = 0x0,
    Common = 0x01,
    Dwarvish = 0x02,
    Elvish = 0x04,
    Giant = 0x08,
    Gnomish = 0x10,
    Goblin = 0x20,
    Halfling = 0x40,
    Orc = 0x80,
    Abyssal = 0x100,
    Celestial = 0x200,
    DeepSpeech = 0x400,
    Draconic = 0x800,
    Infernal = 0x1000,
    Primordial = 0x2000,
    Sylvan = 0x4000,
    Undercommon = 0x8000,
    //v3.5
    Aquan = 0x10000,
    //v3.5
    Auran = 0x20000,
    //v3.5
    Druidic = 0x40000,
    //v3.5
    Gnoll = 0x80000,
    //v3.5
    Ignan = 0x100000,
    //v3.5
    Terran = 0x200000
  }

  [Flags]
  public enum Alignment : uint
  {
    None = 0,
    LawfulGood = 1,
    LawfulNeutral = 2,
    LawfulEvil = 4,
    NeutralGood = 8,
    Neutral = 16,
    NeutralEvil = 32,
    ChaoticGood = 64,
    ChaoticNeutral = 128,
    ChaoticEvil = 256
  }

  [Flags]
  public enum StatUsed : uint
  {
    None = 0,
    Strength = 1,
    Dexterity = 2,
    Constitution = 4,
    Intelligence = 8,
    Wisdom = 16,
    Charisma = 32
  }

  /// <summary>
  /// v3.5
  /// </summary>
  [Flags]
  public enum Religion : uint
  {
    None = 0,
    Boccob,
    CorellonLarethian,
    Ehlonna,
    Erythnul,
    Fharlanghn,
    GarlGlittergold,
    Grummsh,
    Heironeous,
    Hextor,
    Kord,
    Moradin,
    Nerull,
    ObadHai,
    Olidammara,
    Pelor,
    StCuthbert,
    Vecna,
    WeeJas,
    Yondalla,
  }

  [Flags]
  public enum Background : uint
  {
    Unknown = 0,
  }
}
