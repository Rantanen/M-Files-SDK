// Guids.cs
// MUST match guids.h
using System;

namespace MFiles.SDK.VisualStudio.Application
{
    static class GuidList
    {
        public const string guidApplicationProjectPkgString = "7bb74853-3b95-4d45-a7a5-4ee288be7d07";
        public const string guidApplicationProjectCmdSetString = "100178a7-6957-47cc-a922-e747bfb8da4a";
		public const string guidApplicationProjectFactoryString = "2C6C00A5-BD42-45FB-B6B1-43CC52B94E77";

        public static readonly Guid guidApplicationProjectCmdSet = new Guid(guidApplicationProjectCmdSetString);
        public static readonly Guid guidApplicationProjectFactory = new Guid(guidApplicationProjectFactoryString);
    };
}