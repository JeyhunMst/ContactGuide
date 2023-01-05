using System;
using System.Collections.Generic;

namespace Guider.Models;

public partial class Contact
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string Mail { get; set; } = null!;
}
