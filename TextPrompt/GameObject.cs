using System;
using System.Collections.Generic;

namespace GameJam
{
    class GameObject
    {
        public string description;
        public int idNum;
        public List<string> keywords;

        public GameObject(string inDescription, List<string> inKeywords)
        {
            description = inDescription;
            keywords = inKeywords;
        }

        public void setIdNum(int inIdNum)
        {
            idNum = inIdNum;
        }

        public void setKeywords(List<string> inKeywords)
        {
            keywords = inKeywords;
        }

        public bool Contains(string keyword)
        {
            foreach (string toCheck in keywords)
            {
                bool match = toCheck.Equals(keyword);
                if (match == true)
                {
                    return true;
                }
            }

            return false;
        }

        public virtual string lookAt()
        {
            return description;
        }

        public virtual string pickUp()
        {
            return "You can't pick that up.";
        }

        public virtual string drop()
        {
            return "You're not holding that.";
        }

        public virtual string use()
        {
            return "It has no immediately obvious use.";
        }

        public virtual string useOn()
        {
            return "It has no immediately obvious use.";
        }

        public virtual string openObject()
        {
            return "That isn't something you can open.";
        }

        public virtual string closeObject()
        {
            return "That isn't something you can close.";
        }

        public virtual string equip()
        {
            return "That's not something you can equip.";
        }

        public virtual string attack()
        {
            return "You're not attacking that...";
        }

        public virtual string reload()
        {
            return "That isn't a weapon.";
        }

        public virtual string eat()
        {
            return "you can't eat that.";
        }

        public virtual string drink()
        {
            return "you can't drink that.";
        }

        public virtual string read()
        {
            return "There's nothing to read.";
        }
    }
}