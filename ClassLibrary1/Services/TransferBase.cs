using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class TransferBase
    {
        public virtual bool DoTransfer(Transfer t) {
            return true;
        }
    }
}
