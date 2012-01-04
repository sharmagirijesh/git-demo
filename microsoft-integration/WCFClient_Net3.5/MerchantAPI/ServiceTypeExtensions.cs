using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.IO;

namespace Earthport.MerchantAPI.Extensions
{
    internal static class ServiceTypeExtensions
    {
        internal static XmlElement SerializeAndValidate(this object obj, XmlSchemaSet schemas)
        {
            // create an XmlSerializer for the requestObject's actual Type 
            XmlSerializer serializer = new XmlSerializer(obj.GetType());

            // create a StringWriter for the serializer to write to
            StringWriter writer = new StringWriter();

            // serialize the object to the writer
            serializer.Serialize(writer, obj);

            // create a new xml document and load it up with the xml string from the writer
            XmlDocument requestDoc = new XmlDocument();
            requestDoc.LoadXml(writer.ToString());

            if (schemas != null)
            {
                // set the Schemas property to the standard SchemaSet, specifying which shemas the document should be validated against
                requestDoc.Schemas = schemas;

                // validate against schemas
                requestDoc.Validate(null); // throws XmlSchemaValidationException if request is invalid
            }

            // return the root element of the document
            return requestDoc.DocumentElement;
        }

        internal static T DeserializeToObject<T>(this XmlElement serializedXml)
        {
            // create a StringReader for the serializer to use
            StringReader reader = new StringReader(serializedXml.OuterXml);

            // create a serializer of the Type specified
            XmlSerializer ser = new XmlSerializer(typeof(T));

            // perform the deserialization and return the derialized object
            return (T) ser.Deserialize(reader);
        }
    }
}
