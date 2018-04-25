using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{

    string input = "AAAAA";

    string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    int[] s1 = new int[] { 0, 0, 0, 0, 0 };

    int s1index = 0;

    void Start()
    {
        for (int i = 0; i < input.Length; ++i)
        {
            char it = input[i];
            int alphabetIndex = alphabet.IndexOf(it);
            int indexAfterS1 = s1[alphabetIndex] + s1index;
            
            // if scrambler has more elements than alphabet
            indexAfterS1 = indexAfterS1 % alphabet.Length;
            
            // output value
            Debug.Log(alphabet[indexAfterS1]);
            
            // slide scrambler one left for next turn
            s1index++;
            
            // if we reached end, start over.
            s1index = s1index % s1.Length;
        }
    }

   





}
