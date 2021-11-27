using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class UsedSkill 
{
    public Skill skill;
    public bool isUsed;

    public UsedSkill(Skill skill, bool isUsed)
    {
        this.skill = skill;
        this.isUsed = isUsed;
    }


}
