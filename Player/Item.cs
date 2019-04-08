using System.Collections.Generic;
using System.Linq;

namespace DND5.Player
{
  public class Item : DbTable
  {
    public ItemClass Classification { get; set; }
    public string Name { get; set; }
    public bool HasProficiency { get; set; }
    public int Quantity { get; set; }
    public bool Equipped { get; set; }
    public decimal WeightEach { get; set; }
    public decimal TotalWeight
    {
      get
      {
        return Quantity * WeightEach + SubItems.Sum(x => x.Quantity * x.WeightEach);
      }
    }
    public DiceDescriptor Dice { get; set; }

    /// <summary>
    /// Thinkgs like kits, backpacks, etc. can stuff inside them
    /// </summary>
    public List<Item> SubItems { get; }

    public Item()
    {
      SubItems = new List<Item>();
    }
  }
}
