using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.UI;

public class CombatSkillView : MonoBehaviour
{
    public GameObject indicator;
    public GameObject rangeMax;
    public GameObject hitboxes;

    public LayerMask groundLayers;
    public TypeSkillSpawnPosition projectType;
    public TypeIndicator projectileType;

    private void Update()
    {
        Indicator_Arrow();
        Indicator_Target();
    }
    public void StartSkill()
    {
        rangeMax.gameObject.SetActive(false);
        indicator.gameObject.SetActive(false);
        hitboxes.gameObject.SetActive(true);
    }
   
    public void Indicator_Arrow()
    {
        if(projectileType != TypeIndicator.RotateAroundPlayer) { return; }
        Vector3 position = Vector3.zero;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, groundLayers))
        {
            // Pega a posição do hit no plano horizontal
            position = new Vector3(hit.point.x, transform.position.y, hit.point.z);
        }

        // Calcula a direção do alvo (posição do mouse)
        Vector3 direction = position - transform.position;

        // Calcula a rotação necessária para que a flecha aponte para a posição do mouse
        Quaternion targetRotation = Quaternion.LookRotation(direction);

        // Mantém apenas a rotação no eixo Y para que a flecha gire como um ponteiro
        targetRotation = Quaternion.Euler(0, targetRotation.eulerAngles.y, 0);

        // Aplica a rotação ao indicador
        indicator.transform.rotation = targetRotation;
    }
    public void Indicator_Target()
    {
        if (projectileType != TypeIndicator.FollowMouse) { return; }
        Vector3 position = Vector3.zero;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, groundLayers))
        {
            // Atualiza a posição do indicador para o ponto onde o raycast atingiu
            position = new Vector3(hit.point.x, hit.point.y + 0.1f, hit.point.z);

            // Move o indicador para a posição do mouse
            indicator.transform.position = position;
        }
    }

}

