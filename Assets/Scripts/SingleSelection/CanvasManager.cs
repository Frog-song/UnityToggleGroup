using UnityEngine;
using TMPro;

public class CanvasManager : MonoBehaviour
{

    private SelectionManager selectionManager;

    //selection panel
    public GameObject selectionPanel;
    //result panel
    public GameObject resultPanel;
    public TextMeshProUGUI resultText;

   
   
    // Start is called before the first frame update
    void Start()
    {
        selectionManager = gameObject.transform.Find("Selection").GetComponent<SelectionManager>();
    }

    public void OnNextButtonPressed()
    {
        string selectedText = selectionManager.GetSelectedText();
        if (selectedText != "")
        {
            resultText.text = selectedText;
            selectionPanel.SetActive(false);
            resultPanel.SetActive(true);
        }
    }

    public void OnBackButtonPressed()
    {
        resultText.text = "";
        resultPanel.SetActive(false);
        selectionPanel.SetActive(true);
    }

}
