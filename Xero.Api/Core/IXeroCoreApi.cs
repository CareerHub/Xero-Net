using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xero.Api.Core.Endpoints;
using Xero.Api.Core.Model;

namespace Xero.Api.Core {
    public interface IXeroCoreApi {
        IAccountsEndpoint Accounts { get; }
        IBankTransactionsEndpoint BankTransactions { get; }
        IBankTransfersEndpoint BankTransfers { get; }
        IBrandingThemesEndpoint BrandingThemes { get; }
        IContactsEndpoint Contacts { get; }
        ICreditNotesEndpoint CreditNotes { get; }
        ICurrenciesEndpoint Currencies { get; set; }
        IEmployeesEndpoint Employees { get; }
        IExpenseClaimsEndpoint ExpenseClaims { get; }
        IInvoicesEndpoint Invoices { get; }
        IJournalsEndpoint Journals { get; }
        IManualJournalsEndpoint ManualJournals { get; }
        IOverpaymentsEndpoint Overpayments { get; }
        IPaymentsEndpoint Payments { get; }
        PdfEndpoint PdfFiles { get; }
        IPrepaymentsEndpoint Prepayments { get; }
        IPurchaseOrdersEndpoint PurchaseOrders { get; }
        IReceiptsEndpoint Receipts { get; }
        IRepeatingInvoicesEndpoint RepeatingInvoices { get; }
        ITaxRatesEndpoint TaxRates { get; }
        IUsersEndpoint Users { get; }

        string BaseUri { get; }
        ProductInfoHeaderValue UserAgent { get; set; }

        Task<Organisation> GetOrganisationAsync();

        //Accounts
        Task<IEnumerable<Account>> CreateAsync(IEnumerable<Account> items);
        Task<IEnumerable<Account>> UpdateAsync(IEnumerable<Account> items);
        Task<Account> CreateAsync(Account item);
        Task<Account> UpdateAsync(Account item);

        //BankTransactions
        Task<IEnumerable<BankTransaction>> CreateAsync(IEnumerable<BankTransaction> items);
        Task<IEnumerable<BankTransaction>> UpdateAsync(IEnumerable<BankTransaction> items);
        Task<BankTransaction> CreateAsync(BankTransaction item);
        Task<BankTransaction> UpdateAsync(BankTransaction item);

        //BankTransfers
        Task<IEnumerable<BankTransfer>> CreateAsync(IEnumerable<BankTransfer> items);
        Task<BankTransfer> CreateAsync(BankTransfer item);

        //Contacts
        Task<IEnumerable<Contact>> CreateAsync(IEnumerable<Contact> items);
        Task<IEnumerable<Contact>> UpdateAsync(IEnumerable<Contact> items);
        Task<Contact> CreateAsync(Contact item);
        Task<Contact> UpdateAsync(Contact item);

        //CreditNotes
        Task<IEnumerable<CreditNote>> CreateAsync(IEnumerable<CreditNote> items);
        Task<IEnumerable<CreditNote>> UpdateAsync(IEnumerable<CreditNote> items);
        Task<CreditNote> CreateAsync(CreditNote item);
        Task<CreditNote> UpdateAsync(CreditNote item);

        //Employees
        Task<IEnumerable<Employee>> CreateAsync(IEnumerable<Employee> items);
        Task<IEnumerable<Employee>> UpdateAsync(IEnumerable<Employee> items);
        Task<Employee> CreateAsync(Employee item);
        Task<Employee> UpdateAsync(Employee item);

        //ExpenseClaims
        Task<IEnumerable<ExpenseClaim>> CreateAsync(IEnumerable<ExpenseClaim> items);
        Task<IEnumerable<ExpenseClaim>> UpdateAsync(IEnumerable<ExpenseClaim> items);
        Task<ExpenseClaim> CreateAsync(ExpenseClaim item);
        Task<ExpenseClaim> UpdateAsync(ExpenseClaim item);

        //Invoices
        Task<IEnumerable<Invoice>> CreateAsync(IEnumerable<Invoice> items);
        Task<IEnumerable<Invoice>> UpdateAsync(IEnumerable<Invoice> items);
        Task<Invoice> CreateAsync(Invoice item);
        Task<Invoice> UpdateAsync(Invoice item);

        //ManualJournals
        Task<IEnumerable<ManualJournal>> CreateAsync(IEnumerable<ManualJournal> items);
        Task<IEnumerable<ManualJournal>> UpdateAsync(IEnumerable<ManualJournal> items);
        Task<ManualJournal> CreateAsync(ManualJournal item);
        Task<ManualJournal> UpdateAsync(ManualJournal item);

        //Payments
        Task<IEnumerable<Payment>> CreateAsync(IEnumerable<Payment> items);
        Task<IEnumerable<Payment>> UpdateAsync(IEnumerable<Payment> items);
        Task<Payment> CreateAsync(Payment item);
        Task<Payment> UpdateAsync(Payment item);

        //PurchaseOrders
        Task<IEnumerable<PurchaseOrder>> CreateAsync(IEnumerable<PurchaseOrder> items);
        Task<IEnumerable<PurchaseOrder>> UpdateAsync(IEnumerable<PurchaseOrder> items);
        Task<PurchaseOrder> CreateAsync(PurchaseOrder item);
        Task<PurchaseOrder> UpdateAsync(PurchaseOrder item);

        //Receipts
        Task<IEnumerable<Receipt>> CreateAsync(IEnumerable<Receipt> items);
        Task<IEnumerable<Receipt>> UpdateAsync(IEnumerable<Receipt> items);
        Task<Receipt> CreateAsync(Receipt item);
        Task<Receipt> UpdateAsync(Receipt item);

        //TaxRates
        Task<IEnumerable<TaxRate>> CreateAsync(IEnumerable<TaxRate> items);
        Task<IEnumerable<TaxRate>> UpdateAsync(IEnumerable<TaxRate> items);
        Task<TaxRate> CreateAsync(TaxRate item);
        Task<TaxRate> UpdateAsync(TaxRate item);

        void SummarizeErrors(bool summarize);
    }
}