using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Schema;

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
            EchoType echoRequest = new EchoType();
            echoRequest.version = 1.0M;
            echoRequest.echoRequest = "ping";

            EchoResponseType echoRequestResponse = _serviceProxy.Echo(echoRequest);

            Assert.That(echoRequestResponse.echoResponse.Equals("ping"));
        }

        [Test]
        public void _10_CreateUserTest()
        {
            CreateUserType createUser = new CreateUserType();
            createUser.version = 3.0M;
            createUser.accountCurrency = "GBP";
            createUser.merchantUserIdentity = "Donald Duck " + Guid.NewGuid().ToString();
            createUser.payerIdentity = new PayerIdentityType();
            createUser.payerIdentity.payerIndividualIdentity = new PayerIndividualIdentityType();
            createUser.payerIdentity.payerIndividualIdentity.name = new IndividualNameType();
            createUser.payerIdentity.payerIndividualIdentity.name.givenNames = "Joe";
            createUser.payerIdentity.payerIndividualIdentity.name.familyName = "Doe";
            createUser.payerIdentity.payerIndividualIdentity.address = new AddressType();
            createUser.payerIdentity.payerIndividualIdentity.address.addressLine1 = "21 New Street";
            createUser.payerIdentity.payerIndividualIdentity.address.addressLine2 = "21 New Street 2";
            createUser.payerIdentity.payerIndividualIdentity.address.addressLine3 = "21 New Street 3";
            createUser.payerIdentity.payerIndividualIdentity.address.city = "London";
            createUser.payerIdentity.payerIndividualIdentity.address.postcode = "EC2M 4TP";
            createUser.payerIdentity.payerIndividualIdentity.address.province = "London";
            createUser.payerIdentity.payerIndividualIdentity.address.country = "GB";
            createUser.payerIdentity.payerIndividualIdentity.birthInformation = new BirthInformationType();
            createUser.payerIdentity.payerIndividualIdentity.birthInformation.cityOfBirth = "London";
            createUser.payerIdentity.payerIndividualIdentity.birthInformation.countryOfBirth = "GB";
            createUser.payerIdentity.payerIndividualIdentity.birthInformation.dateOfBirth = new DateTime(1985, 5, 20);
                

            CreateUserResponseType createUserResponse = _serviceProxy.CreateUser(createUser);

            Assert.That(createUserResponse.userID.epUserID != "");

            _epUserID = createUserResponse.userID.epUserID;
            _merchantUserID = createUserResponse.userID.merchantUserID;
        }

        [ExpectedException(typeof(System.Xml.Schema.XmlSchemaValidationException))]
        [Test]
        public void _11_CreateAccountTest_FailValidation()
        {
            CreateUserType createUser= new CreateUserType();
            createUser.version = 3.0M;
            createUser.accountCurrency = "GB";
            createUser.merchantUserIdentity = "Donald Duck " + Guid.NewGuid().ToString();
            createUser.payerIdentity = new PayerIdentityType();
            createUser.payerIdentity.payerIndividualIdentity = new PayerIndividualIdentityType();
            createUser.payerIdentity.payerIndividualIdentity.name = new IndividualNameType();
            createUser.payerIdentity.payerIndividualIdentity.name.givenNames = "Joe";
            createUser.payerIdentity.payerIndividualIdentity.name.familyName = "Doe";
            createUser.payerIdentity.payerIndividualIdentity.address = new AddressType();
            createUser.payerIdentity.payerIndividualIdentity.address.addressLine1 = "21 New Street";
            createUser.payerIdentity.payerIndividualIdentity.address.addressLine2 = "21 New Street 2";
            createUser.payerIdentity.payerIndividualIdentity.address.addressLine3 = "21 New Street 3";
            createUser.payerIdentity.payerIndividualIdentity.address.city = "London";
            createUser.payerIdentity.payerIndividualIdentity.address.postcode = "EC2M 4TP";
            createUser.payerIdentity.payerIndividualIdentity.address.province = "London";
            createUser.payerIdentity.payerIndividualIdentity.address.country = "GB";
            createUser.payerIdentity.payerIndividualIdentity.birthInformation = new BirthInformationType();
            createUser.payerIdentity.payerIndividualIdentity.birthInformation.cityOfBirth = "London";
            createUser.payerIdentity.payerIndividualIdentity.birthInformation.countryOfBirth = "GB";
            createUser.payerIdentity.payerIndividualIdentity.birthInformation.dateOfBirth = new DateTime(1985, 5, 20);
                

            CreateUserResponseType createUserResponse = _serviceProxy.CreateUser(createUser);
        }
        [Test]
        public void _12_CreateOrUpdateUserTest()
        {
            CreateOrUpdateUserType CreateOrUpdateUser = new CreateOrUpdateUserType();
            CreateOrUpdateUser.version = 1.0M;
            CreateOrUpdateUser.accountCurrency = "GBP";
            CreateOrUpdateUser.merchantUserIdentity = "Donald Duck " + Guid.NewGuid().ToString();
            CreateOrUpdateUser.payerIdentity = new PayerIdentityType();
            CreateOrUpdateUser.payerIdentity.payerIndividualIdentity = new PayerIndividualIdentityType();
            CreateOrUpdateUser.payerIdentity.payerIndividualIdentity.name = new IndividualNameType();
            CreateOrUpdateUser.payerIdentity.payerIndividualIdentity.name.givenNames = "Joe";
            CreateOrUpdateUser.payerIdentity.payerIndividualIdentity.name.familyName = "Doe";
            CreateOrUpdateUser.payerIdentity.payerIndividualIdentity.address = new AddressType();
            CreateOrUpdateUser.payerIdentity.payerIndividualIdentity.address.addressLine1 = "21 New Street";
            CreateOrUpdateUser.payerIdentity.payerIndividualIdentity.address.addressLine2 = "21 New Street 2";
            CreateOrUpdateUser.payerIdentity.payerIndividualIdentity.address.addressLine3 = "21 New Street 3";
            CreateOrUpdateUser.payerIdentity.payerIndividualIdentity.address.city = "London";
            CreateOrUpdateUser.payerIdentity.payerIndividualIdentity.address.postcode = "EC2M 4TP";
            CreateOrUpdateUser.payerIdentity.payerIndividualIdentity.address.province = "London";
            CreateOrUpdateUser.payerIdentity.payerIndividualIdentity.address.country = "GB";
            CreateOrUpdateUser.payerIdentity.payerIndividualIdentity.birthInformation = new BirthInformationType();
            CreateOrUpdateUser.payerIdentity.payerIndividualIdentity.birthInformation.cityOfBirth = "London";
            CreateOrUpdateUser.payerIdentity.payerIndividualIdentity.birthInformation.countryOfBirth = "GB";
            CreateOrUpdateUser.payerIdentity.payerIndividualIdentity.birthInformation.dateOfBirth = new DateTime(1985, 5, 20);


            CreateOrUpdateUserResponseType CreateOrUpdateUserResponse = _serviceProxy.CreateOrUpdateUser(CreateOrUpdateUser);

            Assert.That(CreateOrUpdateUserResponse.userID.epUserID != "");

            _epUserID = CreateOrUpdateUserResponse.userID.epUserID;
            _merchantUserID = CreateOrUpdateUserResponse.userID.merchantUserID;
        }

        [Test]
        public void _19_GetBeneficiaryBankAccountRequiredData()
        {
            GetBeneficiaryBankAccountRequiredDataType getBeneficiaryBankAccountRequiredData = new GetBeneficiaryBankAccountRequiredDataType();
            getBeneficiaryBankAccountRequiredData.version = 3.0M;
            getBeneficiaryBankAccountRequiredData.countryCode = "GB";
            getBeneficiaryBankAccountRequiredData.currencyCode = "GBP";

            GetBeneficiaryBankAccountRequiredDataResponseType getBeneficiaryBankAccountRequiredDataResponse = _serviceProxy.GetBeneficiaryBankAccountRequiredData(getBeneficiaryBankAccountRequiredData);

            Assert.That(getBeneficiaryBankAccountRequiredDataResponse.beneficiaryBankAccountFieldGroupsList.Length > 0);

        }

        [Test]
        [ExpectedException(typeof(ServiceErrorException))]
        public void _20_ValidateBeneficiaryAccountTest_Failure()
        {
            BankAccountDetailsType badt0 = new BankAccountDetailsType();
            badt0.key = "description";
            badt0.value = "Test";

            BankAccountDetailsType badt1 = new BankAccountDetailsType();
            badt1.key = "bankName";
            badt1.value = "Test";

            BankAccountDetailsType badt2 = new BankAccountDetailsType();
            badt2.key = "accountName";
            badt2.value = "Test";

            BankAccountDetailsType badt3 = new BankAccountDetailsType();
            badt3.key = "accountNumber";
            badt3.value = "03870448";

            BankAccountDetailsType badt4 = new BankAccountDetailsType();
            badt4.key = "sortCode";
            badt4.value = "536125";

            BankAccountDetailsType[] bankAccountDetailsSet = new BankAccountDetailsType[] { badt0, badt1, badt2, badt3, badt4 };

            BeneficiaryBankAccountType beneficiaryBankAccount = new BeneficiaryBankAccountType();
            beneficiaryBankAccount.countryCode = "GB";
            beneficiaryBankAccount.description = "UK Account - ABCD";
            beneficiaryBankAccount.bankAccountDetails = bankAccountDetailsSet;
            beneficiaryBankAccount.beneficiaryIdentity = new BeneficiaryIdentityType();
            beneficiaryBankAccount.beneficiaryIdentity.beneficiaryIndividualIdentity = new BeneficiaryIndividualIdentityType();
            beneficiaryBankAccount.beneficiaryIdentity.beneficiaryIndividualIdentity.name = new IndividualNameType();
            beneficiaryBankAccount.beneficiaryIdentity.beneficiaryIndividualIdentity.name.givenNames = "Akshay";
            beneficiaryBankAccount.beneficiaryIdentity.beneficiaryIndividualIdentity.name.familyName = "Luther";

            ValidateBeneficiaryBankAccountType validateBeneficiaryBankAccount = new ValidateBeneficiaryBankAccountType();
            validateBeneficiaryBankAccount.version = 3.0M;
            validateBeneficiaryBankAccount.beneficiaryBankAccount = beneficiaryBankAccount;

            try
            {
                _serviceProxy.ValidateBeneficiaryBankAccount(validateBeneficiaryBankAccount);
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
            BankAccountDetailsType badt0 = new BankAccountDetailsType();
            badt0.key = "description";
            badt0.value = "Test";

            BankAccountDetailsType badt1 = new BankAccountDetailsType();
            badt1.key = "bankName";
            badt1.value = "Test";

            BankAccountDetailsType badt2 = new BankAccountDetailsType();
            badt2.key = "accountName";
            badt2.value = "Test";

            BankAccountDetailsType badt3 = new BankAccountDetailsType();
            badt3.key = "accountNumber";
            badt3.value = "03870448";

            BankAccountDetailsType badt4 = new BankAccountDetailsType();
            badt4.key = "sortCode";
            badt4.value = "536124";

            BankAccountDetailsType[] bankAccountDetailsSet = new BankAccountDetailsType[] { badt0, badt1, badt2, badt3, badt4 };

            BeneficiaryBankAccountType beneficiaryBankAccount = new BeneficiaryBankAccountType();
            beneficiaryBankAccount.countryCode = "GB";
            beneficiaryBankAccount.description = "UK Account - ABCD";
            beneficiaryBankAccount.bankAccountDetails = bankAccountDetailsSet;
            beneficiaryBankAccount.beneficiaryIdentity = new BeneficiaryIdentityType();
            beneficiaryBankAccount.beneficiaryIdentity.beneficiaryIndividualIdentity = new BeneficiaryIndividualIdentityType();
            beneficiaryBankAccount.beneficiaryIdentity.beneficiaryIndividualIdentity.name = new IndividualNameType();
            beneficiaryBankAccount.beneficiaryIdentity.beneficiaryIndividualIdentity.name.givenNames = "Akshay";
            beneficiaryBankAccount.beneficiaryIdentity.beneficiaryIndividualIdentity.name.familyName = "Luther";

            ValidateBeneficiaryBankAccountType validateBeneficiaryBankAccount = new ValidateBeneficiaryBankAccountType();
            validateBeneficiaryBankAccount.version = 3.0M;
            validateBeneficiaryBankAccount.beneficiaryBankAccount = beneficiaryBankAccount;

            _serviceProxy.ValidateBeneficiaryBankAccount(validateBeneficiaryBankAccount);

            _beneficiaryBankAccount = beneficiaryBankAccount;
        }

        [Test]
        public void _40_AddBeneficiaryAccountTest()
        {
            AddBeneficiaryBankAccountType addBeneficiaryBankAccount = new AddBeneficiaryBankAccountType();
            addBeneficiaryBankAccount.version = 3.0M;
            addBeneficiaryBankAccount.merchantBankID = "Mickey Mouse" + Guid.NewGuid().ToString();
            addBeneficiaryBankAccount.beneficiaryBankAccount = _beneficiaryBankAccount;
            addBeneficiaryBankAccount.userID = new UserIDType();
            addBeneficiaryBankAccount.userID.epUserID = _epUserID;

            AddBeneficiaryBankAccountResponseType addBeneficiaryBankAccountResponse = _serviceProxy.AddBeneficiaryBankAccount(addBeneficiaryBankAccount);

            Assert.That(addBeneficiaryBankAccountResponse.usersBankID.benBankID.epBankIDSpecified);

            _epBankID = addBeneficiaryBankAccountResponse.usersBankID.benBankID.epBankID;
            _merchantBankID = addBeneficiaryBankAccountResponse.usersBankID.benBankID.merchantBankID;
            _usersBankID = addBeneficiaryBankAccountResponse.usersBankID;
        }

        [Test]
        public void _50_GetBeneficiaryBankAccountTest()
        {
            getBeneficiaryBankAccountType getBeneficiaryBankAccount = new getBeneficiaryBankAccountType();
            getBeneficiaryBankAccount.version = 3.0M;
            getBeneficiaryBankAccount.usersBankID = _usersBankID;

            getBeneficiaryBankAccountResponseType getBeneficiaryBankAccountsResponse = _serviceProxy.GetBeneficiaryBankAccount(getBeneficiaryBankAccount);

            Assert.That(getBeneficiaryBankAccountsResponse.beneficiaryBankAccountFieldGroupsList.Length > 1);
            Assert.That(getBeneficiaryBankAccountsResponse.countryCode.Equals("GB"));
            Assert.That(getBeneficiaryBankAccountsResponse.currencyCode.Equals("GBP"));
            Assert.That(getBeneficiaryBankAccountsResponse.description.Equals("Test"));
        }

        [Test]
        public void _55_ListBeneficiaryBankAccountsTest()
        {
            ListBeneficiaryBankAccountsType listBeneficiaryBankAccounts = new ListBeneficiaryBankAccountsType();
            listBeneficiaryBankAccounts.version = 4.0M;
            listBeneficiaryBankAccounts.userID = new UserIDType();
            listBeneficiaryBankAccounts.pagination = new PaginationType();
            listBeneficiaryBankAccounts.userID.merchantUserID = _merchantUserID;
            ListBeneficiaryBankAccountsResponseType listBeneficiaryBankAccountsResponse = _serviceProxy.ListBeneficiaryBankAccounts(listBeneficiaryBankAccounts);

            Assert.That(listBeneficiaryBankAccountsResponse.beneficiaryBankAccountSummary.Length == 1);
            Assert.That(listBeneficiaryBankAccountsResponse.userID.merchantUserID == listBeneficiaryBankAccounts.userID.merchantUserID);
        }

        [Test]
        public void _60_GetFXQuoteTest()
        {
            GetFXQuoteType getFXQuote = new GetFXQuoteType();
            getFXQuote.version = 3.0M;
            getFXQuote.buyCurrency = "GBP";
            getFXQuote.sellAmount = new MonetaryValueType();
            getFXQuote.sellAmount.amount = 101;
            getFXQuote.sellAmount.currency = "AUD";
            getFXQuote.usersBankID = new UsersBankIDType();
            getFXQuote.usersBankID.benBankID = new BenBankIDType();
            getFXQuote.usersBankID.benBankID.epBankID = _epBankID;
            getFXQuote.usersBankID.benBankID.epBankIDSpecified = true;
            getFXQuote.usersBankID.userID = new UserIDType();
            getFXQuote.usersBankID.userID.epUserID = _epUserID;

            GetFXQuoteResponseType getFXQuoteResponse = _serviceProxy.GetFXQuote(getFXQuote);

            Assert.That(getFXQuoteResponse.fxTicketID > 0);
        }

        [Test]
        public void _65_GetBalanceTest()
        {
            GetBalanceType getBalanceRequest = new GetBalanceType();
            getBalanceRequest.version = 2.0M;
            getBalanceRequest.currency = "GBP";

            GetBalanceResponseType getBalanceResponse = _serviceProxy.GetBalance(getBalanceRequest);

            Assert.That(getBalanceResponse.balance.Length > 0);
        }

        [Test]
        public void _70_PayoutRequestTest()
        {
            PayoutRequestType payoutRequest = new PayoutRequestType();
            payoutRequest.version = 4.0M;
            payoutRequest.beneficiaryStatementNarrative = "narrative";
            payoutRequest.merchantTransactionReference = Guid.NewGuid().ToString();
            payoutRequest.payerType = PayerType.user;
            payoutRequest.payerTypeSpecified = true;
            PayoutDetailsType payoutDetails0 = new PayoutDetailsType();
            payoutDetails0.key = "Payout 1";
            payoutDetails0.value = "Test 1";
            PayoutDetailsType payoutDetails1 = new PayoutDetailsType();
            payoutDetails1.key = "Payout 1";
            payoutDetails1.value = "Test 1";
            PayoutDetailsType[] payoutDetail = new PayoutDetailsType[] { payoutDetails0, payoutDetails1 };
            payoutRequest.payoutDetails = payoutDetail;
                
            payoutRequest.usersBankID = new UsersBankIDType();
            payoutRequest.usersBankID.benBankID = new BenBankIDType();
            payoutRequest.usersBankID.benBankID.epBankID = _epBankID;
            payoutRequest.usersBankID.benBankID.epBankIDSpecified = true;
            payoutRequest.usersBankID.benBankID.merchantBankID = _merchantBankID;
            payoutRequest.usersBankID.userID = new UserIDType();
            payoutRequest.usersBankID.userID.epUserID = _epUserID;
            payoutRequest.usersBankID.userID.merchantUserID = _merchantUserID;
            payoutRequest.payoutRequestAmount = new MonetaryValueType();
            payoutRequest.payoutRequestAmount.amount = 1000;
            payoutRequest.payoutRequestAmount.currency = "AUD";
            payoutRequest.serviceLevel = ServiceLevelType.standard;

            PayoutRequestResponseType payoutRequestResponse = _serviceProxy.PayoutRequest(payoutRequest);
            // Commented by BNK
            Assert.That(payoutRequestResponse.epTransactionID > 0);
           //Commented by BNK
            _paymentID = payoutRequestResponse.epTransactionID;
            Assert.That(payoutRequestResponse.merchantTransactionReference == payoutRequest.merchantTransactionReference);
        }

//        [Test]
//        public void _75_ValidateCreditTest()
//        {
//            ValidateCreditType validateCreditRequest = new ValidateCreditType();
//            validateCreditRequest.version = 1.1M;
//            validateCreditRequest.paymentID = _paymentID;
//            validateCreditRequest.transactionAmount = new MonetaryValueType();
//            validateCreditRequest.transactionAmount.amount = 1000;
//            validateCreditRequest.transactionAmount.currency = "AUD";
//            validateCreditRequest.userID = _usersBankID.userID;
//
//            ValidateCreditResponseType validateCreditResponse = _serviceProxy.ValidateCredit(validateCreditRequest);
//
//            Assert.That(validateCreditResponse != null);
//        }

        [Test]
        public void _76_GetTransactionDetail()
        {
            GetTransactionDetailType getTransactionDetail = new GetTransactionDetailType();
            getTransactionDetail.version = 3.0M;

            TransactionIDType transactionId = new TransactionIDType();
            transactionId.epTransactionID = _paymentID;
            transactionId.epTransactionIDSpecified = true;

            TransactionIDType[] listOfIds = new TransactionIDType[1];
            listOfIds[0] = transactionId;

            getTransactionDetail.transactionID = listOfIds;

            GetTransactionDetailResponseType getTransactionDetailResponse = _serviceProxy.GetTransactionDetail(getTransactionDetail);
            Assert.That(getTransactionDetailResponse != null);

            _transactionTimestamp = getTransactionDetailResponse.transactionDetailMappings[0].transactionDetail.timestamp;
            Assert.That(_transactionTimestamp != null);
        }

        [Test]
        public void _77_GetStatement()
        {
            GetStatementType getStatementRequest = new GetStatementType();
            getStatementRequest.version = 3.0M;
            getStatementRequest.startDateTime = _transactionTimestamp;
            getStatementRequest.endDateTime = _transactionTimestamp;
            getStatementRequest.currency= "AUD";
            GetStatementResponseType statementResponse = _serviceProxy.GetStatement(getStatementRequest);
            Assert.That(statementResponse != null, "The statement response is null");
            int numTrans = statementResponse.statementLineItemsList.Length;
            Assert.That(numTrans >= 1, "Expected at least one transaction, received " + numTrans);
        }


        [Test]
        public void _78_SearchTransactions()
        {
            Assert.That(_paymentID > 0, "Cannot continue test as payment id to search for is : " + _paymentID);

            SearchTransactionsType searchTransactionRequest = new SearchTransactionsType();
            searchTransactionRequest.version = 3.0M;
            
            SearchSortFieldsType[] sortFields = new SearchSortFieldsType[1];
            sortFields[0] = new SearchSortFieldsType();
            sortFields[0].sortField = SearchSortValueType.Timestamp;

            SearchTransactionsCriteriaType searchCriteria = new SearchTransactionsCriteriaType();
            searchTransactionRequest.searchTransactionsCriteria = searchCriteria;
            searchCriteria.epTransactionID = _paymentID;
            searchCriteria.epTransactionIDSpecified = true;

            DateTime endDateTime = DateTime.Now;
            DateTime startDateTime = endDateTime.AddHours(-1); // BST vs GMT
            startDateTime = startDateTime.AddMinutes(-5);

            searchCriteria.periodStartDateTime = startDateTime;
            searchCriteria.periodEndDateTime = endDateTime;

            searchCriteria.sortCriteria = new SortCriteriaType();
            searchCriteria.sortCriteria.sortOrder = SortOrderType.ASC;
            searchCriteria.sortCriteria.sortFields = sortFields;

            searchTransactionRequest.pagination = new PaginationType();
            searchTransactionRequest.pagination.offset = 0;
            searchTransactionRequest.pagination.pageSize = 1;

            SearchTransactionsResponseType searchTransactionResponse = _serviceProxy.SearchTransactions(searchTransactionRequest);
            Assert.That(searchTransactionResponse != null, "Response is null" );
            Assert.That(searchTransactionResponse.financialTransactions.Length == 1, "Expected one transaction actual: " + searchTransactionResponse.financialTransactions.Length);
            Assert.That(searchTransactionResponse.financialTransactions[0].epTransactionID == _paymentID, "Expected payment id : " + _paymentID + " but was " + searchTransactionResponse.financialTransactions[0].epTransactionID);
        }

        [Test]
        public void _80_DeleteBeneficiaryBankAccountTest()
        {
            DeleteBeneficiaryBankAccountType deleteBeneficiaryBankAccount = new DeleteBeneficiaryBankAccountType();
            deleteBeneficiaryBankAccount.version = 3.0M;
            deleteBeneficiaryBankAccount.usersBankID = new UsersBankIDType();
            deleteBeneficiaryBankAccount.usersBankID.userID = new UserIDType();
            deleteBeneficiaryBankAccount.usersBankID.userID.merchantUserID = _merchantUserID;
            deleteBeneficiaryBankAccount.usersBankID.benBankID = new BenBankIDType();
            deleteBeneficiaryBankAccount.usersBankID.benBankID.merchantBankID = _merchantBankID;

            DeleteBeneficiaryBankAccountResponseType deleteBeneficiaryBankAccountResponse = _serviceProxy.DeleteBeneficiaryBankAccount(deleteBeneficiaryBankAccount);
            Assert.That(deleteBeneficiaryBankAccountResponse.isDeleted);
        }

        [Test]
        public void _90_DisableUserTest()
        {
            DisableUserType disableUserRequest = new DisableUserType();
            disableUserRequest.version = 2.0M;
            disableUserRequest.userID = _usersBankID.userID;

            DisableUserResponseType disableUserResponse = _serviceProxy.DisableUser(disableUserRequest);
            Assert.That(disableUserResponse != null);
        }

        ServiceProxy _serviceProxy;
        string _epUserID;
        string _merchantUserID;
        BeneficiaryBankAccountType _beneficiaryBankAccount;
        long _epBankID;
        long _paymentID;
        string _merchantBankID;
        UsersBankIDType _usersBankID;
        DateTime _transactionTimestamp;
    }
}
