using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

using Earthport.MerchantAPI;

namespace MerchantAPI.Test
{
    /// <summary>
    /// .NET client integration tests. NUnit is used as a test harness although strictly speaking these are not unit tests.  Note that in order 
    /// to force test execution order, the tests have been numbered (since NUnit doesn't have a mechanism to explicitly specify order - it 
    /// executes tests in alphabetical order) 
    /// </summary>
    [TestFixture]
    public class ServiceTests
    {
        [TestFixtureSetUp]
        public void Setup()
        {
            _serviceProxy = new ServiceProxy("https://sandbox.earthport.com:11023/MerchantAPIUAT2", "EarthportPaymentTest", "Passw0rd");
        }
        [Test]
        public void _00_EchoTest()
        {
            var echo = new EchoType()
            {
                version = 1.1M,
                echoRequest = "ping"
            };

            var echoResponse = _serviceProxy.Echo(echo);

            Assert.That(echoResponse.echoResponse == "ping");
        }

        [Test]
        public void _10_CreateUserTest()
        {
            var createUser = new CreateUserType()
            {
                version = 3.0M,
                accountCurrency = "GBP",
                merchantUserIdentity = "Donald Duck " + Guid.NewGuid(),
                
                //managedMerchantIdentity =  Guid.NewGuid().ToString(),
                //Payer Identity
                //Represents the payer identity of an individual or company.
                //You must specify one of either a payer individual  identity or payer company identity.
                payerIdentity = new PayerIdentityType()
                {
                    
                    payerIndividualIdentity = new PayerIndividualIdentityType()
                    {
                          
                        address = new AddressType()
                        {
                            addressLine1 = "21 New Street",
                            addressLine2 = "21 New Street 2",
                            addressLine3 = "21 New Street 3",
                            city = "London",
                            postcode = "EC2M 4TP",
                            country = "GB",
                            province = "London"
                        },
                        birthInformation = new BirthInformationType()
                        {
                            cityOfBirth = "London",
                            countryOfBirth = "GB",
                            dateOfBirth = new DateTime(1985, 5, 20)
                        },
                        name=new IndividualNameType()
                        {
                            givenNames = "Joe",
                            familyName = "Doe"
                        
                        }
                    }
                }

            };

            var createUserResponse = _serviceProxy.CreateUser(createUser);
            Assert.That(createUserResponse.userID.epUserID != "");
            _epUserID = createUserResponse.userID.epUserID;
            _merchantUserID = createUserResponse.userID.merchantUserID;
        }

        [ExpectedException(typeof(System.Xml.Schema.XmlSchemaValidationException))]
        [Test]
        public void _11_CreateUserTest_FailValidation()
        {
            var createUser = new CreateUserType()
            {
                version = 3.0M,
                accountCurrency = "GB",
                merchantUserIdentity = "Donald Duck " + Guid.NewGuid().ToString()
            };

            var createUserResponse = _serviceProxy.CreateUser(createUser);
        }

        [ExpectedException(typeof(System.Xml.Schema.XmlSchemaValidationException))]
        [Test]
        public void _12_CreateUserTest_FailValidationNoIdentity()
        {
            var createUser = new CreateUserType()
            {
                version = 3.0M,
                accountCurrency = "GBP"
            };

            var createUserResponse = _serviceProxy.CreateUser(createUser);
        }

