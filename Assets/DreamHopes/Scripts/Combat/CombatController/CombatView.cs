using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.UI;

using System.Linq;
public class CombatView : MonoBehaviour
{
    public List<CombatSkillHudIconView> hudSkillIconViews;
    public List<CombatHudBarView> combatHudBarView;
    public CombatAnimationHandler combatAnimationHandler;
    public CombatDamageFeedBack combatDamageFeedBack;
    private void Start()
    {
      
    }
    public List<CombatSkillHudIconView> GetHudSKillIcon()
    {
        if (hudSkillIconViews.Count == 0)
        {
            hudSkillIconViews = Object.FindObjectsByType<CombatSkillHudIconView>(FindObjectsSortMode.InstanceID).ToList();
        }
   
        return hudSkillIconViews.ToList();
    }
}
