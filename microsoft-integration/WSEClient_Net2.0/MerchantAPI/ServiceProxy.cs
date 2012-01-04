﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Xml.Schema;
using System.Reflection;

using Microsoft.Web.Services3;
using Microsoft.Web.Services3.Security.Tokens;

namespace Earthport.MerchantAPI
{
    /// <summary>
    /// Used to invoke operations on the Earthport Merchant Web Service.
    /// </summary>
    /// <remarks>
    /// The ServiceProxy is the primary class used to invoke operations on the Earthport Merchant Web Service.  A ServiceProxy must first be 
    /// instantiated by providing a service URL, username and password.  Methods can then be called which automatically invoke the appropriate web 
    /// service operation and return a result. The general format of a method is:
    /// 
    /// [OperationResponseType] Operation([OperationRequestType] operationRequest)
    /// 
    /// Prior to calling the corresponding web service operation, the operationRequest is validated against its schema.  If the request is invalid, 
    /// an XmlSchemaValidationException is thrown.  If the request is valid, but the web service operation returns with an error, a 
    /// ServiceErrorException is thrown.
    /// </remarks>
    public class ServiceProxy
    {
        /// <summary>
        /// Initializes a new instance of the ServiceProxy class
        /// </summary>
        /// <param name="serviceUrl">The URL of the web service (as supplied by Earthport)</param>
        /// <param name="username">The service username (as supplied by Earthport)</param>
        /// <param name="password">The service password (as supplied by Earthport)</param>
        public ServiceProxy(string serviceUrl, string username, string password)
        {
            // create an instance of the autogenerated service proxy and set the url
            _service = new MerchantAPIService();
            _service.Url = serviceUrl;

            // set the username, password and password option so that an appropriate security token can be generated
            _service.RequestSoapContext.Security.Tokens.Add(new UsernameToken(username, password, PasswordOption.SendPlainText));

            // load up schemas embedded in the assembly
            _schemaSet = new XmlSchemaSet();
            _schemaSet.Add(null, GetEmbeddedSchema("Earthport.MerchantAPI.ServiceMetadata.components.bankBaseTypes-3.0.xsd"));
            _schemaSet.Add(null, GetEmbeddedSchema("Earthport.MerchantAPI.ServiceMetadata.components.coreTypes-2.0.xsd"));
            _schemaSet.Add(null, GetEmbeddedSchema("Earthport.MerchantAPI.ServiceMetadata.components.financialTransactionBaseTypes-3.0.xsd"));
            _schemaSet.Add(null, GetEmbeddedSchema("Earthport.MerchantAPI.ServiceMetadata.components.identityBaseTypes-2.0.xsd"));
            _schemaSet.Add(null, GetEmbeddedSchema("Earthport.MerchantAPI.ServiceMetadata.components.payoutBaseTypes-1.2.xsd"));
            _schemaSet.Add(null, GetEmbeddedSchema("Earthport.MerchantAPI.ServiceMetadata.services.addBeneficiaryBankAccount-3.0.xsd"));
            _schemaSet.Add(null, GetEmbeddedSchema("Earthport.MerchantAPI.ServiceMetadata.services.createOrUpdateUser-1.0.xsd"));
            _schemaSet.Add(null, GetEmbeddedSchema("Earthport.MerchantAPI.ServiceMetadata.services.createUser-3.0.xsd"));
            _schemaSet.Add(null, GetEmbeddedSchema("Earthport.MerchantAPI.ServiceMetadata.services.deleteBeneficiaryBankAccount-3.0.xsd"));
            _schemaSet.Add(null, GetEmbeddedSchema("Earthport.MerchantAPI.ServiceMetadata.services.disableUser-2.0.xsd"));
            _schemaSet.Add(null, GetEmbeddedSchema("Earthport.MerchantAPI.ServiceMetadata.services.echo-1.0.xsd"));
            _schemaSet.Add(null, GetEmbeddedSchema("Earthport.MerchantAPI.ServiceMetadata.services.errorResponse-1.0.xsd"));
            _schemaSet.Add(null, GetEmbeddedSchema("Earthport.MerchantAPI.ServiceMetadata.services.getBalance-2.0.xsd"));
            _schemaSet.Add(null, GetEmbeddedSchema("Earthport.MerchantAPI.ServiceMetadata.services.getBeneficiaryBankAccount-3.0.xsd"));
            _schemaSet.Add(null, GetEmbeddedSchema("Earthport.MerchantAPI.ServiceMetadata.services.getBeneficiaryBankAccountRequiredData-3.0.xsd"));
            _schemaSet.Add(null, GetEmbeddedSchema("Earthport.MerchantAPI.ServiceMetadata.services.getFXQuote-3.0.xsd"));
            _schemaSet.Add(null, GetEmbeddedSchema("Earthport.MerchantAPI.ServiceMetadata.services.getIndicativeFXQuote-2.0.xsd"));
            _schemaSet.Add(null, GetEmbeddedSchema("Earthport.MerchantAPI.ServiceMetadata.services.getPayerIdentity-2.0.xsd"));
            _schemaSet.Add(null, GetEmbeddedSchema("Earthport.MerchantAPI.ServiceMetadata.services.getStatement-3.0.xsd"));
            _schemaSet.Add(null, GetEmbeddedSchema("Earthport.MerchantAPI.ServiceMetadata.services.getTransactionDetail-3.0.xsd"));
            _schemaSet.Add(null, GetEmbeddedSchema("Earthport.MerchantAPI.ServiceMetadata.services.listBeneficiaryBankAccounts-4.0.xsd"));
            _schemaSet.Add(null, GetEmbeddedSchema("Earthport.MerchantAPI.ServiceMetadata.services.payoutRequest-4.0.xsd"));
            _schemaSet.Add(null, GetEmbeddedSchema("Earthport.MerchantAPI.ServiceMetadata.services.searchTransactions-3.0.xsd"));
            _schemaSet.Add(null, GetEmbeddedSchema("Earthport.MerchantAPI.ServiceMetadata.services.updatePayerIdentity-2.0.xsd"));
            _schemaSet.Add(null, GetEmbeddedSchema("Earthport.MerchantAPI.ServiceMetadata.services.validateBeneficiaryBankAccount-3.0.xsd"));
            _schemaSet.Add(null, GetEmbeddedSchema("Earthport.MerchantAPI.ServiceMetadata.services.validateCredit-2.0.xsd"));
            _schemaSet.Add(null, GetEmbeddedSchema("Earthport.MerchantAPI.ServiceMetadata.services.validatePayerIdentity-1.0.xsd"));
            
        }

