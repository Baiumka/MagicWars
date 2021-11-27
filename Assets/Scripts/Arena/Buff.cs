using System.Collections;
using System.Collections.Generic;


public class Buff 
{
    public string name;
    public string displayName;
    public string discription;
    public bool isGood;
    public static List<Buff> buffList = new List<Buff>();

    public Buff(string name, string displayName, string discription, bool isGood)
    {
        this.name = name;
        this.displayName = displayName;
        this.discription = discription;
        this.isGood = isGood;
        buffList.Add(this);
    }

    public static Buff GetBuffByName(string buffName)
    {
        foreach (Buff buff in buffList)
        {
            if (buff.name == buffName) return buff;
        }
        return null;
    }

}
