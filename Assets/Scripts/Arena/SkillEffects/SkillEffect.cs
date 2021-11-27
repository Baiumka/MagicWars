using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillEffect : MonoBehaviour
{
    protected PlayerModel owner;
    protected bool isFriendly;

    public void SendMyTargets(PlayerModel owner, bool isFriendly)
    {
        this.owner = owner;
        this.isFriendly = isFriendly;
    }
}
