using System;
using FluentAssertions;
using WebToEpubKindle.Core.Domain.EpubComponents;
using WebToEpubKindle.Core.Properties;
using Xunit;

namespace WebToEpubKindle.UnitTest
{
    
    public class ImageTest
    {
        [Theory]
        [InlineData("prueba.jpg",".jpg", new byte[1]{ 0 }, "image/jpeg")]
        [InlineData("prueba.png",".png", new byte[1]{ 0 }, "image/png")]
        [InlineData("prueba.jpeg",".jpeg", new byte[1]{0}, "image/jpeg")]
        [InlineData("prueba.svg",".svg", new byte[1]{0}, "image/svg+xml")]
        public void load_supported_image_extension_should_load_image(string fileName, string extension, byte[] bytes, string mimeType)
        {
            Func<Image> result = () => Image.LoadImage(fileName, extension, bytes);
            var image = result.Invoke();
            
            image.OriginalFileName.Should().Be(fileName);
            image.Extension.Should().Be(extension);
            image.MimeType.Should().Be(mimeType);
        }

        [Theory]
        [InlineData("prueba.doc", ".doc", new byte[1]{0})]
        [InlineData("prueba.xml", ".xml", new byte[1]{0})]
        public void load_unsuported_image_extension_should_throw_exception(string fileName, string extension, byte[] bytes)
        {
            Func<Image> result = () => Image.LoadImage(fileName, extension, bytes);

            result.Should().Throw<Exception>().WithMessage(CoreStrings.ImageFileExtensionNotSupported);
        }
    }
}