namespace tic_tac_toe.Respond {
    public class StateResp {
        public StateResp() {
            board = new List<string>();
        }
        public string sign { get; set; } = "X";
        public bool isChanged { get; set; } = false;
        public List<string> board { get; set; }

        public string? winner { get; set; }
    }
}
