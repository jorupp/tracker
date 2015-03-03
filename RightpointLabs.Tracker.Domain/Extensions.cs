using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightpointLabs.Tracker.Domain
{
    public static class Extensions
    {
        /// <summary>
        /// If obj is null, returns null immediately.  Otherwises, passes obj to chainCall and returns the result.
        /// 
        /// This essentially lets us fake null propagation.  Description: http://kontrawize.blogs.com/kontrawize/2007/03/null_propagatio.html.
        /// </summary>
        /// <typeparam name="T1">The type of the input object</typeparam>
        /// <typeparam name="T2">The return type</typeparam>
        /// <param name="obj">The object to be checked for null and passed to chainCall</param>
        /// <param name="chainCall">The delegate to be called with obj as an argument</param>
        /// <returns>null if obj is null, or the result of calling chainCall with obj as an argument.</returns>
        public static T2 ChainIfNotNull<T1, T2>(this T1 obj, Func<T1, T2> chainCall) where T1 : class
        {
            if (null == obj)
                return default(T2);
            return chainCall(obj);
        }
    }
}
