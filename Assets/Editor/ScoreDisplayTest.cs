using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using System.Linq;

[TestFixture]
public class ScoreDisplayTest
{

    [Test]
    public void T00PassingTest()
    {
        Assert.AreEqual(1, 1);
    }

    [Test]
    public void T01Bowl1 ()
    {
        int[] rolls = {1};
        string rollString = "1";
        Assert.AreEqual(rollString, ScoreDisplay.FormatRolls(rolls.ToList()));
    }

    [Test]
    public void T02Bowl12()
    {
        int[] rolls = { 1,2 };
        string rollString = "12";
        Assert.AreEqual(rollString, ScoreDisplay.FormatRolls(rolls.ToList()));
    }

    [Test]
    public void T03Bowl234()
    {
        int[] rolls = { 2, 3, 4 };
        string rollString = "234";
        Assert.AreEqual(rollString, ScoreDisplay.FormatRolls(rolls.ToList()));
    }

    [Test]
    public void T04BowlX1()
    {
        int[] rolls = { 10, 1};
        string rollString = "X 1";
        Assert.AreEqual(rollString, ScoreDisplay.FormatRolls(rolls.ToList()));
    }

    [Test]
    public void T05Bowl19()
    {
        int[] rolls = { 1,9 };
        string rollString = "1/";
        Assert.AreEqual(rollString, ScoreDisplay.FormatRolls(rolls.ToList()));
    }
    [Test]
    public void T06Bowl123455()
    {
        int[] rolls = { 1, 2, 3, 4, 5, 5 };
        string rollString = "12345/";
        Assert.AreEqual(rollString, ScoreDisplay.FormatRolls(rolls.ToList()));
    }

    [Test]
    public void T07BowlStrikeBonus()
    {
        int[] rolls = { 10, 3, 4 };
        string rollString = "X 34";
        Assert.AreEqual(rollString, ScoreDisplay.FormatRolls(rolls.ToList()));
    }

    [Test]
    public void T08BowlStrikeBonus2()
    {
        int[] rolls = { 1, 2, 3, 4, 5, 4, 3, 2, 10, 1, 3, 3, 4 };
        string rollString = "12345432X 1334";
        Assert.AreEqual(rollString, ScoreDisplay.FormatRolls(rolls.ToList()));
    }

    [Test]
    public void T08BowlStrikeBonus3()
    {
        int[] rolls = { 10, 10, 2, 3 };
        string rollString = "X X 23";
        Assert.AreEqual(rollString, ScoreDisplay.FormatRolls(rolls.ToList()));
    }

    [Test]
    public void T09TestGutterGame()
    {
        int[] rolls = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        string rollString = "--------------------";
        Assert.AreEqual(rollString, ScoreDisplay.FormatRolls(rolls.ToList()));
    }

    [Test]
    public void T10TestAllOnes()
    {
        int[] rolls = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        string rollString = "11111111111111111111";
        Assert.AreEqual(rollString, ScoreDisplay.FormatRolls(rolls.ToList()));
    }

    [Test]
    public void T11TestAllStrikes()
    {
        int[] rolls = { 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10 };
        string rollString = "X X X X X X X X X XXX";
        Assert.AreEqual(rollString, ScoreDisplay.FormatRolls(rolls.ToList()));
    }

    [Test]
    public void T12SpareInLastFrame()
    {
        int[] rolls = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 9, 7 };
        string rollString = "1111111111111111111/7";
        Assert.AreEqual(rollString, ScoreDisplay.FormatRolls(rolls.ToList()));
    }

    [Test]
    public void T13StrikeInLastFrame()
    {
        int[] rolls = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 10, 2, 3 };
        string rollString = "111111111111111111X23";
        Assert.AreEqual(rollString, ScoreDisplay.FormatRolls(rolls.ToList()));
    }

    [Test]
    public void T14GoldenCopyA()
    {
        int[] rolls = { 10, 7, 3, 9, 0, 10, 0, 8, 8, 2, 0, 6, 10, 10, 10, 8, 1 };
        string rollString = "X 7/9-X -88/-6X X X81";
        Assert.AreEqual(rollString, ScoreDisplay.FormatRolls(rolls.ToList()));
    }

    [Test]
    public void T15GoldenCopy2()
    {
        int[] rolls = { 10, 9, 1, 9, 1, 9, 1, 9, 1, 7, 0, 9, 0, 10, 8, 2, 8, 2, 10 };
        string rollString = "X 9/9/9/9/7-9-X 8/8/X";
        Assert.AreEqual(rollString, ScoreDisplay.FormatRolls(rolls.ToList()));
    }

    [Test]
    public void T16GoldenCopy3()
    {
        int[] rolls = { 7, 2, 10, 10, 10, 10, 7, 3, 10, 10, 9, 1, 10, 10, 9 };
        string rollString = "72X X X X 7/X X 9/XX9";
        Assert.AreEqual(rollString, ScoreDisplay.FormatRolls(rolls.ToList()));
    }

}
