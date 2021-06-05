using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class MultipleSelectionsManager : MonoBehaviour
{


    public SelectionText[] selectionTexts;
    public ToggleGroup toggleGroupInstance;
    public Toggle toggle;
    private List<Toggle> toggles = new List<Toggle>();
    private Toggle _currentSelection
    {
        get { return toggleGroupInstance.ActiveToggles().FirstOrDefault(); }
    }
    public TextMeshProUGUI questionText;
    public TextAsset jsonTextFile;
    [System.Serializable]
    public class SelectionTextInstance
    {
        public MultipleSelectionText[] selectionTexts;
    }
    public SelectionTextInstance selectionTextInstance = new SelectionTextInstance();
    private Dictionary<string, string> userLogs = new Dictionary<string, string>();
    public Dictionary<string, string> UserLogs
    {
        get { return userLogs; }
    }
    private int currentSelectionIndex = 0;
    private int sizeOfSelections;

    // Start is called before the first frame update
    void Start()
    {
        selectionTextInstance = JsonUtility.FromJson<SelectionTextInstance>(jsonTextFile.text);
        sizeOfSelections = selectionTextInstance.selectionTexts.Length - 1;
        SetToggleTexts();
    }

    private void SetToggleTexts()
    {
        InitToggleGroup();
        MultipleSelectionText currentSelections = selectionTextInstance.selectionTexts[currentSelectionIndex];
        questionText.text = currentSelections.KeyString;
        int i = 0;
        foreach (string selection in currentSelections.selections)
        {
            Toggle newToggle = (Toggle)Instantiate(toggle, Vector3.one, Quaternion.identity);
            if (i == 0)
            { //set default selection
                newToggle.isOn = true;
            }
            else
            {
                newToggle.isOn = false;
            }
            TextMeshProUGUI toggleText = newToggle.transform.Find("Background/ToggleText").GetComponent<TextMeshProUGUI>();
            toggleText.text = selection;
            toggleText.fontSize = 14;
            newToggle.group = toggleGroupInstance;
            newToggle.transform.SetParent(toggleGroupInstance.transform, false);
            toggles.Add(newToggle);
            i++;
        }
    }

    private void InitToggleGroup()
    {
        if (toggles.Count > 0)
        {
            //toggleGroupInstance.transform.DetachChildren();
            foreach (Toggle t in toggles)
            {
                Destroy(t.gameObject);
            }
            toggles = new List<Toggle>();
        }
    }

    private void SaveUsersSelectedValue()
    {
        string keyStr = selectionTextInstance.selectionTexts[currentSelectionIndex].KeyString;
        string selectedText = GetSelectedTexts();
        userLogs.Add(keyStr, selectedText);
    }

    public bool doesNextSelectionExist()
    {
        if (currentSelectionIndex == sizeOfSelections ) return false;
        return true;
    }

    public void GoToNextSelection()
    {
        //this functions is called from MultipleSelectionsManager when there exists other selections than selections has shown
        //get current user selection, append the value selected and display the other selection
        SaveUsersSelectedValue();
        currentSelectionIndex++;
        SetToggleTexts();
    }

    public string GetUserLogsText()
    {
        SaveUsersSelectedValue();
        string selectedText = "";
        foreach (string key in userLogs.Keys)
        {
            selectedText = selectedText + key + " : " + userLogs[key] + "\n";
        }
        return selectedText;
    }

    public string GetSelectedTexts()
    {
        if (_currentSelection == null) return "";

        var selectedOption = _currentSelection.transform.Find("Background/ToggleText").GetComponent<TextMeshProUGUI>();
        string selectedText = selectedOption.text;
        return selectedText;
    }

    public void InitializeQuestions()
    {
        currentSelectionIndex = 0;
        userLogs = new Dictionary<string, string>();
        SetToggleTexts();
    }

}