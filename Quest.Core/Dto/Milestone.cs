namespace Quest.Core.Dto
{
    public class Milestone : MilestoneCompleted
    {
        public Milestone(int index, int chipsAwarded) : base(index, chipsAwarded)
        {
        }

        public int QuestPointsRequired { get; set; }

    }
}
