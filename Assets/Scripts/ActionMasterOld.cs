using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionMasterOld  {

    public enum myAction { Tidy, Reset, EndTurn, EndGame};

    private int[] bowls = new int[21];    //store the score from each bowl into this array, max bowls you can roll is 21;
    private int bowl = 1;

    public static myAction NextAction(List<int> pinFalls)
    {
        ActionMasterOld am = new ActionMasterOld();
        myAction currentAction = new myAction();
        foreach (int pins in pinFalls)
        {
            currentAction = am.Bowl(pins);
        }
        return currentAction;
    }

    public myAction Bowl(int pins)
    {
        if (pins<0||pins>10)
        {
            throw new UnityException("invalid pins");
        }


        // Other behaviour here, e.g. last frame.
        bowls[bowl - 1] = pins;  // each time this function get called, we store the score(pins) into the array; 

        if (bowl==21)
        {
            return myAction.EndGame;
        }

        // Hanld last-frame special cases
        if (bowl==19&&pins==10)
        {
            bowl++;
            return myAction.Reset;
        }
        else if (bowl==20)
        {
            bowl++;
            if (bowls[18] ==10&&bowls[19]==0)  //  if bowl 19 is a strike and bowl 20 is 0;
            {
                return myAction.Tidy;
            }
            else if(bowls[18]+bowls[19]==10|| bowls[19] == 10)    //  if bowl 20 is a spare or strike;
            {
                return myAction.Reset;
            }
            else if (GetOneMoreReward())      
            {
                return myAction.Tidy;
            }
            else
            {
                return myAction.EndGame;
            }
        }
       
       if (bowl % 2 != 0)  //first bow of 1-9 frame, it always is odd number; 
        {   // Mid frame 
            if (pins==10)
            {
                bowl += 2;  //then we don't need the second roll in the frame ,turn to the first roll in the next frame; 
                return myAction.EndTurn;
            }
            else
            {              
                bowl += 1;
                return myAction.Tidy;
            }
           
       } else if(bowl%2 == 0)  //second bowl of 1-9 frame
        {
            // End of frame
            bowl += 1;
            return myAction.EndTurn;
        }

        throw new UnityException("not sure what action yet");
    }
    

    private bool GetOneMoreReward()
    {
         return bowls[19 - 1] + bowls[20 - 1] >=10;      
    }

    public int bowlCount()
    {
        return bowl;
    } 
}
