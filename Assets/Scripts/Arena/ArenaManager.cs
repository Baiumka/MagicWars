using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaManager : MonoBehaviour
{

    [SerializeField]
    PlayerModel enemyModel;
    [SerializeField]
    PlayerModel playerModel;

    void Start()
    {
        Main.client.newEnemyFound += ShowCurrentEnemy;

        Main.client.onUserCastOnEnemy += UserCastOnEnemy;
        Main.client.onUserCastOnSelf += UserCastOnSelf;
        Main.client.onEnemyCastOnUser += EnemyCastOnUser;
        Main.client.onEnemyCastOnSelf += EnemyCastOnSelf;
        Main.client.onUserGoToShadow += UserGoToShadow;
        Main.client.onEnemyGoToShadow += EnemyGoToShadow;
        Main.client.onUserDead += Die;
        Main.client.onEnemyDead += Kill;
        Main.client.onSkillWasUseless += UselessSkill;
        Main.client.onUserChengeEnemy += ChangeEnemy;
        Main.client.onEnemyChengeEnemy += ChangeEnemy;

        Main.client.onUserEvade += UserEvade;
        Main.client.onEnemyEvade += EnemyEvade;

        Main.client.onUserAddedTime += UserAddTime;
        Main.client.onEnemyAddedTime += EnemyAddTime;

        Main.client.onUserMissed += Miss;
        Main.client.onEnemyMissed += EnemyMiss;


        Main.client.onUserTakeDamage += UserTakeDamage;
        Main.client.onEnemyTakeDamage += EnemyTakeDamage;

        Main.client.onUserTakeBuff += UserTakeBuff;
        Main.client.onEnemyTakeBuff += EnemyTakeBuff;

        Main.client.onUserTakeEffectDamage += UserTakeEffectDamage;
        Main.client.onEnemyTakeEffectDamage += EnemyTakeEffectDamage;
        Main.client.onUserClear += Clear;
        Main.client.onEnemyClear += EnemyClear;
        
        
        
        Main.client.onUserTakeHeal += UserTakeHeal;
        Main.client.onEnemyTakeHeal += EnemyTakeHeal;
        Main.client.SpellKnown += SpellKnown;
        Main.client.EnemySpellKnown += ShowEnemySpell;

    }

    void ShowEnemySpell(List<Skill> skills)
    {
        enemyModel.SpellKnown();
    }


    void SpellKnown()
    {
        playerModel.SpellKnown();
    }

    void UserTakeHeal(int hp, bool isIce)
    {
        playerModel.TakeHeal(hp, isIce);
    }

    void EnemyTakeHeal(int hp, bool isIce)
    {
        enemyModel.TakeHeal(hp, isIce);
    }

    void Clear(int type, string dealer)
    {
        playerModel.Clear(type, dealer);
    }

    void EnemyClear(int type, string dealer)
    {
        enemyModel.Clear(type, dealer);
    }


    void UserTakeEffectDamage(int hp, string reason, string dealer, bool isIce)
    {
        playerModel.TakeEffectDamage(hp, reason, dealer, isIce);
    }

    void EnemyTakeEffectDamage(int hp, string reason, string dealer, bool isIce)
    {
        enemyModel.TakeEffectDamage(hp, reason, dealer, isIce);
    }

    void UserTakeBuff(string buff, int turns, string dealer)
    {
        playerModel.TakeBuff(buff, turns, dealer);
    }

    void EnemyTakeBuff(string buff, int turns, string dealer)
    {
        enemyModel.TakeBuff(buff, turns, dealer);
    }

    void UserTakeDamage(int hp, int def, string dealer, bool isCrit, bool isIce)
    {
        playerModel.TakeDamage(hp, def, dealer, isCrit, isIce);
    }

    void EnemyTakeDamage(int hp, int def, string dealer, bool isCrit, bool isIce)
    {
        enemyModel.TakeDamage(hp, def, dealer, isCrit, isIce);
    }

    void Miss(int chance, double random)
    {
        playerModel.Missed(chance, random);
    }

    void EnemyMiss(int chance, double random)
    {
        enemyModel.Missed(chance, random);
    }

    void UserAddTime(int value, string dealer)
    {
        playerModel.AddTime(value, dealer);
    }

    void EnemyAddTime(int value, string dealer)
    {
        enemyModel.AddTime(value, dealer);
    }

    void UserEvade(int chance, double random, string dealer)
    {
        playerModel.Evade(chance, random, dealer);
    }

    void EnemyEvade(int chance, double random, string dealer)
    {
        enemyModel.Evade(chance, random, dealer);
    }

    void ChangeEnemy()
    {
        enemyModel.Hide();
    }

    void UselessSkill(string skill)
    {
        playerModel.UselessSkill(skill);
    }

    void Kill()
    {
        enemyModel.Kill();
    }
    void Die()
    {
        playerModel.Kill();
        enemyModel.Hide();
    }
    void UserGoToShadow()
    {
        playerModel.Shadow();
    }    
    void EnemyGoToShadow()
    {
        enemyModel.Shadow();
    }
    void UserCastOnEnemy(Skill skill)
    {
        playerModel.Attack(skill.name);
    }
    void UserCastOnSelf(Skill skill)
    {
        playerModel.CastOnMe(skill.name);
    }
    void EnemyCastOnUser(Skill skill)
    {
        enemyModel.Attack(skill.name);
    }
    void EnemyCastOnSelf(Skill skill)
    {
        enemyModel.CastOnMe(skill.name);
    }

    void ShowCurrentEnemy(ArenaPlayer arenaPlayer)
    {
        enemyModel.gameObject.SetActive(true);
        enemyModel.nickname = arenaPlayer.name;
    }

}
