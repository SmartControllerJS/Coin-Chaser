using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectorSpawn : MonoBehaviour
{
    public List<GameObject> effectors;
    public float respawnTime = 1f;
    private Vector2 screenBounds;

    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(
            Screen.width, Screen.height, Camera.main.transform.position.z));

        StartCoroutine(EffectTimer());
    }

    private void SpawnEffectors()
    {
        // Spawn a random effector from the given effector list every one second
        int i = Random.Range(0, effectors.Count);
        GameObject effector = Instantiate(effectors[i]) as GameObject; 
        effectors[i].transform.position = new Vector2(Random.Range(-screenBounds.x, screenBounds.x), screenBounds.y * -2);

    }

    IEnumerator EffectTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(respawnTime);
            SpawnEffectors(); 
        }
    }
}