        [Test]
        public void _13_CreateOrUpdateUserTest()
        {
            var CreateOrUpdateUser = new CreateOrUpdateUserType()
            {
                version = 1.0M,
                accountCurrency = "GBP",
                merchantUserIdentity = "Donald Duck " + Guid.NewGuid(),

                //managedMerchantIdentity =  Guid.NewGuid().ToString(),
                //Payer Identity
                //Represents the payer identity of an individual or company.
                //You must specify one of either a payer individual  identity or payer company identity.
                payerIdentity = new PayerIdentityType()
                {

                    payerIndividualIdentity = new PayerIndividualIdentityType()
                    {

                        address = new AddressType()
                        {
                            addressLine1 = "21 New Street",
                            addressLine2 = "21 New Street 2",
                            addressLine3 = "21 New Street 3",
                            city = "London",
                            postcode = "EC2M 4TP",
                            country = "GB",
                            province = "London"
                        },
                        birthInformation = new BirthInformationType()
                        {
                            cityOfBirth = "London",
                            countryOfBirth = "GB",
                            dateOfBirth = new DateTime(1985, 5, 20)
                        },
                        name = new IndividualNameType()
                        {
                            givenNames = "Joe",
                            familyName = "Doe"

                        }
                    }
                }

            };

            var CreateOrUpdateUserResponse = _serviceProxy.CreateOrUpdateUser(CreateOrUpdateUser);
            Assert.That(CreateOrUpdateUserResponse.userID.epUserID != "");
            _epUserID = CreateOrUpdateUserResponse.userID.epUserID;
            _merchantUserID = CreateOrUpdateUserResponse.userID.merchantUserID;
        }


        [Test]
        public void _19_GetBeneficiaryBankAccountRequiredData()
        {
            var getBeneficiaryBankAccountRequiredData = new GetBeneficiaryBankAccountRequiredDataType()
            {
                version = 3.0M,
                countryCode = "GB",
                currencyCode = "GBP"                
            };

            var getBeneficiaryBankAccountRequiredDataResponse = _serviceProxy.GetBeneficiaryBankAccountRequiredData(getBeneficiaryBankAccountRequiredData);

            Assert.That(getBeneficiaryBankAccountRequiredDataResponse.beneficiaryBankAccountFieldGroupsList.Length > 0);

        }

        [Test]
        [ExpectedException(typeof(ServiceErrorException))]
        public void _20_ValidateBeneficiaryAccountTest_Failure()
        {
            var bankAccountDetailsSet = new BankAccountDetailsType[]
            {
                new BankAccountDetailsType()
                {
                    key = "description",
                    value = "Test"
                },
                new BankAccountDetailsType()
                {
                    key = "bankName",
                    value = "Test"
                },
                new BankAccountDetailsType()
                {
                    key = "accountName",
                    value = "Test"
                },
                new BankAccountDetailsType()
                {
                    key = "accountNumber",
                    value = "03870448"
                },
                new BankAccountDetailsType()
                {
                    key = "sortCode",
                    value = "536125"
                }
            };

            var beneficiaryBankAccount = new BeneficiaryBankAccountType()
            {
                countryCode = "GB",
                description = "UK Account - ABCD",

                bankAccountDetails = bankAccountDetailsSet,
                beneficiaryIdentity = new BeneficiaryIdentityType()
                {
                    beneficiaryIndividualIdentity = new BeneficiaryIndividualIdentityType()
                    {
                        name = new IndividualNameType()
                        {
                            givenNames = "Akshay",
                            familyName = "Luther"
                        }
                    },
                }
            };

            var validateBeneficiaryAccount = new ValidateBeneficiaryBankAccountType()
            {
                version = 3.0M,
                beneficiaryBankAccount = beneficiaryBankAccount
            };

            try
            {
                _serviceProxy.ValidateBeneficiaryBankAccount(validateBeneficiaryAccount);
            }
            catch (ServiceErrorException ex)
            {
                Assert.That(ex.ServiceErrorResponse.code == 12000); // validation failed
                Assert.That(ex.ServiceErrorResponse.failures[0].key == "bankAccountDetails.sortCode"); // bank account code not supplied

                throw;
            }
        }

