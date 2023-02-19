namespace merge_list_of_objects_using_linq_in_c_sharp
{
    public class ResultClass
    {
        public HashSet<string> VerifiedResults { get; set; } = new();
        public HashSet<string> UnverifiedResults { get; set; } = new();
        public bool IsBlocked { get; set; } = false;
    }


    internal class Program
    {
        private static void Main()
        {
            var results = GetResults();

            var resultSelectMany = new ResultClass()
            {
                VerifiedResults = new HashSet<string>(results.SelectMany(x => x.VerifiedResults)),
                UnverifiedResults = new HashSet<string>(results.SelectMany(x => x.UnverifiedResults)),
                IsBlocked = results.LastOrDefault().IsBlocked,
            };

            var resultAggregate = results.Aggregate(new ResultClass(), (r, next) =>
            {
                r.IsBlocked = r.IsBlocked || next.IsBlocked;
                r.VerifiedResults.UnionWith(next.VerifiedResults);
                r.UnverifiedResults.UnionWith(next.UnverifiedResults);
                return r;
            });
        }

        private static List<ResultClass> GetResults()
        {
            return new List<ResultClass>()
            {
                new ResultClass()
                {
                    VerifiedResults = new HashSet<string> {"first", "second"},
                    UnverifiedResults = new HashSet<string> {"third"},
                    IsBlocked = false
                },
                new ResultClass()
                {
                    VerifiedResults = new HashSet<string> {"first", "fourth"},
                    UnverifiedResults = new HashSet<string> {"fifth"},
                    IsBlocked = true
                },
                new ResultClass()
                {
                    VerifiedResults = new HashSet<string>(),
                    UnverifiedResults = new HashSet<string> {"sixt", "seventh"},
                    IsBlocked = false
                }
            };
        }
    }
}