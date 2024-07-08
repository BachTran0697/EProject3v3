﻿using eProject3.Models;

namespace eProject3.Interfaces
{
    public interface IReservedRepo
    {
        Task<IEnumerable<Reservation>> GetReservations();
        Task<Reservation> GetReservationById(int id);
        Task<Reservation> CreateReservation(Reservation Reservation);
        Task<Reservation> UpdateReservation(Reservation Reservation);
        Task<Reservation> DeleteReservation(int id);
    }
}
