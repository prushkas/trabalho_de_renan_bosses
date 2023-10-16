using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCloneSpawner : MonoBehaviour
{
    public GameObject clonePrefab;  // Prefab do clone
    public float cloneCreationRate = 1.0f; // Taxa de criação de clones
    public int numberOfClones = 2; // Número de clones a serem criados

    private float nextCloneCreationTime;
    private float initialBossHealth;
    private bool shouldCreateClones = false;

    private ControleVida bossHealth; // Referência ao script de controle de vida do chefe

    void Start()
    {
        nextCloneCreationTime = Time.time + cloneCreationRate;
        bossHealth = GetComponent<ControleVida>();
        initialBossHealth = bossHealth.maxHealth / 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (bossHealth.GetCurrentHealth() <= initialBossHealth)
        {
            shouldCreateClones = true;
        }

        if (shouldCreateClones && Time.time > nextCloneCreationTime)
        {
            CreateClones();
            nextCloneCreationTime = Time.time + cloneCreationRate;
        }
    }
    
    void CreateClones()
{
    for (int i = 0; i < numberOfClones; i++)
    {
        // Calcule a posição de criação dos clones com base na posição do chefe
        Vector3 spawnPosition = transform.position + Random.insideUnitSphere * 2.0f;

        // Instancie o clone e adicione-o ao objeto que contém os clones
        GameObject clone = Instantiate(clonePrefab, spawnPosition, Quaternion.identity);
    }
}
}
