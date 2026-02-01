using UnityEngine;

[System.Serializable]
public class SomConfig
{
    public AudioClip arquivo;
    [Range(0f, 1f)] public float volume = 1f;
}

[CreateAssetMenu(fileName = "NovoCatalogoSons", menuName = "Audio/Catalogo de Sons")]
public class SonsData : ScriptableObject
{
    [Header("Sons de Personagem")]
    public SomConfig vestirMascara;
    public SomConfig trocarMascara;
    public SomConfig expirarMascara;
    public SomConfig tomarDano;

    [Header("Sons de Mascaras")]
    public SomConfig maskDash;
    public SomConfig maskPush;
    public SomConfig maskShield;
    public SomConfig maskTeleport;


    [Header("Configuração de Passos")]
    public SomConfig[] passos; // Cada passo pode ter seu volume individual!
    public bool passosAleatorios = true;

    [HideInInspector] public int indiceAtualPasso = 0;

    [Header("Sons de Level")]
    public SomConfig ondaDeInimigo;

    [Header("Sons de UI")]
    public SomConfig cliqueBotao;
}