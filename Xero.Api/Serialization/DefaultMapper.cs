using System;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using ServiceStack.Text;
using Xero.Api.Core.Model.Status;
using Xero.Api.Core.Model.Types;
using Xero.Api.Infrastructure.Interfaces;
using AccountType = Xero.Api.Core.Model.Types.AccountType;
using UnitType = Xero.Api.Core.Model.Types.UnitType;

namespace Xero.Api.Serialization {
    public class DefaultMapper : IJsonObjectMapper, IXmlObjectMapper {
        public DefaultMapper() {
            JsConfig<AccountStatus>.DeSerializeFn = EnumDeserializer<AccountStatus>;
            JsConfig<BankTransactionStatus>.DeSerializeFn = EnumDeserializer<BankTransactionStatus>;
            JsConfig<ContactStatus>.DeSerializeFn = EnumDeserializer<ContactStatus>;
            JsConfig<EmployeeStatus>.DeSerializeFn = EnumDeserializer<EmployeeStatus>;
            JsConfig<ExpenseClaimStatus>.DeSerializeFn = EnumDeserializer<ExpenseClaimStatus>;
            JsConfig<InvoiceStatus>.DeSerializeFn = EnumDeserializer<InvoiceStatus>;
            JsConfig<ManualJournalStatus>.DeSerializeFn = EnumDeserializer<ManualJournalStatus>;
            JsConfig<OrganisationStatus>.DeSerializeFn = EnumDeserializer<OrganisationStatus>;
            JsConfig<PaymentStatus>.DeSerializeFn = EnumDeserializer<PaymentStatus>;
            JsConfig<PurchaseOrderStatus>.DeSerializeFn = EnumDeserializer<PurchaseOrderStatus>;
            JsConfig<ReceiptStatus>.DeSerializeFn = EnumDeserializer<ReceiptStatus>;
            JsConfig<TaxRateStatus>.DeSerializeFn = EnumDeserializer<TaxRateStatus>;
            JsConfig<TrackingCategoryStatus>.DeSerializeFn = EnumDeserializer<TrackingCategoryStatus>;

            JsConfig<AccountClassType>.DeSerializeFn = EnumDeserializer<AccountClassType>;
            JsConfig<AccountType>.DeSerializeFn = EnumDeserializer<AccountType>;
            JsConfig<AddressType>.DeSerializeFn = EnumDeserializer<AddressType>;
            JsConfig<BankTransactionType>.DeSerializeFn = EnumDeserializer<BankTransactionType>;
            JsConfig<CreditNoteType>.DeSerializeFn = EnumDeserializer<CreditNoteType>;
            JsConfig<InvoiceType>.DeSerializeFn = EnumDeserializer<InvoiceType>;
            JsConfig<ObjectGroupType>.DeSerializeFn = EnumDeserializer<ObjectGroupType>;
            JsConfig<ObjectGroupType?>.DeSerializeFn = EnumDeserializerNullable<ObjectGroupType>;
            JsConfig<ObjectType>.DeSerializeFn = EnumDeserializer<ObjectType>;
            JsConfig<ObjectType?>.DeSerializeFn = EnumDeserializerNullable<ObjectType>;
            JsConfig<OrganisationType>.DeSerializeFn = EnumDeserializer<OrganisationType>;
            JsConfig<OrganisationVersion>.DeSerializeFn = EnumDeserializer<OrganisationVersion>;
            JsConfig<OverpaymentType>.DeSerializeFn = EnumDeserializer<OverpaymentType>;
            JsConfig<PaymentTermType>.DeSerializeFn = EnumDeserializer<PaymentTermType>;
            JsConfig<PaymentType>.DeSerializeFn = EnumDeserializer<PaymentType>;
            JsConfig<PhoneType>.DeSerializeFn = EnumDeserializer<PhoneType>;
            JsConfig<PrepaymentType>.DeSerializeFn = EnumDeserializer<PrepaymentType>;
            JsConfig<ReportTaxType>.DeSerializeFn = EnumDeserializer<ReportTaxType>;
            JsConfig<SalesTaxBasisType>.DeSerializeFn = SalesTaxBasis;
            JsConfig<SalesTaxPeriodType>.DeSerializeFn = SalesTaxPeriod;
            JsConfig<SourceType?>.DeSerializeFn = EnumDeserializerNullable<SourceType>;
            JsConfig<SystemAccountType?>.DeSerializeFn = EnumDeserializerNullable<SystemAccountType>;
            JsConfig<UnitType>.DeSerializeFn = EnumDeserializer<UnitType>;
            JsConfig<UserRole>.DeSerializeFn = EnumDeserializer<UserRole>;
        }

        T IJsonObjectMapper.From<T>(string result) {
            var item = JsonSerializer.DeserializeFromString<T>(result);
            return item;
        }

        string IJsonObjectMapper.To<T>(T request) {
            var json = JsonSerializer.SerializeToString(request);
            return json;
        }

        T IXmlObjectMapper.From<T>(string result) {
            var item = XmlSerializer.DeserializeFromString<T>(result);
            return item;
        }

        string IXmlObjectMapper.To<T>(T request) {
            string xml = XmlSerializer.SerializeToString(request);
            return xml;
        }

        private static TEnum EnumDeserializer<TEnum>(string s)
            where TEnum : struct {
            return EnumDeserializerRun<TEnum>(s);
        }

        private static TEnum? EnumDeserializerNullable<TEnum>(string s)
            where TEnum : struct {
            // first off, is this an empty string? 
            if(String.IsNullOrEmpty(s)) {
                // ... then just return null
                return null;
            }
            return EnumDeserializerRun<TEnum>(s);
        }

        private static TEnum EnumDeserializerRun<TEnum>(string s) where TEnum : struct {
            TEnum t;

            // If all goes well then we are done
            if(Enum.TryParse(s, out t))
                return t;

            // get the EnumMember attribute and see if the Value attribute matches the string
            foreach(var p in t.GetType().GetMembers()) {
                var attributes = p.GetCustomAttributes(typeof(EnumMemberAttribute), false).Cast<EnumMemberAttribute>();
                if(attributes.All(a => String.Compare(a.Value, s, StringComparison.OrdinalIgnoreCase) != 0))
                    continue;
                Enum.TryParse(p.Name, out t);
            }

            return t;
        }

        private static SalesTaxBasisType SalesTaxBasis(string s) {
            switch(s.ToUpper()) {
                case "ACCURAL":
                case "ACCURALS": {
                        return SalesTaxBasisType.Accural;
                    }

                default: {
                        return EnumDeserializer<SalesTaxBasisType>(s);
                    }
            }
        }

        private static SalesTaxPeriodType SalesTaxPeriod(string s) {
            switch(s.ToUpper()) {
                case "MONTHLY":
                case "ONEMONTHS":
                case "1MONTHLY": {
                        return SalesTaxPeriodType.Monthly;
                    }
                case "TWOMONTHS":
                case "2MONTHLY": {
                        return SalesTaxPeriodType.TwoMonths;
                    }
                case "3MONTHLY":
                case "QUARTERLY": {
                        return SalesTaxPeriodType.Quarterly;
                    }
                case "SIXMONTHS":
                case "6MONTHLY": {
                        return SalesTaxPeriodType.SixMonths;
                    }
                case "ANNUALLY":
                case "YEARLY": {
                        return SalesTaxPeriodType.Annually;
                    }

                default: {
                        return EnumDeserializer<SalesTaxPeriodType>(s);
                    }
            }
        }
    }
}