using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class clickScript : MonoBehaviour {
    
    public Transform target;
    public float speed;
    public float distance;


    public bool pickedUp = false;

    private int counter;


    // Use this for initialization
    void Start () {



    }
	
	// Update is called once per frame
	void Update () {

        if (pickedUp)
        {
            gameObject.GetComponent<Rigidbody>().useGravity = false;
            float step = speed * Time.deltaTime;
            Vector3 targetposition = target.TransformPoint(new Vector3(0, 0, distance));
            transform.position = Vector3.MoveTowards(transform.position, targetposition, step);
        }


	}

    public void MoveClose()
    {

        counter++;

        if (counter == 1)
        {
            pickedUp = true;

        } else if (counter == 2)
        {
            pickedUp = false;
            counter = 0;

            GameObject.Find("ScoreBoard").GetComponent<scoreManager>().scored = true;

            gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * 40);
            gameObject.transform.Rotate(Random.Range(-30.0f, 30.0f), Random.Range(-30.0f, 30.0f), Random.Range(-30.0f, 30.0f));
            gameObject.GetComponent<Rigidbody>().useGravity = true;
        }

        //Debug.Log("hallo Noura");
        
    }

    public void ChangeScene()
    {

        SceneManager.LoadScene("mainScene");
    }
}
