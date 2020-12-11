using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    public GameObject block;
    public float StartTimeBtwSpawn;

    private float TimeBtwSpawn;
    private int NumberOfSpawn;

    public GameManager gam;

    private void Update()
    {
        if (!gam.GetDeath())
        {
            if (TimeBtwSpawn < 0)
            {
                SpawnBlock();
                TimeBtwSpawn = StartTimeBtwSpawn;
            }
            else
            {
                TimeBtwSpawn -= Time.deltaTime;
            }
            if (NumberOfSpawn >= 10)
            {
                NumberOfSpawn = 0;
                StartTimeBtwSpawn -= Time.deltaTime;
                TimeBtwSpawn -= Time.deltaTime;
            }
        }
    }

    void SpawnBlock()
    {

        int randy1 = Random.Range((int)(transform.position.y), (int)(transform.position.y) + 10);
        int randy2 = Random.Range((int)(transform.position.y), (int)(transform.position.y) + 3);
        int randy3 = Random.Range((int)(transform.position.y), (int)(transform.position.y) + 5);

        NumberOfSpawn++;
        int rand1 = Random.Range(-30,31);
        GameObject spawn1 = Instantiate(block,new Vector3(rand1, randy1, 0f),Quaternion.identity);

        NumberOfSpawn++;
        int rand2 = Random.Range(-30, 31);
        GameObject spawn2 = Instantiate(block, new Vector3(rand2, randy2, 0f), Quaternion.identity);

        NumberOfSpawn++;
        int rand3 = Random.Range(-30, 31);
        GameObject spawn3 = Instantiate(block, new Vector3(rand3, randy3, 0f), Quaternion.identity);

    }
}