        [Test]
        public void _30_ValidateBeneficiaryAccountTest_Success()
        {
            var bankAccountDetailsSet = new BankAccountDetailsType[]
            {
                new BankAccountDetailsType()
                {
                    key = "description",
                    value = "Test"
                },
                new BankAccountDetailsType()
                {
                    key = "bankName",
                    value = "Test"
                },
                new BankAccountDetailsType()
                {
                    key = "accountName",
                    value = "Test"
                },
                new BankAccountDetailsType()
                {
                    key = "accountNumber",
                    value = "03870448"
                },
                new BankAccountDetailsType()
                {
                    key = "sortCode",
                    value = "536124"
                }
            };

            var beneficiaryBankAccount = new BeneficiaryBankAccountType()
            {
                countryCode = "GB",
                description = "UK Account - ABCD",

                bankAccountDetails = bankAccountDetailsSet,
                beneficiaryIdentity = new BeneficiaryIdentityType()
                {
                    beneficiaryIndividualIdentity = new BeneficiaryIndividualIdentityType()
                    {
                        name = new IndividualNameType()
                        {
                            givenNames = "Akshay",
                            familyName = "Luther"
                        }
                    },
                }
            };

            var validateBeneficiaryAccount = new ValidateBeneficiaryBankAccountType()
            {
                version = 3.0M,
                beneficiaryBankAccount = beneficiaryBankAccount
            };

            _serviceProxy.ValidateBeneficiaryBankAccount(validateBeneficiaryAccount);

            _beneficiaryBankAccount = beneficiaryBankAccount;
        }

        [Test]
        public void _40_AddBeneficiaryAccountTest()
        {
            var addBeneficiaryBankAccount = new AddBeneficiaryBankAccountType()
            {
                version = 3.0M,
                beneficiaryBankAccount = _beneficiaryBankAccount,
                merchantBankID = "Mickey Mouse " + Guid.NewGuid(),
                userID = new UserIDType()
                {
                    epUserID = _epUserID                    
                }
            };

            var addBeneficiaryBankAccountResponse = _serviceProxy.AddBeneficiaryBankAccount(addBeneficiaryBankAccount);

            Assert.That(addBeneficiaryBankAccountResponse.usersBankID.benBankID.epBankIDSpecified);

            _epBankID = addBeneficiaryBankAccountResponse.usersBankID.benBankID.epBankID;

            _usersBankID = addBeneficiaryBankAccountResponse.usersBankID;

            Assert.That(addBeneficiaryBankAccountResponse.usersBankID.benBankID.merchantBankID != null);

            _merchantBankID = addBeneficiaryBankAccountResponse.usersBankID.benBankID.merchantBankID;
        }

        [Test]
        public void _50_GetBeneficiaryBankAccountTest()
        {
            var getBeneficiaryBankAccount = new getBeneficiaryBankAccountType()
            {
                version = 3.0M,
                usersBankID = _usersBankID
            };

            var getBeneficiaryBankAccountsResponse = _serviceProxy.GetBeneficiaryBankAccount(getBeneficiaryBankAccount);

            Assert.That(getBeneficiaryBankAccountsResponse.beneficiaryBankAccountFieldGroupsList.Length > 1);
            Assert.That(getBeneficiaryBankAccountsResponse.countryCode.Equals("GB"));
            Assert.That(getBeneficiaryBankAccountsResponse.currencyCode.Equals("GBP"));
            Assert.That(getBeneficiaryBankAccountsResponse.description.Equals("Test"));
        }

        [Test]
        public void _55_ListBeneficiaryBankAccountsTest()
        {
            var listBeneficiaryBankAccounts = new ListBeneficiaryBankAccountsType()
            {
                version = 4.0M,
                pagination = new PaginationType(),
                userID = _usersBankID.userID
            };

            var listBeneficiaryBankAccountsResponse = _serviceProxy.ListBeneficiaryAccounts(listBeneficiaryBankAccounts);

            Assert.That(listBeneficiaryBankAccountsResponse.beneficiaryBankAccountSummary.Length > 0);
        }

