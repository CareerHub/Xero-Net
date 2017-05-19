using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xero.Api.Infrastructure.Exceptions;
using Xero.Api.Infrastructure.Model;

namespace Xero.Api.Tests.Unit {
    [TestClass]
    public class ExceptionTests {
        [TestMethod]
        public void ManageNullValidationErrorsInApiException() {
            var validationException = new ValidationException(ApiException);
            Assert.AreEqual(5, validationException.ValidationErrors.Count);
        }

        private ApiException ApiException {
            get {
                return new ApiException {
                    Elements = new List<DataContractBase>
                    {
                        new DataContractBase
                        {
                            ValidationErrors = new List<ValidationError>
                            {
                                new ValidationError {Message = "Validation Error 1"}
                            }
                        },
                        new DataContractBase
                        {
                            ValidationErrors = new List<ValidationError>
                            {
                                new ValidationError {Message = "Validation Error 2"}
                            }
                        },
                        new DataContractBase
                        {
                            ValidationErrors = new List<ValidationError>
                            {
                                new ValidationError {Message = "Validation Error 3"}
                            }
                        },
                        new DataContractBase
                        {
                            ValidationErrors = new List<ValidationError>
                            {
                                new ValidationError {Message = "Validation Error 4"}
                            }
                        },
                        new DataContractBase
                        {
                            ValidationErrors = new List<ValidationError>
                            {
                                new ValidationError {Message = "Validation Error 5"}
                            }
                        },
                        new DataContractBase
                        {
                            ValidationErrors = null // this is a valid data condition for large responses
                        }
                    }
                };
            }
        }
    }
}