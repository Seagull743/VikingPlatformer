using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LevelSeletor : MonoBehaviour
{
    public SceneFader fader;

    public Button[] levelButtons;
    public LevelNamesData levelNameData;
    bool notFirstLaunch = false;

    private void Awake()
    {
        notFirstLaunch = PlayerPrefs.GetInt("Launch") == 0 ? false : true;

        if (!notFirstLaunch)
        {
            PlayerPrefs.SetInt("Launch", 1);
        }
        if (!PlayerPrefs.HasKey("levelReached"))
        {
            ResetLevelReachedProgress();
        }

        if (!notFirstLaunch)
        {
            PlayerPrefs.SetInt("levelReached", 1);
        }
    }

    void Start()
    {
        ShowButtons();
    }

    // This gets called by the level buttons
    public void Select (int levelNumber)
    {
        fader.FadeTo(levelNameData.levelNames[levelNumber - 1]);
    }

    public void ShowButtons()
    {
        int levelReached = PlayerPrefs.GetInt("levelReached");

        foreach(Button b in levelButtons)
        {
            b.interactable = false;
        }

        for(int i = 0; i < levelButtons.Length; i++)
        {

            if (i < levelReached)
            {
                levelButtons[i].interactable = true;
            }
        }
    }

    public void ResetLevelReachedProgress()
    {
        PlayerPrefs.SetInt("levelReached", 1);
    }

    public void SetLevelReachedProgress(int levelReached)
    {
        PlayerPrefs.SetInt("levelReached", levelReached);
    }

    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            ResetLevelReachedProgress();
            ShowButtons();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetLevelReachedProgress(1);
            ShowButtons();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetLevelReachedProgress(2);
            ShowButtons();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SetLevelReachedProgress(3);
            ShowButtons();
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SetLevelReachedProgress(13);
            ShowButtons();
        }
#endif
        }
}
