using System.Collections.Generic;
using System.Reflection;
using NUnit.Framework;
using UnityEngine;

public class RingControllerTests
{
    private GameObject go;
    private RingController ringController;

    [SetUp]
    public void SetUp()
    {
        go = new GameObject();
        ringController = go.AddComponent<RingController>();
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(go);
    }

    [Test]
    public void GetSegmentColorAtAngle_ReturnsWhiteWhenNoSegmentsDefined()
    {
        Assert.AreEqual(Color.white, ringController.GetSegmentColorAtAngle(0f));
    }

    [Test]
    public void GetSegmentColorAtAngle_ReturnsFirstColorAtZeroDegrees()
    {
        SetSegmentColors(new List<Color> { Color.red, Color.blue });

        Assert.AreEqual(Color.red, ringController.GetSegmentColorAtAngle(0f));
    }

    [Test]
    public void GetSegmentColorAtAngle_ReturnsSecondColorAtHalfwayAngle()
    {
        SetSegmentColors(new List<Color> { Color.red, Color.blue });

        Assert.AreEqual(Color.blue, ringController.GetSegmentColorAtAngle(180f));
    }

    [Test]
    public void GetSegmentColorAtAngle_WrapsAroundCorrectly()
    {
        SetSegmentColors(new List<Color> { Color.red, Color.blue });

        Assert.AreEqual(Color.red, ringController.GetSegmentColorAtAngle(360f));
    }

    [Test]
    public void SetRotationDirection_SetsPositiveOne()
    {
        ringController.SetRotationDirection(1);

        Assert.AreEqual(1, GetRotationDirectionField());
    }

    [Test]
    public void SetRotationDirection_SetsNegativeOne()
    {
        ringController.SetRotationDirection(-1);

        Assert.AreEqual(-1, GetRotationDirectionField());
    }

    [Test]
    public void SetRotationDirection_SetsZero()
    {
        ringController.SetRotationDirection(0);

        Assert.AreEqual(0, GetRotationDirectionField());
    }

    [Test]
    public void SetRotationDirection_ClampsValueAboveOne()
    {
        ringController.SetRotationDirection(5);

        Assert.AreEqual(1, GetRotationDirectionField());
    }

    [Test]
    public void SetRotationDirection_ClampsValueBelowNegativeOne()
    {
        ringController.SetRotationDirection(-5);

        Assert.AreEqual(-1, GetRotationDirectionField());
    }

    [Test]
    public void SegmentColors_ReturnsReadOnlyList()
    {
        Assert.IsInstanceOf<IReadOnlyList<Color>>(ringController.SegmentColors);
    }

    private int GetRotationDirectionField()
    {
        var field = typeof(RingController).GetField(
            "rotationDirection",
            BindingFlags.NonPublic | BindingFlags.Instance);
        return (int)field.GetValue(ringController);
    }

    private void SetSegmentColors(List<Color> colors)
    {
        var field = typeof(RingController).GetField(
            "segmentColors",
            BindingFlags.NonPublic | BindingFlags.Instance);
        field.SetValue(ringController, colors);
    }
}