        [Test]
        public void _60_GetFXQuoteTest()
        {
            var getFxQuote = new GetFXQuoteType()
            {
                version = 3.0M,
                buyCurrency = "GBP",
                sellAmount = new MonetaryValueType()
                {
                    currency = "AUD",
                    amount = 101
                },
                usersBankID = new UsersBankIDType()
                {
                    benBankID = new BenBankIDType()
                    {
                        epBankID = _epBankID,
                        epBankIDSpecified = true,
                        merchantBankID = _merchantBankID
                    },
                    userID = new UserIDType()
                    {
                        epUserID = _epUserID,
                        merchantUserID = _merchantUserID
                    }
                }
            };

            var getFXQuoteResponse = _serviceProxy.GetFXQuote(getFxQuote);

            Assert.That(getFXQuoteResponse.fxTicketID > 0);
        }

        [Test]
        public void _65_GetBalanceTest()
        {
            var getBalanceRequest = new GetBalanceType()
            {
                version = 2.0M,
                currency = "GBP"
            };

            var getBalanceResponse = _serviceProxy.GetBalance(getBalanceRequest);

            Assert.That(getBalanceResponse.balance.Length > 0);
        }  

        [Test]
        public void _70_PayoutRequestTest()
        {
            var payoutRequest = new PayoutRequestType()
            {
                version = 4.0M,
                beneficiaryStatementNarrative = "narrative",
                merchantTransactionReference = Guid.NewGuid().ToString(),
                payerType = PayerType.user,
                payerTypeSpecified = true,
                payoutRequestAmount = new MonetaryValueType()
                {
                    amount = 1000,
                    currency = "AUD"
                },
                serviceLevel = ServiceLevelType.standard,
                //The type of Payer making the payment authenticatedCaller - Payout is being requested on behalf of the requesting merchant
                    //managedMerchant - Payout is being requested on behalf of a managed merchant user - Payout is being requested on behalf of a user

                    payoutDetails = new PayoutDetailsType[]
                    {
                         new PayoutDetailsType()
                         {
                             key = "Payout 1",
                             value = "Test 1"
                         },
                         new PayoutDetailsType()
                         {
                             key = "Payout 2",
                             value = "Test 2"
                         }
                    },
                    payoutType = "Payout Type 1",
                usersBankID = new UsersBankIDType()
                {
                    benBankID = new BenBankIDType()
                    {
                        merchantBankID = _merchantBankID
                    },
                    userID = new UserIDType()
                    {
                        epUserID = _epUserID
                    }
                }
            };

            var payoutRequestResponse = _serviceProxy.PayoutRequest(payoutRequest);
            
            Assert.That(payoutRequestResponse.epTransactionID > 0);
            _paymentID = payoutRequestResponse.epTransactionID;
            Assert.That(payoutRequestResponse.merchantTransactionReference.Equals(payoutRequest.merchantTransactionReference));
        }

//        [Test]
//        public void _75_ValidateCreditTest()
//        {
//            var validateCreditRequest = new ValidateCreditType()
//            {
//                version = 1.1M,
//                paymentID = _paymentID, 
//                transactionAmount = new MonetaryValueType()
//                {
//                    amount = 1000,
//                    currency = "AUD"
//                },
//                userID = _usersBankID.userID
//            };
//
//            var validateCreditResponse = _serviceProxy.ValidateCredit(validateCreditRequest);
//
//            Assert.That(validateCreditResponse != null);
//        }

        [Test]
        public void _76_GetTransactionDetailTest()
        {
            var tranIdArray = new TransactionIDType[1];
            tranIdArray[0] = new TransactionIDType()
            {
                epTransactionID = _paymentID,
                epTransactionIDSpecified = true
            };

            var getTransactionDetailRequest = new GetTransactionDetailType()
            {
                version = 3.0M,
                transactionID = tranIdArray
                // Only needed when querying on behalf of managed merchant
                // managedMerchantIdentity = _usersBankID.userID.epUserID,
            };

            var getTransactionDetailResponse = _serviceProxy.GetTransactionDetail(getTransactionDetailRequest);
            Assert.That(getTransactionDetailResponse != null, "Invalid null response returned");

            int numTransactions = getTransactionDetailResponse.transactionDetailMappings.Length;
            Assert.That(numTransactions == 1, "Unexpected number of items returned. Excepted 1 received " + numTransactions);
        }


