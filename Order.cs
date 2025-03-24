using System;
using System.Collections.Generic;

namespace project1.Data.Models;

public partial class Order
{
    public int IdOrder { get; set; }

    public int? IdMedicine { get; set; }

    public string SupplierName { get; set; } = null!;

    public DateOnly OrderDate { get; set; }

    public int QuantityOrdered { get; set; }

    public int? EmployeeId { get; set; }

    public virtual Employee? Employee { get; set; }

    public virtual Medicine? IdMedicineNavigation { get; set; }
}
