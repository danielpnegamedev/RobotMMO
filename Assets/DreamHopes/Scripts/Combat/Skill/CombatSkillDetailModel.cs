using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections;
public class CombatSkillDetailLogic
{
    [Header("TARGET")]
    public List<TargetHit> targets = new List<TargetHit>();
    public List<CombatSkillEffectLogic> effectLogic;
    public CombatSkillDetailModel combatSkillDetailModel;

    #region Skill Logic

    public void Initialize(CombatSkillDetailModel tCombatSkillDetailModel)
    {
        combatSkillDetailModel = tCombatSkillDetailModel;

       effectLogic = new List<CombatSkillEffectLogic>();

        foreach (Effect tEffect in combatSkillDetailModel.effects)
        {
            CombatSkillEffectLogic tEffectLogic = new CombatSkillEffectLogic();

            effectLogic.Add(tEffectLogic);
        }
    }


    public void SetAddNewTarget(CombatController target)
    {
        targets.RemoveAll(item => item.target == null);

        bool exists = targets.Any(t => t.target == target);

        if (exists) { return; }

        TargetHit newTarget = new TargetHit
        {
            target = target,
            hitNumber = 0 // Inicialize conforme necessário
        };

        targets.Add(newTarget);
        Debug.Log("Target adicionado.");
    }

    public void SetApplyEffect(float bio, float nature, float magik, float castRate)
    {
        int effectCount = combatSkillDetailModel.effects.Count;

        for (int i = 0; i < effectCount; i++)
        {
            CombatSkillEffectLogic tEffectLogic = effectLogic[i];
            if (!tEffectLogic.CheckIfCanHit(castRate)) { continue; }
            Effect tEffectModel = combatSkillDetailModel.effects[i];
            // if(hit.hitNumber >= tEffect.hitsMaxPerEnemy) { continue; }

            //   if(tEffect.hitTotal >= tEffect.hitTotalCurrent) { continue; }
            Debug.Log("targetscount = " + targets.Count);

            foreach (TargetHit targets in targets)
            {
                if (targets.target == null || targets.target.combatAttributesModel == null)
                {
                    Debug.LogError("Target is null in SetApplyEffect");
                    continue;
                }
             
                tEffectLogic.hittedSomeOne = true;
                targets.hitNumber += 1;
                tEffectLogic.hitTotalCurrent += 1;

                switch (tEffectModel.typeEffect)
                {
                    case TypeEffect.Damage:

                        bio = GetCalculateSkillValue(tEffectModel.bioBaseValue, bio, tEffectModel.bioScaleValue);
                        nature = GetCalculateSkillValue(tEffectModel.natureBaseValue, nature, tEffectModel.natureScaleValue);
                        magik = GetCalculateSkillValue(tEffectModel.magikBaseValue, magik, tEffectModel.magikScaleValue);

                        float total = magik + nature + bio;

                        targets.target.combatAttributesModel.ApplyDamage(bio, nature, magik, total);

                        break;
                    case TypeEffect.Stun:
                        break;
                    case TypeEffect.Root:
                        break;
                    case TypeEffect.Marca:
                        break;
                    case TypeEffect.Slow:
                        break;
                }
            }
        }
    }


    public float GetCalculateSkillValue(float baseValue, float attributeValue, float scalePercentValue)
    {
        float calculatedValue = baseValue + (attributeValue * (1 + scalePercentValue));
        return calculatedValue;
    }
    public bool SetCheckCanEndEffects()
    {
        int effectCount = combatSkillDetailModel.effects.Count;
        for (int i = 0; i < effectCount; i++)
        {
            CombatSkillEffectLogic tEffectLogic = effectLogic[i];
            Effect tEffectModel = combatSkillDetailModel.effects[i];

            tEffectLogic.hitDurationPreCurrent += 0.05f;
            foreach (EndSkill end in tEffectModel.endSkills)
            {
                switch (end.effectEndTrigger)
                {
                    case TypeEffectEndTrigger.Time:
                        return true;
                        break;
                    case TypeEffectEndTrigger.AfterHits:
                        return CheckHitEnd(tEffectModel,tEffectLogic);
                        break;
                    case TypeEffectEndTrigger.ContinueCombo:
                        return end.effectEndTriggerValue <= 0;
                        break;

                }
            }
        } 
           
        return false;
    }

