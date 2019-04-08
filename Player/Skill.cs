using System.Collections.Generic;

namespace DND5.Player
{
  public class Skill : DbTable
  {
    public string Name { get; set; }
    public StatUsed StatUsed { get; set; }
    public bool HasProficiency { get; set; }
    public int SkillPoints { get; set; }

    public Skill() { }
    public Skill(string n, StatUsed stat, bool proficient = false, int points = 0)
    {
      Name = n;
      StatUsed = stat;
      HasProficiency = proficient;
      SkillPoints = points;
    }

    public static List<Skill> GetBaseSkillList()
    {
      return new List<Skill>(new Skill[] {
        new Skill("Acrobatics", StatUsed.Dexterity),
        new Skill("Animal Handling", StatUsed.Wisdom),
        new Skill("Arcana", StatUsed.Intelligence),
        new Skill("Athletics", StatUsed.Strength),
        new Skill("Deception", StatUsed.Charisma),
        new Skill("History", StatUsed.Intelligence),
        new Skill("Insight", StatUsed.Wisdom),
        new Skill("Intimidation", StatUsed.Charisma),
        new Skill("Investigation", StatUsed.Intelligence),
        new Skill("Medicine", StatUsed.Wisdom),
        new Skill("Nature", StatUsed.Intelligence),
        new Skill("Perception", StatUsed.Wisdom),
        new Skill("Performance", StatUsed.Charisma),
        new Skill("Persuasion", StatUsed.Charisma),
        new Skill("Religion", StatUsed.Intelligence),
        new Skill("Sleight of Hand", StatUsed.Dexterity),
        new Skill("Stealth", StatUsed.Dexterity),
        new Skill("Survival", StatUsed.Wisdom),
      });
    }
  }
}