        /// <summary>
        /// Used to create a virtual account in the system.  Takes the merchant's reference (e.g. johnxcitizen1), currency that the account should
        /// use, and a sub-brand as input and returns a UserID token.
        /// </summary>
        /// <param name="createUser">A CreateUserType request</param>
        /// <returns>A response of type CreateUserTypeResponse</returns>
        public CreateUserResponseType CreateUser(CreateUserType createUser)
        {
            return (CreateUserResponseType)InvokeService(
                createUser,
                typeof(CreateUserResponseType)
                );
        }
        /// <summary>
        /// Used to create a virtual account in the system.  Takes the merchant's reference (e.g. johnxcitizen1), currency that the account should
        /// use, and a sub-brand as input and returns a UserID token.
        /// </summary>
        /// <param name="createUser">A CreateUserType request</param>
        /// <returns>A response of type CreateUserTypeResponse</returns>
        public CreateOrUpdateUserResponseType CreateOrUpdateUser(CreateOrUpdateUserType createUser)
        {
            return (CreateOrUpdateUserResponseType)InvokeService(
                createUser,
                typeof(CreateOrUpdateUserResponseType)
                );
        }

        /// <summary>
        /// Used to add a beneficiary bank account and associate it with a specified UserID token.  Takes the UserID token and the bank details, along with an optional 
        /// merchant bank ID of the beneficiary bank account.  
        /// Returns a unique UsersBankID that identified the new beneficiary bank account.
        /// </summary>
        /// <param name="addBeneficiaryBankAccount">An AddBeneficiaryBankAccountType request</param>
        /// <returns>A response of type AddBeneficiaryBankAccountResponseType</returns>
        public AddBeneficiaryBankAccountResponseType AddBeneficiaryBankAccount(AddBeneficiaryBankAccountType addBeneficiaryBankAccount)
        {
            return (AddBeneficiaryBankAccountResponseType)InvokeService(
                addBeneficiaryBankAccount,
                typeof(AddBeneficiaryBankAccountResponseType)
                );
        }

