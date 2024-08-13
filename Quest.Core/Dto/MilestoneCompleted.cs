namespace Quest.Core.Dto
{
    public class MilestoneCompleted(int index, int chipsAwarded)
    {
        public int Index { get; set; } = index;
        public int ChipsAwarded { get; set; } = chipsAwarded;
    }
}
