namespace Quest.Infrastructure.Data
{
    public class PlayerQuestState
    {
        public required string PlayerId { get; set; }
        public float TotalQuestPercentCompleted { get; set; }
        public int LastMilestoneIndexCompleted { get; set; }
        public int QuestPointsEarned { get; set; }
    }
}
