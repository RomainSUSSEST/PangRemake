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
        yield return new WaitForSeconds(random.Next(10, 30));

        m_SpawningItems = SpawningItems();

        StartCoroutine(m_SpawningItems);
    }

    IEnumerator SpawningItems()
    {
        int indexObject = random.Next(m_ListObjects.Count);
        int indexSpawn = random.Next(m_ListSpawners.Count);
        int delayBetweenSpawns = random.Next(10, 30);

        GameObject bonusItem = Instantiate(m_ListObjects[indexObject], m_ListSpawners[indexSpawn].position, m_ListObjects[indexObject].transform.rotation, null);

        yield return new WaitForSeconds(delayBetweenSpawns);

        yield return SpawningItems();
    }
}
