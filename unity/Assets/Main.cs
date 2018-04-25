using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{

    string alphabet = "ABCDEF";

    string input = "AAAAAAAAA";
    
    // scramblers:
    int[] s1 = new int[] { 0, 1, 2, 3, 4, 5 }; 

    // Reflector is a mirror, so very important that e.g. 0->2 then 2->0!
    // NB this ensures a letter is never itself when encrypted which is a major flaw in this system.
    int[] reflector = new int[] { 2, 3, 0, 1, 5, 4 }; 

    // use an index to fake slide left/right
    int s1index = 0;

    void Start()
    {
        for (int i = 0; i < input.Length; ++i)
        {
            char it = input[i];
            int indexInput = alphabet.IndexOf(it);
            // scramblers
            int indexS1 = (s1[indexInput] + s1index) % s1.Length;
            // reflector
            int indexS1R1 = reflector[indexS1];
            // walk back up from reflector
            int indexS1R1S1 = s1[indexS1R1];

            // output value
            Debug.Log(alphabet[indexS1R1S1]);

            // slide scrambler one left for next turn, if we reached end, start over.
            s1index++;
            s1index = (s1index + s1.Length) % s1.Length;

        }
    }

   





}
