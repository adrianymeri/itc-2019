using Timetabling.Internal.Specialized;

namespace Timetabling.Internal.Constraints
{
    public class MinGap : TimeConstraint
    {
        public MinGap(int id, int g, bool required, int penalty, int[] classes)
            : base(id, required, penalty, classes)
        {
            G = g;
        }

        public readonly int G;

        protected override (int hardPenalty, int softPenalty) Evaluate(Problem problem, Schedule[] configuration)
        {
            var penalty = 0;
            for (var i = 0; i < Classes.Length - 1; i++)
            {
                var ci = configuration[i];
                for (var j = i + 1; j < Classes.Length; j++)
                {
                    var cj = configuration[j];
                    if ((ci.Days & cj.Days) == 0u
                        || (ci.Weeks & cj.Weeks) == 0u
                        || ci.End + G <= cj.Start
                        || cj.End + G <= ci.Start)
                    {
                        continue;
                    }

                    penalty++;
                }
            }

            return Required ? (penalty, 0) : (0, Penalty * penalty);
        }
    }
}
