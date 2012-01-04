using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SampleClient.Extensions
{
    public static class ObjectExtensions
    {
        /// <summary>
        /// Extension method that uses "deep reflection" to set a value on an object's nested property 
        /// </summary>
        /// <param name="target">The target object</param>
        /// <param name="propertyName">The nested property name e.g. "Object1.Object2.Object3"</param>
        /// <param name="propertyValue">The value to set</param>
        public static void SetNestedPropertyValue(this object target, string propertyName, object propertyValue)
        {
            object nestedObject = target.GetNestedObjectContainingProperty(propertyName);

            string[] qualifiedPropertyName = propertyName.Split('.');

            nestedObject.GetType().GetProperty(qualifiedPropertyName[qualifiedPropertyName.Length - 1]).SetValue(nestedObject, propertyValue, null);
        }

        /// <summary>
        /// Extension method that uses "deep reflection" to get the value of an object's nested property
        /// </summary>
        /// <param name="target">The target object</param>
        /// <param name="propertyName">The nested property name e.g. "Property1.Property2.Property3"</param>
        /// <returns>The value of the specified property</returns>
        public static object GetNestedPropertyValue(this object target, string propertyName)
        {
            object nestedObject = target.GetNestedObjectContainingProperty(propertyName);

            string[] qualifiedPropertyName = propertyName.Split('.');

            return nestedObject.GetType().GetProperty(qualifiedPropertyName[qualifiedPropertyName.Length - 1]).GetValue(nestedObject, null);
        }

        /// <summary>
        /// Extension method that uses "deep reflection" to get an object's nested object containing a given property 
        /// </summary>
        /// <param name="target">The target object</param>
        /// <param name="name">The nested property name e.g. "Property1.Property2.Property3"</param>
        /// <returns>The object containing the specified property</returns>
        private static object GetNestedObjectContainingProperty(this object target, string name)
        {
            string[] qualifiedPropertyName = name.Split('.');

            object currentObj = target;

            for (int i = 0; i < qualifiedPropertyName.Length - 1; i++)
            {
                currentObj = currentObj.GetType().GetProperty(qualifiedPropertyName[i]).GetValue(currentObj, null);
            }

            return currentObj;
        }
    }
}
