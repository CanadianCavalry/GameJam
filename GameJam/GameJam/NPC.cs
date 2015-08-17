using System.Collections.Generic;

namespace GameJam
{
    public class Dialog
    {
        public string response { get; private set; }
        public List<string> keywords;

        public Dialog(string inResponse, List<string> inKeywords)
        {
            response = inResponse;
            keywords = inKeywords;
        }
    }

    public class NPC : GameObject
    {
        private List<Dialog> dialogOptions;
        private string name;
        public string seenDesc;
        public string initSeenDesc;
        public string talkResponse;
        public bool firstSeen;
        public Area currentLocation;

        public NPC(string inDescription, List<string> inKeywords, string inName, string inSeenDesc)
            : base(inDescription, inKeywords)
        {
            description = inDescription;
            keywords = inKeywords;
            name = inName;
            seenDesc = inSeenDesc;
            initSeenDesc = inSeenDesc;
            dialogOptions = new List<Dialog>();
            currentLocation = null;
            talkResponse = null;
        }

        public NPC(string inDescription, List<string> inKeywords, string inName, string inSeenDesc, string inInitSeenDesc) : base(inDescription, inKeywords)
        {
            description = inDescription;
            keywords = inKeywords;
            name = inName;
            seenDesc = inSeenDesc;
            initSeenDesc = inInitSeenDesc;
            dialogOptions = new List<Dialog>();
            currentLocation = null;
            talkResponse = null;
        }

        public override string lookAt()
        {
            return description;
        }

        public override string talk()
        {
            return talkResponse;
        }

        public string ask(Dialog dialog)
        {
            return dialog.response;
        }

        public void setTalkResponse(string inTalkResponse)
        {
            talkResponse = inTalkResponse;
        }

        public void addDialog(Dialog inDialog)
        {
            dialogOptions.Add(inDialog);
        }

        public void removeDialog(Dialog inDialog)
        {
            dialogOptions.Remove(inDialog);
        }

        public void clearDialog()
        {
            dialogOptions = new List<Dialog>();
        }
    }
}