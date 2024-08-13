namespace Quest.Core.Dto.Requests
{
    public class ProgressRequest
    {
        public string PlayerId { get; set; }
        public int PlayerLevel { get; set; }
        public int ChipAmountBet { get; set; }

        public ProgressRequest(string playerId, int playerLevel, int chipAmountBet)
        {
            PlayerId = playerId;
            PlayerLevel = playerLevel;
            ChipAmountBet = chipAmountBet;
        }

    }
}
