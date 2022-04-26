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

    public bool Equals(SummaryKeyEquatable other)
    {
      if (ReferenceEquals(null, other)) return false;
      if (ReferenceEquals(this, other)) return true;
      return PartitionType == other.PartitionType && MarketType == other.MarketType && UserGroup == other.UserGroup;
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (obj.GetType() != this.GetType()) return false;
      return Equals((SummaryKeyEquatable) obj);
    }

    public override int GetHashCode()
    {
      return HashCode.Combine(PartitionType, MarketType, UserGroup);
    }

    public static bool operator ==(SummaryKeyEquatable left, SummaryKeyEquatable right)
    {
      return Equals(left, right);
    }

    public static bool operator !=(SummaryKeyEquatable left, SummaryKeyEquatable right)
    {
      return !Equals(left, right);
    }
  }

  //public class SummaryKeyEquatable : IEquatable<SummaryKeyEquatable>
  //{
  //  public string PartitionType { get; }
  //  public string MarketType { get; }
  //  public string UserGroup { get; }

  //  public SummaryKeyEquatable(string partitionType, string marketType, string userGroup)
  //  {
  //    PartitionType = partitionType;
  //    MarketType = marketType;
  //    UserGroup = userGroup;
  //  }

  //  public bool Equals(SummaryKeyEquatable aKey) => (aKey != null) && (ReferenceEquals(this, aKey) || (aKey.PartitionType == PartitionType && aKey.MarketType == MarketType && aKey.UserGroup == UserGroup));

  //  public override bool Equals(object aCompareToKey)
  //  {
  //    if (aCompareToKey == null)
  //      return false;

  //    if (aCompareToKey is SummaryKeyEquatable key)
  //      return Equals(key);

  //    throw new ArgumentException("You can only use a SummaryKeyEquatable type.", nameof(aCompareToKey));
  //  }

  //  public override int GetHashCode() => PartitionType.GetHashCode() + MarketType.GetHashCode() + UserGroup.GetHashCode();

  //  public static bool operator ==(SummaryKeyEquatable aOne, SummaryKeyEquatable aTwo)
  //  {
  //    if (((object)aOne == null) && ((object)aTwo == null))
  //      return true;

  //    if (((object)aOne == null) || ((object)aTwo == null))
  //      return false;

  //    return aOne.Equals(aTwo);
  //  }

  //  public static bool operator !=(SummaryKeyEquatable aOne, SummaryKeyEquatable aTwo) => !(aOne == aTwo);
  //}
}
