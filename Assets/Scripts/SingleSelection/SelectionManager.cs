using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class SelectionManager : MonoBehaviour
{

    public SelectionText[] selectionTexts;
    public ToggleGroup toggleGroupInstance;
    public Toggle toggle;
    private Toggle _currentSelection
    {
        get { return toggleGroupInstance.ActiveToggles().FirstOrDefault(); }
    }


    // Start is called before the first frame update
    void Start()
    {
        ToggleInit();
    }

    void ToggleInit()
    {
        if (selectionTexts.Length > 0)
        {
            int i = 0;
            foreach (SelectionText selectionText in selectionTexts)
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
                toggleText.text = selectionText.Text;
                toggleText.fontSize = 14;
                newToggle.group = toggleGroupInstance;
                newToggle.transform.SetParent(toggleGroupInstance.transform, false);
                i++;
            }
        }
    }

    public string GetSelectedText()
    {
        if (_currentSelection == null) return "";

        var selectedOption = _currentSelection.transform.Find("Background/ToggleText").GetComponent<TextMeshProUGUI>();
        string selectedText = selectedOption.text;
        return selectedText;
    }
}