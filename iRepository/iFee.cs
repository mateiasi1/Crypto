﻿using System;
using System.Collections.Generic;
using System.Text;
using WebApplication17.Models;

namespace iRepository
{
    public interface IFee
    {

        #region Flat rate fee
        double GetAllFlatRateFees();
        FlatRateFee AddFlatRateFee(FlatRateFee flatRateFee);
        FlatRateFee DeleteFlatRateFee(int id);
        #endregion

        #region Fee
        List<Fee> GetAllFees();
        Fee AddFee(Fee fee);
        Fee DeleteFee(int id);
        #endregion
    }
}
