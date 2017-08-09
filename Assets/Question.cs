using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class DivisionClass {
    //We used custom class on Division Mode instead of generating values to avoid negative values
    public int num1, num2;
}

public class Question : MonoBehaviour {

    public GameObject AnswerButtonPrefab;
    public Transform AnswerButtonParent;
    public Text Problem_txt;

    public enum Operations
    {
        Addition,Subtraction,Multiplication,Division
    }
    public Operations Operation;
    public int NumberOfChoices = 3;
    int CurrentChoices;

    //use this when on division mode
    public DivisionClass[] DivisionProblems;

    //instantiated Choices Buttons are stored here, ready to use
    public List<Button> ChoicesBtn = new List<Button>();

    private void OnEnable()
    {
        GenerateProblem();
    }

    void GenerateProblem()
    {
        ResetAnswers();
        int num1, num2, answer;

        //identify the correct answer button
        int rnd = Random.Range(0, ChoicesBtn.Count);
        Button CorrectBtn = ChoicesBtn[rnd];

        switch (Operation)
        {
            case Operations.Addition:

                //generate values
                num1 = Random.Range(1, 10);
                num2 = Random.Range(1, 10);

                //important: compute the answer
                answer = num1 + num2;

                //generate wrong answer
                for (int i = 0; i < ChoicesBtn.Count; i++)
                {
                    Text t = ChoicesBtn[i].GetComponentInChildren<Text>();
                    t.text = Random.Range(answer - 10, answer + 10).ToString();

                    //important: remove the last listener, and replace with new ones to update fucntion
                    ChoicesBtn[i].onClick.RemoveAllListeners();
                    ChoicesBtn[i].onClick.AddListener(() => Choice(false));
                }
                //then select random choice to become a correct answer
                CorrectBtn.onClick.RemoveAllListeners();
                CorrectBtn.onClick.AddListener(()=>Choice(true));
                CorrectBtn.GetComponentInChildren<Text>().text = answer.ToString();

                //display the math problem
                Problem_txt.text = num1 + " + " + num2;

                break;
            case Operations.Subtraction:
                num1 = Random.Range(1, 10);
                num2 = Random.Range(1, 10);
                answer = num1 - num2;

                for (int i = 0; i < ChoicesBtn.Count; i++)
                {
                    Text t = ChoicesBtn[i].GetComponentInChildren<Text>();
                    if (answer > 0)
                    {
                        t.text = Random.Range(answer - 10, answer + 10).ToString();
                    }
                    else
                    {
                        t.text = Random.Range(answer, answer + 10).ToString();
                    }

                    //important: remove the last listener, and replace with new ones to update fucntion
                    ChoicesBtn[i].onClick.RemoveAllListeners();
                    ChoicesBtn[i].onClick.AddListener(() => Choice(false));
                }
                //then select random choice to become a correct answer
                CorrectBtn.onClick.RemoveAllListeners();
                CorrectBtn.onClick.AddListener(() => Choice(true));
                CorrectBtn.GetComponentInChildren<Text>().text = answer.ToString();

                //display the math problem
                Problem_txt.text = num1 + " - " + num2;

                break;
            case Operations.Multiplication:
                num1 = Random.Range(1, 10);
                num2 = Random.Range(1, 10);

                //important: compute the answer
                answer = num1 * num2;

                for (int i = 0; i < ChoicesBtn.Count; i++)
                {
                    Text t = ChoicesBtn[i].GetComponentInChildren<Text>();
                    t.text = Random.Range(answer - 10, answer + 10).ToString();

                    //important: remove the last listener, and replace with new ones to update fucntion
                    ChoicesBtn[i].onClick.RemoveAllListeners();
                    ChoicesBtn[i].onClick.AddListener(() => Choice(false));
                }
                //then select random choice to become a correct answer
                CorrectBtn.onClick.RemoveAllListeners();
                CorrectBtn.onClick.AddListener(() => Choice(true));
                CorrectBtn.GetComponentInChildren<Text>().text = answer.ToString();

                //display the math problem
                Problem_txt.text = num1 + " x " + num2;

                break;
            case Operations.Division:

                //we're going to get values from our division class
                num1 = DivisionProblems[Random.Range(0, DivisionProblems.Length)].num1;
                num2 = DivisionProblems[Random.Range(0, DivisionProblems.Length)].num2;
                
                //important: compute the answer
                answer = num1 / num2;

                for (int i = 0; i < ChoicesBtn.Count; i++)
                {
                    Text t = ChoicesBtn[i].GetComponentInChildren<Text>();
                    t.text = Random.Range(answer - 10, answer + 10).ToString();

                    //important: remove the last listener, and replace with new ones to update fucntion
                    ChoicesBtn[i].onClick.RemoveAllListeners();
                    ChoicesBtn[i].onClick.AddListener(() => Choice(false));
                }
                //then select random choice to become a correct answer
                CorrectBtn.onClick.RemoveAllListeners();
                CorrectBtn.onClick.AddListener(() => Choice(true));
                CorrectBtn.GetComponentInChildren<Text>().text = answer.ToString();

                //display the math problem
                Problem_txt.text = num1 + " / " + num2;

                break;
        }
        
    }

    void ResetAnswers() {

        if (CurrentChoices < NumberOfChoices)
        {
            //Clear the choices
            foreach (Button ChoiceBtn in ChoicesBtn)
            {
                Destroy(ChoiceBtn.gameObject);
            }
            ChoicesBtn.Clear();
        }
        CurrentChoices = 0;
        //Instantiate new choicies
        for (int i = 0; i < NumberOfChoices; i++)
        {
            GameObject go = Instantiate(AnswerButtonPrefab, AnswerButtonParent, false);
            //make sure to clear the button listener first
            go.GetComponent<Button>().onClick.RemoveAllListeners();
            ChoicesBtn.Add(go.GetComponent<Button>());
            CurrentChoices = i;
        }

    }

    void Choice(bool Correct) {
        if (Correct)
        {
            print("Correct!");
            GenerateProblem();
            GameManager.instance.AddScore();
        }
        else
        {
            print("Incorrect!");
            GameManager.instance.State = GameManager.GameState.GameOver;
        }
    }

}
