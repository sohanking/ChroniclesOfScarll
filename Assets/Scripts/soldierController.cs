using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Collections;



public class soldierController : MonoBehaviour {

    // Display the question on the drone
    public Text questionShow;
    private string displayPattern, question;

    // mine generated from the mineController Script is stored in addPaths
    public mineController mineCtrlScript;

   // chatEvents store the question with ? 
   // qindex store the answers 
    List<string> qindex;
   
   //actualPath has the mine attached with the question pattern
    List<Transform> actualPath;

   // testrnd has the random numbers witout repetation used to randomise the missin numbers to the mine. 
    List<int> rndIndexValues = new List<int>();

    // To control the missin answers
    [SerializeField]
    int minAns, maxAns, diff;

    //used for random generation q,a,s,r,m,rnd,rndindex
    // level is used to check the level the player is solving, 
    int queSelected, rnd, rndIndex, level, minesCount, numOfQue, lengthOfQue, numOfsoln, currentLevel;

   

    private int[] numbers;

    //Random value generation without repetation and store in a list
    private void rndNum(int min,int max,List<int> rndList)
    {
        while (rndList.Count < max)
        {
            rnd = Random.Range(min, max);
            if (!rndList.Contains(rnd))
            {
                rndList.Add(rnd);
            }

        }
    }
    
    private void AP()
    {
        numbers = new int[mineCtrlScript.SeriesLength];
    int firstTerm =  Random.Range(0, 500);
    int difference = Random.Range(2, 10);
      
        
        for (int i=0;i < mineCtrlScript.SeriesLength; i++)
        {
            numbers[i] = firstTerm + ( (i) * difference );
           
           
        }
    }

    private void GP()
    {
        numbers = new int[mineCtrlScript.SeriesLength];
        int firstTerm = Random.Range(0, 10);
        int ratio = Random.Range(2, 3);
       
        for (int i = 0; i < mineCtrlScript.SeriesLength; i++)
        {
            // numbers[i] = firstTerm * (difference * i );
            numbers[i] = firstTerm * ((int)Mathf.Pow(ratio, i));

            
        }
    }
    static bool IsPrimeNumber(int num)
    {
        bool bPrime = true;
        int factor = num / 2;

        

        for (int i = 2; i <= factor; i++)
        {
            if ((num % i) == 0)
                bPrime = false;
        }
        return bPrime;
    }
    private void Prime()
    {

        numbers = new int[mineCtrlScript.SeriesLength];
         int firstTerm = Random.Range(2, 100);
       
     
        
        int countPrime = 0;
        for (int i = firstTerm; i < 1000; i++)
        {
            if (countPrime == (mineCtrlScript.SeriesLength))
                break;

            if (IsPrimeNumber(i) == true)
            {
                
                numbers[countPrime] = i;
                countPrime++;
            }
                
        }

    }

    private void power()
    {
        numbers = new int[mineCtrlScript.SeriesLength];
       int firstTerm = Random.Range(2, 3);
  
        int countPower = 0;
        while (countPower != mineCtrlScript.SeriesLength)
        {
            numbers[countPower] = (int)Mathf.Pow(firstTerm, countPower + 1);
            countPower++;
        }
    }



    private void square()
    {

        numbers = new int[mineCtrlScript.SeriesLength];
        int firstTerm = Random.Range(0, 10);
        int powerNUm = Random.Range(2, 3);
        for (int i = 0; i < mineCtrlScript.SeriesLength; i++)
        {

            numbers[i] = (int)Mathf.Pow(firstTerm, powerNUm);
            firstTerm++;
        }
    }