    public bool CheckHitEnd(Effect tEffectModel, CombatSkillEffectLogic tEffectLogic)
    {
        bool hitEnded = false;

        if (tEffectModel.hitTotal <= tEffectLogic.hitTotalCurrent) { hitEnded = true; }

        if (!tEffectModel.hitDurationPostInfinity)
        {
            if (tEffectModel.hitDurationPostHit <= tEffectLogic.hitDurationPostHitCurrent) { hitEnded = true; }
        }
        if (!tEffectModel.hitDurationPreInfinity)
        {
            if (tEffectModel.hitDurationPreHit <= tEffectLogic.hitDurationPreCurrent) { hitEnded = true; }
        }


        return hitEnded;
    }
    /* 
    public bool CheckMaxHit()
    {
        bool canHit = true;
        foreach(Effect teffect in effects)
        {
          //  if(teffect.hitsMaxTotal < )
        }
        return canHit;
    }
    public bool CheckTime()
    {
        bool canHit = true;
        foreach (Effect teffect in effects)
        {
            //  if(teffect.hitsMaxTotal < )
        }
        return canHit;
    }
    public bool CheckNextEvent()
    {
        bool canHit = true;
        foreach (Effect teffect in effects)
        {
            //  if(teffect.hitsMaxTotal < )
        }
        return canHit;
    }
     */



    #endregion

}
public class CombatSkillEffectLogic
{
    public float hitCastRateTimeSinceLast = 0f; // Variável para rastrear o tempo desde o último hit
    public float hitDurationPreCurrent;
    public float hitAOELimitCurrent;
    public float hitDurationPostHitCurrent;
    public bool hittedSomeOne;
    public float hitTotalCurrent; // valor atual dos hits
    public List<float> hitsCurrentPerEnemys;

    public bool CheckIfCanHit(float hitsPerSecond)
    {
        float hitInterval = 1f / hitsPerSecond; // Intervalo entre hits com base na taxa de hits por segundo

        hitCastRateTimeSinceLast += Time.fixedDeltaTime; // Atualiza o tempo desde o último hit

        if (hitCastRateTimeSinceLast >= hitInterval)
        {
            // Aplica o hit aqui
            

            // Reseta o tempo desde o último hit
            hitCastRateTimeSinceLast -= hitInterval;
            return true;
        }
        return false;
    }
    /*

    void CheckIfCanHit(float hitsPerSecond)
{
   float hitInterval = 1f / hitsPerSecond; // Intervalo entre hits com base na taxa de hits por segundo

   timeSinceLastHit += Time.fixedDeltaTime; // Atualiza o tempo desde o último hit

   while (timeSinceLastHit >= hitInterval)
   {
       // Aplica o hit aqui
       ApplyHit();

       // Ajusta o tempo passado para manter o tempo extra
       timeSinceLastHit -= hitInterval;
   }
}
*/
}


[CreateAssetMenu(fileName = "New CombatSkillDetailModel", menuName = "CombatSkillDetailModel")]
[System.Serializable]
public class CombatSkillDetailModel : ScriptableObject
{
    [Header("General Infos")]
    public Sprite icon; // 
    public new string skillName; //
    public string description; //
    [Header("PreCast")]
    public GameObject skillPrefab;
    [Header("Cast")]
    public float skillCastRate;
    public TypeAnimation animationType;
    public GameObject castVFX;         // deixar direto na skillprefab // deixar cadastrarvel // deixar num enum
    public AudioClip castSound;        // deixar direto na skillprefab // deixar cadastrarvel // deixar num enum
    [Header("Projectile")]
    public TypeSkillSpawnPosition projectileSpawnPosition;
    public TypeSkillMovement projectileMoveType;
    public float projectileSpeed;
    public GameObject projectileCollisionVFX;
    public AudioClip projectileCollisionSound;
    [Header("TARGET")]
   
    public TypeTarget target;
    public List<Effect> effects;

    [Header("PostCast")]  // >> ACONTECE APÓS O FIM DE EFFECTtYPE
    public bool isAutoAttack;

}

