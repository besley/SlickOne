using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SlickOne.Module.AuthImpl.Entity
{
    /// <summary>
    /// resource type
    /// </summary>
    public enum AppResourceTypeEnum
    {
        /// <summary>
        /// system
        /// </summary>
        System = 1,

        /// <summary>
        /// module
        /// </summary>
        Module = 2,

        /// <summary>
        /// menu
        /// </summary>
        Menu = 3,

        /// <summary>
        /// field
        /// </summary>
        Field = 4,

        /// <summary>
        /// button
        /// </summary>
        Button = 5,

        /// <summary>
        /// method
        /// </summary>
        Function = 6
    }
}
