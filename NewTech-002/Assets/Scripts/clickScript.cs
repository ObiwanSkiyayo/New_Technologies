using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class clickScript : MonoBehaviour {
    
    public Transform target;

    public bool selected = false;

    private int counter;


    // Use this for initialization
    void Start () {



    }
	
	// Update is called once per frame
	void Update () {

        if (selected)
        {
            transform.GetChild(0).gameObject.SetActive(true);
        } else
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }


	}

    public void ShowInfo()
    {

        counter++;

        if (counter == 1)
        {
            selected = true;

        } else if (counter == 2)
        {
            selected = false;
            counter = 0;
        }

        
    }

}
