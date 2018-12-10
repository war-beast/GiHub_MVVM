using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MvvmCross.Platform.IoC;
using MvvmCross.Plugins.JsonLocalization;

namespace GiHub_MVVM.Core.Helpers
{
    public class TextProviderBuilder
        : MvxTextProviderBuilder
    {
        public TextProviderBuilder()
            : base(Constants.LocalizationNamespace, Constants.RootFolderForResources)
        {
        }

        protected override IDictionary<string, string> ResourceFiles
        {
            get
            {
                var dictionary = this.GetType()
                                     .GetTypeInfo()
                                     .Assembly
                                     .CreatableTypes()
                                     .Where(t => t.Name.EndsWith("ViewModel"))
                                     .ToDictionary(t => t.Name, t => t.Name);

                return dictionary;
            }
        }
    }
}
