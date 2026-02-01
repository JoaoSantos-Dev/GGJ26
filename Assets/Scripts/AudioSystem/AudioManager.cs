using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public SonsData listaDeSons;
    public GameObject audioSourcePrefab;
    public int poolSize = 10;
    private AudioSource musicSource;

    private float lastStepTime;
    public float stepCooldown = 0.1f;

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

        musicSource = gameObject.AddComponent<AudioSource>();
        musicSource.loop = true; // Música sempre repete
        musicSource.playOnAwake = false;

        InicializarPool();

        // Se você já tiver a música no SonsData, ela começa aqui:
        if (listaDeSons != null && listaDeSons.musicaDeFundo != null)
        {
            PlayMusic(listaDeSons.musicaDeFundo);
        }
    }

    public void PlayMusic(SomConfig config)
    {
        if (config == null || config.arquivo == null) return;

        musicSource.clip = config.arquivo;
        musicSource.volume = config.volume;
        musicSource.loop = true;
        musicSource.playOnAwake = false;
        musicSource.spatialBlend = 0;
        musicSource.Play();
    }

    public void StopMusic()
    {
        musicSource.Stop();
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

            source.transform.position = posicao;

            source.clip = config.arquivo;
            source.volume = config.volume;
            source.pitch = Random.Range(0.85f, 1.15f);
            source.Play();

            StartCoroutine(DeactivateSource(source, config.arquivo.length));
        }
    }

    public void PlayPasso(Vector3 posicao)
    {
        if (Time.time - lastStepTime < stepCooldown) return;

        lastStepTime = Time.time;

        if (listaDeSons.passos == null || listaDeSons.passos.Length == 0) return;

        SomConfig escolhido;

        if (listaDeSons.passosAleatorios)
        {
            int index = Random.Range(0, listaDeSons.passos.Length);
            escolhido = listaDeSons.passos[index];
        }
        else
        {

            if (listaDeSons.indiceAtualPasso >= listaDeSons.passos.Length)
            {
                listaDeSons.indiceAtualPasso = 0;
            }

            escolhido = listaDeSons.passos[listaDeSons.indiceAtualPasso];

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