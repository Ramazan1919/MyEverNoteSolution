using MyEverNote.Entities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEverNote.BusınessLayer.Results
{
    public class BusınessLayerResult<T> where T :class
    {

        public List<ErrorMessageObject> Errors { get; set; }

        public T Result { get; set; }


        public BusınessLayerResult()
        {
            Errors = new List<ErrorMessageObject>();
          


        }

        public void AddErrors(ErrorMessageCode code,string message)
        {

            Errors.Add(new ErrorMessageObject() { Code=code,Message=message}); 

        }
    }
}
