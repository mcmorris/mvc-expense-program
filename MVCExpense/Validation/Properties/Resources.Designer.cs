﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Validation.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Validation.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to All credit cards entered into the system must have the middle eight digits asterixed out..
        /// </summary>
        public static string CCMustBeMasked {
            get {
                return ResourceManager.GetString("CCMustBeMasked", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Credit card numbers must be exactly 16 digits long.
        /// </summary>
        public static string InvalidCCLength {
            get {
                return ResourceManager.GetString("InvalidCCLength", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Credit card numbers must be in the format ####********####.
        /// </summary>
        public static string InvalidCCNumber {
            get {
                return ResourceManager.GetString("InvalidCCNumber", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Dates cannot take place after the present.
        /// </summary>
        public static string InvalidDateAboveMaximum {
            get {
                return ResourceManager.GetString("InvalidDateAboveMaximum", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Dates are restricted to a minimum value of 1990/01/01.
        /// </summary>
        public static string InvalidDateBelowMinimum {
            get {
                return ResourceManager.GetString("InvalidDateBelowMinimum", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The date value provided is invalid.
        /// </summary>
        public static string InvalidDateFormat {
            get {
                return ResourceManager.GetString("InvalidDateFormat", resourceCulture);
            }
        }
    }
}
