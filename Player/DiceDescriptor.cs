namespace DND5.Player
{
  public class DiceDescriptor : DbTable
  {
    /// <summary>
    /// Number of D2 dice required (coin flip)
    /// </summary>
    public int D2 { get; set; }
    /// <summary>
    /// Number of D3 dice required
    /// </summary>
    public int D3 { get; set; }
    /// <summary>
    /// Number of D4 dice required
    /// </summary>
    public int D4 { get; set; }
    /// <summary>
    /// Number of D6 dice required
    /// </summary>
    public int D6 { get; set; }
    /// <summary>
    /// Number of D8 dice required
    /// </summary>
    public int D8 { get; set; }
    /// <summary>
    /// Number of D10 dice required
    /// </summary>
    public int D10 { get; set; }
    /// <summary>
    /// Number of D12 dice required
    /// </summary>
    public int D12 { get; set; }
    /// <summary>
    /// Number of D20 dice required
    /// </summary>
    public int D20 { get; set; }
    /// <summary>
    /// Number of D100 dice required (D10 + Percentile)
    /// </summary>
    public int D100 { get; set; }
  }
}
