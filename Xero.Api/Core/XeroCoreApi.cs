using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xero.Api.Common;
using Xero.Api.Core.Endpoints;
using Xero.Api.Core.Model;
using Xero.Api.Infrastructure.Interfaces;
using Xero.Api.Infrastructure.RateLimiter;
using Xero.Api.Serialization;

namespace Xero.Api.Core {
    public class XeroCoreApi : XeroApi, IXeroCoreApi {
        private IOrganisationEndpoint OrganisationEndpoint { get; set; }

        public XeroCoreApi(string baseUri, IAuthenticator auth, IConsumer consumer, IUser user,
            IJsonObjectMapper readMapper, IXmlObjectMapper writeMapper)
            : this(baseUri, auth, consumer, user, readMapper, writeMapper, null) {
        }

        public XeroCoreApi(string baseUri, IAuthenticator auth, IConsumer consumer, IUser user, IJsonObjectMapper readMapper, IXmlObjectMapper writeMapper, IRateLimiter rateLimiter)
            : base(baseUri, auth, consumer, user, readMapper, writeMapper, rateLimiter) {
            Connect();
        }

        public XeroCoreApi(string baseUri, IAuthenticator auth, IConsumer consumer, IUser user)
            : this(baseUri, auth, consumer, user, null) {
        }

        public XeroCoreApi(string baseUri, IAuthenticator auth, IConsumer consumer, IUser user, IRateLimiter rateLimiter)
            : this(baseUri, auth, consumer, user, new DefaultMapper(), new DefaultMapper(), rateLimiter) {
        }

        public IAccountsEndpoint Accounts { get; private set; }
        public IBankTransactionsEndpoint BankTransactions { get; private set; }
        public IBankTransfersEndpoint BankTransfers { get; private set; }
        public IBrandingThemesEndpoint BrandingThemes { get; private set; }
        public IContactsEndpoint Contacts { get; private set; }
        public ICreditNotesEndpoint CreditNotes { get; private set; }
        public ICurrenciesEndpoint Currencies { get; set; }
        public IEmployeesEndpoint Employees { get; private set; }
        public IExpenseClaimsEndpoint ExpenseClaims { get; private set; }
        public IInvoicesEndpoint Invoices { get; private set; }
        public IJournalsEndpoint Journals { get; protected set; }
        public IManualJournalsEndpoint ManualJournals { get; private set; }
        public IOverpaymentsEndpoint Overpayments { get; private set; }
        public IPaymentsEndpoint Payments { get; private set; }
        public PdfEndpoint PdfFiles { get; private set; }
        public IPrepaymentsEndpoint Prepayments { get; private set; }
        public IPurchaseOrdersEndpoint PurchaseOrders { get; private set; }
        public IReceiptsEndpoint Receipts { get; private set; }
        public IRepeatingInvoicesEndpoint RepeatingInvoices { get; private set; }
        public ITaxRatesEndpoint TaxRates { get; private set; }
        public IUsersEndpoint Users { get; private set; }


        private void Connect() {
            OrganisationEndpoint = new OrganisationEndpoint(Client);

            Accounts = new AccountsEndpoint(Client);
            BankTransactions = new BankTransactionsEndpoint(Client);
            BankTransfers = new BankTransfersEndpoint(Client);
            BrandingThemes = new BrandingThemesEndpoint(Client);
            Contacts = new ContactsEndpoint(Client);
            CreditNotes = new CreditNotesEndpoint(Client);
            Currencies = new CurrenciesEndpoint(Client);
            Employees = new EmployeesEndpoint(Client);
            ExpenseClaims = new ExpenseClaimsEndpoint(Client);
            Invoices = new InvoicesEndpoint(Client);
            Journals = new JournalsEndpoint(Client);
            ManualJournals = new ManualJournalsEndpoint(Client);
            Overpayments = new OverpaymentsEndpoint(Client);
            Payments = new PaymentsEndpoint(Client);
            PdfFiles = new PdfEndpoint(Client);
            Prepayments = new PrepaymentsEndpoint(Client);
            PurchaseOrders = new PurchaseOrdersEndpoint(Client);
            Receipts = new ReceiptsEndpoint(Client);
            RepeatingInvoices = new RepeatingInvoicesEndpoint(Client);
            TaxRates = new TaxRatesEndpoint(Client);
            Users = new UsersEndpoint(Client);
        }

