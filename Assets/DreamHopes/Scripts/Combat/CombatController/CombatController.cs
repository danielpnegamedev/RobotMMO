using System.Collections.Generic;
using UnityEngine;

public class CombatController : MonoBehaviour
{

    public CombatAttributesModel combatAttributesModel;

    public List<CombatSkillModel> combatSkillModel;

    public List<CombatSkillController> combatSkillControllers = new List<CombatSkillController>();

    public CombatSkillController combatSkillControllerCurrent;


    public CombatView combatView;

    public Transform indicatorContent;

    public bool isMob;

    private void Awake()
    {
       
    }
    
    private void Start()
    {
        
        combatAttributesModel.OnDamaged.AddListener(combatView.combatDamageFeedBack.SetActiveOnDemend);
        combatAttributesModel.OnDamaged.AddListener(UpdateAttributesUI);
    }
    
    private void OnDestroy()
    {
        // Remover o listener quando o objeto for destruído
        combatAttributesModel.OnDamaged.RemoveListener(combatView.combatDamageFeedBack.SetActiveOnDemend);
        combatAttributesModel.OnDamaged.RemoveListener(UpdateAttributesUI);
    }
    private void OnEnable()
    {
        combatView = GetComponent<CombatView>();

        if(TryGetComponent<NetPlayerController>(out NetPlayerController playerController))
        {
            SetHudSkillsInfos(combatSkillModel, combatView.GetHudSKillIcon());
        }
        else
        {
            isMob = true;
        }

        combatView.combatHudBarView[0].SetBarUI(combatAttributesModel.lifeMax);
        combatView.combatHudBarView[1].SetBarUI(combatAttributesModel.aetherMax);
        combatView.combatHudBarView[2].SetBarUI(combatAttributesModel.bioMax);
        combatView.combatHudBarView[3].SetBarUI(combatAttributesModel.natureCurrent);
    }

    public void UpdateAttributesUI(string stringg)
    {
        combatView.combatHudBarView[0].AtualizarBarraDeVida(combatAttributesModel.lifeMax,combatAttributesModel.lifeCurrent);
        combatView.combatHudBarView[1].AtualizarBarraDeVida(combatAttributesModel.aetherMax, combatAttributesModel.aetherCurrent);
        combatView.combatHudBarView[2].AtualizarBarraDeVida(combatAttributesModel.bioMax, combatAttributesModel.bioCurrent) ;
        combatView.combatHudBarView[3].AtualizarBarraDeVida(combatAttributesModel.natureMax, combatAttributesModel.natureCurrent);

    }

    public void SetHudSkillsInfos(List<CombatSkillModel> T_skills, List<CombatSkillHudIconView> combatHudSkillIconView)
    {
        for (int i = 0; i < T_skills.Count; i++)
        {
            Sprite T_skillIcon = T_skills[i].Skills[0].icon;
            string T_skillName = T_skills[i].Skills[0].skillName;
            string T_skillDescription = T_skills[i].Skills[0].description;

            combatHudSkillIconView[i].SetInfos(T_skillIcon, T_skillName, T_skillDescription);
        }
    }
    public void Update()
    {
        if (isMob) { return; }
        SkillInputCheck();
    }
    public void SkillInputCheck()
    {

        foreach(CombatSkillModel skillController in combatSkillModel)
        {
            if (Input.GetKeyDown(skillController.keyToUseSkill))
            {
                SetTryPrecastSkill(skillController);
            }

            if (Input.GetKeyUp(skillController.keyToUseSkill))
            {
                SetTryCastkill(skillController);
                // validação se pode etc...
                // se não pode feedback na tela
                // se pode solta a skill
            }
        }
    }

    public void SetTryPrecastSkill(CombatSkillModel skillModel)
    {
        CombatSkillDetailModel skillModelDetail = skillModel.Skills[0];

        List<TypeCombatResources> t_resourceTypes = skillModel.resourceTypes;
        List<float> t_resourceRequirements = skillModel.resourceRequirements;

        bool t_hasResourceRequirement = combatAttributesModel.CheckHasRequirement(t_resourceTypes, t_resourceRequirements);
        
     //   if (!t_hasResourceRequirement) { return; }

        GameObject t_indicator = Instantiate(skillModelDetail.skillPrefab, indicatorContent);

        CombatSkillController t_combatSkillController = t_indicator.GetComponent<CombatSkillController>();

        t_combatSkillController.SetReferences(this, skillModel, skillModelDetail);

        combatSkillControllers.Add(t_combatSkillController);

        combatSkillControllerCurrent = t_combatSkillController;

        //combatSkillController.combatSkillView.projectileType = t_skill.proje

        //  combatAttributesModel.ApplyConsumeResource(t_resourceTypes, t_resourceRequirements);

    }
    public void SetTryCastkill(CombatSkillModel skillModel)
    {
        CombatSkillDetailModel skillDetailModel = skillModel.Skills[0];

        List<TypeCombatResources> t_resourceTypes = skillModel.resourceTypes;

        List<float> t_resourceRequirements = skillModel.resourceRequirements;

        bool t_hasResourceRequirement = combatAttributesModel.CheckHasRequirement(t_resourceTypes, t_resourceRequirements);

        //if (!t_hasResourceRequirement) { return; }

        GameObject t_indicator = combatSkillControllerCurrent.gameObject;

        CombatSkillController t_combatSkillController = t_indicator.GetComponent<CombatSkillController>();

        combatAttributesModel.ApplyConsumeResource(t_resourceTypes, t_resourceRequirements);

        combatView.combatAnimationHandler.CastAnimation(skillDetailModel.animationType, t_combatSkillController);

        //combatSkillController.combatSkillView.projectileType = t_skill.proje

    }

    public void SetActiveSkill(CombatSkillController t_combatSkillController)
    {
        combatSkillControllers.RemoveAll(item => item == null);

        
        t_combatSkillController.StartSkill();
        
    }
}
