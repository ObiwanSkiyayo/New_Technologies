using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Text;
using System.Linq;
using System;


public class MainGameManager : MonoBehaviour
{
	internal static MainGameManager _instance;

	public static MainGameManager Instance {
		get {

			if (_instance == null) {
				// Create the Singleton class if non-existent and add it.

				GameObject go = new GameObject ("MainGameManager");
				var GameManager = go.AddComponent<MainGameManager> ();
				_instance = GameManager;

			}
			return _instance;

		}
	}

	// ----------------------------------------------------------------------------------------------------------| * |

	internal GameController _GameController; // GameController script

	public void Awake()
	{

		// We don't need to check our private backing field. 
		// using our public one is enough, as all our logic to check _instance for null is in there
		if(_instance == null)
		{
			_instance = this;
		} 
		else{
			Destroy (this);
		}
	}

	void Start(){

		_GameController = GameObject.FindGameObjectWithTag("Player").GetComponent<GameController>() as GameController; // GameController script reference.
   
	}

	void Update()
	{

	}

	public void PauseGame()
	{
		_GameController._multiplier = 0.0f;
	}

	public void Restart()
	{ // Restart the current scene (by pressing 'r').
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
	}

}