        /// <summary>
        /// Used to validate beneficiary bank account details without adding them into the system.  Operates as per AddBank but is "read-only".
        /// </summary>
        /// <param name="validateBeneficiary">A ValidateBeneficiaryBankAccountType request</param>
        /// <returns>
        /// A response of type ValidateBeneficiaryBankAccountResponseType (there are no properties on this class.  If a response is 
        /// returned, that means the bank account was validated successfully.  If the validation failed, a ServiceErrorException is thrown
        /// with details of why the validation failed.
        /// </returns>
        public ValidateBeneficiaryBankAccountResponseType ValidateBeneficiaryBankAccount(ValidateBeneficiaryBankAccountType validateBeneficiary)
        {
            return (ValidateBeneficiaryBankAccountResponseType)InvokeService(
                validateBeneficiary,
                typeof(ValidateBeneficiaryBankAccountResponseType)
                );
        }

        /// <summary>
        /// Gets a guaranteed FX rate between two currencies.  Takes a sell Monetary Amount (currency and amount) and a buy currency.  
        /// Will return an FXTicketID and the exchange rate.  Valid for 30 seconds.
        /// </summary>
        /// <param name="getFXQuote">A GetFXQuote request</param>
        /// <returns>A response of type GetFXQuoteResponse</returns>
        public GetFXQuoteResponseType GetFXQuote(GetFXQuoteType getFXQuote)
        {
            return (GetFXQuoteResponseType)InvokeService(
                getFXQuote,
                typeof(GetFXQuoteResponseType)
                );
        }

        /// <summary>
        /// Pays money to an already registered beneficiary bank account.  takes a UsersBankID token to identify a benficiary bank account, a merchant reference, and an optional id for
        /// the bank account to payout to, an optional FXTicketID, and a Monetary Amount (currency, amount) to payout
        /// </summary>
        /// <param name="payoutRequest">A PayoutRequest request</param>
        /// <returns>A response of type PayoutRequestResponse</returns>
        public PayoutRequestResponseType PayoutRequest(PayoutRequestType payoutRequest)
        {
            return (PayoutRequestResponseType)InvokeService(
                payoutRequest,
                typeof(PayoutRequestResponseType)
                );
        }

        /// <summary>
        /// Used to obtain the list of beneficiary bank account fields that should be supplied in order to successfully register a bank account.
        /// The response can be used to dynamically generate UI elements and set properties on a BeneficiaryBankAccount prior to 
        /// validating / adding a beneficiary bank account.
        /// account.
        /// </summary>
        /// <param name="getBeneficiaryBankAccountRequiredData">A GetBeneficiaryBankAccountRequiredData request</param>
        /// <returns>A response of type GetBeneficiaryBankAccountRequiredDataResponseType</returns>
        public GetBeneficiaryBankAccountRequiredDataResponseType GetBeneficiaryBankAccountRequiredData(GetBeneficiaryBankAccountRequiredDataType getBeneficiaryBankAccountRequiredData)
        {
            return (GetBeneficiaryBankAccountRequiredDataResponseType)InvokeService(
                getBeneficiaryBankAccountRequiredData,
                typeof(GetBeneficiaryBankAccountRequiredDataResponseType)
                );
        }

        /// <summary>
        /// Delete a beneficiary bank account for a UsersBankID token provided. Takes a UsersBankID which will contain
        /// the BenBankID token which is the bank account that will be deleted.
        /// </summary>
        /// <param name="deleteBeneficiaryBankAccountRequest">A DeleteBeneficiaryBankAccount request</param>
        /// <returns>A response of type DeleteBeneficiaryBankAccountResponse</returns>
        public DeleteBeneficiaryBankAccountResponseType DeleteBeneficiaryBankAccount(DeleteBeneficiaryBankAccountType deleteBeneficiaryBankAccount)
        {
            return (DeleteBeneficiaryBankAccountResponseType)InvokeService(
                deleteBeneficiaryBankAccount,
                typeof(DeleteBeneficiaryBankAccountResponseType)
                );
        }

        /// <summary>
        /// List a set of beneficiary bank accounts for a UserID token provided. Takes a UserID token which
        /// can consist of an EP User ID (VAN) and/or merchant user ID.
        /// </summary>
        /// <param name="listActiveBeneficiaryAccountsRequest">A ListActiveBeneficiaryBankAccounts request</param>
        /// <returns>A response of type ListActiveBeneficiaryBankAccountResponse</returns>
        public ListBeneficiaryBankAccountsResponseType ListBeneficiaryBankAccounts(ListBeneficiaryBankAccountsType listBeneficiaryBankAccounts)
        {
            return (ListBeneficiaryBankAccountsResponseType)InvokeService(
                listBeneficiaryBankAccounts,
                typeof(ListBeneficiaryBankAccountsResponseType)
                );
        }

