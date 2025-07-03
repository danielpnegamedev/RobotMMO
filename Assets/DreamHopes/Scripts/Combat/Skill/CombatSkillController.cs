using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CombatSkillController : MonoBehaviour
{
    public CombatSkillView combatSkillView;
    public CombatController combatController;
    public CombatSkillModel combatSkillModel;

    public CombatSkillDetailModel combatSkillDetailModel;

    public CombatSkillDetailLogic combatSkillDetailLogic = new CombatSkillDetailLogic();

    public int effectIndex;
    public List<CombatController> targetsAvaibles = new List<CombatController>(); 
    public void SetReferences(CombatController tCombatController, CombatSkillModel tCombatSkillModel, CombatSkillDetailModel tCombatSkillDetailModel)
    {
        combatController = tCombatController;
        combatSkillModel = tCombatSkillModel;
        combatSkillDetailModel = tCombatSkillDetailModel;
   
    }
    private void Start()
    {
        // Obtém todos os Transforms dentro do primeiro filho do objeto atual, incluindo os inativos
        SetAddReferences();
    }
    public void SetAddReferences()
    {
        List<CombatSkillHitBoxView> hitboxPrefabs = transform.GetChild(0).GetComponentsInChildren<CombatSkillHitBoxView>().ToList();
        Debug.Log($"Total de hitboxPrefabs encontrados: {hitboxPrefabs.Count}");
        // Atribui a referência do controlador a cada CombatSkillHitBoxView na lista
        foreach (CombatSkillHitBoxView h in hitboxPrefabs)
        {
            h.combatSkillController = this;
            Debug.Log($"Referencia combatSkillController atribuída ao objeto: {h.gameObject.name}");
        }
    }







    public void StartSkill()
    {
        combatSkillView.StartSkill();

        combatSkillDetailLogic.Initialize(combatSkillDetailModel);

        TickManager.OnTick += HandleTick;
    }
   
    void HandleTick()
    {
        // Processa a skill aqui
        SetAddTargetsToSkill();

        float HitPerSeconds = GetCastRate();

        SetApplyEffect(HitPerSeconds);

        if (!SetCheckCanEndEffect()) { return; }

        Destroy(gameObject);

    }
   
    public void EndSkill()
    {
        Destroy(gameObject);
    }

    void OnDestroy()
    {
        TickManager.OnTick -= HandleTick;
    }


    #region Middle funcs
    public float GetCastRate()
    {
        float castSpeed = combatController.combatAttributesModel.castRate + combatSkillDetailModel.skillCastRate;

        return Mathf.Max(0.05f, castSpeed);
    }
    public void SetAddTargetQueue(CombatController target)
    {
       targetsAvaibles.RemoveAll(item => item == null);

        bool exists = targetsAvaibles.Any(t => t == target);

        if (exists) { return; }

        TargetHit newTarget = new TargetHit
        {
            target = target,
            hitNumber = 0 // Inicialize conforme necessário
        };

        targetsAvaibles.Add(target);

        Debug.Log("Target adicionado.");
    }
    public void SetRemoveTarget(CombatController target)
    {
        targetsAvaibles.Remove(target);
    }
    public void SetAddTargetsToSkill()
    {
        if(targetsAvaibles.Count <= 0) { return; }

        foreach(CombatController newTargets in targetsAvaibles)
        {
            combatSkillDetailLogic.SetAddNewTarget(newTargets);
        }
        targetsAvaibles.Clear();
    }

    public void SetApplyEffect(float castRate)
    {
        float bio = combatController.combatAttributesModel.bioCurrent;
        float nature = combatController.combatAttributesModel.natureCurrent;
        float magik = combatController.combatAttributesModel.aetherCurrent;

        combatSkillDetailLogic.SetApplyEffect(bio, nature, magik, castRate);
    }
    public bool SetCheckCanEndEffect()
    {
        return combatSkillDetailLogic.SetCheckCanEndEffects();
    }
    #endregion
}
