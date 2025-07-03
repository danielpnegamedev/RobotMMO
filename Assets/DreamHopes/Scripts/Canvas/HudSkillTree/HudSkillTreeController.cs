using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class HudSkillTreeController : MonoBehaviour
{

    public GameObject hudSkillTreeIconViewPrefab;
    public GameObject hudSkillTreePanelViewPrefab;
    public Transform  hudSkillTreePanelViewContent;

    public List<HudSkillTreeModel> skillTreeModel; // 1 represents spawn 1 panel view


  
    //public 

    private void OnEnable()
    {
        Invoke("SetSpawnTreePanels", 1);
    }

    public void SetSpawnTreePanels()
    {
        foreach (HudSkillTreeModel skillTreeModel in skillTreeModel)
        {
            GameObject skillTreePanelGO = Instantiate(hudSkillTreePanelViewPrefab, hudSkillTreePanelViewContent.transform);

            HudSkillTreePanelView skillTreePanel = skillTreePanelGO.GetComponent<HudSkillTreePanelView>();
            skillTreePanel.archetypeImg.sprite = skillTreeModel.archetypeSprite;
            skillTreePanel.archetypeTxt.text = skillTreeModel.skillArchetype;
            SetSpawnTreeIcons(skillTreeModel.skillsTierS, skillTreePanel.tierSContent);

            SetSpawnTreeIcons(skillTreeModel.skillsTierA, skillTreePanel.tierAContent);
            SetSpawnTreeIcons(skillTreeModel.skillsTierB, skillTreePanel.tierBContent);
        }
    }

    public void SetSpawnTreeIcons( List<CombatSkillModel> data, Transform content)
    {
        if (data == null)
        {
            Debug.LogError("Data is null!");
            return;
        }

        if (content == null)
        {
            Debug.LogError("Content transform is null!");
            return;
        }
        int count = data.Count;

        for (int i = 0; i < count; i++)
        {
            GameObject skillTreeIconGO = Instantiate(hudSkillTreeIconViewPrefab, content) as GameObject;
           
            HudSkillTreeIconView skillTreeIconView = skillTreeIconGO.GetComponent<HudSkillTreeIconView>();
            Image img = skillTreeIconGO.GetComponent<Image>();

            if (img == null)
            {
                Debug.Log("HudSkillTreeIconView component is null on instantiated GameObject!");
                continue;
            }

            if (data[i].Skills == null || data[i].Skills.Count == 0)
            {
                Debug.Log("Skills data is null or empty!");
                continue;
            }


            Sprite T_sprite = data[i].Skills[0].icon;
            string T_skillName = data[i].Skills[0].skillName;
            string T_description = data[i].Skills[0].description;

            Debug.Log("T_sprite"+ T_sprite +   " Skill Name> " + T_skillName + "T_description>" + T_description);

            skillTreeIconView.SetInfos(T_sprite, T_skillName, T_description);
        }
    }
    
    
   
}
