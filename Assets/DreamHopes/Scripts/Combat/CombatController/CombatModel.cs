using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.UI;

public class CombatModel : MonoBehaviour
{
    [HideInInspector]
    public UnityEvent<string> OnSkillInput = new UnityEvent<string>();
    [HideInInspector]
    public UnityEvent<string> OnSkillValidation = new UnityEvent<string>();
    [HideInInspector]
    public UnityEvent<string> OnSkillStart = new UnityEvent<string>();
    [HideInInspector]
    public UnityEvent<string> OnSkillMiddle = new UnityEvent<string>();
    [HideInInspector]
    public UnityEvent<string> OnSkillEnd = new UnityEvent<string>();

    
}
