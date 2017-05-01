using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PDFToolbox.Behaviors
{
    interface IDropable
    {
        /// <summary>
        /// Type of the data item
        /// </summary>
        Type DataType { get; }

        /// <summary>
        /// Drop data into the collection.
        /// </summary>
        /// <param name="data">The data to be dropped</param>
        /// <param name="index">optional: The index location to insert the data</param>
        void Drop(object data, int index = -1);

        /// <summary>
        /// Remove data from the collection.
        /// </summary>
        /// <param name="data">The data to be removed</param>
        void Remove(object data);
    }
}
