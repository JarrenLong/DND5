using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;

namespace DND5.Player
{
  public class Character : DbTable
  {
    private const string Category_Info = "1. Character Info";
    private const string Category_Stats = "2. Character Stats";
    private const string Category_Movement = "3. Character Movement";
    private const string Category_Skills = "4. Character Skills";
    private const string Category_Inventory = "5. Character Inventory";
    private const string Category_Spells = "6. Character Spells";

    #region The Absolute Basics
    [Category(Category_Info)]
    [DisplayName("Player's Name")]
    [Description("The name of the player controlling this character")]
    public string PlayerName { get; set; }

    [Category(Category_Info)]
    [DisplayName("Character's Name")]
    [Description("The name of this character")]
    public string CharacterName { get; set; }

    [Category(Category_Info)]
    [DisplayName("Race")]
    [Description("The race of this character")]
    public Race Race { get; set; }

    [Category(Category_Info)]
    [DisplayName("Class")]
    [Description("The class of this character")]
    public Class Class { get; set; }

    [Category(Category_Info)]
    [DisplayName("Background")]
    [Description("Describes this characters background")]
    public Background Background { get; set; }

    [Category(Category_Info)]
    [DisplayName("Alignment")]
    [Description("The character's alignment (lawful, neutral, chaotic + good, neutral, evil)")]
    public Alignment Alignment { get; set; }

    [Category(Category_Info)]
    [DisplayName("Level")]
    [Description("The character's current level.")]
    public int Level { get; set; }

    [Category(Category_Info)]
    [DisplayName("Experiene Points (XP)")]
    [Description("The amount of experience points this character has.")]
    public int ExperiencePoints { get; set; }

    [Category(Category_Info)]
    [DisplayName("Max. HP")]
    [Description("The maximum amount of HP this character has.")]
    public int MaxHitPoints { get; set; }

    [Category(Category_Info)]
    [DisplayName("Current HP")]
    [Description("This character's current HP.")]
    public int CurrentHitPoints { get; set; }

    [Category(Category_Info)]
    [DisplayName("Temporary HP")]
    [Description("The amount of temporary HP this character currently has.")]
    public int TemporaryHitPoints { get; set; }

    [Category(Category_Info)]
    [DisplayName("Current Age")]
    [Description("How old this character is in-game.")]
    public int Age { get; set; }

    [Category(Category_Info)]
    [DisplayName("Height (ft.)")]
    [Description("How tall the character is (in feet).")]
    public decimal HeightInFeet { get; set; }

    [Category(Category_Info)]
    [DisplayName("Weight (lbs.)")]
    [Description("How heavy the character is (in pounds).")]
    public int Weight { get; set; }

    [Category(Category_Info)]
    [DisplayName("Size Class")]
    [Description("The character's size group.")]
    public SizeClass Size { get; set; }

    [Category(Category_Info)]
    [DisplayName("Eye Color")]
    [Description("The character's eye color.")]
    public string EyeColor { get; set; }

    [Category(Category_Info)]
    [DisplayName("Skin Color/Tone")]
    [Description("The character's skin color/tone.")]
    public string SkinColor { get; set; }

    [Category(Category_Info)]
    [DisplayName("Hair Color")]
    [Description("The character's hair color.")]
    public string HairColor { get; set; }

    [Category(Category_Info)]
    [DisplayName("Languages")]
    [Description("Languages that this character can speak.")]
    public Language Languages { get; set; }

    [Category(Category_Info)]
    [DisplayName("Personality - Traits")]
    [Description("Description of this character's personality traits.")]
    public string PersonalityTraits { get; set; }

    [Category(Category_Info)]
    [DisplayName("Personality - Ideals")]
    [Description("Description of this character's ideals.")]
    public string Ideals { get; set; }

    [Category(Category_Info)]
    [DisplayName("Personality - Bonds")]
    [Description("Description of this character's bonds to the world.")]
    public string Bonds { get; set; }

    [Category(Category_Info)]
    [DisplayName("Personality - Flaws")]
    [Description("Description of this character's personal flaws.")]
    public string Flaws { get; set; }

    [Category(Category_Info)]
    [DisplayName("Money")]
    [Description("The amount of money that this character has.")]
    public Money MoneyOnHand { get; set; }
    #endregion

