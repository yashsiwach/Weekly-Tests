using System;
using NUnit.Framework;

public enum Role
{
    Admin, Manager, Agent
}

public enum Permission
{
    CreateLoan, ApproveLoan, RejectLoan, ViewAll, ViewSelf
}
public class User
{
    public string Id;
    public Role Role;
    public decimal ApprovalLimit;
}
public class Resource
{
    public string OwnerId;
    public decimal Amount;
}
public class Authorizer
{
    /// <summary>
    /// Evaluates access based on role, permission, and resource context.
    /// Enforces self-ownership for agents and approval limits for managers.
    /// Admin has full access.
    /// </summary>
    public bool Authorize(User user, Permission permission, Resource resource)
    {
        if (user.Role == Role.Admin)
            return true;
        if (user.Role == Role.Agent)
        {
            if (permission == Permission.ViewSelf)
                return resource.OwnerId == user.Id;
            return false;
        }

        if (user.Role == Role.Manager)
        {
            if (permission == Permission.ViewAll)
                return true;
            if (permission == Permission.ApproveLoan)
                return resource.Amount <= user.ApprovalLimit;
            if (permission == Permission.RejectLoan)
                return true;
        }
        return false;
    }
}

[TestFixture]
public class AuthorizerTests
{
    [Test]
    public void Agent_Can_View_Only_Self()
    {
        var agent = new User { Id = "A1", Role = Role.Agent };
        var res = new Resource { OwnerId = "A1", Amount = 100 };
        Assert.IsTrue(new Authorizer().Authorize(agent, Permission.ViewSelf, res));
        Assert.IsFalse(new Authorizer().Authorize(agent, Permission.ViewAll, res));
    }

    [Test]
    public void Manager_Approval_Limit_Enforced()
    {
        var mgr = new User { Id = "M1", Role = Role.Manager, ApprovalLimit = 500 };
        var res = new Resource { OwnerId = "X", Amount = 600 };
        Assert.IsFalse(new Authorizer().Authorize(mgr, Permission.ApproveLoan, res));
    }

    [Test]
    public void Admin_Has_Full_Access()
    {
        var admin = new User { Id = "AD", Role = Role.Admin };
        var res = new Resource { OwnerId = "X", Amount = 1000 };

        Assert.IsTrue(new Authorizer().Authorize(admin, Permission.ApproveLoan, res));
        Assert.IsTrue(new Authorizer().Authorize(admin, Permission.ViewAll, res));
    }
}