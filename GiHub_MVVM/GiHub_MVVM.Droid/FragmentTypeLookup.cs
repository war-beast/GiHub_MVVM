using MvvmCross.Droid.Support.V4;
using MvvmCross.Platform.IoC;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GiHub_MVVM.Droid
{
    public interface IFragmentTypeLookup
    {
        bool TryParseFragmentType(Type viewModelType, out Type fragmentType);
    }

    public class FragmentTypeLookup : IFragmentTypeLookup
    {
        private readonly IDictionary<string, Type> _fragmentLookup = new Dictionary<string, Type>();

        public FragmentTypeLookup()
        {
            _fragmentLookup =
              (from type in GetType().Assembly.ExceptionSafeGetTypes()
               where !type.IsAbstract
                  && !type.IsInterface
                  && typeof(MvxFragment).IsAssignableFrom(type)
                  && type.Name.EndsWith("View", StringComparison.CurrentCulture)
               select type).ToDictionary(TrimmedName);
        }


        public bool TryParseFragmentType(Type viewModelType, out Type fragmentType)
        {
            var strippedName = TrimmedName(viewModelType);

            if (!_fragmentLookup.ContainsKey(strippedName))
            {
                fragmentType = null;

                return false;
            }

            fragmentType = _fragmentLookup[strippedName];

            return true;
        }

        private string TrimmedName(Type type)
        {
            return type.Name
                       .TrimEnd("View".ToCharArray())
                       .TrimEnd("ViewModel".ToCharArray());
        }
    }
}