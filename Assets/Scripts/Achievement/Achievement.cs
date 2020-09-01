namespace Achievement
{
    public class Achievement
    {
        public Achievement(string name, string description, int progression)
        {
            _name = name;
            _description = description;
            _maxProg = progression;
        }

        private string _name;
        private string _description;
        private int _maxProg;
    }
}
