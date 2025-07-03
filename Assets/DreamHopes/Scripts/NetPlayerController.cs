using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;
public class NetPlayerController : MonoBehaviour
{

    [HideInInspector]
    public UnityEvent<string> OnStartPlayerController = new UnityEvent<string>();

    public CombatController combatController;

    //public HudController hudController;

    private void OnEnable()
    {
        combatController = GetComponent<CombatController>();

       // hudController = FindObjectOfType<HudController>();

        // hudController.SetHudSkillsInfos(combatController.skillControllers);

    }
    
    private void OnDestroy()
    {
        
    }
}