    #region Base Stats
    [Category(Category_Stats)]
    [DisplayName("Strength (STR)")]
    [Description("The amount of strength this character has.")]
    public int Strength { get; set; }

    [Category(Category_Stats)]
    [DisplayName("STR Modifier")]
    [Description("The characters strength modifier.")]
    public int StrengthModifier { get { return (int)Math.Floor((Strength - 10) / 2m); } }

    [Category(Category_Stats)]
    [DisplayName("Dexterity (DEX)")]
    [Description("The amount of dexterity this character has.")]
    public int Dexterity { get; set; }

    [Category(Category_Stats)]
    [DisplayName("DEX Modifier")]
    [Description("The characters dexterity modifier.")]
    public int DexterityModifier { get { return (int)Math.Floor((Dexterity - 10) / 2m); } }

    [Category(Category_Stats)]
    [DisplayName("Constitution (CON)")]
    [Description("The amount of constitution this character has.")]
    public int Constitution { get; set; }

    [Category(Category_Stats)]
    [DisplayName("CON Modifier")]
    [Description("The characters constitution modifier.")]
    public int ConstitutionModifier { get { return (int)Math.Floor((Constitution - 10) / 2m); } }

    [Category(Category_Stats)]
    [DisplayName("Intelligence (INT)")]
    [Description("The amount of intelligence this character has.")]
    public int Intelligence { get; set; }

    [Category(Category_Stats)]
    [DisplayName("INT Modifier")]
    [Description("The characters intelligence modifier.")]
    public int IntelligenceModifier { get { return (int)Math.Floor((Intelligence - 10) / 2m); } }

    [Category(Category_Stats)]
    [DisplayName("Wisdom (WIS)")]
    [Description("The amount of wisdom this character has.")]
    public int Wisdom { get; set; }

    [Category(Category_Stats)]
    [DisplayName("WIS Modifier")]
    [Description("The characters wisdom modifier.")]
    public int WisdomModifier { get { return (int)Math.Floor((Wisdom - 10) / 2m); } }

    [Category(Category_Stats)]
    [DisplayName("Passive Wisdom Perception Modifier")]
    [Description("The characters passive wisdom/perception modifier.")]
    public int PassiveWisdomPerceptionModifier
    {
      get
      {
        return 10 + WisdomModifier + Skills?.FirstOrDefault(x => x.Name == "Perception" && x.HasProficiency)?.SkillPoints ?? 0;
      }
    }

    [Category(Category_Stats)]
    [DisplayName("Charisma (CHA)")]
    [Description("The amount of charisma this character has.")]
    public int Charisma { get; set; }

    [Category(Category_Stats)]
    [DisplayName("CHA Modifier")]
    [Description("The characters charisma modifier.")]
    public int CharismaModifier { get { return (int)Math.Floor((Charisma - 10) / 2m); } }


    [Category(Category_Stats)]
    [DisplayName("Armor Class (AC)")]
    [Description("The character's Armor Class.")]
    public int ArmorClass { get; set; }

    [Category(Category_Stats)]
    [DisplayName("Has Inspiration?")]
    [Description("Shows if the character currently has Inspiration available.")]
    public bool HasInspiration { get; set; }

    [Category(Category_Stats)]
    [DisplayName("Proficiency Bonus")]
    [Description("The character's proficiency bonus.")]
    public int ProficiencyBonus { get; set; }
    #endregion

    #region Movement
    [Category(Category_Movement)]
    [DisplayName("Base Walk Speed")]
    [Description("The character's base walking speed.")]
    public int BaseSpeed { get; set; }

    [Category(Category_Movement)]
    [DisplayName("Current Speed")]
    [Description("The current speed that the character can walk at.")]
    public int CurrentSpeed
    {
      get
      {
        if (IsPushingDraggingLiftingSomething) // If you're pushing, dragging, or lifting something, speed = 5
          return 5;
        if (IsProne) // If you're prone, you use 2x your speed to move anywhere, so you have effectively 1/2 your speed to work with.
          return BaseSpeed / 2;

        int speed = BaseSpeed;
        if (IsMounted)
          speed = MountSpeed;

        if (IsHeavilyEncumbered) // Heavily Encumbered; speed = speed - 20 feet. + disadvantage on ability checks, attack rolls, and saving throws that use STR, DEX, and CON.
          return speed - 20;
        if (IsEncumbered) // Encumbered; speed = speed - 10 feet.
          return speed - 10;
        // Nothing unusual, return the unmodified speed
        return speed;
      }
    }

