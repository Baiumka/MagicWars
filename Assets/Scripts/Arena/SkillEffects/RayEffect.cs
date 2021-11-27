using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayEffect : SkillEffect
{
    public float lifetime;

    private void Start()
    {
        transform.LookAt(owner.projectileTarget);
        StartCoroutine(End());
    }

    IEnumerator End()
    {
        yield return new WaitForSeconds(lifetime);
        owner.enemy.GetHit();
        Destroy(gameObject);
    }

}
