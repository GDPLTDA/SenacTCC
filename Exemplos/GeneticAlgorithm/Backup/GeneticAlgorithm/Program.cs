using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using SimpleGeneticAlgorithm;

namespace SimpleGeneticAlgorithm
{
    public delegate int FitnessDelegate(object chromosomeRepresentation);
    public delegate ChromosomePair SelectionDelegate();
    public struct ChromosomePair { public Chromosome parent1; public Chromosome parent2;}
    public class Chromosome
    {
        public string ChromosomeString { set; get; }
        public int CalculateFitness(FitnessDelegate fitnessFunction) { return fitnessFunction(ChromosomeString); }

        public static int SumStringCharacter(object chromosomestring)
        {
            int sum = 0; foreach (char c in (string)chromosomestring)
            {
                if (c == '1') { sum++; }
                else if (c == '0') { }
                else
                {
                    //tODO:AddErrorCondition
                }
            } return sum;
        }
        public Chromosome()
        {
            Random r = new Random(Convert.ToInt32(DateTime.Now.Ticks % Int16.MaxValue)); this.ChromosomeString = String.Empty;
            for (int i = 0; i < 8; i++)
            { this.ChromosomeString += "" + r.Next() % 2; } Thread.Sleep(1);
        }
    }
    public class GenerationManager
    {
        public Chromosome[] CurrentGen;
        public List<object> PastGenerations = new List<object>();
        public SelectionDelegate SelectionAlgorithm;
        public int CrossoverProbability = 50;
        //This is a percentagepublic 
        int MutationProbability = 25;
        public GenerationManager()
        {
            CurrentGen = new Chromosome[4];
            for (int i = 0; i < 4; i++)
            { CurrentGen[i] = new Chromosome(); }
        }
        public ChromosomePair BasicSelection()
        {
            ChromosomePair pair = new ChromosomePair(); int currentHighest = -1; int secondHighest = -1; for (int i = 0; i < 4; i++)
            {
                Random r = new Random(Convert.ToInt32(DateTime.Now.Ticks % Int16.MaxValue));
                int val = (CurrentGen[i].CalculateFitness(Chromosome.SumStringCharacter) + (r.Next() % 14));
                if (val >= currentHighest) { pair.parent2 = pair.parent1; pair.parent1 = CurrentGen[i]; secondHighest = currentHighest; currentHighest = val; }
                Thread.Sleep(1);
            } if (secondHighest == -1) { return BasicSelection(); } return pair;
        }
        public ChromosomePair Crossover(ChromosomePair pair)
        {
            ChromosomePair par = new ChromosomePair();
            Random r = new Random(Convert.ToInt32(DateTime.Now.Ticks % Int16.MaxValue)); bool doCrossover = (((r.Next() % 100) + 1) < CrossoverProbability);
            if (doCrossover) { par.parent1 = GenerateChild(pair); } else { par.parent1 = pair.parent1; par.parent2 = pair.parent2; } return par;
        }
        private Chromosome GenerateChild(ChromosomePair pair)
        {
            Byte[] parentOneArray = getByteArrayFromString(pair.parent1.ChromosomeString);
            Byte[] parentTwoArray = getByteArrayFromString(pair.parent2.ChromosomeString); Chromosome ret = new Chromosome();
            for (int i = 0; i < parentOneArray.Length; i++) { parentOneArray[i] = (byte)(parentOneArray[i] | parentTwoArray[i]); }
            ret.ChromosomeString = getStringFromByteArray(parentOneArray); return ret;
        }
        private byte[] getByteArrayFromString(string p)
        {
            List<byte> ret = new List<byte>(); foreach (Char c in p) { if (c == '1') { ret.Add(1); } else { ret.Add(0); } }
            return ret.ToArray();
        }private string getStringFromByteArray(byte[] p) { string a = string.Empty; foreach (byte c in p) { a += c; } return a; }
        public Chromosome Mutate(Chromosome entry)
        {
            Random r = new Random(Convert.ToInt32(DateTime.Now.Ticks % Int16.MaxValue));
            bool doMutation = (((r.Next() % 100)) < MutationProbability);
            Chromosome ret = new Chromosome(); ret.ChromosomeString = entry.ChromosomeString; if (doMutation)
            {
                if (entry.ChromosomeString.IndexOf('0') >= 0)
                {
                    byte[] tmp = getByteArrayFromString(entry.ChromosomeString); tmp[entry.ChromosomeString.IndexOf('0')] = 1;
                    ret.ChromosomeString = getStringFromByteArray(tmp);
                }
            } return ret;
        }
    }
}

