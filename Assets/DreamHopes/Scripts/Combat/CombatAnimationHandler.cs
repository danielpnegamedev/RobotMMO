using UnityEngine;


public class CombatAnimationHandler : MonoBehaviour
{
    public Animator animator;
    public float hitsPerSecond = 1f; // N�mero de hits por segundo

    private string currentAnimation;
    private int currentAnimationInt;
    private bool isAnimationPlaying = false;
    private float animationDuration;

    // Flags para a��es em diferentes porcentagens da anima��o
    private bool[] actionTriggered = new bool[10];

    public  CombatController combatController;
    public CombatSkillController combatSkillController;

    void Start()
    {
        animator = GetComponent<Animator>();
        combatController = GetComponent<CombatController>();
        // Verifica se o par�metro 'Motion' � do tipo 'Motion' e altera o estado para a nova anima��

    }

    void Update()
    {
        
        if (isAnimationPlaying)
        {
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            if (stateInfo.normalizedTime >= 1f) // Checa se a anima��o terminou
            {
                EndAnimation(currentAnimation); // Finaliza a anima��o
                OnAnimationComplete(); // Dispara o evento de anima��o completa
                isAnimationPlaying = false; // Reseta o estado de anima��o

            }
            else
            {
                // Verifica o progresso da anima��o
                CheckAnimationProgress(stateInfo.normalizedTime);
            }
        }

        // Exemplo para iniciar a anima��o com uma tecla
      
    }

    public void CastAnimation(TypeAnimation animationName, CombatSkillController t_combatSkillController)
    {
        // Reseta o estado da anima��o
        if (isAnimationPlaying) { return; }
        combatSkillController = t_combatSkillController;
        ResetAnimationState();

        // Ativa a anima��o especificada
        animator.SetBool("Attack", true);
        animator.SetInteger("AttackType", ((int)animationName)); // Configura o tipo de ataque

        currentAnimation = "Attack";
        currentAnimationInt = ((int)animationName);
        // Ajusta a velocidade da anima��o com base em hitsPerSecond
        animationDuration = 1f / hitsPerSecond;
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        float originalDuration = stateInfo.length;
        float newSpeed = originalDuration / animationDuration;
        animator.speed = newSpeed;

        // Marca a anima��o como em execu��o
        isAnimationPlaying = true;

        // Reseta as flags de porcentagem
        for (int i = 0; i < actionTriggered.Length; i++)
        {
            actionTriggered[i] = false;
        }
    }

    public void EndAnimation(string animationName)
    {
        // Desativa a anima��o especificada usando SetBool
        animator.SetBool(animationName, false);
        animator.speed = 1f; // Reseta a velocidade da anima��o
        
    }

    private void ResetAnimationState()
    {
        // Reseta a anima��o
        if (!string.IsNullOrEmpty(currentAnimation))
        {
            animator.SetBool(currentAnimation, false);
        }
    }

    private void OnAnimationComplete()
    {
        //Debug.Log("A anima��o foi conclu�da.");
        // Adicione sua l�gica aqui para o que deve acontecer ap�s a anima��o terminar
    }

    private void CheckAnimationProgress(float normalizedTime)
    {
        // Verifica o progresso da anima��o e realiza a��es em cada percentual relevante
        for (int i = 0; i < 10; i++)
        {
            float progress = (i + 1) * 0.1f; // Percentual de progresso (0.1 = 10%)
            if (normalizedTime >= progress && !actionTriggered[i])
            {
                ActionAtPercentage(progress * 100);
                actionTriggered[i] = true;
            }
        }
    }

    private void ActionAtPercentage(float percentage)
    {
        // Mensagem de depura��o para indicar o progresso da anima��o
       // Debug.Log("A��o realizada em " + percentage + "% da anima��o.");
             // L�gica espec�fica para cada percentual da anima��o
             if(percentage >= 50) {  }
        switch (percentage)
        {
            case 10f:
                // L�gica para 10% da anima��o
               //  Debug.Log("A��o em 10% da anima��o.");
                // Adicione sua l�gica para 10% aqui, como gerar um efeito visual ou som
                break;

            case 20f:
                // L�gica para 20% da anima��o
                //Debug.Log("A��o em 20% da anima��o.");
                // Adicione sua l�gica para 20% aqui, como aplicar um status no inimigo
                break;

            case 30f:
                // L�gica para 30% da anima��o
               // Debug.Log("A��o em 30% da anima��o.");
                // Adicione sua l�gica para 30% aqui, como criar um impacto
                break;

            case 40f:
                // L�gica para 40% da anima��o
                ///Debug.Log("A��o em 40% da anima��o.");
                // Adicione sua l�gica para 40% aqui, como mudar a posi��o do personagem
                combatController.SetActiveSkill(combatSkillController);
                break;

            case 50f:
                // L�gica para 50% da anima��o
                //Debug.Log("A��o em 50% da anima��o.");
               

                // Adicione sua l�gica para 50% aqui, como iniciar um combo
                break;

            case 60f:
                // L�gica para 60% da anima��o
              //  Debug.Log("A��o em 60% da anima��o.");
                // Adicione sua l�gica para 60% aqui, como gerar part�culas
              
                break;

            case 70f:
                // L�gica para 70% da anima��o
                //    Debug.Log("A��o em 70% da anima��o.");
             

                break;

            case 80f:
                // L�gica para 80% da anima��o
          //      Debug.Log("A��o em 80% da anima��o.");
               
                break;

            case 90f:
                
                //       Debug.Log("A��o em 90% da anima��o.");

                break;

            case 100f:
                // L�gica para 100% da anima��o
           //     Debug.Log("A��o em 100% da anima��o.");
                // Adicione sua l�gica para 100% aqui, como finalizar o ataque ou resetar o estado
                break;

            default:
                // L�gica para percentuais n�o especificados
                break;
        }
    }

}
