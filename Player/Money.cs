namespace DND5.Player
{
  public class Money : DbTable
  {
    public int Copper { get; set; }
    public int Silver { get; set; }
    public int Electrum { get; set; }
    public int Gold { get; set; }
    public int Platinum { get; set; }

    /// <summary>
    /// = 50 coins per pound
    /// </summary>
    public decimal TotalWeight
    {
      get { return (Copper + Silver + Electrum + Gold + Platinum) / 50; }
    }

    public decimal TotalInPlatinum { get { return Platinum + (Gold / 10m) + (Electrum / 20m) + (Silver / 100m) + (Copper / 1000m); } }
    public decimal TotalInGold { get { return Gold + (Copper / 100m) + (Silver / 10m) + (Electrum / 2m) + (Platinum * 10); } }
    public decimal TotalInElectrum { get { return Electrum + (Copper / 50m) + (Silver / 5m) + (Gold * 2) + (Platinum * 20); } }
    public decimal TotalInSilver { get { return Silver + (Copper / 10m) + (Electrum * 5) + (Gold * 10) + (Platinum * 100); } }
    public decimal TotalInCopper { get { return Copper + (Silver * 10) + (Electrum * 50) + (Gold * 100) + (Platinum * 1000); } }

    public override string ToString()
    {
      return string.Format("CP: {0}, SP: {1}, EP: {2}, GP: {3}, PP: {4}", Copper, Silver, Electrum, Gold, Platinum);
    }
  }
}
