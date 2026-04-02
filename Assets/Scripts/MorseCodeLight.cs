using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class MorseCodeLight : MonoBehaviour
{
    // public variables/ set in inspector.

    [Header("Light")]
    public Light light;

    [Header("Time")]
    public float dotLength= 0.5f; // how long the dot last for.
    public float brightness=5f; // how bright the light should be.

    public string code= "346"; // this is where we can insert the code. it will be converted to morse.

    // private variable and dictionary that is used to get the morse commponents.
    private Dictionary<char,string> morseCode= new Dictionary<char, string>()
    {
        {'0',"-----"}, {'1',".----"},
        {'2',"..---"}, {'3',"...--"},
        {'4',"....-"}, {'5',"....."},
        {'6',"-...."}, {'7',"--..."},
        {'8',"---.."}, {'9',"----."}
    }; // I used https://www.phidgets.com/education/learn/projects/morse-code/ to get the morse translation for the numbers.
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(loopCode()); // start coroutine.
    }

    // Update is called once per frame
    IEnumerator loopCode()
    {
        while (true) // infinite loop for the morse code (makes the message repeat on the flashing light).
        {
            foreach(char digit in code) // goes through each digit in code
            {
               if (!morseCode.ContainsKey(digit)) // skips over char if it is not in the dictionary.
                {
                    continue;
                }
                string translated = morseCode[digit]; // gets the morse code for the digit.

                foreach (char symbol in translated) // the symbols are translated into flashes. "." 0.5 seconds. "-" 1.5 seconds.
                {
                    light.intensity = brightness; // light on.

                    if (symbol == '.')
                    {
                        yield return new WaitForSeconds(dotLength); // shorter flash for dot.
                    } else
                    {
                        yield return new WaitForSeconds(dotLength*3); // longer flash for dash (1.5 seconds).
                    }
                    light.intensity = 0f; // turns light off.
                    
                    yield return new WaitForSeconds(dotLength); // waits between parts of the same digit ex something like ..-.. .
                }
                yield return new WaitForSeconds(dotLength * 2); // time between each digit. ex ----- .----
            }
            yield return new WaitForSeconds(dotLength * 7); // time between each repeat of the code.
        }
    }
}
