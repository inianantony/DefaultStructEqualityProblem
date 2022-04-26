using System;
using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Running;

namespace DefaultStructEqualityProblem
{
  public class Program
  {
    static void Main(string[] args)
    {
      Summary summary = BenchmarkRunner.Run<Analyzer>();
      Console.Read();
      new Analyzer().Analyze();
    }
  }

  [MemoryDiagnoser]
  public class Analyzer
  {
    public readonly Dictionary<SummaryKeyTypeFirst, SummaryKeyTypeFirst> SummaryKeyTypeFirstDictionary = new();
    public readonly Dictionary<SummaryKeyGroupFirst, SummaryKeyGroupFirst> SummaryKeyGroupFirstDictionary = new();
    public readonly Dictionary<SummaryKeyEquatable, SummaryKeyEquatable> SummaryKeyEquatableDictionary = new();

    [Params(2)] public int TypeNeeded { get; set; }
    [Params(20)] public int MarketNeeded { get; set; }
    [Params(100)] public int UserGroupNeeded { get; set; }

    public void Analyze()
    {
      Console.WriteLine("==============================INSERTION===================================");

      ValidateSummaryKeyTypeFirst();

      ValidateSummaryKeyGroupFirst();

      ValidateSummaryKeyEquatable();

      Console.WriteLine();
      Console.WriteLine();
      Console.WriteLine();
      Console.WriteLine("================================LOOKUP=================================");

      ValidateSummaryKeyTypeFirstLookUp();

      ValidateSummaryKeyGroupFirstLookUp();

      ValidateSummaryKeyEquatableLookUp();

      Console.WriteLine();
      Console.WriteLine();
      Console.WriteLine();
      Console.WriteLine("================================HashCode Count =================================");
      var typeFirstHashCode = SummaryKeyTypeFirstDictionary.Keys.Select(key => key.GetHashCode()).Distinct();
      PrintToConsole(typeFirstHashCode.Count(), "SummaryKeyTypeFirstDictionaryHashCode");
      var groupFirstHashCode = SummaryKeyGroupFirstDictionary.Keys.Select(key => key.GetHashCode()).Distinct();
      PrintToConsole(groupFirstHashCode.Count(), "SummaryKeyGroupFirstDictionaryHashCode");
      var equitableHashCode = SummaryKeyEquatableDictionary.Keys.Select(key => key.GetHashCode()).Distinct();
      PrintToConsole(equitableHashCode.Count(), "SummaryKeyEquatableDictionaryHashCode");


      Console.WriteLine();
      Console.WriteLine();
      Console.WriteLine();
      Console.WriteLine("================================HashCode=================================");

      Console.WriteLine();
      Console.WriteLine(new SummaryKeyTypeFirst {PartitionType = "0", MarketType = "0", UserGroup = "0"}.GetHashCode());
      Console.WriteLine(
        new SummaryKeyTypeFirst {PartitionType = "0", MarketType = "0", UserGroup = "123"}.GetHashCode());
      Console.WriteLine(
        new SummaryKeyTypeFirst {PartitionType = "0", MarketType = "19", UserGroup = "223"}.GetHashCode());
      Console.WriteLine("=================================================================");

      Console.WriteLine();
      Console.WriteLine(new SummaryKeyGroupFirst
        {PartitionType = "0", MarketType = "0", UserGroup = "0"}.GetHashCode());
      Console.WriteLine(
        new SummaryKeyGroupFirst {PartitionType = "0", MarketType = "0", UserGroup = "123"}.GetHashCode());
      Console.WriteLine(
        new SummaryKeyGroupFirst {PartitionType = "0", MarketType = "19", UserGroup = "123"}.GetHashCode());
      Console.WriteLine("=================================================================");

      Console.WriteLine();
      Console.WriteLine(new SummaryKeyEquatable("0", "0", "0").GetHashCode());
      Console.WriteLine(new SummaryKeyEquatable("0", "0", "123").GetHashCode());
      Console.WriteLine(new SummaryKeyEquatable("0", "19", "123").GetHashCode());
      Console.WriteLine("=================================================================");

      Console.Read();
    }

    [GlobalSetup]
    public void Init()
    {
      ValidateSummaryKeyTypeFirst();

      ValidateSummaryKeyGroupFirst();

      ValidateSummaryKeyEquatable();
    }

    [Benchmark]
    public bool ValidateSummaryKeyEquatableLookUp()
    {
      return LoopAndProcess(LookUp);

      void LookUp(int partitionType, int marketType, int userGroup)
      {
        var key = new SummaryKeyEquatable(partitionType.ToString(), marketType.ToString(), userGroup.ToString());

        var summaryKeyEquatableData = SummaryKeyEquatableDictionary.ContainsKey(key);
      }
    }

    [Benchmark]
    public bool ValidateSummaryKeyTypeFirstLookUp()
    {
      return LoopAndProcess(LookUp);

      void LookUp(int partitionType, int marketType, int userGroup)
      {
        var key = new SummaryKeyTypeFirst
        {
          PartitionType = partitionType.ToString(), MarketType = marketType.ToString(), UserGroup = userGroup.ToString()
        };

        var summaryKeyTypeFirstData = SummaryKeyTypeFirstDictionary.ContainsKey(key);
      }
    }

    [Benchmark]
    public bool ValidateSummaryKeyGroupFirstLookUp()
    {
      return LoopAndProcess(LookUp);

      void LookUp(int partitionType, int marketType, int userGroup)
      {
        var key = new SummaryKeyGroupFirst
        {
          PartitionType = partitionType.ToString(), MarketType = marketType.ToString(), UserGroup = userGroup.ToString()
        };

        var summaryKeyGroupFirstData = SummaryKeyGroupFirstDictionary.ContainsKey(key);
      }
    }
    
    public bool ValidateSummaryKeyEquatable()
    {
      return LoopAndProcess(AddToDictionary);

      void AddToDictionary(int partitionType, int marketType, int userGroup)
      {
        var key = new SummaryKeyEquatable(partitionType.ToString(), marketType.ToString(), userGroup.ToString());

        SummaryKeyEquatableDictionary.Add(key, key);
      }
    }
    
    public bool ValidateSummaryKeyGroupFirst()
    {
      return LoopAndProcess(AddToDictionary);

      void AddToDictionary(int partitionType, int marketType, int userGroup)
      {
        var key = new SummaryKeyGroupFirst
        {
          PartitionType = partitionType.ToString(), MarketType = marketType.ToString(), UserGroup = userGroup.ToString()
        };

        SummaryKeyGroupFirstDictionary.Add(key, key);
      }
    }
    
    public bool ValidateSummaryKeyTypeFirst()
    {
      return LoopAndProcess(AddToDictionary);

      void AddToDictionary(int partitionType, int marketType, int userGroup)
      {
        var key = new SummaryKeyTypeFirst
        {
          PartitionType = partitionType.ToString(), MarketType = marketType.ToString(), UserGroup = userGroup.ToString()
        };
        SummaryKeyTypeFirstDictionary.Add(key, key);
      }
    }
    
    private void PrintToConsole(int count, string action)
    {
      Console.WriteLine();
      Console.WriteLine($"{action} has {count} records");
      Console.WriteLine("=================================================================");
    }

    private bool LoopAndProcess(Action<int, int, int> processFunc)
    {
      for (var partitionType = 0; partitionType < TypeNeeded; partitionType++)
      for (var marketType = 0; marketType < MarketNeeded; marketType++)
      for (var userGroup = 0; userGroup < UserGroupNeeded; userGroup++)
      {
        processFunc(partitionType, marketType, userGroup);
      }

      return true;
    }
  }
}