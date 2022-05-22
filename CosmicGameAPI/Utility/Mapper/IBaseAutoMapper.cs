using System;
using System.Collections.Generic;
using System.Text;

namespace CosmicGameAPI.Utility.Mapper
{
    public interface IBaseAutoMapper<Source, Destination>
    {
        //  Method to autoMap 2 objects in a list
        List<Destination> ToList(IEnumerable<Source> sources);

        //  Method to autoMap 2 objects
        Destination ToSingle(Source source);
    }
}