        public async Task<Organisation> GetOrganisationAsync() {
            return (await OrganisationEndpoint.FindAsync()).FirstOrDefault();
        }

        #region Accounts

        public Task<IEnumerable<Account>> CreateAsync(IEnumerable<Account> items) {
            return Accounts.CreateAsync(items);
        }

        public Task<IEnumerable<Account>> UpdateAsync(IEnumerable<Account> items) {
            return Accounts.UpdateAsync(items);
        }

        public Task<Account> CreateAsync(Account item) {
            return Accounts.CreateAsync(item);
        }

        public Task<Account> UpdateAsync(Account item) {
            return Accounts.UpdateAsync(item);
        }

        #endregion

        #region BankTransactions

        public Task<IEnumerable<BankTransaction>> CreateAsync(IEnumerable<BankTransaction> items) {
            return BankTransactions.CreateAsync(items);
        }

        public Task<IEnumerable<BankTransaction>> UpdateAsync(IEnumerable<BankTransaction> items) {
            return BankTransactions.UpdateAsync(items);
        }

        public Task<BankTransaction> CreateAsync(BankTransaction item) {
            return BankTransactions.CreateAsync(item);
        }

        public Task<BankTransaction> UpdateAsync(BankTransaction item) {
            return BankTransactions.UpdateAsync(item);
        }

        #endregion

        #region BankTransfers

        public Task<IEnumerable<BankTransfer>> CreateAsync(IEnumerable<BankTransfer> items) {
            return BankTransfers.CreateAsync(items);
        }

        public Task<BankTransfer> CreateAsync(BankTransfer item) {
            return BankTransfers.CreateAsync(item);
        }

        #endregion

        #region Contacts

        public Task<IEnumerable<Contact>> CreateAsync(IEnumerable<Contact> items) {
            return Contacts.CreateAsync(items);
        }

        public Task<IEnumerable<Contact>> UpdateAsync(IEnumerable<Contact> items) {
            return Contacts.UpdateAsync(items);
        }

        public Task<Contact> CreateAsync(Contact item) {
            return Contacts.CreateAsync(item);
        }

        public Task<Contact> UpdateAsync(Contact item) {
            return Contacts.UpdateAsync(item);
        }

        #endregion

        #region CreditNotes

        public Task<IEnumerable<CreditNote>> CreateAsync(IEnumerable<CreditNote> items) {
            return CreditNotes.CreateAsync(items);
        }

        public Task<IEnumerable<CreditNote>> UpdateAsync(IEnumerable<CreditNote> items) {
            return CreditNotes.UpdateAsync(items);
        }

        public Task<CreditNote> CreateAsync(CreditNote item) {
            return CreditNotes.CreateAsync(item);
        }

        public Task<CreditNote> UpdateAsync(CreditNote item) {
            return CreditNotes.UpdateAsync(item);
        }

        #endregion

        #region Employees

        public Task<IEnumerable<Employee>> CreateAsync(IEnumerable<Employee> items) {
            return Employees.CreateAsync(items);
        }

        public Task<IEnumerable<Employee>> UpdateAsync(IEnumerable<Employee> items) {
            return Employees.UpdateAsync(items);
        }

        public Task<Employee> CreateAsync(Employee item) {
            return Employees.CreateAsync(item);
        }

        public Task<Employee> UpdateAsync(Employee item) {
            return Employees.UpdateAsync(item);
        }

        #endregion

        #region ExpenseClaims

        public Task<IEnumerable<ExpenseClaim>> CreateAsync(IEnumerable<ExpenseClaim> items) {
            return ExpenseClaims.CreateAsync(items);
        }

        public Task<IEnumerable<ExpenseClaim>> UpdateAsync(IEnumerable<ExpenseClaim> items) {
            return ExpenseClaims.UpdateAsync(items);
        }

        public Task<ExpenseClaim> CreateAsync(ExpenseClaim item) {
            return ExpenseClaims.CreateAsync(item);
        }

        public Task<ExpenseClaim> UpdateAsync(ExpenseClaim item) {
            return ExpenseClaims.UpdateAsync(item);
        }

        #endregion

        #region Invoices

        public Task<IEnumerable<Invoice>> CreateAsync(IEnumerable<Invoice> items) {
            return Invoices.CreateAsync(items);
        }

