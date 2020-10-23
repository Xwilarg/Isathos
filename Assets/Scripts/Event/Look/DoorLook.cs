namespace Event.Look
{
    public class DoorLook : ALook
    {
        public override string GetText(string lockReason)
        {
            if (lockReason == "Etahnia") return "The door is locked. \"Etahnia Alltunirya\" is written on the doorplate";
            if (lockReason == "Salenae") return "The door is locked. \"Salenae Ydrahill\" is written on the doorplate";
            if (lockReason == "Nachi") return "The door is locked. \"Nachi Shaenn\" is written on the doorplate";
            if (lockReason == "Yumena")
            {
                if (InformationManager.S.DidSummonYumena)
                    return "The door is locked. \"Yumena Miyuki\" is written on the doorplate";
                return "The door is locked. The doorplate is left empty.";
            }
            if (lockReason != "")
                return "The door is locked. The doorplate is left empty.";
            return "The door is locked.";
        }
    }
}