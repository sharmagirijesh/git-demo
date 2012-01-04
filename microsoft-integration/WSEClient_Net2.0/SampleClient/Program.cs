using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Schema;
using System.Reflection;
using System.Web.Services.Protocols;
using System.Net;

using Earthport.MerchantAPI;

namespace SampleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("WSE Client\n----------\n");

                ServiceProxy serviceProxy = new ServiceProxy("https://sandbox.earthport.com:11023/MerchantAPIUAT2", "EarthportPaymentTest", "Passw0rd");

                // create account

                Console.WriteLine("CreateUser ...");

                CreateUserType createUser = new CreateUserType();
                createUser.version = 3.0M;
                createUser.accountCurrency = "GBP";
                createUser.merchantUserIdentity = "Donald Duck " + Guid.NewGuid().ToString();
                //Payer Identity
                //Represents the payer identity of an individual or company.
                //You must specify one of either a payer individual  identity or payer company identity.
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
                CreateUserResponseType createUserResponse = serviceProxy.CreateUser(createUser);
                Console.WriteLine("\tUserID.epUserID={0}", createUserResponse.userID.epUserID);
                Console.WriteLine("\tUserID.merchantUserID={0}", createUserResponse.userID.merchantUserID);


                Console.WriteLine("Create Or Update User ...");

                CreateOrUpdateUserType createOrUpdateUser = new CreateOrUpdateUserType();
                createOrUpdateUser.version = 1.0M;
                createOrUpdateUser.accountCurrency = "GBP";
                createOrUpdateUser.merchantUserIdentity = "Donald Duck " + Guid.NewGuid().ToString();
                //Payer Identity
                //Represents the payer identity of an individual or company.
                //You must specify one of either a payer individual  identity or payer company identity.
                createOrUpdateUser.payerIdentity = new PayerIdentityType();
                createOrUpdateUser.payerIdentity.payerIndividualIdentity = new PayerIndividualIdentityType();
                createOrUpdateUser.payerIdentity.payerIndividualIdentity.name = new IndividualNameType();
                createOrUpdateUser.payerIdentity.payerIndividualIdentity.name.givenNames = "Joe";
                createOrUpdateUser.payerIdentity.payerIndividualIdentity.name.familyName = "Doe";
                createOrUpdateUser.payerIdentity.payerIndividualIdentity.address = new AddressType();
                createOrUpdateUser.payerIdentity.payerIndividualIdentity.address.addressLine1 = "21 New Street";
                createOrUpdateUser.payerIdentity.payerIndividualIdentity.address.addressLine2 = "21 New Street 2";
                createOrUpdateUser.payerIdentity.payerIndividualIdentity.address.addressLine3 = "21 New Street 3";
                createOrUpdateUser.payerIdentity.payerIndividualIdentity.address.city = "London";
                createOrUpdateUser.payerIdentity.payerIndividualIdentity.address.postcode = "EC2M 4TP";
                createOrUpdateUser.payerIdentity.payerIndividualIdentity.address.province = "London";
                createOrUpdateUser.payerIdentity.payerIndividualIdentity.address.country = "GB";
                createOrUpdateUser.payerIdentity.payerIndividualIdentity.birthInformation = new BirthInformationType();
                createOrUpdateUser.payerIdentity.payerIndividualIdentity.birthInformation.cityOfBirth = "London";
                createOrUpdateUser.payerIdentity.payerIndividualIdentity.birthInformation.countryOfBirth = "GB";
                createOrUpdateUser.payerIdentity.payerIndividualIdentity.birthInformation.dateOfBirth = new DateTime(1985, 5, 20);
                CreateOrUpdateUserResponseType createOrUpdateUserResponse = serviceProxy.CreateOrUpdateUser(createOrUpdateUser);
                Console.WriteLine("\tUserID.epUserID={0}", createOrUpdateUserResponse.userID.epUserID);
                Console.WriteLine("\tUserID.merchantUserID={0}", createOrUpdateUserResponse.userID.merchantUserID);
                // construct beneficiary bank account

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

                GetBeneficiaryBankAccountRequiredDataType getBeneficiaryAccountRequiredData = new GetBeneficiaryBankAccountRequiredDataType();
                getBeneficiaryAccountRequiredData.version = 3.0M;
                getBeneficiaryAccountRequiredData.serviceLevel = ServiceLevelType.standard;
                getBeneficiaryAccountRequiredData.countryCode = "GB";
                getBeneficiaryAccountRequiredData.currencyCode = "GBP";

                Console.WriteLine("GetBeneficiaryBankAccountRequiredData ...");
                GetBeneficiaryBankAccountRequiredDataResponseType beneficiaryAccountRequiredData = serviceProxy.GetBeneficiaryBankAccountRequiredData(getBeneficiaryAccountRequiredData);

                foreach (BeneficiaryBankAccountGroupType group in beneficiaryAccountRequiredData.beneficiaryBankAccountFieldGroupsList)
                {
                    if ("true".Equals(group.mandatory))
                    {
                        foreach (BeneficiaryBankAccountFieldType field in group.beneficiaryBankAccountFieldsList)
                        {
                            //Retrieve default value for current field parameter name from beneficiary bank account.
                            string defaultValue = null;
                            BankAccountDetailsType currentDetail = null;
                            foreach (BankAccountDetailsType detail in beneficiaryBankAccount.bankAccountDetails)
                            {
                                if (field.parameterName.Equals(detail.key))
                                {
                                    currentDetail = detail;
                                    defaultValue = detail.value;
                                }
                            }

                            Console.Write("\nPlease enter a value for {0}\n(hit <enter> to use default '{1}':\n>", field.parameterName, defaultValue);
                            string line = Console.ReadLine();

                            if (line == "")
                            {
                                Console.WriteLine("(using default)");
                            }
                            else
                            {
                                currentDetail.value = line;
                            }
                        }
                    }
                }              
                
                // validate beneficiary account

                Console.WriteLine("ValidateBeneficiaryBankAccount ...");

                ValidateBeneficiaryBankAccountType validateBeneficiaryAccount = new ValidateBeneficiaryBankAccountType();
                validateBeneficiaryAccount.version = 3.0M;
                validateBeneficiaryAccount.beneficiaryBankAccount = beneficiaryBankAccount;

                serviceProxy.ValidateBeneficiaryBankAccount(validateBeneficiaryAccount);

                // create beneficiary account

                AddBeneficiaryBankAccountType addBeneficiaryBankAccount = new AddBeneficiaryBankAccountType();
                addBeneficiaryBankAccount.version = 3.0M;
                addBeneficiaryBankAccount.beneficiaryBankAccount = beneficiaryBankAccount;
                addBeneficiaryBankAccount.userID = createUserResponse.userID;
                addBeneficiaryBankAccount.merchantBankID = Guid.NewGuid().ToString();

                Console.WriteLine("AddBeneficiaryBankAccount ...");

                AddBeneficiaryBankAccountResponseType addBeneficiaryBankAccountResponse = serviceProxy.AddBeneficiaryBankAccount(addBeneficiaryBankAccount);
                
                Console.WriteLine("\tusersBankID.userID.epUserID={0}", addBeneficiaryBankAccountResponse.usersBankID.userID.epUserID);
                Console.WriteLine("\tusersBankID.userID.merchantUserID={0}", addBeneficiaryBankAccountResponse.usersBankID.userID.merchantUserID);
                Console.WriteLine("\tusersBankID.benBankID.epBankID={0}", addBeneficiaryBankAccountResponse.usersBankID.benBankID.epBankID);
                Console.WriteLine("\tusersBankID.benBankID.merchantBankID={0}", addBeneficiaryBankAccountResponse.usersBankID.benBankID.merchantBankID);

                // get beneficiary bank account

                Console.WriteLine("Get Beneficiary Bank Account ...");
                getBeneficiaryBankAccountType getBeneficiaryBankAccount = new getBeneficiaryBankAccountType();
                getBeneficiaryBankAccount.version = 3.0M;
                getBeneficiaryBankAccount.usersBankID = addBeneficiaryBankAccountResponse.usersBankID;

                getBeneficiaryBankAccountResponseType getBeneficiaryBankAccountResponse = serviceProxy.GetBeneficiaryBankAccount(getBeneficiaryBankAccount);

                Console.WriteLine("\tbeneficiaryIdentityID={0}", getBeneficiaryBankAccountResponse.beneficiaryIdentity);
                Console.WriteLine("\tdescription={0}", getBeneficiaryBankAccountResponse.description);
                Console.WriteLine("\tcurrencyCode={0}", getBeneficiaryBankAccountResponse.currencyCode);
                Console.WriteLine("\tcountryCode={0}", getBeneficiaryBankAccountResponse.countryCode);
                Console.WriteLine("\tUsersBankID.UserID.epUserID={0}", getBeneficiaryBankAccountResponse.usersBankID.userID.epUserID);
                Console.WriteLine("\tUsersBankID.UserID.merchantUserID={0}", getBeneficiaryBankAccountResponse.usersBankID.userID.merchantUserID);
                Console.WriteLine("\tUsersBankID.BenBankID.epBankID={0}", getBeneficiaryBankAccountResponse.usersBankID.benBankID.epBankID);
                Console.WriteLine("\tUsersBankID.BenBankID.merchantBankID={0}", getBeneficiaryBankAccountResponse.usersBankID.benBankID.merchantBankID);

                int groupIndex = 0;
                foreach (BeneficiaryBankAccountGroupType group in getBeneficiaryBankAccountResponse.beneficiaryBankAccountFieldGroupsList)
                {
                    Console.WriteLine("\tbeneficiaryBankAccountFieldGroupsList.beneficiaryBankAccountGroup[" + groupIndex + "].groupLabel={0}", group.groupLabel);
                    Console.WriteLine("\tbeneficiaryBankAccountFieldGroupsList.beneficiaryBankAccountGroup[" + groupIndex + "].mandatory={0}", group.mandatory);

                    int fieldIndex = 0;
                    foreach (BeneficiaryBankAccountFieldType field in group.beneficiaryBankAccountFieldsList)
                    {
                        Console.WriteLine("\tbeneficiaryBankAccountFieldGroupsList.beneficiaryBankAccountGroup[" + groupIndex + "].beneficiaryBankAccountField[" + fieldIndex + "].description={0}", field.description);
                        Console.WriteLine("\tbeneficiaryBankAccountFieldGroupsList.beneficiaryBankAccountGroup[" + groupIndex + "].beneficiaryBankAccountField[" + fieldIndex + "].displaySize={0}", field.displaySize);
                        Console.WriteLine("\tbeneficiaryBankAccountFieldGroupsList.beneficiaryBankAccountGroup[" + groupIndex + "].beneficiaryBankAccountField[" + fieldIndex + "].inputType={0}", field.inputType);

                        //Iterate through list items if field is of type "list".
                        if ("list".Equals(field.inputType))
                        {
                            int listIndex = 0;
                            foreach (BeneficiaryBankAccountListItemType listItem in field.listItems)
                            {
                                Console.WriteLine("\tbeneficiaryBankAccountFieldGroupsList.beneficiaryBankAccountGroup[" + groupIndex + "].beneficiaryBankAccountField[" + fieldIndex + "].beneficiaryBankAccountListItem[" + listIndex + "].label={0}", listItem.label);
                                Console.WriteLine("\tbeneficiaryBankAccountFieldGroupsList.beneficiaryBankAccountGroup[" + groupIndex + "].beneficiaryBankAccountField[" + fieldIndex + "].beneficiaryBankAccountListItem[" + listIndex + "].value={0}", listItem.value);
                                listIndex++;
                            }
                        }

                        Console.WriteLine("\tbeneficiaryBankAccountFieldGroupsList.beneficiaryBankAccountGroup[" + groupIndex + "].beneficiaryBankAccountField[" + fieldIndex + "].locale={0}", field.locale);
                        Console.WriteLine("\tbeneficiaryBankAccountFieldGroupsList.beneficiaryBankAccountGroup[" + groupIndex + "].beneficiaryBankAccountField[" + fieldIndex + "].maxSize={0}", field.maxSize);
                        Console.WriteLine("\tbeneficiaryBankAccountFieldGroupsList.beneficiaryBankAccountGroup[" + groupIndex + "].beneficiaryBankAccountField[" + fieldIndex + "].parameterName={0}", field.parameterName);
                        Console.WriteLine("\tbeneficiaryBankAccountFieldGroupsList.beneficiaryBankAccountGroup[" + groupIndex + "].beneficiaryBankAccountField[" + fieldIndex + "].separator={0}", field.separator);
                        Console.WriteLine("\tbeneficiaryBankAccountFieldGroupsList.beneficiaryBankAccountGroup[" + groupIndex + "].beneficiaryBankAccountField[" + fieldIndex + "].subTitle={0}", field.subTitle);
                        Console.WriteLine("\tbeneficiaryBankAccountFieldGroupsList.beneficiaryBankAccountGroup[" + groupIndex + "].beneficiaryBankAccountField[" + fieldIndex + "].tabOrder={0}", field.tabOrder);
                        Console.WriteLine("\tbeneficiaryBankAccountFieldGroupsList.beneficiaryBankAccountGroup[" + groupIndex + "].beneficiaryBankAccountField[" + fieldIndex + "].value={0}", field.value);
                        fieldIndex++;
                    }

                    groupIndex++;
                }

                //list beneficiary bank accounts
                ListBeneficiaryBankAccountsType listBeneficiaryBankAccounts = new ListBeneficiaryBankAccountsType();
                listBeneficiaryBankAccounts.version = 4.0M;
                PaginationType pagination = new PaginationType();
                pagination.offset = 0;
                pagination.pageSize = 10;
                listBeneficiaryBankAccounts.pagination = pagination;
                listBeneficiaryBankAccounts.userID = createUserResponse.userID;
                ListBeneficiaryBankAccountsResponseType listBeneficiaryBankAccountsResponse = serviceProxy.ListBeneficiaryBankAccounts(listBeneficiaryBankAccounts);

                foreach (BeneficiaryBankAccountSummaryType benBank in listBeneficiaryBankAccountsResponse.beneficiaryBankAccountSummary)
                {
                    Console.WriteLine("\n\tBeneficiaryBankAccount key={0}, description={1}", benBank.benBankID, benBank.description);
                }
                Console.WriteLine("\tuserID.epUserID={0}", listBeneficiaryBankAccountsResponse.userID.epUserID);
                Console.WriteLine("\tuserID.merchantUserID={0}", listBeneficiaryBankAccountsResponse.userID.merchantUserID);

                // get balance
                Console.WriteLine("GetBalance ...");

                GetBalanceType getBalanceRequest = new GetBalanceType();
                getBalanceRequest.version = 2.0M;
                getBalanceRequest.currency = "GBP";

                GetBalanceResponseType getBalanceResponse = serviceProxy.GetBalance(getBalanceRequest);

                Console.WriteLine("\tbalance[0].currency={0}", getBalanceResponse.balance[0].currency);
                Console.WriteLine("\tbalance[0].amount={0}", getBalanceResponse.balance[0].amount);
                Console.WriteLine("\tbalance[0].balanceTimestamp={0}", getBalanceResponse.balance[0].balanceTimestamp);

                Console.WriteLine("\tbalance[0].lastMovementTimestamp={0}", getBalanceResponse.balance[0].lastMovementTimestamp);

                // get fx quote

                Console.WriteLine("GetFXQuote ...");

                GetFXQuoteType getFXQuote = new GetFXQuoteType();
                getFXQuote.version = 3.0M;
                getFXQuote.usersBankID = addBeneficiaryBankAccountResponse.usersBankID;
                getFXQuote.buyCurrency = "GBP";
                getFXQuote.sellAmount = new MonetaryValueType();
                getFXQuote.sellAmount.currency = "AUD";
                getFXQuote.sellAmount.amount = 101;

                GetFXQuoteResponseType getFXQuoteResponse = serviceProxy.GetFXQuote(getFXQuote);
                Console.WriteLine("\tbuyAmount.currency={0}", getFXQuoteResponse.fxDetail.buyAmount.currency);
                Console.WriteLine("\tbuyAmount.amount={0}", getFXQuoteResponse.fxDetail.buyAmount.amount);
                Console.WriteLine("\tsellAmount.currency={0}", getFXQuoteResponse.fxDetail.sellAmount.currency);
                Console.WriteLine("\tsellAmount.amount={0}", getFXQuoteResponse.fxDetail.sellAmount.amount);
                Console.WriteLine("\tfxRate={0}", getFXQuoteResponse.fxDetail.fxRate);
                Console.WriteLine("\tfxTicketID={0}", getFXQuoteResponse.fxTicketID);
                               
                // request payout

                Console.WriteLine("PayoutRequest ...");

                PayoutRequestType payoutRequest = new PayoutRequestType();
                payoutRequest.version = 4.0M;
                payoutRequest.usersBankID = addBeneficiaryBankAccountResponse.usersBankID;
                payoutRequest.beneficiaryStatementNarrative = "foo";
                payoutRequest.fxTicketID = getFXQuoteResponse.fxTicketID;
                payoutRequest.merchantTransactionReference = Guid.NewGuid().ToString();
                //The type of Payer making the payment authenticatedCaller - Payout is being requested on behalf of the requesting merchant
                //managedMerchant - Payout is being requested on behalf of a managed merchant user - Payout is being requested on behalf of a user
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
                              
                
                payoutRequest.payoutRequestAmount = new MonetaryValueType();
                payoutRequest.payoutRequestAmount.currency = "AUD";
                // To Test Further /getFXQuoteResponse.fxDetail.fxRate.rate has been removed off
                payoutRequest.payoutRequestAmount.amount = getFXQuote.sellAmount.amount;
                //payoutRequest.payoutRequestAmount.amount = getFXQuote.sellAmount.amount / getFXQuoteResponse.fxDetail.fxRate.rate;
                payoutRequest.serviceLevel = ServiceLevelType.standard;
                PayoutRequestResponseType payoutRequestResponse = serviceProxy.PayoutRequest(payoutRequest);
                Console.WriteLine("\tpaymentID={0}", payoutRequestResponse.epTransactionID);
                Console.WriteLine("\tmerchantTransactionReference={0}", payoutRequestResponse.merchantTransactionReference);
               

