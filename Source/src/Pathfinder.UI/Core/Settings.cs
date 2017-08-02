using Microsoft.Extensions.Configuration;
using System.IO;
using System.Linq;
namespace Pathfinder
{
    public class UISettings
    {
        public static IConfigurationRoot Configuration { get; set; }
        public static ViewerEnum MapViwer { get; set; }
        public static int[] Batch_list_finders { get; set; }
        public static int[] Batch_list_heuristics { get; set; }
        public static int[] Batch_list_Mutation { get; set; }
        public static int[] Batch_list_Crossover { get; set; }
        public static int[] Batch_list_Fitness { get; set; }
        public static int[] Batch_list_Selection { get; set; }

        public static int[] Batch_list_generate_pattern { get; set; }
        public static int[] Batch_list_map_diagonal { get; set; }

        public static AppModeEnum AppMode { get; set; }
        public static string Batch_folder { get; set; }

        public static int Batch_map_origin { get; set; }
        public static int Batch_map_qtd_to_generate { get; set; }
        public static int Batch_GATimesToRunPerMap { get; set; }
        static UISettings()
        {
            var builder = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            Configuration = builder.Build();
            MapViwer = (ViewerEnum)int.Parse(Configuration[nameof(MapViwer)]);
            AppMode = (AppModeEnum)int.Parse(Configuration[nameof(AppMode)].ToString());
            Batch_folder = Configuration[nameof(Batch_folder)].ToString();
            Batch_map_origin = int.Parse(Configuration[nameof(Batch_map_origin)].ToString());
            Batch_map_qtd_to_generate = int.Parse(Configuration[nameof(Batch_map_qtd_to_generate)].ToString());
            Batch_GATimesToRunPerMap = int.Parse(Configuration[nameof(Batch_GATimesToRunPerMap)].ToString());
            Batch_list_generate_pattern = Configuration.GetSection(nameof(Batch_list_generate_pattern)).GetChildren().Select(e => int.Parse(e.Value)).ToArray();
            Batch_list_map_diagonal = Configuration.GetSection(nameof(Batch_list_map_diagonal)).GetChildren().Select(e => int.Parse(e.Value)).ToArray();
            Batch_list_finders = Configuration.GetSection(nameof(Batch_list_finders)).GetChildren().Select(e => int.Parse(e.Value)).ToArray();
            Batch_list_heuristics = Configuration.GetSection(nameof(Batch_list_heuristics)).GetChildren().Select(e => int.Parse(e.Value)).ToArray();
            Batch_list_Mutation = Configuration.GetSection(nameof(Batch_list_Mutation)).GetChildren().Select(e => int.Parse(e.Value)).ToArray();
            Batch_list_Crossover = Configuration.GetSection(nameof(Batch_list_Crossover)).GetChildren().Select(e => int.Parse(e.Value)).ToArray();
            Batch_list_Fitness = Configuration.GetSection(nameof(Batch_list_Fitness)).GetChildren().Select(e => int.Parse(e.Value)).ToArray();
            Batch_list_Selection = Configuration.GetSection(nameof(Batch_list_Selection)).GetChildren().Select(e => int.Parse(e.Value)).ToArray();
        }
    }
}
