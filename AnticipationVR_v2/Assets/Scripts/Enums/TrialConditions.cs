namespace Enums
{
    public class TrialConditions
    {
        public enum ShotPlacement
        {
            Straight,
            Cross,
            ND
        }

        public enum TempOcclCondition
        {
            BI_42MS,
            AI_126MS,
            AI_336MS,
            None
        }

        public enum SpatOcclCondition
        {
            VisibleAvatar,
            HiddenAvatar
        }
    }
}