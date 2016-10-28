using UnityEngine;
using System.Collections;
//using ValentijnsAssets.CocainStash;

public class destroyMeScript : MonoBehaviour {

    public float delay = 2.0f;

    void Start()
    {

    }

    void Update()
    {

    }

    void OnTriggerExit(Collider other)
    {

        if (other.tag == "Player")
        {
            tileSpawner.Instance.SpawnTile();
            StartCoroutine(FallDown());
        }
    }

    IEnumerator FallDown()
    {
        yield return new WaitForSeconds(delay);
        GetComponent<Rigidbody>().isKinematic = false;
        yield return new WaitForSeconds(2);

        switch (gameObject.name)
        {
            case "floorTile":
                tileSpawner.Instance.FloorTiles.Push(gameObject);
                gameObject.transform.GetChild(0).GetComponent<Rigidbody>().isKinematic = true;
                gameObject.SetActive(false);
                break;

            case "tunnelTile":
                tileSpawner.Instance.TunnelTiles.Push(gameObject);
                gameObject.transform.GetChild(0).GetComponent<Rigidbody>().isKinematic = true;
                gameObject.SetActive(false);
                break;
        }
    }

}
