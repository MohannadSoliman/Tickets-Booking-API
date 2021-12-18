﻿using System.Collections.Generic;
using System.Threading.Tasks;
using TanvirArjel.Extensions.Microsoft.DependencyInjection;
using TicketsBooking.Application.Common.Responses;
using TicketsBooking.Application.Components.Events.DTOs.Queries;
using TicketsBooking.Application.Components.Events.DTOs.Results;
using TicketsBooking.Domain.Entities;

namespace TicketsBooking.Application.Components.Events
{
    [ScopedService]
    public interface IEventRepo
    {
        Task<List<Event>> ListAsync(ListEventsQuery query);
    }
}