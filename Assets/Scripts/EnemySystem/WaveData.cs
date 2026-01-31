using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class Wave
{
    public string nomeDaHorda;
    public GameObject[] inimigosPossiveis;
    public int totalDeInimigos;
    public float intervaloEntreSpawn;
    public int quantidadePorVez = 1;
}

[CreateAssetMenu(fileName = "NovaConfiguracaoHorda", menuName = "Custom/Wave Data")]
public class WaveData : ScriptableObject
{
    public List<Wave> hordas;
}