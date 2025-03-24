﻿using System;
using System.Collections.Generic;

namespace project1.Data.Models;

public partial class Prescription
{
    public int IdPrescriptions { get; set; }

    public int? IdMedicine { get; set; }

    public string DoctorName { get; set; } = null!;

    public string PatientName { get; set; } = null!;

    public DateOnly DateIssued { get; set; }

    public int? EmployeeId { get; set; }

    public virtual Employee? Employee { get; set; }

    public virtual Medicine? IdMedicineNavigation { get; set; }
}
