using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hide : SkillEffect
{

    public float lifetime;

    private void Start()
    {
        transform.position = owner.hideTarget.position;
        
        StartCoroutine(End());
    }

    IEnumerator End()
    {
        yield return new WaitForSeconds(lifetime);
        owner.enemy.GetHit();
        Destroy(gameObject);
    }
}
