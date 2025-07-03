**Visão Geral**

Projeto ambicioso em fase inicial Feito com Unity que oferece um “skill framework” genérico: basta configurar parâmetros e referências no Inspector do Unity para criar **qualquer** habilidade de jogo mesmo que complexas como as de League of Legends, sem escrever código extra.

![RobotHit](https://github.com/user-attachments/assets/66c2b7d5-9e1a-4b65-a631-391a01b87152)

**Principais Características**

- **MVC**
    - **Model**: armazena atributos, cooldowns e dados de skills.
    - **View**: exibe barras de vida, ícones e efeitos visuais.
    - **Controller**: coordena a lógica de combate e de cada skill.
- **Princípios SOLID**
    - cada classe (handlers, managers, controllers) tem responsabilidade única.
- **Event‑Driven**
    - **TickManager.OnTick** dispara atualizações fixas para todos os sistemas.
    - **Callbacks de Animação** acionados em porcentagens definidas (ex.: lançar efeito aos 50% do clipe).
    - **UnityEvent** em `CombatAttributesModel` notifica UI e feedback de dano.
- **ScriptableObjects**
    - Dados de skill (dano, alcance, cooldown) configuráveis no Editor.
- **Modularidade & Extensibilidade**
    - Designers só arrastam, ajustam valores e ligam referências no Inspector para criar novas mecânicas

### 🎮 Sistema de Combate (Protótipo)

O **objetivo principal, por enquanto, é atacar uma slime** e o sistema está contabilizando **dano real na vida da slime**.

A estrutura já permite **criar novas skills simples.**

### 🎮 Controles

- **W, A, S, D** — Movimentação do personagem
- **Q** — Usa uma skill

