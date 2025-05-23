﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using NotifyHub.Domain.Aggregates;
using NotifyHub.Domain.Interfaces;
using System.Reflection;

namespace NotifyHub.Infrastructure.Persistence;

public class ApplicationDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>, IApplicationDbContext
{
    public virtual DbSet<SmsMessage> SmsMessages { get; set; }
    public virtual DbSet<SmsSendRequest> SmsSendRequests { get; set; }
    public virtual DbSet<UserDevice> UserDevices { get; set; }

    public DatabaseFacade Database => throw new NotImplementedException();

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
        //Database.Migrate();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }
}
