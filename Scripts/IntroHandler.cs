using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

/// <summary>
/// IntroHandler exclusively exists in the first scene never to be replayed after the timer expires or is clicked through
/// Not linked to any other class
/// </summary>
public class IntroHandler : MonoBehaviour {

    //declare probably the only button in this script
    public Button introSkip;
    public float delay;

    // Use this for initialization
    void Start () {

        introSkip = introSkip.GetComponent<Button>();

        StartCoroutine( AutoPress(delay) );
    }
	
	// Update is called once per frame
	public void TapPress()
    {
        SceneManager.LoadScene("StartMenu", LoadSceneMode.Single);
    }

    //to automatically continue to the main menu
    public IEnumerator AutoPress(float delay)
    {
        yield return new WaitForSeconds(delay);

        TapPress();

    }

}
