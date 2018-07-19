using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

[TestFixture]
public class ActionTest
{
    private List<int> pinFalls;
    private ActionMaster.Action endTurn = ActionMaster.Action.EndTurn;
    private ActionMaster.Action tidy = ActionMaster.Action.Tidy;
    private ActionMaster.Action endGame = ActionMaster.Action.EndGame;
    private ActionMaster.Action reset = ActionMaster.Action.Reset;

    [SetUp]
    public void setUp()
    {
        pinFalls = new List<int>();
    }

    [Test]
    public void T00PassingTest()
    {
        Assert.AreEqual(1, 1);
    }

    [Test]
    public void T01_OneStrikeReturnsEndReturn()
    {
        pinFalls.Add(10);
        Assert.AreEqual(endTurn, ActionMaster.NextAction(pinFalls));
    }

    [Test]
    public void T02_8PinsStrikeReturnTidy()
    {
        pinFalls.Add(8);
        Assert.AreEqual(tidy, ActionMaster.NextAction(pinFalls));
    }

    [Test]
    public void T03_Bowl2_SpareReturnEndTurn()
    {
        int[] bowls = { 8, 2 };
        pinFalls.AddRange(bowls);
        Assert.AreEqual(endTurn, ActionMaster.NextAction(pinFalls));// increase bowl number to 2; then bowl % 2 ==0;

        //this is Ben's way to do it: add a using System.Linq; libary at top. 
        //then   Assert.AreEqual(endTurn, ActionMaster.NextAction(bowls.ToList()));
    }

    [Test]
    public void T04_CheckResetAtStrikeLastFrame()
    {
        //the increment is the number of bowls we rolled, e. g the 18th key is the 18th bowl, and the value is the score we get from that bowl;
        int[] rolls = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 10 };
        pinFalls.AddRange(rolls);
        Assert.AreEqual(reset, ActionMaster.NextAction(pinFalls));
        //  Assert.AreEqual(reset, actionMaster.Bowl(9)); the 19th bowl we hit;
    }

    [Test]
    public void T05_CheckResetAtSpareLastFrame()
    {
        //  int[] rolls = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1};
        //foreach (int roll in rolls)
        //{
        //    actionMaster.Bowl(roll);
        //}
        //actionMaster.Bowl(1); //the 19th bowl we hit;
        //Assert.AreEqual(reset, actionMaster.Bowl(9));  //the 20th bowl we hit;

        int[] rolls = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 9 };
        pinFalls.AddRange(rolls);
        Assert.AreEqual(reset, ActionMaster.NextAction(pinFalls));
    }

    [Test]
    public void T06_YBRollsEndInEndGame()
    {
        //    int[] rolls={ 8,2, 7,3, 3,4, 10, 2,8, 10, 10, 8,0, 10, 8,2}; // here we finished the 10 frames;
        //    foreach (int roll in rolls)
        //    {
        //        actionMaster.Bowl(roll);
        //    }
        //    Assert.AreEqual(endGame, actionMaster.Bowl(8));  // this is 21 bowl, it doesn't matter what score we hit, we finish the game anyway;

        int[] rolls = { 8, 2, 7, 3, 3, 4, 10, 2, 8, 10, 10, 8, 0, 10, 8, 2, 8 };
        pinFalls.AddRange(rolls);
        Assert.AreEqual(endGame, ActionMaster.NextAction(pinFalls));
    }

    [Test]
    public void T07_EndAtBowl20()
    {
        //    int[] rolls = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        //    foreach (int roll in rolls)
        //    {
        //        actionMaster.Bowl(roll);
        //    }
        //    Assert.AreEqual(endGame, actionMaster.Bowl(8)); // any score which lower than 9 will end the game;

        int[] rolls = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 8 };
        pinFalls.AddRange(rolls);
        Assert.AreEqual(endGame, ActionMaster.NextAction(pinFalls));
    }

    [Test]
    public void T08_TidyAtBowl20()
    {
        //    int[] rolls= { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 10 };
        //    foreach (int roll in rolls)
        //    {
        //        actionMaster.Bowl(roll);
        //    }
        //    Assert.AreEqual(tidy, actionMaster.Bowl(9));  //any score lower than 10 will work

        int[] rolls = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 10, 9 };
        pinFalls.AddRange(rolls);
        Assert.AreEqual(tidy, ActionMaster.NextAction(pinFalls));
    }

    [Test]
    public void T09_Bowl20_ZeroScoreTidy()
    {
        //    int[] rolls= { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 10 };
        //    foreach (int roll in rolls)
        //    {
        //        actionMaster.Bowl(roll);
        //    }
        //    Assert.AreEqual(tidy, actionMaster.Bowl(0)); // if the 20 bowl get 0 score; should tidy;

        int[] rolls = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 10, 0 };
        pinFalls.AddRange(rolls);
        Assert.AreEqual(tidy, ActionMaster.NextAction(pinFalls));
    }

    //[Test]
    //public void T10_FirstRollZero_SecondRollStrike_case() // compare bowls at this test;
    //{
    //    int[] rolls = { 1, 1, 1, 1, 0,10};
    //    foreach (int  roll in rolls)
    //    {
    //        actionMaster.Bowl(roll);
    //    }
    //    Assert.AreEqual(rolls.Length+1, actionMaster.bowlCount());  // after we roll 3 frames , the start of 4th frame should be 7th bowl;

    //}

    [Test]
    public void T11_TirstTollZero_SecondRollStrike_case2()
    {
        //    int[] rolls = { 1, 1, 1, 1, 0, 10,4 };
        //    foreach (int roll in rolls)
        //    {
        //        actionMaster.Bowl(roll);
        //    }
        //    Assert.AreEqual(endTurn, actionMaster.Bowl(3));

        int[] rolls = { 1, 1, 1, 1, 0, 10, 4, 3 };
        pinFalls.AddRange(rolls);
        Assert.AreEqual(endTurn, ActionMaster.NextAction(pinFalls));
    }

    [Test]
    public void T12_Dondi10thFrameTurkey()
    {
        //    int[] rolls = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        //    foreach (int roll in rolls)
        //    {
        //        actionMaster.Bowl(roll);
        //    }
        //    Assert.AreEqual(reset, actionMaster.Bowl(10));
        //    Assert.AreEqual(reset, actionMaster.Bowl(10));
        //    Assert.AreEqual(endGame, actionMaster.Bowl(10));

        int[] rolls = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 10, };
        pinFalls.AddRange(rolls);
        Assert.AreEqual(reset, ActionMaster.NextAction(pinFalls));
        pinFalls.Add(10);
        Assert.AreEqual(reset, ActionMaster.NextAction(pinFalls));
        pinFalls.Add(10);
        Assert.AreEqual(endGame, ActionMaster.NextAction(pinFalls));
    }

    [Test]
    public void T13_ZeroOneGiveEndTurn()
    {
        int[] rolls = { 0, 1 };
        pinFalls.AddRange(rolls);
        Assert.AreEqual(endTurn, ActionMaster.NextAction(pinFalls));
    }

}