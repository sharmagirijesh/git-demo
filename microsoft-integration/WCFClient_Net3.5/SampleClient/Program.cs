using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Schema;
using System.ServiceModel;
using System.ServiceModel.Security;

using Earthport.MerchantAPI;
using SampleClient.Extensions;

namespace SampleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("WCF Client\n----------\n");

                ServiceProxy serviceProxy = new ServiceProxy("https://sandbox.earthport.com:11023/MerchantAPIUAT2", "EarthportPaymentTest", "Passw0rd");

                // create user

                Console.WriteLine("CreateUser ...");

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
                            name = new IndividualNameType()
                            {
                                givenNames = "Joe",
                                familyName = "Doe"

                            }
                        }
                    }

                };

                var createUserResponse = serviceProxy.CreateUser(createUser);

                Console.WriteLine("\tUserID.epUserID={0}", createUserResponse.userID.epUserID);
                Console.WriteLine("\tUserID.merchantUserID={0}", createUserResponse.userID.merchantUserID);

                Console.WriteLine("CreateUser Or Update...");

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

                var CreateOrUpdateUserResponse = serviceProxy.CreateOrUpdateUser(CreateOrUpdateUser);

                Console.WriteLine("\tUserID.epUserID={0}", CreateOrUpdateUserResponse.userID.epUserID);
                Console.WriteLine("\tUserID.merchantUserID={0}", CreateOrUpdateUserResponse.userID.merchantUserID);




                // construct beneficiary bank account on behalf of a company
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
                                familyName= "Luther"
                            }
                        }
                    }
                };

                Console.WriteLine("GetBeneficiaryBankAccountRequiredData ...");

                var getBeneficiaryBankAccountRequredData = new GetBeneficiaryBankAccountRequiredDataType()
                {

                    version = 3.0M,
                    countryCode = "GB",
                    currencyCode = "GBP"
                };

                var getBeneficiaryBankAccountRequiredDataResponse = serviceProxy.GetBeneficiaryBankAccountRequiredData(getBeneficiaryBankAccountRequredData);

                foreach (BeneficiaryBankAccountGroupType group in getBeneficiaryBankAccountRequiredDataResponse.beneficiaryBankAccountFieldGroupsList)
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

                // validate beneficiary bank account

                Console.WriteLine("ValidateBeneficiaryBankAccount ...");

                var validateBeneficiaryBankAccount = new ValidateBeneficiaryBankAccountType()
                {
                    version = 3.0M,
                    beneficiaryBankAccount = beneficiaryBankAccount

                };

                var validateBeneficiaryAccountResponse = serviceProxy.ValidateBeneficiaryBankAccount(validateBeneficiaryBankAccount);

                // add beneficiary bank account

                Console.WriteLine("AddBeneficiaryBankAccount ...");

                var addBeneficiaryBankAccount = new AddBeneficiaryBankAccountType()
                {
                    version = 3.0M,
                    beneficiaryBankAccount = beneficiaryBankAccount,
                    userID = createUserResponse.userID,
                    merchantBankID = "Earthport" + Guid.NewGuid()
                };

                var addBeneficiaryBankAccountResponse = serviceProxy.AddBeneficiaryBankAccount(addBeneficiaryBankAccount);

                Console.WriteLine("\tUsersBankID.UserID.epUserID={0}", addBeneficiaryBankAccountResponse.usersBankID.userID.epUserID);
                Console.WriteLine("\tUsersBankID.UserID.merchantUserID={0}", addBeneficiaryBankAccountResponse.usersBankID.userID.merchantUserID);
                Console.WriteLine("\tUsersBankID.BenBankID.epBankID={0}", addBeneficiaryBankAccountResponse.usersBankID.benBankID.epBankID);
                Console.WriteLine("\tUsersBankID.BenBankID.merchantBankID={0}", addBeneficiaryBankAccountResponse.usersBankID.benBankID.merchantBankID);

                // get beneficiary bank account

                Console.WriteLine("Get Beneficiary Bank Account ...");

                var getBeneficiaryBankAccount = new getBeneficiaryBankAccountType()
                {
                    version = 3.0M,
                    usersBankID = addBeneficiaryBankAccountResponse.usersBankID
                };

                var getBeneficiaryBankAccountResponse = serviceProxy.GetBeneficiaryBankAccount(getBeneficiaryBankAccount);

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
                var listBeneficiaryBankAccount = new ListBeneficiaryBankAccountsType()
                {
                    version = 4.0M,
                    pagination = new PaginationType(),
                    userID = createUserResponse.userID
                };

                var listBeneficiaryBankAccountResponse = serviceProxy.ListBeneficiaryAccounts(listBeneficiaryBankAccount);

                Console.WriteLine("ListBeneficiaryBankAccounts...");
                foreach (BeneficiaryBankAccountSummaryType benBankSummary in listBeneficiaryBankAccountResponse.beneficiaryBankAccountSummary)
                {
                    Console.WriteLine("\n\tBeneficiaryBankAccount key={0}, description={1}", benBankSummary.benBankID, benBankSummary.description);
                }
                Console.WriteLine("\tuserID.epUserID={0}", listBeneficiaryBankAccountResponse.userID.epUserID);
                Console.WriteLine("\tuserID.merchantUserID={0}", listBeneficiaryBankAccountResponse.userID.merchantUserID);

                // get balance
                Console.WriteLine("GetBalance ...");

                var getBalance = new GetBalanceType()
                {
                    version = 2.0M,
                    currency = "GBP"
                };

                var getBalanceResponse = serviceProxy.GetBalance(getBalance);

                Console.WriteLine("\tbalance[0].currency={0}", getBalanceResponse.balance[0].currency);
                Console.WriteLine("\tbalance[0].amount={0}", getBalanceResponse.balance[0].amount);
                Console.WriteLine("\tbalance[0].balanceTimestamp={0}", getBalanceResponse.balance[0].balanceTimestamp);
                Console.WriteLine("\tbalance[0].lastMovementTimestamp={0}", getBalanceResponse.balance[0].lastMovementTimestamp);

                // get fx quote

                Console.WriteLine("GetFXQuote ...");

                var getFxQuote = new GetFXQuoteType()
                {
                    version = 3.0M,
                    usersBankID = addBeneficiaryBankAccountResponse.usersBankID,
                    buyCurrency = "GBP",
                    sellAmount = new MonetaryValueType()
                    {
                        currency = "AUD",
                        amount = 101
                    },
                    serviceLevel = ServiceLevelType.standard
                };

                var getFXQuoteResponse = serviceProxy.GetFXQuote(getFxQuote);

                Console.WriteLine("\tbuyAmount.currency={0}", getFXQuoteResponse.fxDetail.buyAmount.currency);
                Console.WriteLine("\tbuyAmount.amount={0}", getFXQuoteResponse.fxDetail.buyAmount.amount);
                Console.WriteLine("\tsellAmount.currency={0}", getFXQuoteResponse.fxDetail.sellAmount.currency);
                Console.WriteLine("\tsellAmount.amount={0}", getFXQuoteResponse.fxDetail.sellAmount.amount);
                Console.WriteLine("\tfxRate={0}", getFXQuoteResponse.fxDetail.fxRate);
                Console.WriteLine("\tfxTicketID={0}", getFXQuoteResponse.fxTicketID);

                // request payout

                Console.WriteLine("PayoutRequest ...");

                var payoutRequest = new PayoutRequestType()
                {
                    version = 4.0M,
                    usersBankID = addBeneficiaryBankAccountResponse.usersBankID,
                    merchantTransactionReference = Guid.NewGuid().ToString(),
                    payerType = PayerType.user,
                    payerTypeSpecified = true,

                    payoutRequestAmount = new MonetaryValueType()
                    {
                        amount = getFxQuote.sellAmount.amount, /// getFXQuoteResponse.fxDetail.fxRate.rate,
                        currency = "AUD"
                    },
                    serviceLevel = ServiceLevelType.standard,
                    beneficiaryStatementNarrative = "narrative",
                    fxTicketID = getFXQuoteResponse.fxTicketID,

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
                    payoutType = "Payout Type 1"
                };

                var payoutRequestResponse = serviceProxy.PayoutRequest(payoutRequest);

                Console.WriteLine("\tpaymentID={0}", payoutRequestResponse.epTransactionID);
                Console.WriteLine("\tmerchantTransactionReferece={0}", payoutRequestResponse.merchantTransactionReference);

                //                // validate credit
                //                Console.WriteLine("Validate Credit...");
                //
                //                var validateCredit = new ValidateCreditType()
                //                {
                //                    version = 1.1M,
                //                    paymentID = payoutRequestResponse.paymentID,
                //                    transactionAmount = payoutRequest.payoutRequestAmount,
                //                    userID = createUserResponse.userID
                //                };
                //
                //                var validateCreditResponse = serviceProxy.ValidateCredit(validateCredit);


                // Get Transaction Detail
                Console.WriteLine("Get Transaction Detail...");

                var tranIdArray = new TransactionIDType[1];
                tranIdArray[0] = new TransactionIDType()
                {
                    epTransactionID = payoutRequestResponse.epTransactionID,
                    epTransactionIDSpecified = true
                };

                var getTransactionDetailRequest = new GetTransactionDetailType()
                {
                    version = 3.0M,
                    transactionID = tranIdArray
                    // Only needed when querying on behalf of managed merchant
                    // managedMerchantIdentity = _usersBankID.userID.epUserID,
                };

                var getTransactionDetailResponse = serviceProxy.GetTransactionDetail(getTransactionDetailRequest);

                TransactionDetailMappingType[] transctionDetailMappings = getTransactionDetailResponse.transactionDetailMappings;
                FinancialTransactionType ftDetail = transctionDetailMappings[0].transactionDetail;

                Console.WriteLine("\tTransactionDetail.transactionType={0}", ftDetail.transactionType);
                Console.WriteLine("\tTransactionDetail.currency={0}", ftDetail.amount.currency);
                Console.WriteLine("\tTransactionDetail.amount={0}", ftDetail.amount.amount);
                Console.WriteLine("\tTransactionDetail.transactionID={0}", ftDetail.epTransactionID);


                // Get Statement
                Console.WriteLine("Get Statement...");
                GetStatementType getStatementRequest = new GetStatementType()
                {
                    version = 3.0M,
                    startDateTime = ftDetail.timestamp,
                    endDateTime = ftDetail.timestamp,
                    currency = "AUD"

                };
                GetStatementResponseType statementResponse = serviceProxy.GetStatement(getStatementRequest);
                Console.WriteLine("\tStatement opening balance={0}{1}", statementResponse.openingBalance.currency, statementResponse.openingBalance.amount);
                Console.WriteLine("\tStatement closing balance={0}{1}", statementResponse.closingBalance.currency, statementResponse.closingBalance.amount);
                Console.WriteLine("\tStatement number of transactions={0}", statementResponse.statementLineItemsList.Length);
                Console.WriteLine("\tFirst transaction id={0}", statementResponse.statementLineItemsList[0].transaction.epTransactionID);
                Console.WriteLine("\tFirst transaction currency={0}", statementResponse.statementLineItemsList[0].transaction.amount.currency);
                Console.WriteLine("\tFirst transaction amount={0}", statementResponse.statementLineItemsList[0].transaction.amount.amount);

                // Search Transactions
                Console.WriteLine("Search transactions ...");
                SearchSortFieldsType[] sortFields = new SearchSortFieldsType[1];
                sortFields[0] = new SearchSortFieldsType()
                {
                    sortField = SearchSortValueType.Timestamp
                };


                SearchTransactionsCriteriaType searchCriteria = new SearchTransactionsCriteriaType()
                {
                    epTransactionID = ftDetail.epTransactionID,
                    // payoutRequestResponse.epTransactionID,
                    epTransactionIDSpecified = true,

                    periodStartDateTime = ftDetail.timestamp, //new DateTime(),
                    periodEndDateTime = DateTime.Now,
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

                SearchTransactionsResponseType searchTransactionResponse = serviceProxy.SearchTransactions(searchTransactionRequest);

                Console.WriteLine("\tFound {0} transactions matching transaction id", searchTransactionResponse.financialTransactions.Length);
                Console.WriteLine("\tTransaction details: {0}{1}", searchTransactionResponse.financialTransactions[0].amount.currency, searchTransactionResponse.financialTransactions[0].amount.amount);



                //Delete beneficiary bank account
                Console.WriteLine("DeleteBeneficiaryBankAccount ...");

                var deleteBeneficiaryBankAccountRequest = new DeleteBeneficiaryBankAccountType()
                {
                    version = 3.0M,
                    usersBankID = addBeneficiaryBankAccountResponse.usersBankID
                };

                var deleteBeneficiaryBankAccountResponse = serviceProxy.DeleteBeneficiaryBankAccount(deleteBeneficiaryBankAccountRequest);
                Console.WriteLine("\tisDeleted={0}", deleteBeneficiaryBankAccountResponse.isDeleted);
                Console.WriteLine("\tusersBankID.userID.epUserID={0}", deleteBeneficiaryBankAccountResponse.usersBankID.userID.epUserID);
                Console.WriteLine("\tusersBankID.userID.merchantUserID={0}", deleteBeneficiaryBankAccountResponse.usersBankID.userID.merchantUserID);
                Console.WriteLine("\tusersBankID.benBankID.epBankID={0}", deleteBeneficiaryBankAccountResponse.usersBankID.benBankID.epBankID);
                Console.WriteLine("\tusersBankID.benBankID.merchantBankID={0}", deleteBeneficiaryBankAccountResponse.usersBankID.benBankID.merchantBankID);

                //Disable User
                Console.WriteLine("DisableUser ...");

                var disableUserRequest = new DisableUserType()
                {
                    version = 2.0M,
                    userID = createUserResponse.userID
                };

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
            catch (SecurityAccessDeniedException securityAccessDeniedException)
            {
                Console.WriteLine("\nSecurity failure:\n{0}", securityAccessDeniedException.Message);
            }
            catch (EndpointNotFoundException endpointNotFoundException)
            {
                Console.WriteLine("\nThe endpoint was not found:\n{0}", endpointNotFoundException.Message);
            }
            //catch (Exception exception)
            //{
            //Console.WriteLine("\nAn unexpected exception occurred:\n{0}", exception.Message);
            //}

            Console.WriteLine("\nPress <enter> to continue ...");
            Console.ReadLine();
        }
    }
}

