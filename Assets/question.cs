using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Question {
    public string clue { get; set; }
    public string[] answers { get; set; }

    public Question(string clue, string[] answers)
    {
        this.clue = clue;
        this.answers = answers;
    }

}
