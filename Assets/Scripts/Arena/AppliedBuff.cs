using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppliedBuff 
{
    public Buff buff;
    public int turns;
    public int value;

    public AppliedBuff(Buff buff, int turns)
    {
        this.buff = buff;
        this.turns = turns;
        this.value = 0;
    }

}
