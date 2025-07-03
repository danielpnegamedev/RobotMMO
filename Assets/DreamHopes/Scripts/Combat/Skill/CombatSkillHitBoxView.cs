using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections;

public class CombatSkillHitBoxView : MonoBehaviour
{
    public CombatSkillController combatSkillController;

    private void OnTriggerEnter(Collider other)
    {
        if(!other.gameObject.TryGetComponent<CombatController>(out CombatController combatController)) { return; }

        combatSkillController.SetAddTargetQueue(combatController);

    }
    private void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.TryGetComponent<CombatController>(out CombatController combatController)) { return; }

        combatSkillController.SetRemoveTarget(combatController);

    }
    
}
