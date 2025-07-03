using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New HudSkillTreeModel", menuName = "HudSkillTreeModel")]

[System.Serializable]
public class HudSkillTreeModel : ScriptableObject
{
    public new string skillArchetype;
    public Sprite archetypeSprite;
    public List<CombatSkillModel> skillsTierS;
    public List<CombatSkillModel> skillsTierA;
    public List<CombatSkillModel> skillsTierB;
}
