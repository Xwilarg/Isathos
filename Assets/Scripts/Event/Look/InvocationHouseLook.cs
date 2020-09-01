namespace Event.Look
{
    public class InvocationHouseLook : ALook
    {
        public override string GetText(string name)
        {
            string res = null;
            if (name == "CRATE")
            {
                if (_currProgress == 0)
                    res = "An old dusty crate, I can't really look inside without removing the nails.";
            }
            else if (name == "BOOKCASE")
            {
                if (_currProgress == 0)
                    res = "There are a lot of well known novels taking dust, I however don't have the time to look at them now.";
            }
            else if (name == "DOOR")
            {
                if (_currProgress == 0)
                    res = "Things wouldn't go well if I were to leave right now.";
            }
            _currProgress++;
            return res;
        }
    }
}