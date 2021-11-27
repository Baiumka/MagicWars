using System.Collections;
using System.Collections.Generic;


public class Skill 
{
    public string name;
    public string displayName;
    public string friendlyDiscription;
    public string enemyDiscription;
    public string color;
    public int tier;
    public static List<Skill> skillList = new List<Skill>();
    public string displayCombo;

    public Skill(string name, string displayName, string displayCombo, string enemyDiscription, string friendlyDiscription,string color, int tier)
    {
        this.name = name;
        this.displayName = displayName;
        this.displayCombo = displayCombo;
        this.friendlyDiscription = friendlyDiscription;
        this.enemyDiscription = enemyDiscription;
        this.color = color;
        this.tier = tier;
        skillList.Add(this);
    }

    public void UpdateDesriprion(string  enemyDiscription, string friendlyDiscription)
    {
        this.friendlyDiscription = friendlyDiscription;
        this.enemyDiscription = enemyDiscription;
    }
    public static Skill GetSkillByName(string skillName)
    {
        foreach (Skill skill in skillList)
        {
            if (skill.name == skillName) return skill;
        }
        return null;
    }

}