//                // validate credit
                  // Cannot create a credit via the API so cannot test
//                Console.WriteLine("Validate Credit...");
//
//                ValidateCreditType validateCredit = new ValidateCreditType();
//                validateCredit.version = 1.1M;
//                validateCredit.paymentID = payoutRequestResponse.paymentID;
//                validateCredit.transactionAmount = payoutRequest.payoutRequestAmount;
//                validateCredit.userID = createUserResponse.userID;
//
//                ValidateCreditResponseType validateCreditResponse = serviceProxy.ValidateCredit(validateCredit);

                // Get Transaction Detail
                Console.WriteLine("Get Transaction Detail...");

                GetTransactionDetailType getTransactionDetail = new GetTransactionDetailType();
                getTransactionDetail.version = 3.0M;
                TransactionIDType transactionId = new TransactionIDType();
               //Commented By BNK
              
                transactionId.epTransactionID =  payoutRequestResponse.epTransactionID;
                transactionId.epTransactionIDSpecified = true;
                TransactionIDType[] listOfIds = new TransactionIDType[1];
                listOfIds[0] = transactionId;
                getTransactionDetail.transactionID = listOfIds;
                GetTransactionDetailResponseType getTransactionDetailResponse = serviceProxy.GetTransactionDetail(getTransactionDetail);

                TransactionDetailMappingType[] mappings = getTransactionDetailResponse.transactionDetailMappings;
                FinancialTransactionType ftDetail = mappings[0].transactionDetail;

                Console.WriteLine("\tTransactionDetail.transactionType={0}", ftDetail.transactionType );
                Console.WriteLine("\tTransactionDetail.currency={0}", ftDetail.amount.currency);
                Console.WriteLine("\tTransactionDetail.amount={0}", ftDetail.amount.amount);
                Console.WriteLine("\tTransactionDetail.transactionID={0}", ftDetail.epTransactionID);

                // Get Statement
                Console.WriteLine("Get Statement...");
                GetStatementType getStatementRequest = new GetStatementType();
                getStatementRequest.version = 3.0M;
                getStatementRequest.startDateTime = ftDetail.timestamp;
                getStatementRequest.endDateTime = ftDetail.timestamp;
                getStatementRequest.currency = "AUD";
                GetStatementResponseType statementResponse = serviceProxy.GetStatement(getStatementRequest);
                Console.WriteLine("\tStatement opening balance={0}{1}", statementResponse.openingBalance.currency, statementResponse.openingBalance.amount);
                Console.WriteLine("\tStatement closing balance={0}{1}", statementResponse.closingBalance.currency, statementResponse.closingBalance.amount);
                Console.WriteLine("\tStatement number of transactions={0}", statementResponse.statementLineItemsList.Length);
                Console.WriteLine("\tFirst transaction id={0}", statementResponse.statementLineItemsList[0].transaction.epTransactionID);
                Console.WriteLine("\tFirst transaction currency={0}", statementResponse.statementLineItemsList[0].transaction.amount.currency);
                Console.WriteLine("\tFirst transaction amount={0}", statementResponse.statementLineItemsList[0].transaction.amount.amount);
               
                // Search Transactions
                SearchTransactionsType searchTransactionRequest = new SearchTransactionsType();
                searchTransactionRequest.version = 3.0M;
                SearchSortFieldsType[] sortFields = new SearchSortFieldsType[1];
                sortFields[0] = new SearchSortFieldsType();
                sortFields[0].sortField = SearchSortValueType.Timestamp;
                SearchTransactionsCriteriaType searchCriteria = new SearchTransactionsCriteriaType();
                searchTransactionRequest.searchTransactionsCriteria = searchCriteria;
                searchCriteria.epTransactionID = ftDetail.epTransactionID;
                searchCriteria.epTransactionIDSpecified = true;

                searchCriteria.periodStartDateTime = ftDetail.timestamp; // will default to beginning of time.
                searchCriteria.periodEndDateTime = DateTime.Now;

                searchCriteria.sortCriteria = new SortCriteriaType();
                searchCriteria.sortCriteria.sortOrder = SortOrderType.ASC;
                searchCriteria.sortCriteria.sortFields = sortFields;

                searchTransactionRequest.pagination = new PaginationType();
                searchTransactionRequest.pagination.offset = 0;
                searchTransactionRequest.pagination.pageSize = 1;

                SearchTransactionsResponseType searchTransactionResponse = serviceProxy.SearchTransactions(searchTransactionRequest);

                Console.WriteLine("\tFound {0} transactions matching transaction id", searchTransactionResponse.financialTransactions.Length);
                Console.WriteLine("\tTransaction details: {0}{1}", searchTransactionResponse.financialTransactions[0].amount.currency, searchTransactionResponse.financialTransactions[0].amount.amount);


                //delete beneficiary bank account
                Console.WriteLine("DeleteBeneficiaryBankAccount ...");
                DeleteBeneficiaryBankAccountType deleteBeneficiaryBankAccount = new DeleteBeneficiaryBankAccountType();
                deleteBeneficiaryBankAccount.version = 3.0M;
                deleteBeneficiaryBankAccount.usersBankID = addBeneficiaryBankAccountResponse.usersBankID;

                DeleteBeneficiaryBankAccountResponseType deleteBeneficiaryBankAccountResponse = serviceProxy.DeleteBeneficiaryBankAccount(deleteBeneficiaryBankAccount);
                
                Console.WriteLine("\tisDeleted={0}", deleteBeneficiaryBankAccountResponse.isDeleted);
                Console.WriteLine("\tusersBankID.userID.epUserID={0}", deleteBeneficiaryBankAccountResponse.usersBankID.userID.epUserID);
                Console.WriteLine("\tusersBankID.userID.merchantUserID={0}", deleteBeneficiaryBankAccountResponse.usersBankID.userID.merchantUserID);
                Console.WriteLine("\tusersBankID.benBankID.epBankID={0}", deleteBeneficiaryBankAccountResponse.usersBankID.benBankID.epBankID);
                Console.WriteLine("\tusersBankID.benBankID.merchantBankID={0}", deleteBeneficiaryBankAccountResponse.usersBankID.benBankID.merchantBankID);

                //Disable User
                Console.WriteLine("DisableUser ...");

                DisableUserType disableUserRequest = new DisableUserType();
                disableUserRequest.version = 2.0M;
                disableUserRequest.userID = createUserResponse.userID;

                serviceProxy.DisableUser(disableUserRequest);
            }
            catch (XmlSchemaValidationException validationException)
            {
                Console.WriteLine("\nThe service request is not valid:\n{0}", validationException.Message);
            }
            catch (ServiceErrorException serviceException)
            {
                ErrorResponseType errorResponse = serviceException.ServiceErrorResponse;

                Console.WriteLine(
                    "\nThe service returned with an error:\n\tfailureType={0}\n\tcode={1}\n\tuniqueErrorID={2}\n\tshortMsg={3}\n\tlongMsg={4}\n\ttimeOfFailure={5}",
                    errorResponse.failureType,
                    errorResponse.code,
                    errorResponse.uniqueErrorID,
                    errorResponse.shortMsg,
                    errorResponse.longMsg,
                    errorResponse.timeOfFailure
                    );

                if (errorResponse.failures != null)
                {
                    foreach (FailureItemType failureItem in errorResponse.failures)
                    {
                        Console.WriteLine("\n\tfailureitem key={0}, value={1}", failureItem.key, failureItem.value);
                    }
                }
            }
            catch (SoapException soapException)
            {
                Console.WriteLine("\nThe service responded with a SOAP Fault:\n{0}", soapException);
            }
            catch (WebException webException)
            {
                Console.WriteLine("\nA web exception occurred:\n{0}", webException);
            }
            //catch (Exception exception)
            //{
            //    Console.WriteLine("\nAn unexpected exception occurred:\n{0}", exception);
            //}

            Console.WriteLine("\nPress <enter> to continue ...");
            Console.ReadLine();
        }
    }
}

