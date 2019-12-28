using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningBonusObjects : MonoBehaviour
{
    [SerializeField] List<GameObject> m_ListObjects;
    [SerializeField] List<Transform> m_ListSpawners;

    private IEnumerator m_SpawningItems;
    private readonly System.Random random = new System.Random();

    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.5f);

        m_SpawningItems = SpawningItems();

        StartCoroutine(m_SpawningItems);
    }

    IEnumerator SpawningItems()
    {
        int indexObject = random.Next(m_ListObjects.Count);
        int indexSpawn = random.Next(m_ListSpawners.Count);
        int delayBetweenSpawns = random.Next(5, 10);

        GameObject bonusItem = Instantiate(m_ListObjects[indexObject], m_ListSpawners[indexSpawn].position, Quaternion.identity, null);

        yield return new WaitForSeconds(delayBetweenSpawns);

        yield return SpawningItems();
    }
}
