🛡️ Arena Multi-Player Project
Um jogo de combate em arena desenvolvido em Unity 2D, focado em mecânicas de movimento e sobrevivência contra ondas de inimigos. Suporta até 4 jogadores locais.

🕹️ Funcionalidades
Multiplayer Local: Sistema de entrada e saída de jogadores (até 4 players) via Gamepad ou Teclado.

Sistema de Waves: Gerenciador de ondas totalmente configurável via Inspector (quantidade, tipo de inimigo e intervalos).

Inimigos Inteligentes:

FollowerEnemy: Persegue o jogador mais próximo.

RandomTargetEnemy: Escolhe um alvo aleatório e troca periodicamente.

ChargeEnemy: Patrulha a arena e realiza um ataque de investida (Dash) ao detectar um player.

📁 Estrutura do Namespace: GameplaySystem
O projeto segue uma arquitetura organizada para garantir escalabilidade:

GameplaySystem: Gerenciamento do ciclo de vida dos jogadores e inputs.

GameplaySystem.AI: Comportamentos e lógica de inteligência artificial.

GameplaySystem.Spawning: Lógica de instanciamento de inimigos e controle de ondas.

🚀 Como Configurar
Players: Certifique-se de que o Prefab do jogador tenha o componente PlayerController e a Tag Player.

Spawner: No objeto EnemySpawner, adicione as Waves na lista e arraste os prefabs dos inimigos para os campos correspondentes.

Inimigos: Todos os inimigos devem possuir um Rigidbody2D (Gravity Scale: 0) e as camadas de colisão configuradas adequadamente.
