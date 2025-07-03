using UnityEngine;


public class CombatAnimationHandler : MonoBehaviour
{
    public Animator animator;
    public float hitsPerSecond = 1f; // Número de hits por segundo

    private string currentAnimation;
    private int currentAnimationInt;
    private bool isAnimationPlaying = false;
    private float animationDuration;

    // Flags para ações em diferentes porcentagens da animação
    private bool[] actionTriggered = new bool[10];

    public  CombatController combatController;
    public CombatSkillController combatSkillController;

    void Start()
    {
        animator = GetComponent<Animator>();
        combatController = GetComponent<CombatController>();
        // Verifica se o parâmetro 'Motion' é do tipo 'Motion' e altera o estado para a nova animaçã

    }

    void Update()
    {
        
        if (isAnimationPlaying)
        {
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            if (stateInfo.normalizedTime >= 1f) // Checa se a animação terminou
            {
                EndAnimation(currentAnimation); // Finaliza a animação
                OnAnimationComplete(); // Dispara o evento de animação completa
                isAnimationPlaying = false; // Reseta o estado de animação

            }
            else
            {
                // Verifica o progresso da animação
                CheckAnimationProgress(stateInfo.normalizedTime);
            }
        }

        // Exemplo para iniciar a animação com uma tecla
      
    }

    public void CastAnimation(TypeAnimation animationName, CombatSkillController t_combatSkillController)
    {
        // Reseta o estado da animação
        if (isAnimationPlaying) { return; }
        combatSkillController = t_combatSkillController;
        ResetAnimationState();

        // Ativa a animação especificada
        animator.SetBool("Attack", true);
        animator.SetInteger("AttackType", ((int)animationName)); // Configura o tipo de ataque

        currentAnimation = "Attack";
        currentAnimationInt = ((int)animationName);
        // Ajusta a velocidade da animação com base em hitsPerSecond
        animationDuration = 1f / hitsPerSecond;
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        float originalDuration = stateInfo.length;
        float newSpeed = originalDuration / animationDuration;
        animator.speed = newSpeed;

        // Marca a animação como em execução
        isAnimationPlaying = true;

        // Reseta as flags de porcentagem
        for (int i = 0; i < actionTriggered.Length; i++)
        {
            actionTriggered[i] = false;
        }
    }

    public void EndAnimation(string animationName)
    {
        // Desativa a animação especificada usando SetBool
        animator.SetBool(animationName, false);
        animator.speed = 1f; // Reseta a velocidade da animação
        
    }

    private void ResetAnimationState()
    {
        // Reseta a animação
        if (!string.IsNullOrEmpty(currentAnimation))
        {
            animator.SetBool(currentAnimation, false);
        }
    }

    private void OnAnimationComplete()
    {
        //Debug.Log("A animação foi concluída.");
        // Adicione sua lógica aqui para o que deve acontecer após a animação terminar
    }

    private void CheckAnimationProgress(float normalizedTime)
    {
        // Verifica o progresso da animação e realiza ações em cada percentual relevante
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
        // Mensagem de depuração para indicar o progresso da animação
       // Debug.Log("Ação realizada em " + percentage + "% da animação.");
             // Lógica específica para cada percentual da animação
             if(percentage >= 50) {  }
        switch (percentage)
        {
            case 10f:
                // Lógica para 10% da animação
               //  Debug.Log("Ação em 10% da animação.");
                // Adicione sua lógica para 10% aqui, como gerar um efeito visual ou som
                break;

            case 20f:
                // Lógica para 20% da animação
                //Debug.Log("Ação em 20% da animação.");
                // Adicione sua lógica para 20% aqui, como aplicar um status no inimigo
                break;

            case 30f:
                // Lógica para 30% da animação
               // Debug.Log("Ação em 30% da animação.");
                // Adicione sua lógica para 30% aqui, como criar um impacto
                break;

            case 40f:
                // Lógica para 40% da animação
                ///Debug.Log("Ação em 40% da animação.");
                // Adicione sua lógica para 40% aqui, como mudar a posição do personagem
                combatController.SetActiveSkill(combatSkillController);
                break;

            case 50f:
                // Lógica para 50% da animação
                //Debug.Log("Ação em 50% da animação.");
               

                // Adicione sua lógica para 50% aqui, como iniciar um combo
                break;

            case 60f:
                // Lógica para 60% da animação
              //  Debug.Log("Ação em 60% da animação.");
                // Adicione sua lógica para 60% aqui, como gerar partículas
              
                break;

            case 70f:
                // Lógica para 70% da animação
                //    Debug.Log("Ação em 70% da animação.");
             

                break;

            case 80f:
                // Lógica para 80% da animação
          //      Debug.Log("Ação em 80% da animação.");
               
                break;

            case 90f:
                
                //       Debug.Log("Ação em 90% da animação.");

                break;

            case 100f:
                // Lógica para 100% da animação
           //     Debug.Log("Ação em 100% da animação.");
                // Adicione sua lógica para 100% aqui, como finalizar o ataque ou resetar o estado
                break;

            default:
                // Lógica para percentuais não especificados
                break;
        }
    }

}