        [Test]
        public void _77_GetStatement()
        {
            GetStatementType getStatementRequest = new GetStatementType()
            {
                version = 3.0M,
                startDateTime = _transactionTimestamp,
                endDateTime = _transactionTimestamp,
                currency = "AUD"
            };
            GetStatementResponseType statementResponse = _serviceProxy.GetStatement(getStatementRequest);
            Assert.That(statementResponse != null, "The statement response is null");
            int numTrans = statementResponse.statementLineItemsList.Length;
            Assert.That(numTrans >= 1, "Expected at least one transaction, received " + numTrans);
        }

        [Test]
        public void _78_SearchTransactions()
        {
            DateTime endDateTime = DateTime.Now;
            DateTime startDateTime = endDateTime.AddHours(-1); // BST vs GMT
            startDateTime = startDateTime.AddMinutes(-5);

            SearchSortFieldsType[] sortFields = new SearchSortFieldsType[1];
            sortFields[0] = new SearchSortFieldsType()
            {
                sortField = SearchSortValueType.Timestamp
            };

            SearchTransactionsCriteriaType searchCriteria = new SearchTransactionsCriteriaType()
            {
                epTransactionID = _paymentID,
                epTransactionIDSpecified = true,
                periodStartDateTime = startDateTime,
                periodEndDateTime = endDateTime,
                sortCriteria = new SortCriteriaType()
                {
                    sortOrder = SortOrderType.ASC,
                    sortFields = sortFields
                }
            };

            SearchTransactionsType searchTransactionRequest = new SearchTransactionsType()
            {
                version = 3.0M,
                searchTransactionsCriteria = searchCriteria,
                pagination = new PaginationType()
                {
                    offset = 0,
                    pageSize = 1
                }
            };

            SearchTransactionsResponseType searchTransactionResponse = _serviceProxy.SearchTransactions(searchTransactionRequest);
            Assert.That(searchTransactionResponse != null, "Response is null");
            Assert.That(searchTransactionResponse.financialTransactions.Length == 1, "Expected one transaction actual: " + searchTransactionResponse.financialTransactions.Length);
            Assert.That(searchTransactionResponse.financialTransactions[0].epTransactionID == _paymentID, "Expected payment id : " + _paymentID + " but was " + searchTransactionResponse.financialTransactions[0].epTransactionID);
        }
        

        [Test]
        public void _80_DeleteBeneficiaryBankAccountTest()
        {
            var deleteBeneficiaryBankAccountRequest = new DeleteBeneficiaryBankAccountType()
            {
                version = 3.0M,
                usersBankID = new UsersBankIDType()
                {
                    benBankID = new BenBankIDType()
                    {
                        epBankID = _epBankID,
                        epBankIDSpecified = true
                    },
                    userID = new UserIDType()
                    {
                        epUserID = _epUserID
                    }
                }
            };

            var deleteBeneficiaryBankAccountResponse = _serviceProxy.DeleteBeneficiaryBankAccount(deleteBeneficiaryBankAccountRequest);

            Assert.That(deleteBeneficiaryBankAccountResponse.isDeleted);
        }

        [Test]
        public void _90_DisableUserTest()
        {
            var disableUserRequest = new DisableUserType()
            {
                version = 2.0M,
                userID = _usersBankID.userID
            };

            var disableUserResponse = _serviceProxy.DisableUser(disableUserRequest);
            Assert.That(disableUserResponse != null);
        }

        ServiceProxy _serviceProxy;
        string _epUserID;
        string _merchantUserID;
        BeneficiaryBankAccountType _beneficiaryBankAccount;
        long _epBankID;
        string _merchantBankID;
        UsersBankIDType _usersBankID;
        long _paymentID;
        DateTime _transactionTimestamp;
    }
}
