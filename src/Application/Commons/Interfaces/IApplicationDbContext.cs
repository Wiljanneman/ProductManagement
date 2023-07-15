﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Commons.Interfaces;
public interface IApplicationDbContext
{
    public DbSet<ApplicationUser> ApplicationUsers { get; set; }
}