    [Category(Category_Movement)]
    [DisplayName("Is Mounted?")]
    [Description("True if the character is currently riding something.")]
    public bool IsMounted { get; set; }

    [Category(Category_Movement)]
    [DisplayName("Mount Speed")]
    [Description("The speed of the mount the character is currently riding")]
    public int MountSpeed { get; set; }

    [Category(Category_Movement)]
    [DisplayName("Is Prone?")]
    [Description("True if the characcter is currently laying down on the ground.")]
    public bool IsProne { get; set; }

    [Category(Category_Movement)]
    [DisplayName("Pushing/Dragging/Lifting Something?")]
    [Description("True if the character is currently pushing, dragging, or lifting something.")]
    public bool IsPushingDraggingLiftingSomething { get; set; }

    [Category(Category_Movement)]
    [DisplayName("Max Carry Weight")]
    [Description("The maximum weight of stuff the character can carry.")]
    public int MaxCarryWeight { get { return Strength * 15; } }

    [Category(Category_Movement)]
    [DisplayName("Max Push/Drag/Lift Weight")]
    [Description("The maximum weight of stuff the character can push, drag, or lift.")]
    public int MaxPushDragLiftWeight { get { return Strength * 30; } }
    #endregion

    #region Skills
    [Category(Category_Skills), DisplayName("Skills List"), Description("Various skills that this character has.")]
    public List<Skill> Skills { get; } = new List<Skill>();
    #endregion

    #region Inventory
    /// <summary>
    /// A list of everything the character has in their inventory, equipped or not.
    /// </summary>
    [Category(Category_Inventory)]
    [DisplayName("Inventory List")]
    [Description("Various items and equipment that the character has.")]
    public List<Item> Inventory { get; } = new List<Item>();

    /// <summary>
    /// The total weight of everything the player is carrying
    /// </summary>
    [Category(Category_Inventory)]
    [DisplayName("Total Inventory Weight")]
    [Description("The total weight of everything the character is carrying.")]
    public decimal InventoryWeight { get { return Inventory.Sum(x => x.TotalWeight) + MoneyOnHand.TotalWeight; } }

    /// <summary>
    /// If carrying Strength*5 weight, you are encumbered; speed = speed - 10 feet.
    /// </summary>
    [Category(Category_Movement)]
    [DisplayName("Is Encumbered?")]
    [Description("True if the character is carring more than 1/3 of their max carry weight.")]
    public bool IsEncumbered { get { return InventoryWeight >= (MaxCarryWeight / 3); } }
    /// <summary>
    /// If carrying Strength*10 weight, you are heavily encumbered; speed = speed - 20 feet. + disadvantage to STR, DEX, and CON rolls.
    /// </summary>
    [Category(Category_Movement)]

    [DisplayName("Is Heavily Encumbered?")]
    [Description("True if the character is carring more than 2/3 of their max carry weight.")]
    public bool IsHeavilyEncumbered { get { return InventoryWeight >= ((MaxCarryWeight * 2) / 3); } }
    #endregion

    #region Spells/Cantrips
    [Category(Category_Spells)]
    [DisplayName("Spell Casting Ability Stat")]
    [Description("The stat which spell casting is based on for this character.")]
    public StatUsed SpellCastingAbility { get; set; }

    [Category(Category_Spells)]
    [DisplayName("Spell Save DC")]
    [Description("The characters saving DC for spell casting.")]
    public int SpellSaveDC { get { return 0; } }

    [Category(Category_Spells)]
    [DisplayName("Spell Attack Bonus")]
    [Description("The attack bonus the character gets when casting spells.")]
    public int SpellAttackBonus { get { return 0; } }

    [Category(Category_Spells)]
    [DisplayName("Spell List")]
    [Description("List of spells this character knows.")]
    public List<Spell> Spells { get; } = new List<Spell>();
    #endregion
  }
}
