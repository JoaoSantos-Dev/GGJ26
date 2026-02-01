using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public SonsData listaDeSons;
    public GameObject audioSourcePrefab;
    public int poolSize = 10;

    private List<AudioSource> pool = new List<AudioSource>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        InicializarPool();
    }

    private void InicializarPool()
    {
        if (pool.Count > 0) return;

        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(audioSourcePrefab, transform);
            AudioSource source = obj.GetComponent<AudioSource>();
            obj.SetActive(false);
            pool.Add(source);
        }
    }

    // --- NOVA FUNÇÃO PLAYEFFECT ---
    // Agora ela recebe o "SomConfig" (o pacotinho com volume)
    public void PlayEffect(SomConfig config, Vector3 posicao)
    {
        if (config == null || config.arquivo == null)
        {
            Debug.LogWarning("AudioManager: Tentando tocar um som nulo ou não configurado!");
            return;
        }

        AudioSource source = GetAvailableSource();

        if (source != null)
        {
            source.gameObject.SetActive(true);

            // Se o jogo for 2D, você pode usar a posição do player ou da câmera
            source.transform.position = posicao;

            source.clip = config.arquivo;
            source.volume = config.volume; // <--- AQUI ELE APLICA O SEU VOLUME DO SLIDER
            source.pitch = Random.Range(0.9f, 1.1f);
            source.Play();

            StartCoroutine(DeactivateSource(source, config.arquivo.length));
        }
    }

    // --- NOVA FUNÇÃO PLAYPASSO ---
    public void PlayPasso(Vector3 posicao)
    {
        // 1. Verifica se a lista existe e tem algo dentro
        if (listaDeSons.passos == null || listaDeSons.passos.Length == 0) return;

        SomConfig escolhido;

        if (listaDeSons.passosAleatorios)
        {
            int index = Random.Range(0, listaDeSons.passos.Length);
            escolhido = listaDeSons.passos[index];
        }
        else
        {
            // 2. SEGURANÇA: Se o índice atual for maior ou igual ao tamanho da lista (erro de bounds), 
            // a gente reseta ele para 0 imediatamente.
            if (listaDeSons.indiceAtualPasso >= listaDeSons.passos.Length)
            {
                listaDeSons.indiceAtualPasso = 0;
            }

            escolhido = listaDeSons.passos[listaDeSons.indiceAtualPasso];

            // 3. Incrementa o índice e usa o operador % (resto da divisão) 
            // para garantir que ele sempre volte a 0 quando chegar no fim.
            listaDeSons.indiceAtualPasso = (listaDeSons.indiceAtualPasso + 1) % listaDeSons.passos.Length;
        }

        PlayEffect(escolhido, posicao);
    }

    private AudioSource GetAvailableSource()
    {
        for (int i = 0; i < pool.Count; i++)
        {
            if (pool[i] != null && !pool[i].gameObject.activeInHierarchy)
            {
                return pool[i];
            }
        }
        return null;
    }

    private IEnumerator DeactivateSource(AudioSource source, float delay)
    {
        yield return new WaitForSeconds(delay);
        source.gameObject.SetActive(false);
    }
}