#region SkillEnums
public enum TypeSkillTier { Tier_B, Tier_A, Tier_S }
public enum TypeSkillStage { Stage_B, Stage_A, Stage_S }
public enum TypeIndicator { None, FollowMouse, RotateAroundPlayer }
public enum TypeSkillCurrentStatus { Unlearned, LearnedLock, Learned, Enable }
public enum TypeAnimation { None, Punch, OneHandWeapon, TwoHandWeapon, OneHandMagic, TwoHandMagic, Bow }
public enum TypeSkillSpawnPosition { None, Caster, Indicator }
public enum TypeSkillMovement { None, Line, Orbit, ZigZag }
public enum TypeSkillCooldownRuleToStart { AfterCast, EffectEnd }
#endregion

[System.Serializable]
public class Effect
{
    [Header("EFFECT")]
    public TypeEffect typeEffect; // meche nos atributos do alvo


    [Header("Bio")]
    public float bioBaseValue;
    public float bioScaleValue;
    public float bioTotalValue;

    [Header("Nature")]
    public float natureBaseValue;
    public float natureScaleValue;
    public float natureTotalValue;

    [Header("Magik")]
    public float magikBaseValue;
    public float magikScaleValue;
    public float magikTotalValue;

    [Header("Hit Configs")]

    public float  hitCastRateSkill; // ex: 20 hits por segundo então seria 0.05 interval no caso o interval é sempre 0.05 oq vai ser cadastrado é 20 ou 10 por exemplo que representa hits por segundo
    
    public float hitDurationPreHit; // ex: 1 segundo
    public bool  hitDurationPreInfinity;
    public float hitDurationPostHit; // ex: 1 segundo
    public bool  hitDurationPostInfinity;
    public float hitTotal; // ex: 100 hits no maximo posso dar no geral
    public float hitAOELimit;//  quantos inimigos eu posso acertar em uma area 2 3 4 5 inimigos ?
    public float hitsMaxPerEnemy; // ex: 10 hits por inimigo no maximo 
    public bool  hitCanClearTargetsAfterHit;

    [Header("SPECIFIC END CONDITION")]
    public List<EndSkill> endSkills;
}
#region Effect 
public enum TypeCombatResources { None, Life, Mana, CPU, Resource }
public enum TypeSkillResourceItem { None, Bone, Leaf, Rock }
public enum TypeSkillDamage { BioChip, Magik, Nature }
public enum TypeSkillDamageProprety { Physic, Arcane, Dark, Divine, Fire, Water, Light, Cold, Venom, Wind }
[System.Serializable]
public class TargetHit
{
    public CombatController target = new CombatController();
    public int hitNumber;
}

[System.Serializable]
public class EndSkill
{
    public TypeEffectEndTrigger effectEndTrigger;
    public float effectEndTriggerValue;
    public bool isEffectEnded;

}

public enum TypeEffect { None, Silence, Stun, Slow, Root, Damage, Heal, Marca, ContinueCombo }
public enum TypeTarget { None, Self, Enemy, Ally, SelfAndAlly, SelfAndEnemy, AllyAndEnemy }
public enum TypeEffectEndTrigger { None, Button, DistancePercorrida, DistanceOfCaster, MovementCancel, ContinueCombo, AfterHits, Time }

#endregion







/*

[Header("REPENSAR NECESSIDADE DESSAS VARS")]
public string dropRate;
public SkillStage stage;
public SkillResourceItem costResourceItem;
public bool canUseWhileStunned; // does this skill can be use while stunned?
public bool doesCancelStealth; // cancel stealth from the user when this skill is used?
public bool startGCD; // says if this skill start a global cooldown
public SkillCooldownRuleToStart skillCooldownRuleToStart;
public bool canUseWhileGCD;//says if this skill can be used while a global cooldown is running
public bool canUseWhileMounted;
public bool canBeInterrupted;  // can this skill be interrupted when the character is attacked?
public bool canMoveAndCasting;
[Header("Casting")]

public float castingDuration;
public AnimationType animationCastingType;

public GameObject castingVFX;
public AudioClip castingSound;
}


*/
