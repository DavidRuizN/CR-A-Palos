using Common.Domain.Entities;
using MemberManagement.Domain.Aggregates.MemberAggregate.Enums;

namespace MemberManagement.Domain.Aggregates.MemberAggregate;

public class MembershipPayment : Entity<Guid>
{
    public Guid MemberId { get; private set; }
    public double Amount { get; private set; }
    public DateTime? PaidAt { get; private set; }
    public PaymentMethod Method { get; private set; }
    public PaymentStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private MembershipPayment()
    {
        // EF Core requires a parameterless constructor for materialization
    }

    private MembershipPayment(Guid memberId, double amount, DateTime? paidAt, PaymentMethod method, PaymentStatus status)
    {
        MemberId = memberId;
        Amount = amount;
        PaidAt = paidAt;
        Method = method;
        Status = status;
        CreatedAt = DateTime.UtcNow;
    }

    public static MembershipPayment Create(Guid memberId, double amount, DateTime? paidAt, PaymentMethod method, PaymentStatus status)
    {
        return new MembershipPayment(memberId, amount, paidAt, method, status);
    }

    public void ChangePaidAt(DateTime? newPaidAt)
    {
        PaidAt = newPaidAt;
        Status = PaymentStatus.Completed;
    }

    public void ChangeStatus(PaymentStatus newStatus)
    {
        Status = newStatus;
    }
}

