using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commons.Helpers;
public static class VATHelper
{
    private static decimal _vatPercentage;

    public static decimal VATPercentage
    {
        get { return _vatPercentage; }
        set { _vatPercentage = value; }
    }

    static VATHelper()
    {
        VATPercentage = 15; // Set the VAT percentage to 15%
    }

    public static decimal CalculateVAT(decimal amount)
    {
        return amount * (_vatPercentage / 100);
    }
}
