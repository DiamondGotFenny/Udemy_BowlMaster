using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class ScoreMaster
{

    //this return a list of individual frame score, not cumulative;

    public static List<int> ScoreFrames(List<int> rolls)
    {
        List<int> frameList = new List<int>();

        //your code is here
        int frameNum = 0;

        for (int i = 0; i < rolls.Count; i++)
        {
            if (i + 1 < rolls.Count )   //only add score to the list if there two bowl completed after i;
            {
                if (rolls[i] + rolls[i + 1] < 10 && frameNum < 10)  //frameNum < 10 prevents 11th frame score
                {
                    frameList.Add(rolls[i] + rolls[i + 1]);
                    frameNum++;
                    i += 1;     //count the score every two bowl, here we increase 1 because the for loop increment condition has add one already;
                }              
                else if((rolls[i] == 10  || rolls[i] + rolls[i + 1] == 10)&& rolls.Count > i + 2)
                {
                    frameList.Add(rolls[i] + rolls[i + 1] + rolls[i + 2]);
                    frameNum++;
                    if ( rolls[i]!=10)
                    {
                        i += 1;     //if it was spare we increase as normal; if it was strike we increase only 1 bowl, which is the for loop increment condition
                    }
                }
            }
        }

        //foreach (var frameScore in frameList)
        //{
        //    Debug.Log(frameScore + " " + "index" + " " + frameList.IndexOf(frameScore));

        //}
        //Debug.Log("frameList.Count" + " " + frameList.Count);
        return frameList;
    }


    //returns a list of cumulative scores, like a normal score card;
    public static List<int> ScoreCumulative(List<int> rolls)
    {
        List<int> CumulativeScores = new List<int>();
        int runningTotal = 0;

        foreach (int frameScore in ScoreFrames(rolls))
        {
            runningTotal += frameScore;
            CumulativeScores.Add(runningTotal);
        }

        return CumulativeScores;
    }
}
