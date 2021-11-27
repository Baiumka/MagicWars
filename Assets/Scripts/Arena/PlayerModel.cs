using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PlayerModel : MonoBehaviour
{
    
    public PlayerModel enemy;
    public Transform projectileTarget;
    public Transform hideTarget;    
    public Transform startPosition;

    [SerializeField]
    float textX;

    public string nickname;

    [SerializeField]
    string thisColor = "";
    [SerializeField]
    string enemyColor = "";
    [SerializeField]
    GUISkin mySkin;

    Animator animator;
    eState currentState = eState.IDLE;
    List<string> spellSequence = new List<string>();
    List<SpellLabel> guiSequence = new List<SpellLabel>();
    bool isCurrentSpellAttack;

    public void Attack(string skill)
    {
        SetAnimation(eState.ATTACK);
        isCurrentSpellAttack = true;
        spellSequence.Add(skill);
    }
    public void CastOnMe(string skill)
    {
        SetAnimation(eState.ON_ME);
        isCurrentSpellAttack = false;
        spellSequence.Add(skill);
    }
    public void GetHit()
    {
        SetAnimation(eState.DAMAGE);
    }
    public void Evade(int chance, double random, string dealer)
    {
        string guiText = "<color=#" + "7FFFD4" + "FF>Уворот</color>";
        string chatText = "[<color=#" + thisColor + ">" + nickname + "</color>]" + " увернулся(<color=#4682B4>Шанс: " + chance + "</color>) от атаки " + "[<color=#" + enemyColor + ">" + dealer + "</color>]";

        guiSequence.Add(new SpellLabel(guiText));
        Chat.BattleLog(chatText);
    }
    public void Clear(int type, string dealer)
    {
        string guiText = "<color=#" + "ffffff" + "FF>Очищен</color>";
        string chatText = "";
        switch (type)
        {
            case -1:
                chatText = "[<color=#" + thisColor + ">" + dealer + "</color>]" + " очистел от <color=#DC143C>негативных</color> эффектов " + "[<color=#" + thisColor + ">" + nickname + "</color>]";
                
                break;
            case 0:
                chatText = "[<color=#" + enemyColor + ">" + dealer + "</color>]" + " очистел от эффектов " + "[<color=#" + thisColor + ">" + nickname + "</color>]";
                break;
            case 1:
                chatText = "[<color=#" + enemyColor + ">" + dealer + "</color>]" + " очистел от <color=#00FF7F>положительынх</color> эффектов " + "[<color=#" + thisColor + ">" + nickname + "</color>]";
                break;
            default:
                break;
        }
        guiSequence.Add(new SpellLabel(guiText));
        Chat.BattleLog(chatText);
    }

    public void UselessSkill(string skill)
    {
        string chatText = "Элемент <"+skill+"> не подействовал, так как противник вне видемости.";
        Chat.BattleLog(chatText);
    }

    public void SpellKnown()
    {
        string guiText = "<color=#" + "D2691E" + "FF>Мысли прочитаны</color>";
        string chatText = "[<color=#" + thisColor + ">" + nickname + "</color>]" + " узнает какое заклинание произнесет его противник.";

        guiSequence.Add(new SpellLabel(guiText));
        Chat.BattleLog(chatText);
    }

    public void AddTime(int value, string dealer)
    {
        string guiText = "<color=#" + "D2691E" + "FF>Продление: +" + value + "</color> ";
        string chatText = "[<color=#" + enemyColor + ">" + dealer + "</color>]" + " продлил время эффектов на <color=#D2691E>+" + value + "</color> игроку " + "[<color=#" + thisColor + ">" + nickname + "</color>]";

        if (value < 0)
        {
             guiText = "<color=#" + "D2691E" + "FF>Cокращение: " + value + "</color> ";
             chatText = "[<color=#" + enemyColor + ">" + dealer + "</color>]" + " сократил время эффектов на <color=#D2691E>" + value + "</color> игроку " + "[<color=#" + thisColor + ">" + nickname + "</color>]";
        }

        guiSequence.Add(new SpellLabel(guiText));
        Chat.BattleLog(chatText);
    }

    public void Missed(int chance, double random)
    {
        string guiText = "<color=#" + "5F9EA0" + "FF>Промах</color>";
        string chatText = "[<color=#" + thisColor + ">" + nickname + "</color>]" + " промахнулся(<color=#4682B4>Шанс: " + chance + "</color>)";

        guiSequence.Add(new SpellLabel(guiText));
        Chat.BattleLog(chatText);
    }
    public void Kill()
    {
        SetAnimation(eState.DEAD);
        string chatText = "[<color=#" + thisColor + ">" + nickname + "</color>]" + " <color=#808080>умер.</color>";
        Chat.BattleLog(chatText);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
        SetAnimation(eState.IDLE);
    }
    public void Shadow()
    {
        string guiText = "<color=#" + "808080" + "FF>Тень</color>";
        string chatText = "[<color=#" + thisColor + ">" + nickname + "</color>]" + " скрылся в тени.";

        guiSequence.Add(new SpellLabel(guiText));
        Chat.BattleLog(chatText);
    }

    public void TakeDamage(int hp, int def, string dealer, bool isCrit, bool isIce)
    {
        string guiText = "Урон: <color=#" + "ff0000" + "FF>-" + hp + "</color>";
        string chatText = "[<color=#" + enemyColor + ">" + dealer + "</color>]" + " наносит <color=#DC143CFF>" + hp + "</color>(<color=#4682B4>Защита: " + def + "</color>) <color=#DC143CFF>урона</color> " + "[<color=#" + thisColor + ">" + nickname + "</color>]";
        if (isCrit)
        {
            guiText = "<color=#" + "ff0000" + "FF>Крит.</color>Урон: <color=#" + "ff0000" + "FF>-" + hp + "</color>";
            chatText = "[<color=#" + enemyColor + ">" + dealer + "</color>]" + " наносит <color=#FF0000>" + hp + "</color>(<color=#FF0000FF>Крит.</color>)(<color=#4682B4>Защита: " + def + "</color>) <color=#FF0000>урона</color> " + "[<color=#" + thisColor + ">" + nickname + "</color>]";
        }
        if (isIce)
        {
            guiText = "<color=#" + "4682B4" + "FF>Блок</color>";
            chatText = "[<color=#" + thisColor + ">" + nickname + "</color>]" + " заблокировал(<color=#4682B4>Льдом</color>) урон от " + "[<color=#" + enemyColor + ">" + dealer + "</color>]";
        }
        guiSequence.Add(new SpellLabel(guiText));
        Chat.BattleLog(chatText);
    }
    
    public void TakeBuff(string buff, int turns, string dealer)
    {
        //new Buff("Fire", "Ожог", "Бобобобо", false);
        Buff takeBuff = Buff.GetBuffByName(buff);
        string effectColor = "DC143C";
        if (takeBuff.isGood) effectColor = "ADFF2F";
        string guiText = "Эффект: <color=#" + effectColor + "FF>" + takeBuff.displayName + "</color>("+turns+")";
        string chatText = "[<color=#" + enemyColor + ">" + dealer + "</color>]" + " наложил эффект <color=#" + effectColor + "FF>" + takeBuff.displayName + "</color> (Длительность: <color=#4682B4>" + turns +"</color>) на " + "[<color=#" + thisColor + ">" + nickname + "</color>]";
        guiSequence.Add(new SpellLabel(guiText));
        Chat.BattleLog(chatText);        
    }

    public void TakeHeal(int hp, bool isIce)
    {
        string guiText = "<color=#" + "ADFF2F" + "FF>Исцеление:</color> +" + hp + "";
        string chatText = "[<color=#" + thisColor + ">" + nickname + "</color>]" + " исцеляеться на <color=#ADFF2F>" + hp + "</color> здоровья.";

        if (isIce)
        {
            guiText = "<color=#" + "4682B4" + "FF>Мороз</color>";
            chatText = "[<color=#" + thisColor + ">" + nickname + "</color>]" + " не получил исцеление (<color=#4682B4>Мороз</color>)";
        }
        guiSequence.Add(new SpellLabel(guiText));
        Chat.BattleLog(chatText);
    }

    public void TakeEffectDamage(int hp, string reason, string dealer, bool isIce)
    {
        Buff reasonBuff = Buff.GetBuffByName(reason);
        string reasonName;
        if (reasonBuff == null) reasonName = "";
        else reasonName = reasonBuff.displayName;


        string guiText = "<color=#" + "FF8C00" + "FF>" + reasonName + ": " + "</color><color=#ffffff>-" + hp + "</color>";
        string chatText = "[<color=#" + enemyColor + ">" + dealer + "</color>]" + " наносит <color=#DC143CFF>" + hp + "</color>(<color=#FF7F50>" + reasonName + "</color>) <color=#DC143CFF>урона</color> " + "[<color=#" + thisColor + ">" + nickname + "</color>]";

        if (isIce)
        {
            guiText = "<color=#" + "4682B4" + "FF>Блок</color>";
            chatText = "[<color=#" + thisColor + ">" + nickname + "</color>]" + " заблокировал(<color=#4682B4>Льдом</color>) урон от " + "[<color=#" + enemyColor + ">" + dealer + "</color>]";
        }

        guiSequence.Add(new SpellLabel(guiText));
        Chat.BattleLog(chatText);

    }

    void CastSpellLabel(string skillName)
    {
        Skill skill = Skill.GetSkillByName(skillName);
        string skillNameLabel = "<color=#"+ skill.color +">"+skill.displayName+"</color>";
        string chatText = "[<color=#" + thisColor + ">" + nickname + "</color>]" + " использует элемент: "+ skillNameLabel + ".";
        guiSequence.Add(new SpellLabel(skillNameLabel, Resources.Load<Sprite>("Sprites/Skills/" + skill.name)));
        Chat.BattleLog(chatText);
    }


    private void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
    }
    private void Start()
    {
        SetAnimation(eState.IDLE);
        transform.LookAt(enemy.gameObject.transform);
    }

    private void Update()
    {
        if (guiSequence != null)
        {
            if (guiSequence.Count > 0)
            {
                for (int i = 0; i < guiSequence.Count; i++)
                {
                    if (guiSequence[i].h < 400)
                    {
                        guiSequence[i].h += 2;
                        if (guiSequence[i].h >= 400)
                        {
                            guiSequence.Remove(guiSequence[i]);
                        }
                    }
                }
            }
        }
    }

    void OnGUI()
    {
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        Vector3 cameraRelative = Camera.main.transform.InverseTransformPoint(transform.position);
        if (cameraRelative.z > 0)
        {
            if (guiSequence != null)
            {
                if (guiSequence.Count > 0)
                {
                    int i = 0;
                    foreach (SpellLabel spellLabel in guiSequence)
                    {
                        Rect position = new Rect(screenPosition.x - 120, Screen.height - screenPosition.y - spellLabel.h + i * 10, 240f, 65f);
                        GUIContent content;
                        if (spellLabel.guiImage != null) content = new GUIContent(spellLabel.message, spellLabel.guiImage.texture);
                        else content = new GUIContent(spellLabel.message);
                        GUI.Label(position, content, mySkin.GetStyle("label"));
                        i++;
                    }
                }
            }
        }

    }

    void OnGetHit()
    {
        SetAnimation(eState.IDLE);
    }

    void OnCast(int isAttack)
    {
        if (spellSequence.Count > 0)
        {
            GameObject prefab = Resources.Load<GameObject>("Prefabs/Skills/" + spellSequence[0]);
            if (prefab == null) prefab = Resources.Load<GameObject>("Prefabs/Skills/null");
            GameObject objectEffect = GameObject.Instantiate(prefab, startPosition.position, prefab.transform.rotation);
            if (isAttack == 1) objectEffect.GetComponent<SkillEffect>().SendMyTargets(this, false);
            else objectEffect.GetComponent<SkillEffect>().SendMyTargets(enemy, true);
            CastSpellLabel(spellSequence[0]);
            spellSequence.Remove(spellSequence[0]);
        }
        SetAnimation(eState.IDLE);
    }
   

    void OnAttackEnded()
    {
        
        
    }

    void SetAnimation(eState state)
    {
        animator.SetBool("idle", false);
        animator.SetBool("damage", false);
        animator.SetBool("die", false);
        animator.SetBool("attack", false);
        animator.SetBool("victory", false);
        animator.SetBool("onMe", false);

        switch (state)
        {
            case eState.IDLE:
                animator.SetBool("idle", true);
                currentState = eState.IDLE;
                break;
            case eState.ATTACK:
                animator.SetBool("attack", true);
                currentState = eState.ATTACK;
                break;
            case eState.ON_ME:
                animator.SetBool("onMe", true);
                currentState = eState.ON_ME;
                break;
            case eState.DEAD:
                animator.SetBool("die", true);
                currentState = eState.DEAD;
                break;
            case eState.DAMAGE:
                animator.SetBool("damage", true);
                currentState = eState.DAMAGE;
                break;
            case eState.VICTORY:
                animator.SetBool("victory", true);
                currentState = eState.VICTORY;
                break;

        }
    }

}
class SpellLabel
{

    public string message;
    public int h;
    public Sprite guiImage;
    public SpellLabel(string message, Sprite img = null)
    {
        this.message = message;
        h = 150;
        guiImage = img;
    }
}

public enum eState
{
    IDLE,
    ATTACK,
    DEAD,
    DAMAGE,
    VICTORY,
    ON_ME,
}