        public Task<IEnumerable<Invoice>> UpdateAsync(IEnumerable<Invoice> items) {
            return Invoices.UpdateAsync(items);
        }

        public Task<Invoice> CreateAsync(Invoice item) {
            return Invoices.CreateAsync(item);
        }

        public Task<Invoice> UpdateAsync(Invoice item) {
            return Invoices.UpdateAsync(item);
        }

        #endregion

        #region ManualJournals

        public Task<IEnumerable<ManualJournal>> CreateAsync(IEnumerable<ManualJournal> items) {
            return ManualJournals.CreateAsync(items);
        }

        public Task<IEnumerable<ManualJournal>> UpdateAsync(IEnumerable<ManualJournal> items) {
            return ManualJournals.UpdateAsync(items);
        }

        public Task<ManualJournal> CreateAsync(ManualJournal item) {
            return ManualJournals.CreateAsync(item);
        }

        public Task<ManualJournal> UpdateAsync(ManualJournal item) {
            return ManualJournals.UpdateAsync(item);
        }

        #endregion

        #region Payments

        public Task<IEnumerable<Payment>> CreateAsync(IEnumerable<Payment> items) {
            return Payments.CreateAsync(items);
        }

        public Task<IEnumerable<Payment>> UpdateAsync(IEnumerable<Payment> items) {
            return Payments.UpdateAsync(items);
        }

        public Task<Payment> CreateAsync(Payment item) {
            return Payments.CreateAsync(item);
        }

        public Task<Payment> UpdateAsync(Payment item) {
            return Payments.UpdateAsync(item);
        }

        #endregion

        #region PurchaseOrders

        public Task<IEnumerable<PurchaseOrder>> CreateAsync(IEnumerable<PurchaseOrder> items) {
            return PurchaseOrders.CreateAsync(items);
        }

        public Task<IEnumerable<PurchaseOrder>> UpdateAsync(IEnumerable<PurchaseOrder> items) {
            return PurchaseOrders.UpdateAsync(items);
        }

        public Task<PurchaseOrder> CreateAsync(PurchaseOrder item) {
            return PurchaseOrders.CreateAsync(item);
        }

        public Task<PurchaseOrder> UpdateAsync(PurchaseOrder item) {
            return PurchaseOrders.UpdateAsync(item);
        }

        #endregion

        #region Receipts

        public Task<IEnumerable<Receipt>> CreateAsync(IEnumerable<Receipt> items) {
            return Receipts.CreateAsync(items);
        }

        public Task<IEnumerable<Receipt>> UpdateAsync(IEnumerable<Receipt> items) {
            return Receipts.UpdateAsync(items);
        }

        public Task<Receipt> CreateAsync(Receipt item) {
            return Receipts.CreateAsync(item);
        }

        public Task<Receipt> UpdateAsync(Receipt item) {
            return Receipts.UpdateAsync(item);
        }

        #endregion

        #region TaxRates

        public Task<IEnumerable<TaxRate>> CreateAsync(IEnumerable<TaxRate> items) {
            return TaxRates.CreateAsync(items);
        }

        public Task<IEnumerable<TaxRate>> UpdateAsync(IEnumerable<TaxRate> items) {
            return TaxRates.UpdateAsync(items);
        }

        public Task<TaxRate> CreateAsync(TaxRate item) {
            return TaxRates.CreateAsync(item);
        }

        public Task<TaxRate> UpdateAsync(TaxRate item) {
            return TaxRates.UpdateAsync(item);
        }

        #endregion

        public void SummarizeErrors(bool summarize) {
            Accounts.SummarizeErrors(summarize);
            BankTransactions.SummarizeErrors(summarize);
            BankTransfers.SummarizeErrors(summarize);
            Contacts.SummarizeErrors(summarize);
            CreditNotes.SummarizeErrors(summarize);
            Employees.SummarizeErrors(summarize);
            Employees.SummarizeErrors(summarize);
            Invoices.SummarizeErrors(summarize);
            ManualJournals.SummarizeErrors(summarize);
            Payments.SummarizeErrors(summarize);
            PurchaseOrders.SummarizeErrors(summarize);
            Receipts.SummarizeErrors(summarize);
            TaxRates.SummarizeErrors(summarize);
        }
    }
}
