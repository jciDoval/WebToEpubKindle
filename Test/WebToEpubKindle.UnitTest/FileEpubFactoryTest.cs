using d = WebToEpubKindle.Core.Domain;
using i = WebToEpubKindle.Core.Infrastructure;
using WebToEpubKindle.Core.Domain.Enum;
using Xunit;
using System;
using FluentAssertions;

namespace WebToEpubKindle.UnitTest
{
    public class FileEpubFactoryTest
    {
        [Theory]
        [InlineData(EpubVersion.V3_0, typeof(WebToEpubKindle.Core.Infrastructure.Versions.V3_0.Creator))]
        public void build_file_creator_concrete_version(EpubVersion version, Type type)
        {
            
            var creator = i.FileEpubFactory.Initialize(version, null).BuildCreator();

            creator.Should().BeOfType(type);
        }

        
    }
}
