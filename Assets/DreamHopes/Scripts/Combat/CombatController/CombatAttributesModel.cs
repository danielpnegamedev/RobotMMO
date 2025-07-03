using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.Events;


[System.Serializable]
public class CombatAttributesModel 
{
   
    public new string statsName;

    public float lifeCurrent;
    public float lifeMax;
    public float lifeRegen;

    public float bioCurrent;
    public float bioMax;
    public float bioRegen;
    public float bioDefense;
    public float bioDamage;


    public float natureCurrent;
    public float natureMax;
    public float natureRegen;
    public float natureResist;
    public float natureDamage;

    public float aetherCurrent;
    public float aetherMax;
    public float aetherRegen;
    public float aetherProtection;
    public float aetherDamage;

    public float criticalHit;
    public float castRate;
    public float precision;
    public float penetration;

    public float block;
    public float dodge;

    public float tenacity;
    public float stun;
    public float moveSpeed;

    public float range;
    public float AOE;

    [HideInInspector]
    public UnityEvent<string> OnDamaged = new UnityEvent<string>();

    public CombatAttributesModel Duplicate()
    {
        CombatAttributesModel newInstance = new CombatAttributesModel
        {
            statsName = this.statsName,
            lifeCurrent = this.lifeCurrent,
            lifeMax = this.lifeMax,
            lifeRegen = this.lifeRegen,
            bioCurrent = this.bioCurrent,
            bioMax = this.bioMax,
            bioRegen = this.bioRegen,
            bioDefense = this.bioDefense,
            bioDamage = this.bioDamage,
            natureCurrent = this.natureCurrent,
            natureMax = this.natureMax,
            natureRegen = this.natureRegen,
            natureResist = this.natureResist,
            natureDamage = this.natureDamage,
            aetherCurrent = this.aetherCurrent,
            aetherMax = this.aetherMax,
            aetherRegen = this.aetherRegen,
            aetherProtection = this.aetherProtection,
            aetherDamage = this.aetherDamage,
            criticalHit = this.criticalHit,
            castRate = this.castRate,
            precision = this.precision,
            penetration = this.penetration,
            block = this.block,
            dodge = this.dodge,
            tenacity = this.tenacity,
            stun = this.stun,
            moveSpeed = this.moveSpeed,
            range = this.range,
            AOE = this.AOE
        };

        // Se necessário, você pode duplicar o UnityEvent, mas tenha em mente que
        // UnityEvents não podem ser duplicados diretamente. Você pode precisar
        // configurar novos eventos se necessário.

        return newInstance;
    }
    public bool CheckHasRequirement(List<TypeCombatResources> resourceTypes,List<float> requiredValues)
    {

        if (resourceTypes.Count != requiredValues.Count)
        {
            throw new ArgumentException("As listas de tipos de recursos e valores exigidos devem ter o mesmo tamanho.");
        }

        int count = resourceTypes.Count;

        for (int i = 0; i < count; i++)
        {
            TypeCombatResources resource = resourceTypes[i];
            float requiredValue = requiredValues[i];
            switch (resource)
            {
                case TypeCombatResources.Mana:
                    return aetherCurrent >= requiredValue;
                    break;
                case TypeCombatResources.Life:
                    return aetherCurrent >= requiredValue;
                    break;
                case TypeCombatResources.CPU:
                    return aetherCurrent >= requiredValue;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(resource), resource, null);
            }
        }

        return false;
    }

    public void ApplyConsumeResource(List<TypeCombatResources> t_resourceTypes, List<float> t_consumedValues)
    {
        if (t_resourceTypes.Count != t_consumedValues.Count)
        {
            throw new ArgumentException("As listas de tipos de recursos e valores exigidos devem ter o mesmo tamanho.");
        }

        int count = t_resourceTypes.Count;

        for (int i = 0; i < count; i++)
        {
            TypeCombatResources resource = t_resourceTypes[i];

            float t_consumedValue = t_consumedValues[i];

            switch (resource)
            {
                case TypeCombatResources.Mana:
                    aetherCurrent -= t_consumedValue;
                    break;
                case TypeCombatResources.Life:
                     lifeCurrent -= t_consumedValue;
                    break;
                case TypeCombatResources.CPU:
                    aetherCurrent -= t_consumedValue;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(resource), resource, null);
            }
        }
    }
    
    public void ApplyDamage(float bio , float nature, float magik, float skillTotalDamage)
    {
        bio = CalculateBioChipvsDefense(bio);
        nature = CalculateNaturevsResist(nature);
        magik = CalculateNaturevsResist(magik);

        float totalDmgCalculated = bio + nature + magik;

        lifeCurrent -= totalDmgCalculated;
        OnDamaged.Invoke(totalDmgCalculated.ToString());
    }

    public float CalculateBioChipvsDefense(float TbioDamage)
    {   
        float damageAfterBioVsDefense = TbioDamage - bioDefense;
        return Mathf.Max(0, damageAfterBioVsDefense);
    }
    public float CalculateNaturevsResist(float TnatureDamage)
    {
        float damageAfterNatureVsResist = TnatureDamage - natureResist;
        return Mathf.Max(0,damageAfterNatureVsResist);
        
    }
    public float CalculateMagikvsProtection(float TmagikDamage)
    {
        float damageAfterMagikVsProtection = TmagikDamage - aetherProtection;
        return Mathf.Max(0, damageAfterMagikVsProtection);
    }


}
