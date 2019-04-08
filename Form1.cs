using DND5.Player;
using System;
using System.Linq;
using System.Windows.Forms;

namespace DND5
{
  public partial class Form1 : Form
  {
    private Character player = new Character();

    public Form1()
    {
      InitializeComponent();
    }

    private void Form1_Load(object sender, EventArgs e)
    {
      player = new Character()
      {
        PlayerName = "Jarren",
        CharacterName = "Hulok",
        Race = Race.Human,
        Class = Class.Warlock,
        Background = Background.Unknown,
        Alignment = Alignment.NeutralEvil,
        Level = 1,
        Age = 24,
        HeightInFeet = 5.5m,
        Size = SizeClass.Medium,
        ExperiencePoints = 0,
        HairColor = "Blonde",
        EyeColor = "Blue",
        SkinColor = "Fair/Pale",
        Weight = 160,
        PersonalityTraits = "I lie about most everything, even when there's no good reason to.",
        Ideals = "Aspiration: I'm determined to make something of myself.",
        Bonds = "I fleeced the wrong person and must avoid them at all costs if I want to live.",
        Flaws = "I'm always in debt and spend my money on a decadent lifestyle.",
        MoneyOnHand = new Money() { Gold = 100 },

        MaxHitPoints = 11,
        TemporaryHitPoints = 0,

        Strength = 10,
        Dexterity = 11,
        Constitution = 16,
        Intelligence = 13,
        Wisdom = 11,
        Charisma = 16,
        ProficiencyBonus = 2,

        HasInspiration = false,
        BaseSpeed = 30,
        ArmorClass = 11,
        SpellCastingAbility = StatUsed.Charisma,
        Languages = Language.Common | Language.Goblin
      };

      player.Skills.AddRange(Skill.GetBaseSkillList());
      player.Skills.FirstOrDefault(x => x.Name == "Arcana").HasProficiency = true;
      player.Skills.FirstOrDefault(x => x.Name == "Deception").HasProficiency = true;
      player.Skills.FirstOrDefault(x => x.Name == "Investigation").HasProficiency = true;
      player.Skills.FirstOrDefault(x => x.Name == "Sleight of Hand").HasProficiency = true;
    }

    private void Form1_Shown(object sender, EventArgs e)
    {
      propertyGridPlayer.SelectedObject = player;
    }
  }
}
