﻿// <auto-generated />

using System;
using System.Reflection;
using System.Resources;
using System.Threading;
using JetBrains.Annotations;
using Microsoft.Extensions.Logging;

namespace WebToEpubKindle.Core.Properties
{
    /// <summary>
    ///     <para>
    ///		    String resources used in EF exceptions, etc.
    ///     </para>
    ///     <para>
    ///		    These strings are exposed publicly for use by database providers and extensions.
    ///         It is unusual for application code to need these strings.
    ///     </para>
    /// </summary>
    public static class CoreStrings
    {
        private static readonly ResourceManager _resourceManager
            = new ResourceManager("WebToEpubKindle.Core.Properties.CoreStrings", typeof(CoreStrings).GetTypeInfo().Assembly);

        /// <summary>
        ///     The page can´t be null.
        /// </summary>
        public static string NullPage
            => GetString("NullPage");

        /// <summary>
        ///     The page with identifier {identifier} doesn´t exist.
        /// </summary>
        public static string PageIdentifierNotExist([CanBeNull] object identifier)
            => string.Format(
                GetString("PageIdentifierNotExist", nameof(identifier)),
                identifier);

        /// <summary>
        ///     The page doesn´t have a relation between content and images. All the images added to the page need have a reference in the content of the page.
        /// </summary>
        public static string PageInvalidImageContent
            => GetString("PageInvalidImageContent");

        /// <summary>
        ///     The extension  of the file is not supported. The image format files supported are: jpe, jpeg, jpg, tiff, png.
        /// </summary>
        public static string ImageFileExtensionNotSupported
            => GetString("ImageFileExtensionNotSupported");

        private static string GetString(string name, params string[] formatterNames)
        {
            var value = _resourceManager.GetString(name);
            for (var i = 0; i < formatterNames.Length; i++)
            {
                value = value.Replace("{" + formatterNames[i] + "}", "{" + i + "}");
            }

            return value;
        }
    }
}

namespace WebToEpubKindle.Core.Properties.Internal
{
    /// <summary>
    ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
    ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
    ///     any release. You should only use it directly in your code with extreme caution and knowing that
    ///     doing so can result in application failures when updating to a new Entity Framework Core release.
    /// </summary>
    public static class CoreResources
    {
        private static readonly ResourceManager _resourceManager
            = new ResourceManager("WebToEpubKindle.Core.Properties.CoreStrings", typeof(CoreResources).GetTypeInfo().Assembly);
    }
}
