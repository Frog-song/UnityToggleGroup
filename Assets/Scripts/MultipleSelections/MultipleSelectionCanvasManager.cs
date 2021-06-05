using UnityEngine;
using TMPro;

public class MultipleSelectionCanvasManager : MonoBehaviour
{

    private MultipleSelectionsManager selectionManager;

    //selection panel
    public GameObject selectionPanel;
    //result panel
    public GameObject resultPanel;
    public TextMeshProUGUI resultText;



    // Start is called before the first frame update
    void Start()
    {
        selectionManager = gameObject.transform.Find("Selection").GetComponent<MultipleSelectionsManager>();
    }

    public void OnNextButtonPressed()
    {
        if (selectionManager.doesNextSelectionExist())
        {
            selectionManager.GoToNextSelection();
        }
        else
        {
            resultText.text = selectionManager.GetUserLogsText();
            selectionPanel.SetActive(false);
            resultPanel.SetActive(true);
        }

    }

    public void OnBackButtonPressed()
    {
        resultText.text = "";
        selectionManager.InitializeQuestions();
        resultPanel.SetActive(false);
        selectionPanel.SetActive(true);
    }

}
