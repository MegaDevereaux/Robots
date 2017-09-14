using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class settingMenu : MonoBehaviour {

    public Button musicButton;
    public Button sfxButton;
    public Button backButton;
    public Button menuButton;
    public Button settingButton;

    public Slider musicSlider;
    public Slider sfxSlider;

    public GameObject SettingMenu;
    
    // Use this for initialization
    void Start () {

        //GUI initializing
        settingButton = settingButton.GetComponent<Button>();

        //Setting menu UI initializing
        musicButton = musicButton.GetComponent<Button>();
        sfxButton = sfxButton.GetComponent<Button>();
        backButton = backButton.GetComponent<Button>();
        menuButton = menuButton.GetComponent<Button>();
        
        musicSlider = musicSlider.GetComponent<Slider>();
        sfxSlider = sfxSlider.GetComponent<Slider>();

        //setting menu initializing. All the buttons are under the Canvas and will only be visible when SettingMenu is enabled
        SettingMenu.SetActive(false);
    }

    // When pressing the setting button
    public void SettingPress()
    {
        //reveal the setting pop up
        SettingMenu.SetActive(true);

        //turn off the start menu buttons
        settingButton.enabled = false;

    }

    //to mute or unmute the music
    public void MusicPress()
    {

    }

    //to mute or unmute the sound effects
    public void SFXPress()
    {

    }

    public void SaveButtonPress()
    {
        GlobalScript.i.Save();
    }

    //when pressing the back button
    public void BackPress()
    {

        //turn on the start menu buttons
        settingButton.enabled = true;

        //turn off the credit pop up
        SettingMenu.SetActive(false);

    }

    //when pressing the back button
    public void MainMenuPress()
    {
        //loads the mentioned scene. The "single" mode closes all other scenes.
        SceneManager.LoadScene("StartMenu", LoadSceneMode.Single);
    }
}
