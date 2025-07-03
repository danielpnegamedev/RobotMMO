using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections;
using TMPro;
public class CombatDamageFeedBack : MonoBehaviour
{
    public List<TextMeshProUGUI> dmgTxts;
    
    public void SetActiveOnDemend(string value)
    {
        int random = UnityEngine.Random.RandomRange(0, dmgTxts.Count);

        dmgTxts[random].text = value;

        dmgTxts[random].gameObject.SetActive(true);
       
    }
}
