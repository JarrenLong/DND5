namespace DND5.Player
{
  public class Spell : DbTable
  {
    public string Name { get; set; }
    public string Description { get; set; }
    public DiceDescriptor Dice { get; set; }
  }
}
