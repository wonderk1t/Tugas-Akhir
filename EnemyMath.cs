using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyMath : MonoBehaviour
{
    public TMP_Text questionText; 

    public string question;
    public int correctAnswer;

    void Start()
    {
        GenerateQuestion();
        questionText.text = question;
    }

    void GenerateQuestion()
    {
        int a = Random.Range(1, 5);
        int b = Random.Range(1, 5);

        int type = Random.Range(0, 3);

        if (type == 0)
        {
            // Penjumlahan
            question = a + " + " + b;
            correctAnswer = a + b;
        }
        else if (type == 1)
        {
            // Perkalian 
            question = a + " x " + b;
            correctAnswer = a * b;
        }
        else
        {
            // Pangkat sederhana
            question = a + "²";
            correctAnswer = a * a;
        }
    }

    // Dipanggil saat enemy diklik
    void OnMouseDown()
    {
        
    }
}