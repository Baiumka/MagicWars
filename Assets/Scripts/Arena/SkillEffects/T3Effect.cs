using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T3Effect : SkillEffect
{
    public float lifetime;

    private void Start()
    {

        transform.position = new Vector3(-22.9f, 4.6f, -24.8f);
        GameObject prefab = Resources.Load<GameObject>("Prefabs/Skills/" + gameObject.name + "_P");
        if (prefab == null) prefab = Resources.Load<GameObject>("Prefabs/Skills/null");
        GameObject objectEffect = GameObject.Instantiate(prefab, owner.startPosition.position, prefab.transform.rotation);
        objectEffect.GetComponent<SkillEffect>().SendMyTargets(owner, isFriendly);
        StartCoroutine(End());
    }

    IEnumerator End()
    {
        yield return new WaitForSeconds(lifetime);
        owner.enemy.GetHit();
        Destroy(gameObject);
    }

}
