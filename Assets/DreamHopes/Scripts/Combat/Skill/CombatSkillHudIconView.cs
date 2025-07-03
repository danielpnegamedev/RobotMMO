using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
public class CombatSkillHudIconView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image skillIconBG;

    public Image skillIcon;

    public Image skillIconFeedback;

    public Button skillHudBtn;

    public GameObject skillInfosGO;
    public TextMeshProUGUI skillNameTxt;
    public TextMeshProUGUI skillDescriptionTxt;


    public void OnPointerEnter(PointerEventData eventData)
    {
        // Coloque aqui o que você quer que aconteça
       
        if(eventData.pointerEnter == skillHudBtn.gameObject)
        {
            Debug.Log("Mouse entrou no botão!");
            SetSkillInfosOn();
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (eventData.pointerEnter == skillHudBtn.gameObject)
        {
            Debug.Log("Mouse Saiu no botão!");
            SetSkillInfosOff();
        }
    }
    public void SetSkillInfosOn()
    {
        skillInfosGO.gameObject.SetActive(true);
    }
    public void SetSkillInfosOff()
    {
        skillInfosGO.gameObject.SetActive(false);
    }
    public void SetInfos(Sprite T_skillIcon, string T_skillName, string T_skillDescription)
    {
        //Debug.Log(T_skillName);

        skillIcon.sprite = T_skillIcon;
        skillNameTxt.text = T_skillName;
        skillDescriptionTxt.text = T_skillDescription;
    }

 
}
