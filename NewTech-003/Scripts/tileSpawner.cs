using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//using ValentijnsAssets.CocainStash;

public class tileSpawner : MonoBehaviour
{
    // --------------------------------- [ CREATE SINGLETON, TO MAKE ACCESS EASY ]

    private static tileSpawner instance;

    public static tileSpawner Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<tileSpawner>();
            }
            return instance;
        }
    }

    // --------------------------------- [ CREATE STACKS, TO USE SAME MEMORY / BETTER CPU PERFORMANCE ]


    private Stack<GameObject> floorTiles = new Stack<GameObject>();

    public Stack<GameObject> FloorTiles
    {
        get { return floorTiles; }
        set { floorTiles = value; }
    }

    private Stack<GameObject> tunnelTiles = new Stack<GameObject>();

    public Stack<GameObject> TunnelTiles
    {
        get { return tunnelTiles; }
        set { tunnelTiles = value; }
    }

    // --------------------------------- [ REST OF THE SHIT ]

    internal GameObject[] tilePrefabs;

    internal GameObject currentTile;

    internal Vector3 startPosition;
    internal Vector3 spawnPosition;

    internal GameObject player;
    internal Vector3 positionPlayer;

    internal float tileLength;

    // START ()
    void Start()
    {

        player = GameObject.FindWithTag("Player"); // Used for collision

        startPosition = transform.position;

        tilePrefabs = new GameObject[2]; // MAYBE 1 ??????

        tilePrefabs[0] = (GameObject)Resources.Load("Prefabs/FloorTile"); // I don't like using public/editor window to add object manually..
        tilePrefabs[1] = (GameObject)Resources.Load("Prefabs/TunnelTile");

        CreateTiles(10);

        for (int i = 0; i < 10; i++)
        {
            SpawnTile();
        }

    }

    // UPDATE ()
    void Update()
    {
        positionPlayer = player.transform.position; // Set to the tileSpawners position
    }

    // CREATE TILES ()
    public void CreateTiles(int num) // Instantiate tiles in the stack..
    {
        for (int i = 0; i < num; i++)
        {
            FloorTiles.Push(Instantiate(tilePrefabs[0]));
            TunnelTiles.Push(Instantiate(tilePrefabs[1]));

            // Using Peek to talk with the last added tile in the stack and set it inactive.. (non-visible)
            FloorTiles.Peek().SetActive(false);
            TunnelTiles.Peek().SetActive(false);
        }
    }

    // SPAWN TILES ()
    public void SpawnTile()
    {
        if (FloorTiles.Count == 0 || TunnelTiles.Count == 0)
        {
            CreateTiles(10); // We need 10 tiles of both to make sure that the player thinks it's an endless path.
        }

        int randomIndex = Random.Range(0, 2); // Random Index

        if (randomIndex == 0 && currentTile != null )
        {
            GameObject temp = FloorTiles.Pop(); // Pop it out of the stack, into the Game World.
            temp.SetActive(true); // Make visible
            temp.transform.position = currentTile.transform.GetChild(0).transform.GetChild(0).position; // Set position equal to the last tiles' first child: "AttachPoint"
            currentTile = temp; // set current tile equal to our new object
        }
        else if(randomIndex == 1 && currentTile != null)
        {
            GameObject temp = TunnelTiles.Pop();
            temp.SetActive(true);
            temp.transform.position = currentTile.transform.GetChild(0).transform.GetChild(0).position;
            currentTile = temp;
        }

        else // STARTING TILE
        {
            currentTile = tilePrefabs[1]; // Set tunnel as start tile.
            GameObject temp = TunnelTiles.Pop();
            temp.SetActive(true);
            temp.transform.position = startPosition; // Set the position equal to pathspawners position
            currentTile = temp;

        }

    }

}
