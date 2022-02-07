﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TicketsBooking.Application.Components.Purchases;
using TicketsBooking.Application.Components.Purchases.DTOs.Commands;
using TicketsBooking.Application.Components.Purchases.DTOs.RepoDTO;
using TicketsBooking.Domain.Entities;
using TicketsBooking.Infrastructure.Persistence;

namespace TicketsBooking.Infrastructure.Repos
{
    public class PurchaseRepo:IPurchaseRepo
    {
        private readonly AppDbContext _dbContext;

        public PurchaseRepo(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Purchase> CreateNewPurchase(CreateNewPurchaseCommand command)
        {
            Event eventObject = await _dbContext.Events
                .Include(e => e.Purchases)
                .FirstOrDefaultAsync(e => e.EventID == command.EventID);
            
            Customer customer = await _dbContext.Customers
                .Include(e => e.Purchases)
                .FirstOrDefaultAsync(e => e.Email == command.CustomerID);
            
            Purchase purchase = new Purchase
            {
                PurchaseID = command.CustomerID + command.EventID + eventObject.BoughtTickets,
                ReservationDate = command.PurchaseTime,
                TicketsCount = command.TicketsCount,
                SingleTicketCost = eventObject.SingleTicketPrice,
                //customerObject = customer,
                //eventObject = eventObject
            };

            eventObject.Purchases.Add(purchase);
            
            customer.Purchases.Add(purchase);

            eventObject.BoughtTickets += command.TicketsCount;
            await _dbContext.Purchases.AddAsync(purchase);
            await _dbContext.SaveChangesAsync();
            
            return purchase;
            throw new NotImplementedException();

        }

        /*public async Task<List<Purchase>> GetAll(string CustomerID)
        {
            Customer customer = await _dbContext.Customers
                .Include(c => c.Purchases)
                .FirstOrDefaultAsync(c => c.Email == CustomerID);
            
            List<Event> eventsList = _dbContext.Events
                .Include(c => c.Purchases)
                .Where(e => e.Purchases.Intersect(customer.Purchases).Count() != 0).ToList();
            
            PurchaseRepoDTO purchaseRepoDTO = new PurchaseRepoDTO();
            List<PurchaseRepoDTO> purListDTO = new List<PurchaseRepoDTO>();

            foreach (Event e  )

            return customer.Purchases.ToList();
        }*/
        public async Task<List<Purchase>> GetAll(string CustomerID)
        {
            Customer customer = await _dbContext.Customers
               .Include(c => c.Purchases)
               .FirstOrDefaultAsync(c => c.Email == CustomerID);

            List<Event> eventsList = _dbContext.Events.Include(e => e.Purchases).AsQueryable().ToList();

            List<PurchaseRepoDTO> purchaseRepoDTOs = new List<PurchaseRepoDTO>();

            foreach(Event e in eventsList)
            {
                foreach(Purchase purchase in customer.Purchases)
                {
                    if()
                }
            }
        }

        public async Task<PurchaseRepoDTO> GetSingle(string purchaseID)
        {
                
            Purchase obj =  await _dbContext.Purchases
                     .FirstOrDefaultAsync(p => p.PurchaseID == purchaseID);

            if (obj == null)
            {
                return null;
            }


            PurchaseRepoDTO dto = new PurchaseRepoDTO
            {
                PurchaseID = obj.PurchaseID,
                ReservationDate = obj.ReservationDate,
                TicketsCount = obj.TicketsCount,
                SingleTicketCost = obj.SingleTicketCost,
            };

            List<Event> events = _dbContext.Events.AsQueryable().ToList();
            List<Customer> customers = _dbContext.Customers.AsQueryable().ToList();
            bool isFound = false;
            foreach (Event e in events)
            {
                foreach(Purchase p in e.Purchases)
                {
                    if (p.PurchaseID == purchaseID)
                    {
                        isFound = true;
                        dto.EventID = e.EventID;
                        break;
                    }
                }
                if (isFound)
                {
                    break;
                }
            }
            
            isFound = false;
            foreach (Customer customer in customers) 
            {
                foreach (Purchase p in customer.Purchases)
                {
                    if(p.PurchaseID == purchaseID)
                    {
                        isFound=true;
                        dto.CustomerID = customer.Email;
                        break;
                    }
                }
                if (isFound)
                {
                    break;
                }
            }
            return dto;
        }
    }
}