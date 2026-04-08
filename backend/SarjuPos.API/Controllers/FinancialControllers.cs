using Microsoft.AspNetCore.Authorization;
using SarjuPos.API.Data;
using SarjuPos.API.Models;

namespace SarjuPos.API.Controllers
{
    [Authorize]
    public class ExpensesController : BaseController<Expense>
    {
        public ExpensesController(IRepository<Expense> repository) : base(repository)
        {
        }
    }

    [Authorize]
    public class StaffController : BaseController<Staff>
    {
        public StaffController(IRepository<Staff> repository) : base(repository)
        {
        }
    }

    [Authorize]
    public class PurchasesController : BaseController<Purchase>
    {
        public PurchasesController(IRepository<Purchase> repository) : base(repository)
        {
        }
    }

    [Authorize]
    public class CreditNotesController : BaseController<CreditNote>
    {
        public CreditNotesController(IRepository<CreditNote> repository) : base(repository)
        {
        }
    }

    [Authorize]
    public class TransactionsController : BaseController<PaymentTransaction>
    {
        public TransactionsController(IRepository<PaymentTransaction> repository) : base(repository)
        {
        }
    }

    [Authorize]
    public class AuditLogsController : BaseController<AuditLog>
    {
        public AuditLogsController(IRepository<AuditLog> repository) : base(repository)
        {
        }
    }
}
