using System;

namespace DefaultStructEqualityProblem
{
  public struct SummaryKeyTypeFirst
  {
    public string PartitionType { get; set; }
    public string MarketType { get; set; }
    public string UserGroup { get; set; }
  }

  public struct SummaryKeyGroupFirst
  {
    public string UserGroup { get; set; }
    public string PartitionType { get; set; }
    public string MarketType { get; set; }
  }

  public class SummaryKeyEquatable : IEquatable<SummaryKeyEquatable>
  {
    public string PartitionType { get; }
    public string MarketType { get; }
    public string UserGroup { get; }

    public SummaryKeyEquatable(string partitionType, string marketType, string userGroup)
    {
      PartitionType = partitionType;
      MarketType = marketType;
      UserGroup = userGroup;
    }

    public bool Equals(SummaryKeyEquatable aKey) => (aKey != null) && (ReferenceEquals(this, aKey) || (aKey.PartitionType == PartitionType && aKey.MarketType == MarketType && aKey.UserGroup == UserGroup));

    public override bool Equals(object aCompareToKey)
    {
      if (aCompareToKey == null)
        return false;

      if (aCompareToKey is SummaryKeyEquatable key)
        return Equals(key);

      throw new ArgumentException("You can only use a SummaryKeyEquatable type.", nameof(aCompareToKey));
    }

    public override int GetHashCode() => PartitionType.GetHashCode() + MarketType.GetHashCode() + UserGroup.GetHashCode();

    public static bool operator ==(SummaryKeyEquatable aOne, SummaryKeyEquatable aTwo)
    {
      if (((object)aOne == null) && ((object)aTwo == null))
        return true;

      if (((object)aOne == null) || ((object)aTwo == null))
        return false;

      return aOne.Equals(aTwo);
    }

    public static bool operator !=(SummaryKeyEquatable aOne, SummaryKeyEquatable aTwo) => !(aOne == aTwo);
  }
}
