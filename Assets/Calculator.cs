using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Calculator : MonoBehaviour
{
    public string text;
    public string answertxt;
    public char[] SplitRun;
    public float Pi = 3.14f;

    public TextMeshProUGUI textui;
    public TextMeshProUGUI Answerui;

    public void Add(string c)
    {
        FullAdd(c);
        string[] RunBy = text.Split(SplitRun);
        if (RunBy[0].EndsWith("%"))
        {
            text = "0";
        }
        answertxt = "";
        textui.SetText(text);
        Answerui.SetText(answertxt);
    }

    public void FullAdd(string c)
    {
        if (text.Contains("+") || text.Contains("-") || text.Contains("X") || text.Contains("/"))
        {
            if (c == "+" || c == "-" || c == "X" || c == "/")
            {
                c = "";
            }
        }
        if (c != "")
        {
            text = text + c;
        }
    }

    public void Remove(bool Clear)
    {
        if (Clear == false)
        {
            if (text.Length > 0)
            {
                text = text.Remove(text.Length - 1);
            }
        }
        else
        {
            text = "";
        }
        answertxt = "";
        textui.SetText(text);
        Answerui.SetText(answertxt);
    }

    public void Equals()
    {
        string[] RunBy = text.Split(SplitRun);
        if (RunBy.Length > 1)
        {
            float run1 = float.Parse(RunBy[0]);
            float run2 = float.Parse(RunBy[1].Replace("%", ""));
            float run12 = run1;
            if (text.Contains("+"))
            {
                if (RunBy[1].EndsWith("%"))
                {
                    run1 *= run2;
                    run1 /= 100;
                    run12 += run1;
                    run1 = run12;
                }
                else
                {
                    run1 = run1 + run2;
                }
            }
            else if (text.Contains("-"))
            {
                if (RunBy[1].EndsWith("%"))
                {
                    run1 *= run2;
                    run1 /= 100;
                    run12 -= run1;
                    run1 = run12;
                }
                else
                {
                    run1 = run1 - run2;
                }
            }
            else if (text.Contains("X"))
            {
                run1 = run1 * run2;
            }
            else if (text.Contains("/"))
            {
                run1 = run1 / run2;
            }
            answertxt = run1.ToString();
            Answerui.SetText(answertxt);
        }
    }

    public void Area()
    {
        float run1 = float.Parse(text);
        float Area = Pi * Double(run1);

        answertxt = Area.ToString();
        Answerui.SetText(answertxt);
    }

    public void Circ()
    {
        float run1 = float.Parse(text);
        float Circ = Pi * run1;

        answertxt = Circ.ToString();
        Answerui.SetText(answertxt);
    }

    public float Double(float by)
    {
        return by * by;
    }

    public void InputPi(TMP_InputField TMPut)
    {
        if (TMPut.text.Length > 0)
        {
            Pi = float.Parse(TMPut.text);
        }
        else
        {
            Pi = 0;
        }
    }

    public void getRemainder()
    {
        string[] s = text.Split('/');
        string answer = answertxt.Split('.')[0];
        float Remainder = float.Parse(s[0]) - float.Parse(answertxt.Split('.')[0]) * float.Parse(s[1]);
        answertxt = answer + "R" + Remainder;
        Answerui.SetText(answertxt);
    }

    public void getCheck()
    {
        if (text.Contains("/")) {
            float Remainder = 0;
            string answer = answertxt;
            if (answertxt.Contains("R")) { Remainder = float.Parse(answertxt.Split('R')[1]); answer = answertxt.Split('R')[0]; }
            text = text.Split('/')[1] + "X" + answer + "+" + Remainder;
            answertxt = (float.Parse(text.Split('X')[0]) * float.Parse(answer) + Remainder) + "";
        } else if(text.Contains("X")) {
            string txt = answertxt + "/" + text.Split('X')[1];
            string answer = (float.Parse(answertxt) / float.Parse(txt.Split('/')[1])) + "";
            if (answer != answertxt)
            {
                txt = answertxt + "/" + text.Split('X')[0];
                answer = (float.Parse(answertxt) / float.Parse(txt.Split('/')[0])) + "";
            }
            if (text.Contains("+"))
            {
                answer = answer.Split('.')[0] + "R" + text.Split('+')[1];
            }
            text = txt;
            answertxt = answer;
        }
        else if (text.Contains("+")) {
            text = text.Split('+')[1] + "-" + answertxt;
            answertxt = (float.Parse(text.Split('-')[0]) - float.Parse(answertxt)) + "";
        }
        else if (text.Contains("-")) {
            text = text.Split('-')[1] + "+" + answertxt;
            answertxt = (float.Parse(text.Split('+')[0]) + float.Parse(answertxt)) + "";
        }
        textui.SetText(text);
        Answerui.SetText(answertxt);
    }

}
