﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.Enums;
using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.Id);
            builder.Property(o => o.Id).HasConversion(
                            orderId => orderId.Value,
                            dbId => OrderId.Of(dbId));

            builder.HasOne<Customer>()
                .WithMany()
                .HasForeignKey(o => o.CustomerId)
                .IsRequired();

            builder.HasMany(o => o.OrderItems)
                .WithOne()
                .HasForeignKey(oi => oi.OrderId);

            builder.ComplexProperty(
                o => o.OrderName, nameBuilder =>
                {
                    nameBuilder.Property(n => n.Value)
                        .HasColumnName(nameof(Order.OrderName))
                        .IsRequired()
                        .HasMaxLength(100);
                });

            builder.ComplexProperty(
                o => o.ShippingAddress, addressBuilder =>
                {
                    addressBuilder.Property(a => a.FirstName)
                        .IsRequired()
                        .HasMaxLength(50);

                    addressBuilder.Property(a => a.LastName)
                        .IsRequired()
                        .HasMaxLength(50);

                    addressBuilder.Property(a => a.EmailAddress)
                        .IsRequired()
                        .HasMaxLength(100);

                    addressBuilder.Property(a => a.AddressLine)
                        .IsRequired()
                        .HasMaxLength(100);

                    addressBuilder.Property(a => a.Country)
                        .HasMaxLength(50);

                    addressBuilder.Property(a => a.State)
                        .HasMaxLength(50);

                    addressBuilder.Property(a => a.ZipCode)
                        .HasMaxLength(5)
                        .IsRequired();
                });

            builder.ComplexProperty(
                o => o.BillingAddress, addressBuilder =>
                {
                    addressBuilder.Property(a => a.FirstName)
                        .IsRequired()
                        .HasMaxLength(50);

                    addressBuilder.Property(a => a.LastName)
                        .IsRequired()
                        .HasMaxLength(50);

                    addressBuilder.Property(a => a.EmailAddress)
                        .IsRequired()
                        .HasMaxLength(100);

                    addressBuilder.Property(a => a.AddressLine)
                        .IsRequired()
                        .HasMaxLength(100);

                    addressBuilder.Property(a => a.Country)
                        .HasMaxLength(50);

                    addressBuilder.Property(a => a.State)
                        .HasMaxLength(50);

                    addressBuilder.Property(a => a.ZipCode)
                        .HasMaxLength(5)
                        .IsRequired();
                });

            builder.ComplexProperty(
                o => o.Payment, paymentBuilder =>
                {
                    paymentBuilder.Property(p => p.CardName)
                        .HasMaxLength(50);

                    paymentBuilder.Property(p => p.CardNumber)
                        .IsRequired()
                        .HasMaxLength(24);

                    paymentBuilder.Property(p => p.Expiration)
                        .HasMaxLength(10);

                    paymentBuilder.Property(p => p.CVV)
                        .HasMaxLength(3);
                    paymentBuilder.Property(p => p.PaymentMethod);
                });

            builder.Property(o => o.Status)
                .HasDefaultValue(OrderStatus.Draft)
                .HasConversion(
                    s => s.ToString(),
                    dbStatus => (OrderStatus)Enum.Parse(typeof(OrderStatus), dbStatus));
                // .HasConversion is used to convert the enum to a string for storage in the database

            builder.Property(o => o.TotalPrice);
        }
    }
}
