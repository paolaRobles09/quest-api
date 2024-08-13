using Newtonsoft.Json;

namespace Quest.Core.Dto
{
    public sealed class QuestConfig
    {
        public int TotalQuestPointsNeeded { get; set; }
        public float RateFromBet { get; set; }
        public float LevelBonusRate { get; set; }
        public List<Milestone> Milestones { get; set; }

        private QuestConfig()
        {
            Milestones = new List<Milestone>();
        }

        private static QuestConfig _config;

        public static QuestConfig GetConfig() 
        {
            if (_config == null) 
            {
                _config = JsonConvert.DeserializeObject<QuestConfig>(File.ReadAllText("questConfig.json"));
            }

            return _config ?? new QuestConfig();
        }

    }
}
