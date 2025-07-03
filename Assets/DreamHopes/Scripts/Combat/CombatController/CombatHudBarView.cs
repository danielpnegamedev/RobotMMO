using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class CombatHudBarView : MonoBehaviour
{
    [SerializeField] Transform content;
    [SerializeField] List<Image> barImgs;
    [SerializeField] GameObject imgPrefab;

    public float lifePerCube = 100; // Vida por bloco
    public Color color;

    private void Start()
    {
        // Inicialize a barra de vida com um exemplo de valor
        // Defina isso conforme necessário no seu contexto
        //SetBarUI(lifePerCube * 10); // Exemplo: 10 blocos para a barra completa
    }

    public void SetBarUI(float Max)
    {
        // Limpa a barra existente
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }
        barImgs.Clear();

        float numeroDeBlocos = CalcularNumeroDeBlocos(Max, lifePerCube);

       // Debug.Log($"Número de Blocos Calculado: {numeroDeBlocos}");

        for (int i = 0; i < numeroDeBlocos; i++)
        {
            GameObject go = Instantiate(imgPrefab, content);
            Image img = go.GetComponent<Image>();
            img.color = color;
            barImgs.Add(img);
        }

        //Debug.Log($"Total de Blocos Criados: {barImgs.Count}");
    }

    private int CalcularNumeroDeBlocos(float vidaTotal, float vidaPorBloco)
    {
        int numeroDeBlocos = Mathf.CeilToInt(vidaTotal / vidaPorBloco);
        //Debug.Log($"CalcularNumeroDeBlocos: vidaTotal={vidaTotal}, vidaPorBloco={vidaPorBloco}, númeroDeBlocos={numeroDeBlocos}");
        return numeroDeBlocos;
    }

    public void AtualizarBarraDeVida(float vidaMaxima, float vidaAtual)
    {
        
       // Debug.Log($"AtualizarBarraDeVida - Vida Máxima: {vidaMaxima}, Vida Atual: {vidaAtual}");

        float vidaAtualClamp = Mathf.Clamp(vidaAtual, 0f, vidaMaxima); // Garante que a vida atual esteja dentro dos limites

        int blocosCompletos = Mathf.FloorToInt(vidaAtualClamp / lifePerCube); // Calcula o número de blocos inteiros
        float preenchimentoParcial = (vidaAtualClamp % lifePerCube) / lifePerCube; // Calcula o preenchimento parcial

        //Debug.Log($"Blocos Completos: {blocosCompletos}, Preenchimento Parcial: {preenchimentoParcial}");

        // Define o preenchimento para os blocos completos
        for (int i = 0; i < blocosCompletos && i < barImgs.Count; i++)
        {
            barImgs[i].fillAmount = 1f;
      //      Debug.Log($"Bloco {i} preenchido completamente");
        }

        // Define o preenchimento parcial para o último bloco, se houver
        if (blocosCompletos < barImgs.Count)
        {
            barImgs[blocosCompletos].fillAmount = preenchimentoParcial;
        //    Debug.Log($"Bloco {blocosCompletos} preenchido parcialmente: {preenchimentoParcial}");
        }

        // Zera o preenchimento para os blocos além do último bloco preenchido
        for (int i = blocosCompletos + 1; i < barImgs.Count; i++)
        {
            barImgs[i].fillAmount = 0f;
         //   Debug.Log($"Bloco {i} zerado");
        }
    }
}
