using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New CombatSkillModel", menuName = "CombatSkillModel")]
[System.Serializable]
public class CombatSkillModel : ScriptableObject
{
    public List<CombatSkillDetailModel> Skills;
    public int currentSkillIndex;
    public float cooldown;
    public KeyCode keyToUseSkill;
    public float price;
    public TypeSkillTier tier;
    public TypeSkillCurrentStatus currentStatus;
    public int requiredLvl;
    public int currentLvl;
    public List<TypeCombatResources> resourceTypes;
    public List<float> resourceRequirements;

    // Método para duplicar CombatSkillModel
  
  
}
