using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using OasysNet.Application.Clients;

namespace OasysNet.IoC.AutoMapper
{
    public static class AutoMapperConfig
    {
        public static Type[] Setup()
        {
            var profiles = RegisterMappings();
            return profiles.Select(c => c.GetType()).ToArray();
        }

        public static IEnumerable<Profile> RegisterMappings()
        {
            yield return new ClientMappingProfile();
        }
    }
}