using System;
using System.ComponentModel;

namespace DND5.Player
{
  public class DbTable
  {
    [Browsable(false)]
    public int Id { get; set; }
    [Browsable(false)]
    public DateTime CreatedAt { get; set; }
    [Browsable(false)]
    public bool Deleted { get; set; }
  }
}
