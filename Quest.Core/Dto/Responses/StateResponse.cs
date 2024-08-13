namespace Quest.Core.Dto.Responses
{
    public class StateResponse
    {
        public float TotalQuestPercentCompleted { get; set; }
        public int LastMilestoneIndexCompleted { get; set; }

        public StateResponse(float totalQuestPercentCompleted, int lastMilestoneIndexCompleted) { 
            TotalQuestPercentCompleted = totalQuestPercentCompleted;
            LastMilestoneIndexCompleted = lastMilestoneIndexCompleted;
        }
    }
}
