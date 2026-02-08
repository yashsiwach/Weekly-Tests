using System;
using System.Collections.Generic;
using NUnit.Framework;

public enum LoanState
{
    Draft, Submitted, InReview, Approved, Rejected, Disbursed
}

public class LoanApp
{
    public string Id;
    public LoanState State = LoanState.Draft;
    public List<LoanState> History = new List<LoanState> { LoanState.Draft };
}

public class WorkflowEngine
{
    Dictionary<string, LoanApp> store = new Dictionary<string, LoanApp>();

    /// <summary>
    /// Validates state transitions based on current state and action
    /// Records every state change in history
    /// Enforces rule: Disburse allowed only after Approved
    /// </summary>
    public void ChangeState(string appId, string action)
    {
        if (!store.ContainsKey(appId))
            store[appId] = new LoanApp { Id = appId };

        var app = store[appId];
        var next = GetNext(app.State, action);

        app.State = next;
        app.History.Add(next);
    }

    LoanState GetNext(LoanState cur, string action)
    {
        return (cur, action) switch
        {
            (LoanState.Draft, "Submit") => LoanState.Submitted,
            (LoanState.Submitted, "Review") => LoanState.InReview,
            (LoanState.InReview, "Approve") => LoanState.Approved,
            (LoanState.InReview, "Reject") => LoanState.Rejected,
            (LoanState.Approved, "Disburse") => LoanState.Disbursed,
            _ => throw new InvalidOperationException("Invalid transition")
        };
    }

    public LoanApp Get(string id) => store[id];
}

[TestFixture]
public class WorkflowEngineTests
{
    [Test]
    public void HappyFlow_Works()
    {
        var wf = new WorkflowEngine();
        wf.ChangeState("1", "Submit");
        wf.ChangeState("1", "Review");
        wf.ChangeState("1", "Approve");
        wf.ChangeState("1", "Disburse");

        var app = wf.Get("1");
        Assert.AreEqual(LoanState.Disbursed, app.State);
        Assert.AreEqual(5, app.History.Count);
    }

    [Test]
    public void CannotDisburseWithoutApproval()
    {
        var wf = new WorkflowEngine();
        wf.ChangeState("2", "Submit");
        wf.ChangeState("2", "Review");

        Assert.Throws<InvalidOperationException>(() =>
            wf.ChangeState("2", "Disburse"));
    }
}