        /// <summary>
        /// Used to check the availability of the Earthport system, and to provide a simple service
        /// for help with integration.
        /// 
        /// This service simple responds with the exact same data as sent in the request.
        /// </summary>
        /// <param name="echoData">An Echo request</param>
        /// <returns>A response of type EchoResponseType</returns>
        public EchoResponseType Echo(EchoType echoData)
        {
            return (EchoResponseType)InvokeService(
                echoData,
                typeof(EchoResponseType)
                );
        }

        /// <summary>
        /// This function is used by customers to list all of the details for the specified beneficiary bank account.
        /// 
        /// The message needs to contain a mandatory user bank ID which contains the 'userID' token and a 'benBankID' 
        ///	token representing the beneficiary	bank account to get.
        /// </summary>
        /// <param name="getBeneficiaryBankAccountRequest">A getBeneficiaryBankAccount request</param>
        /// <returns>A response of type getBeneficiaryBankAccountResponse</returns>
        public getBeneficiaryBankAccountResponseType GetBeneficiaryBankAccount(getBeneficiaryBankAccountType getBeneficiaryBankAccountRequest)
        {
            return (getBeneficiaryBankAccountResponseType)InvokeService(
                getBeneficiaryBankAccountRequest,
                typeof(getBeneficiaryBankAccountResponseType)
                );
        }

        /// <summary>
        /// This function is used by customers to mark a user as closed/deleted.  
        /// This will prevent subsequent payouts/deposits to/from this user from succeeding.  
        ///	Note that it is not possible to re-activate a closed account at a later date.
        /// </summary>
        /// <param name="disableUserRequest">A DisableUser request</param>
        /// <returns>A response of type DisableUserResponseType</returns>
        public DisableUserResponseType DisableUser(DisableUserType disableUserRequest)
        {
            return (DisableUserResponseType)InvokeService(
                disableUserRequest,
                typeof(DisableUserResponseType)
                );
        }

        /// <summary>
        /// This function is used by Earthport merchants
        ///	to retrieve a set of balance figures represented by a
        ///	monetary amount for a given user account for each
        ///	currency registered with the user account.
        ///
        ///	If a single currency is registered for a user account,
        ///	only one monetary value will be returned. If multiple
        ///	currencies are registered, then all balances for each
        ///	currency will be returned.
        /// </summary>
        /// <param name="getBalanceRequest">A GetBalance request</param>
        /// <returns>A response of type GetBalanceResponseType</returns>
        public GetBalanceResponseType GetBalance(GetBalanceType getBalanceRequest)
        {
            return (GetBalanceResponseType)InvokeService(
                getBalanceRequest,
                typeof(GetBalanceResponseType)
                );
        }

        /// <summary>
        /// This function is used by customers 
        ///	to verify that the credit details sent during a deposit/refund 
        ///	notification are correct and valid.
        /// </summary>
        /// <param name="validateCreditRequest">A ValidateCredit request</param>
        /// <returns>A response of type ValidateCreditResponseType</returns>
        public ValidateCreditResponseType ValidateCredit(ValidateCreditType validateCreditRequest)
        {
            return (ValidateCreditResponseType)InvokeService(
                validateCreditRequest,
                typeof(ValidateCreditResponseType)
                );
        }

        /// <summary>
        /// Function used by customers to retrieve a statement of transactions
        /// for items appearing on their van
        /// </summary>
        /// <param name="getStatementRequest">A GetStatementType request</param>
        /// <returns>A response of type GetStatementResponseType</returns>
        public GetStatementResponseType GetStatement(GetStatementType getStatementRequest)
        {
            return (GetStatementResponseType)InvokeService(
                getStatementRequest,
                typeof(GetStatementResponseType)
                );
        }

        /// <summary>
        /// Function used to search for transactions on a merchant VAN
        /// </summary>
        /// <param name="searchTransactionsRequest">A SearchTransactionsType request</param>
        /// <returns>A response of type SearchTransactionsResponseType</returns>
        public SearchTransactionsResponseType SearchTransactions(SearchTransactionsType searchTransactionsRequest)
        {
            return (SearchTransactionsResponseType)InvokeService(
                searchTransactionsRequest,
                typeof(SearchTransactionsResponseType)
                );
        }



