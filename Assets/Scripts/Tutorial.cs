using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class Tutorial : MonoBehaviour
{
    public static Tutorial Instance;
    [TextArea(3, 10)]
    public List<string> tutorialText = new List<string>();
    private Queue<string> sentence = new Queue<string>();

    public TextMeshProUGUI textBox;    

    private List<GameObject> active = new List<GameObject>();

    //Inventory
    public GameObject inventory;
    //UI
    public GameObject recipe;
    //Bedroom
    public GameObject bedroom;
    //Shop
    public GameObject shop;
    //Kitchen
    public GameObject kitchen;

    public GameObject continueButton;
    //SwapScene
    private SwapScenes swapScenes;

    private void Start()
    {
        swapScenes = SwapScenes.Instance;
    }

    private void Awake()
    {
        Instance = this;
    }
    public void TutorialStart()
    {
        if (!GlobalManager.Instance.playTutorial)
        {
            EndTutorial();
        }
        else
        {
            kitchen.SetActive(false);
            swapScenes.ToKitchen();
            foreach(string text in tutorialText)
            {
                sentence.Enqueue(text);
            }
            ContinueButton();
        }
    }
    
    public void ContinueButton()
    {
        StartCoroutine(ContinueButtonEnumerator());
    }
    private IEnumerator ContinueButtonEnumerator()
    {
        if (sentence.Count > 0)
        {
            Animator animatorRecipe = recipe.GetComponent<Animator>();

            string text = sentence.Dequeue();
            switch (text)
            {
                case "{loadRecipe}":
                    animatorRecipe.SetTrigger("isOpen");
                    continueButton.GetComponent<Button>().enabled = false;
                    yield return new WaitForSeconds(0.5f);
                    continueButton.GetComponent<Button>().enabled = true;
                    
                    text = sentence.Dequeue();
                    break;
                case "{loadBedroom}":
                    animatorRecipe.SetTrigger("isOpen");
                    continueButton.GetComponent<Button>().enabled = false;
                    yield return new WaitForSeconds(0.5f);
                    continueButton.GetComponent<Button>().enabled = true;                   

                    swapScenes.ToBedroom();
                    text = sentence.Dequeue();
                    break;
                case "{loadShop}":
                    swapScenes.ToShopFront();
                    text = sentence.Dequeue();
                    break;
                case "{loadKitchen}":
                    swapScenes.ToKitchen();
                    text = sentence.Dequeue();
                    break;
                default:
                    Debug.Log("NORMAL TEXT");
                    break;
            }
            textBox.text = text;
        }
        else
        {
            EndTutorial();
        }
    }
    private IEnumerator WaitOnAnimation(Animator animator)
    {
        animator.SetTrigger("isOpen");
        continueButton.GetComponent<Button>().enabled = false;
        yield return new WaitForSeconds(0.14f);
        continueButton.GetComponent<Button>().enabled = true;
    }
    private void EndTutorial()
    {
        GlobalManager.Instance.playTutorial = false;
        kitchen.SetActive(true);
        gameObject.SetActive(false);
    }
}
