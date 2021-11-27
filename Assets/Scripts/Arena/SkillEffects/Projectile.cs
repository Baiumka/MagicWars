using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : SkillEffect
{
    [SerializeField]
    float projectileSpeed;

    Transform target;
    float newSpeed;
    void Update()
    {
        if (owner != null && target == null)
        {
            target = owner.projectileTarget;
            newSpeed = projectileSpeed;
        }
        if (target != null)
        {
            if (Vector3.Distance(target.position, transform.position) > projectileSpeed * 10)
            {
                newSpeed += projectileSpeed / 80f;
                float prev_x = transform.position.x;
                float prev_y = transform.position.y;
                float prev_z = transform.position.z;

                float final_x = target.position.x;
                float final_y = target.position.y;
                float final_z = target.position.z;
                transform.position = new Vector3(Mathf.Lerp(prev_x, final_x, newSpeed), Mathf.Lerp(prev_y, final_y, newSpeed), Mathf.Lerp(prev_z, final_z, newSpeed));
            }
            else
            {
                if (!isFriendly) owner.enemy.GetHit();
                Destroy(gameObject);
            }
        }
    }

   

}
