using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;

namespace DefaultStructEqualityProblem
{
  public class Program
  {
    private static readonly Dictionary<SummaryKeyTypeFirst, SummaryKeyTypeFirst> SummaryKeyTypeFirstDictionary = new();
    private static readonly Dictionary<SummaryKeyGroupFirst, SummaryKeyGroupFirst> SummaryKeyGroupFirstDictionary = new();
    private static readonly Dictionary<SummaryKeyEquatable, SummaryKeyEquatable> SummaryKeyEquatableDictionary = new();

    private static readonly int _typeNeeded = 2;
    private static readonly int _marketNeeded = 20;
    private static readonly int _userGroupNeeded = 500;

    static void Main(string[] args)
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
      Console.WriteLine(new SummaryKeyTypeFirst { PartitionType = "0", MarketType = "0", UserGroup = "0" }.GetHashCode());
      Console.WriteLine(new SummaryKeyTypeFirst { PartitionType = "0", MarketType = "0", UserGroup = "123" }.GetHashCode());
      Console.WriteLine(new SummaryKeyTypeFirst { PartitionType = "0", MarketType = "19", UserGroup = "223" }.GetHashCode());
      Console.WriteLine("=================================================================");

      Console.WriteLine();
      Console.WriteLine(new SummaryKeyGroupFirst { PartitionType = "0", MarketType = "0", UserGroup = "0" }.GetHashCode());
      Console.WriteLine(new SummaryKeyGroupFirst { PartitionType = "0", MarketType = "0", UserGroup = "123" }.GetHashCode());
      Console.WriteLine(new SummaryKeyGroupFirst { PartitionType = "0", MarketType = "19", UserGroup = "123" }.GetHashCode());
      Console.WriteLine("=================================================================");

      Console.WriteLine();
      Console.WriteLine(new SummaryKeyEquatable("0", "0", "0").GetHashCode());
      Console.WriteLine(new SummaryKeyEquatable("0", "0", "123").GetHashCode());
      Console.WriteLine(new SummaryKeyEquatable("0", "19", "123").GetHashCode());
      Console.WriteLine("=================================================================");

      Console.Read();
    }

    private static void ValidateSummaryKeyEquatableLookUp()
    {
      var summaryKeyEquatableStopwatch = Stopwatch.StartNew();
      LoopAndProcess(LookUp);
      summaryKeyEquatableStopwatch.Stop();
      PrintToConsole(summaryKeyEquatableStopwatch);

      void LookUp(int partitionType, int marketType, int userGroup)
      {
        var key = new SummaryKeyEquatable(partitionType.ToString(), marketType.ToString(), userGroup.ToString());

        var summaryKeyEquatableData = SummaryKeyEquatableDictionary.ContainsKey(key);
      }
    }

    private static void ValidateSummaryKeyTypeFirstLookUp()
    {
      var summaryKeyTypeFirstStopwatch = Stopwatch.StartNew();
      LoopAndProcess(LookUp);
      summaryKeyTypeFirstStopwatch.Stop();
      PrintToConsole(summaryKeyTypeFirstStopwatch);

      void LookUp(int partitionType, int marketType, int userGroup)
      {
        var key = new SummaryKeyTypeFirst { PartitionType = partitionType.ToString(), MarketType = marketType.ToString(), UserGroup = userGroup.ToString() };

        var summaryKeyTypeFirstData = SummaryKeyTypeFirstDictionary.ContainsKey(key);
      }
    }

    private static void ValidateSummaryKeyGroupFirstLookUp()
    {
      var summaryKeyGroupFirstStopwatch = Stopwatch.StartNew();
      LoopAndProcess(LookUp);
      summaryKeyGroupFirstStopwatch.Stop();
      PrintToConsole(summaryKeyGroupFirstStopwatch);

      void LookUp(int partitionType, int marketType, int userGroup)
      {
        var key = new SummaryKeyGroupFirst { PartitionType = partitionType.ToString(), MarketType = marketType.ToString(), UserGroup = userGroup.ToString() };

        var summaryKeyGroupFirstData = SummaryKeyGroupFirstDictionary.ContainsKey(key);
      }
    }

    private static void ValidateSummaryKeyEquatable()
    {
      var summaryKeyEquatableStopwatch = Stopwatch.StartNew();
      LoopAndProcess(AddToDictionary);
      summaryKeyEquatableStopwatch.Stop();
      PrintToConsole(SummaryKeyEquatableDictionary.Count, summaryKeyEquatableStopwatch);

      void AddToDictionary(int partitionType, int marketType, int userGroup)
      {
        var key = new SummaryKeyEquatable(partitionType.ToString(), marketType.ToString(), userGroup.ToString());
        
        SummaryKeyEquatableDictionary.Add(key, key);
      }
    }

    private static void ValidateSummaryKeyGroupFirst()
    {
      var summaryKeyGroupFirstStopwatch = Stopwatch.StartNew();
      LoopAndProcess(AddToDictionary);
      summaryKeyGroupFirstStopwatch.Stop();
      PrintToConsole(SummaryKeyGroupFirstDictionary.Count, summaryKeyGroupFirstStopwatch);

      void AddToDictionary(int partitionType, int marketType, int userGroup)
      {
        var key = new SummaryKeyGroupFirst { PartitionType = partitionType.ToString(), MarketType = marketType.ToString(), UserGroup = userGroup.ToString() };

        SummaryKeyGroupFirstDictionary.Add(key, key);
      }
    }

    private static void ValidateSummaryKeyTypeFirst()
    {
      var summaryKeyTypeFirstStopWatch = Stopwatch.StartNew();
      LoopAndProcess(AddToDictionary);
      summaryKeyTypeFirstStopWatch.Stop();
      PrintToConsole(SummaryKeyTypeFirstDictionary.Count, summaryKeyTypeFirstStopWatch);

      void AddToDictionary(int partitionType, int marketType, int userGroup)
      {
        var key = new SummaryKeyTypeFirst { PartitionType = partitionType.ToString(), MarketType = marketType.ToString(), UserGroup = userGroup.ToString() };
        SummaryKeyTypeFirstDictionary.Add(key, key);
      }
    }

    private static void PrintToConsole(int count, Stopwatch summaryKeyTypeFirstStopWatch, [CallerMemberName] string callerName = "")
    {
      Console.WriteLine();
      Console.WriteLine($"{callerName} method took {summaryKeyTypeFirstStopWatch.ElapsedMilliseconds} milliseconds to insert {count} records");
      Console.WriteLine("=================================================================");
    }

    private static void PrintToConsole(int count, string action)
    {
      Console.WriteLine();
      Console.WriteLine($"{action} has {count} records");
      Console.WriteLine("=================================================================");
    }

    private static void PrintToConsole(Stopwatch summaryKeyTypeFirstStopWatch, [CallerMemberName] string callerName = "")
    {
      Console.WriteLine();
      Console.WriteLine($"{callerName} method took {summaryKeyTypeFirstStopWatch.ElapsedMilliseconds} milliseconds to lookup");
      Console.WriteLine("=================================================================");
    }

    private static void LoopAndProcess(Action<int, int, int> processFunc)
    {
      for (var partitionType = 0; partitionType < _typeNeeded; partitionType++)
      for (var marketType = 0; marketType < _marketNeeded; marketType++)
      for (var userGroup = 0; userGroup < _userGroupNeeded; userGroup++)
      {
        processFunc(partitionType, marketType, userGroup);
      }
    }
  }
}