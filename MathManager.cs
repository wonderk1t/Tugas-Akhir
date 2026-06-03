using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MathManager : MonoBehaviour
{
    public TMP_InputField answerInput;

    public void SubmitAnswer()
    {
        int playerAnswer;

        if (!int.TryParse(answerInput.text, out playerAnswer))
        {
            Debug.Log("Masukkan angka!");
            return;
        }

        // ambil semua enemy di scene
        EnemyMath[] enemies = FindObjectsOfType<EnemyMath>();

        bool found = false;

        foreach (EnemyMath enemy in enemies)
        {
            if (enemy.correctAnswer == playerAnswer)
            {
                Debug.Log("BENAR! Bunuh enemy: " + enemy.question);
                Destroy(enemy.gameObject);
                found = true;
                break; // bunuh 1 aja (biar balance)
            }
        }

        if (!found)
        {
            Debug.Log("Tidak ada jawaban yang cocok!");
        }

        answerInput.text = "";
    }
}