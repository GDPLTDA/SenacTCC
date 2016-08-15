using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Randomizations;
using HelperSharp;

namespace GeneticSharp.Domain.Mutations
{
    /// <summary>
    /// This operator replaces the value of the chosen gene with a uniform random value selected 
    /// between the user-specified upper and lower bounds for that gene. 
    /// <see href="http://en.wikipedia.org/wiki/Mutation_(genetic_algorithm)">Wikipedia</see>
    /// </summary>
    [DisplayName("Uniform")]
    public class UniformMutation : MutationBase
    {
        #region Fields
        private int[] m_mutableGenesIndexes;

        [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "mall")]
        private bool m_allGenesMutable;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="GeneticSharp.Domain.Mutations.UniformMutation"/> class.
        /// </summary>
        /// <param name="mutableGenesIndexes">Mutable genes indexes.</param>
        public UniformMutation(params int[] mutableGenesIndexes)
        {
            m_mutableGenesIndexes = mutableGenesIndexes;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GeneticSharp.Domain.Mutations.UniformMutation"/> class.
        /// </summary>
        /// <param name="allGenesMutable">If set to <c>true</c> all genes are mutable.</param>
        public UniformMutation(bool allGenesMutable)
        {
            m_allGenesMutable = allGenesMutable;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GeneticSharp.Domain.Mutations.UniformMutation"/> class.
        /// </summary>
        /// <remarks>Creates an instance of UniformMutation where some random genes will be mutated.</remarks>
        public UniformMutation() : this(false)
        {
        }
        #endregion

        #region Methods
        /// <summary>
        /// Mutate the specified chromosome.
        /// </summary>
        /// <param name="chromosome">The chromosome.</param>
        /// <param name="probability">The probability to mutate each chromosome.</param>
        protected override void PerformMutate(IChromosome chromosome, float probability)
        {
            ExceptionHelper.ThrowIfNull("chromosome", chromosome);

            var genesLength = chromosome.Length;

            if (m_mutableGenesIndexes == null || m_mutableGenesIndexes.Length == 0)
            {
                if (m_allGenesMutable)
                {
                    m_mutableGenesIndexes = Enumerable.Range(0, genesLength).ToArray();
                }
                else
                {
                    m_mutableGenesIndexes = RandomizationProvider.Current.GetInts(1, 0, genesLength);
                }
            }

            foreach (var i in m_mutableGenesIndexes)
            {
                if (i >= genesLength)
                {
                    throw new MutationException(this, "The chromosome has no gene on index {0}. The chromosome genes length is {1}.".With(i, genesLength));
                }

                if (RandomizationProvider.Current.GetDouble() <= probability)
                {
                    chromosome.ReplaceGene(i, chromosome.GenerateGene(i));
                }
            }
        }
        #endregion
    }
}