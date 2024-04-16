using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.SocialPlatforms;


public class TaxMan : MonoBehaviour
{
    [TextArea(3, 10)]
    public string interduction;

    [TextArea(3, 10)]
    public string tax1;

    [TextArea(3, 10)]
    public string tax2;

    [TextArea(3, 10)]
    public string tax3;

    [TextArea(3, 10)]
    public string thanks;

    [TextArea(3, 10)]
    public string warning1;

    [TextArea(3, 10)]
    public string warning2;

    private const double propertyTax = 5;
    private double incomeTax;
    private double titheTax;

    [HideInInspector]
    public double weeklyIncome = 0;

    [HideInInspector]
    public int warnings = 0;

    public Animator textBoxAnimator;
    public Animator spriteAnimator;
    public TextMeshProUGUI text;
    public GameObject acceptButton;
    public GameObject declineButton;
    public GameObject continueButton;

    private int sentenceIndex = 0;

    private bool typing = false;
    private string sentence;
    private string currantSentence;

    public void setWeeklyIncome(double income) { weeklyIncome = income; GlobalManager.Instance.weeklyIncome = income; }
    public void addWeeklyIncome(int income) { weeklyIncome += income; GlobalManager.Instance.weeklyIncome += income; }
    public double getWeeklyIncome() {  return weeklyIncome; }

    public void setWarnings(int amount) { warnings = amount; GlobalManager.Instance.warnings = amount; }
    public void addWarning() { warnings++; GlobalManager.Instance.warnings++; }

    private void Start()
    {
        var global = GlobalManager.Instance;
        setWeeklyIncome(global.weeklyIncome);
        setWarnings(global.warnings);
    }
    public void getWeekTax()
    {
        titheTax = Math.Ceiling(weeklyIncome * 0.1f);
        incomeTax = Math.Ceiling(weeklyIncome * 0.2f);
    }

    public bool payTax()
    {
        var moneyManager = MoneyManager.Instance;
        double tax = propertyTax + incomeTax + titheTax;
        if (moneyManager.Money - tax >= 0)
        {
            moneyManager.RemoveMoney((int)tax);
            setWeeklyIncome(0);
            return true;
        }
        else
        {
            Decline();
            setWeeklyIncome(0);
            return false;
        }
    }
    public void Pay()
    {
        payTax();
        continueButton.SetActive(true);
        acceptButton.SetActive(false);
        declineButton.SetActive(false);

        string thanksText = thanks;
        currantSentence = thanksText;
        StartCoroutine(TypeSentence(thanksText));
    }

    public void Decline()
    {
        addWarning();
        continueButton.SetActive(true);
        acceptButton.SetActive(false);
        declineButton.SetActive(false);

        string warningText = "";
        if(warnings <= 2)
        {
            warningText = warning1 + warnings.ToString();
        }
        else
        {
            warningText = warning2;
        }
        currantSentence = warningText;
        StartCoroutine(TypeSentence(warningText));
        if(warnings >= 4)
        {
            GlobalManager.Instance.EndGame();
        }
    }
    public void StartDialogue()
    {
        textBoxAnimator.SetBool("isOpen", true);
        getWeekTax();
        sentence = tax1 + propertyTax.ToString() + "\n" + tax2 + incomeTax.ToString() + "\n" + tax3 + titheTax.ToString();
        DisplayNextSentence();
    }
    public void DisplayNextSentence()
    {
        StopAllCoroutines();
        if (typing)
        {   
            typing = false;
            text.text = currantSentence;
            sentenceIndex++;
        }
        else
        {
            switch (sentenceIndex)
            {
                case 0:
                    currantSentence = interduction;
                    StartCoroutine(TypeSentence(interduction));
                    break;
                case 1:
                    currantSentence = sentence;
                    StartCoroutine(TypeSentence(sentence));
                    break;
                case 2:
                    continueButton.SetActive(false);
                    acceptButton.SetActive(true);
                    declineButton.SetActive(true);
                    break;
                case 3:
                    EndDialogue();
                    break;
            }
        }
    }
    IEnumerator TypeSentence(string sentence)
    {
        typing = true;
        text.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            text.text += letter;
            yield return new WaitForSeconds(0.08f);
        }
        typing = false;
        sentenceIndex++;
    }

    private void EndDialogue()
    {
        StartCoroutine(PlayEndAnimation());
        textBoxAnimator.SetBool("isOpen", false);
    }

    IEnumerator PlayEndAnimation()
    {
        spriteAnimator.SetTrigger("Walk");
        yield return new WaitForSeconds(spriteAnimator.GetCurrentAnimatorStateInfo(0).length);
        Destroy(gameObject);
    }
    private void OnDestroy()
    {
        EventManager.Instance.setSpawn(true);
    }
    private void Update()
    {
        if (!GameObject.Find("ShopFront"))
            gameObject.SetActive(false);
        else
            gameObject.SetActive(true);
    }
}