    private void displayQuestion()
    {


        //total number of questions
       // numOfQue =SelectQue.Length;
       
        //randomly selecting the question
        queSelected = Random.Range(1, 5);
        //queSelected = 5;

        switch (queSelected)
        {
            case 1: AP();
                print("AP");
                break;

            case 2:GP();
                print("GP");
                break;

            case 3:
                Prime();
                print("Prime");
                break;

            case 4:
                power();
                print("power");
                break;

            case 5:
                square();
                print("square");
                break;

        }
       
        //lenght of the questions
        lengthOfQue = numbers.Length;

       


        //set the number of each mine according to the question by randomly generating the index for the array
        rndNum(0, lengthOfQue, rndIndexValues);
        for (int i = 0; i < lengthOfQue; i++)
        {
            rndIndex = rndIndexValues[i];
            qindex.Add("null");
            actualPath.Add(mineCtrlScript.paths[rndIndex]);
            mineCtrlScript.mineTextArr[rndIndex].text = numbers[i].ToString();
            

            if (i < lengthOfQue-1)
            {
                rndIndexValues[i + 1] += (lengthOfQue * (i + 1));
            }

        }

        rndIndexValues.Clear();
        numOfsoln = Random.Range(minAns,maxAns);
        rndNum(0, lengthOfQue, rndIndexValues);

        for (int j = 0; j < numOfsoln; j++)
        {
            rndIndex = rndIndexValues[j];
           
            if (!qindex.Contains(numbers[rndIndex].ToString()))
            {
                qindex[rndIndex] = numbers[rndIndex].ToString();      
            }
        
        }

        rndIndexValues.Clear();
        rndNum(0, 100, rndIndexValues);

        for (int k=0;k <= mineCtrlScript.paths.Count-1; k++)
        {

           if(k < lengthOfQue)
            {

                if (numbers[k].ToString() == qindex[k])              
                    numbers[k] = -1;                            
                else
                    actualPath[k].transform.gameObject.GetComponent<Renderer>().material.color = Color.green;
            }
           
            if (mineCtrlScript.mineTextArr[k].text == "num")
            {              
                rndIndex = rndIndexValues[k];
                mineCtrlScript.mineTextArr[k].text = rndIndex.ToString();
            }
        }


        for (int j = 0; j <lengthOfQue; j++)
            displayPattern = displayPattern.ToString() + numbers[j].ToString() + "\t";


          questionShow.text = displayPattern;
      
    
        print(displayPattern);  
    }
 
   
    // Use this for initialization
    void Start () {

        minAns = 2;
        maxAns = 4;
        diff = 1;
        currentLevel = 0;
         qindex = new List<string>();
        actualPath = new List<Transform>();
        rndIndex = level = 0;
        displayPattern = "";
        displayQuestion();
 
    }

    bool blast = false;

  void gameNext(int minef,int minel,Color clr)
    {

        for (int start = minef; start < minel; start++)
        {
            if (blast) {
              //  mineCtrlScript.mineParticles[start].SetActive(true);
               // Destroy(mineCtrlScript.paths[start], 1f);
            }
            else
                mineCtrlScript.paths[start].transform.gameObject.GetComponent<Renderer>().material.color =clr;
        }
        
    }


    void gameNext(int minef, int minel, Transform ans)
    {

        for (int start = minef; start < minel; start++)
        {
            if ( mineCtrlScript.paths[start].transform.name == ans.transform.name)
            {
               
                continue;
            }
                
             mineCtrlScript.paths[start].transform.gameObject.GetComponent<Renderer>().material.color = Color.red;
        }

    }
   
    //if player steps on the mine, he dies and game is over
    // the level is cleared when player cross the end path
    void OnTriggerEnter(Collider other)
    {

       
        if (other.transform.name == actualPath[level].name && level == currentLevel)
        {
            ++level;
            currentLevel = level;
          
           
        if((currentLevel-1) > 0)
            {
              //  print(currentLevel - 1);
                if (diff == 1)
                    gameNext(lengthOfQue * (level - 2), lengthOfQue * (level - 1), actualPath[level - 2]);
                else
                    gameNext(lengthOfQue * (level - 2), lengthOfQue * (level - 1), Color.red);
            }

            gameNext(lengthOfQue * (level - 1), lengthOfQue * level, Color.green);

            if(level == lengthOfQue)
                print("won");

        }
        else
        {
            if(other.transform.gameObject.GetComponent<Renderer>().material.color == Color.green)
            {
              //Do nothing
            }
            else
            {
                //Gameover and add force to make the player fly and fall
                print("gameOver");
               
                other.GetComponentInChildren<ParticleSystem>(true).transform.parent.gameObject.SetActive(true);
                blast = true;
               
                Rigidbody rgbdy = this.GetComponent<Rigidbody>();
                Vector3 forceDirection = (Camera.main.transform.position - other.transform.position).normalized;
                rgbdy.constraints = RigidbodyConstraints.None;
                rgbdy.AddForce(forceDirection * 6f, ForceMode.Impulse);
                this.GetComponent<RigidbodyAutowalk>().enabled = false;
                gameNext(0, mineCtrlScript.paths.Count - 1, Color.red);
            }
  
           
        }


    }
    

}
