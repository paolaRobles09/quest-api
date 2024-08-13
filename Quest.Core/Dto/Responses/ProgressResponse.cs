namespace Quest.Core.Dto.Responses
{
    public class ProgressResponse
    {
        public int QuestPointsEarned { get; set; }
        public float TotalQuestPercentCompleted { get; set; }
        public List<MilestoneCompleted>? MilestonesCompleted { get; set; }

        public ProgressResponse(int questPointsEarned, float totalQuestPercentCompleted, List<MilestoneCompleted> milestoneCompleted) {
            QuestPointsEarned = questPointsEarned;
            TotalQuestPercentCompleted = totalQuestPercentCompleted;
            MilestonesCompleted = milestoneCompleted;
        }
    }
}
