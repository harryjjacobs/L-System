using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LSystem {
    [SerializeField]private char[] variables;
    [SerializeField]private char[] constants;
    [SerializeField]private string axiom;
    [SerializeField]private Rule[] rules;

    private int generation = 0;
    private string tree = "";

    public LSystem(char[] variables, char[] constants, string axiom, Rule[] rules)
    {
        this.variables = variables;
        this.constants = constants;
        this.axiom = axiom;
        this.rules = rules;
    }

    public string Run(int iterations)
    {
        generation = 0;
        tree = axiom;
        Grow(iterations);
        return tree;
    }

    void Grow(int iterations)
    {
        if (generation >= iterations)
        {
            PrintOutput();
            return;
        }
        string temp = tree;
        int tempIndex = 0;
        for (int i = 0; i < tree.Length; i++)
        {
            for (int j = 0; j < rules.Length; j++)
            {
                if (temp[tempIndex] == rules[j].Predecessor)
                {
                    //If it's one of the variables then replace it - otherwise ignore if it's a constant
                    if (Array.IndexOf(variables, temp[tempIndex]) > -1)
                    {
                        temp = temp.Remove(tempIndex, 1);
                    }
                    //Add the successor characters into place
                    temp = temp.Insert(tempIndex, rules[j].Successor);
                    tempIndex += rules[j].Successor.Length - 1;
                    break;
                }
            }
            tempIndex++;
        }
        tree = temp;
        generation++;
        Grow(iterations);
    }

    void PrintOutput()
    {
        Debug.Log(tree);
    }

    public string Axiom
    {
        get
        {
            return axiom;
        }

        set
        {
            axiom = value;
        }
    }

    public Rule[] Rules
    {
        get
        {
            return rules;
        }

        set
        {
            rules = value;
        }
    }

    public int Generation
    {
        get
        {
            return generation;
        }
    }

    public char[] Variables
    {
        get
        {
            return variables;
        }

        set
        {
            variables = value;
        }
    }

    public char[] Constants
    {
        get
        {
            return constants;
        }

        set
        {
            constants = value;
        }
    }
}

[System.Serializable]
public struct Rule
{
    [SerializeField]private char predecessor;
    [SerializeField]private string successor;

    public Rule(char predecessor, string successor)
    {
        this.predecessor = predecessor;
        this.successor = successor;
    }

    public char Predecessor
    {
        get
        {
            return predecessor;
        }

        set
        {
            predecessor = value;
        }
    }

    public string Successor
    {
        get
        {
            return successor;
        }

        set
        {
            successor = value;
        }
    }
}