        /// <summary>
        /// Function used by customers to retrieve transaction details for
        /// statement items that appear on their van
        /// </summary>
        /// <param name="getTransactionDetail">A GetTransactionDetailType request</param>
        /// <returns>A response of type GetTransactionDetailResponseType</returns>
        public GetTransactionDetailResponseType GetTransactionDetail(GetTransactionDetailType getTransactionDetail)
        {
            return (GetTransactionDetailResponseType)InvokeService(
                getTransactionDetail,
                typeof(GetTransactionDetailResponseType)
                );
        }

        /// <summary>
        /// Generic method to invoke a web service operation.  Serializes the request object to XML, validates the XML against the corresponding
        /// schema, invokes the service and deserializes the response.
        /// </summary>
        /// <param name="request">The request object</param>
        /// <param name="responseType">The Type to deserialize the response to</param>
        /// <returns>A deserialized object of the specified responseType</returns>
        /// <exception cref="XmlSchemaValidationException">Thrown if the validation of the serialized XML againsts the correspoding schema fails.</exception>
        /// <exception cref="ServiceErrorException">Thrown if the service responds with an error (ack value of 'failure').</exception>
        private object InvokeService(object request, Type responseType)
        {
            // serialize the request object to equivalent xml
            XmlElement requestXml = SerializeRequestObjectToXmlAndValidate(request);
 
            // call the submitDocument web service operation
            Response genericResponse = _service.submitDocument(requestXml);

            if (genericResponse.ack == AckType.failure)
            {
                // throw an exception if the response has an AckType of failure
                throw new ServiceErrorException(
                    "The service returned an error.  The 'ServiceErrorResponse' property contains details about this error. ErrorMsg=" + genericResponse.Any.InnerXml.ToString(),
                    (ErrorResponseType)DeserializeResponseXmlToObject(genericResponse.Any, typeof(ErrorResponseType))
                    );
            }

            // deserialize the response xml to the appropriate response type and return
            return DeserializeResponseXmlToObject(genericResponse.Any, responseType);
        }

        /// <summary>
        /// Serializes an object to XML and validates the serialized XML against against the set of schemas
        /// </summary>
        /// <param name="requestObject">The request object</param>
        /// <returns>An XmlElement containing the serialized XML</returns>
        private XmlElement SerializeRequestObjectToXmlAndValidate(object requestObject)
        {
            // create an XmlSerializer for the requestObject's actual Type 
            XmlSerializer serializer = new XmlSerializer(requestObject.GetType());
            
            // create a StringWriter for the serializer to write to
            StringWriter writer = new StringWriter();

            // serialize the object to the writer
            serializer.Serialize(writer, requestObject);
            
            // create a new xml document and load it up with the xml string from the writer
            XmlDocument requestDoc = new XmlDocument();
            requestDoc.LoadXml(writer.ToString());

            // set the Schemas property to the standard SchemaSet, specifying which shemas the document should be validated against
            requestDoc.Schemas = _schemaSet;

            // validate against schemas
            requestDoc.Validate(null); // throws XmlSchemaValidationException if request is invalid
            
            // return the root element of the document
            return requestDoc.DocumentElement;
        }

        /// <summary>
        /// Deserializes a response XML document to the specified ResponsType
        /// </summary>
        /// <param name="responseXml">The response XML to deserialize</param>
        /// <param name="responseType">The type to deserialize to</param>
        /// <returns>A desereialized object of the specified responseType</returns>
        private object DeserializeResponseXmlToObject(XmlElement responseXml, Type responseType)
        {
            // create a StringReader for the serializer to use
            StringReader reader = new StringReader(responseXml.OuterXml);

            // create a serializer of the Type specified
            XmlSerializer ser = new XmlSerializer(responseType);

            // perform the deserialization and return the derialized object
            return ser.Deserialize(reader);
        }

        /// <summary>
        /// Gets an XML schema embedded in the assembly
        /// </summary>
        /// <param name="name">The name of the schema</param>
        /// <returns>An XmlReader representing the schema</returns>
        private XmlReader GetEmbeddedSchema(string name)
        {
            // get the specified embedded assembly and return an XmlReader for it
            return new XmlTextReader(Assembly.GetExecutingAssembly().GetManifestResourceStream(name));
        }

        private MerchantAPIService _service; // instance of generated service proxy
        private XmlSchemaSet _schemaSet; // set of schemas to validate serialized request XML against
    }
}