class Program
{
    public static void Main(string[] args)
    {
        GenerationManager manager = new GenerationManager(); Console.WriteLine("First gen:");
        Chromosome[] Gen = manager.CurrentGen; foreach (Chromosome c in Gen) { Console.WriteLine(c.ChromosomeString + " " + c.CalculateFitness(Chromosome.SumStringCharacter)); } Console.WriteLine("Pair Matches");
        List<ChromosomePair> pairs = new List<ChromosomePair>(); foreach (Chromosome c in Gen)
        {
            ChromosomePair pair = manager.BasicSelection(); pairs.Add(pair);
            Console.WriteLine(pair.parent1.ChromosomeString + " " + pair.parent2.ChromosomeString);
        }
        Console.WriteLine("First Generation Before mutation Childrens");
        List<Chromosome> nextgen = new List<Chromosome>(); foreach (ChromosomePair p in pairs)
        {
            ChromosomePair pair = manager.Crossover(p);
            if (pair.parent2 == null) { Console.WriteLine(pair.parent1.ChromosomeString); nextgen.Add(pair.parent1); }
            else
            {
                Console.WriteLine(pair.parent1.ChromosomeString + " " + pair.parent2.ChromosomeString)
                    ; nextgen.Add(pair.parent1); nextgen.Add(pair.parent2);
            }
        } if (nextgen.Count > 4)
        { nextgen.RemoveRange(3, nextgen.Count - 1 - 3); }
        //Run mutations
        Console.WriteLine("First Generation After Mutation Childrens");
        for (int i = 0; i < nextgen.Count; i++)
        {
            nextgen[i] = manager.Mutate(nextgen[i]);
            Console.WriteLine(nextgen[i].ChromosomeString);
        } manager.PastGenerations.Add(manager.CurrentGen);
        manager.CurrentGen = nextgen.ToArray(); Console.WriteLine("How many more generations would you like to iterate");
        int iterations = Convert.ToInt32(Console.ReadLine()); for (int i = 0; i < iterations; i++)
        { IterateGeneration(manager); } Console.ReadLine();
    }
    public static void IterateGeneration(GenerationManager manager)
    {Chromosome[] Gen = manager.CurrentGen;
        foreach (Chromosome c in Gen){Console.WriteLine(c.ChromosomeString + " " + c.CalculateFitness(Chromosome.SumStringCharacter));}Console.WriteLine("Pair Matches");List<ChromosomePair> pairs = new List<ChromosomePair>();foreach (Chromosome c in Gen){ChromosomePair pair = manager.BasicSelection();pairs.Add(pair);Console.WriteLine(pair.parent1.ChromosomeString + " " + pair.parent2.ChromosomeString);}Console.WriteLine("First Generation Before mutation Childrens");List<Chromosome> nextgen = new List<Chromosome>();foreach (ChromosomePair p in pairs){ChromosomePair pair = manager.Crossover(p);if (pair.parent2 == null){Console.WriteLine(pair.parent1.ChromosomeString);nextgen.Add(pair.parent1);}else{Console.WriteLine(pair.parent1.ChromosomeString + " " + pair.parent2.ChromosomeString);
            nextgen.Add(pair.parent1);nextgen.Add(pair.parent2);}}if (nextgen.Count > 4){nextgen.RemoveRange(3, nextgen.Count - 1 - 3);}
        //Run mutations
        Console.WriteLine("First Generation After Mutation Childrens");for (int i = 0; i < nextgen.Count; i++
            ){nextgen[i] = manager.Mutate(nextgen[i]);Console.WriteLine(nextgen[i].ChromosomeString);}
        manager.PastGenerations.Add(manager.CurrentGen); 
        manager.CurrentGen = nextgen.ToArray(); }
}