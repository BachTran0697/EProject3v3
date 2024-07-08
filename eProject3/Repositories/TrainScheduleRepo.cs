using eProject3.Interfaces;
using eProject3.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Diagnostics;

namespace eProject3.Repositories
{
    public class TrainScheduleRepo : ITrainScheduleRepo
    {
        private readonly DatabaseContext db;
        public TrainScheduleRepo(DatabaseContext db)
        {
            this.db = db;
        }
        public async Task<Train_Schedule> CreateSchedule(Train_Schedule train_Schedule)
        {
            try
            {
                db.Train_Schedules.Add(train_Schedule);
                await db.SaveChangesAsync();
                return train_Schedule;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Train_Schedule> DeleteSchedule(int id)
        {
            try
            {
                var oldSche = await GetScheduleById(id);
                if (oldSche != null)
                {
                    db.Train_Schedules.Remove(oldSche);
                    await db.SaveChangesAsync();
                    return oldSche;
                }
                else
                {
                    throw new ArgumentException("No ID found");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<Train_Schedule>> GetSchedule()
        {
            return await db.Train_Schedules.ToListAsync();
        }

        public async Task<Train_Schedule> GetScheduleById(int id)
        {
            try
            {
                return await db.Train_Schedules.SingleOrDefaultAsync(s => s.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        

        public async Task<List<Train_Schedule>> Booking(int fromStation, int toStation, DateTime travelTime)
        {
            try
            {
                if (fromStation < toStation)

                {
                    string bookDirect = "down";
                    return await db.Train_Schedules.Where(ts => ts.Station_Code_begin <= fromStation &&
                                                                ts.Station_Code_begin <= toStation &&
                                                                ts.Station_code_end >= fromStation &&
                                                                ts.Station_code_end >= toStation &&
                                                                ts.Time_begin <= travelTime &&
                                                                ts.Time_end >= travelTime &&
                                                                ts.Direction == bookDirect).ToListAsync();
                }
                if (fromStation > toStation)
                {
                    string bookDirect = "up";
                    return await db.Train_Schedules.Where(ts => ts.Station_Code_begin >= fromStation &&
                                                                ts.Station_Code_begin >= toStation &&
                                                                ts.Station_code_end <= fromStation &&
                                                                ts.Station_code_end <= toStation &&
                                                                ts.Time_begin <= travelTime &&
                                                                ts.Time_end >= travelTime &&
                                                                ts.Direction == bookDirect).ToListAsync();
                }
                return new List<Train_Schedule>();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<Train_Schedule> UpdateSchedule(Train_Schedule train_Schedule)
        {
            var oldSche = await GetScheduleById(train_Schedule.Id);
            if (oldSche != null)
            {
                var userType = typeof(Train);
                foreach (var property in userType.GetProperties())
                {
                    var newValue = property.GetValue(train_Schedule);
                    if (newValue != null && !string.IsNullOrWhiteSpace(newValue.ToString()))
                    {
                        property.SetValue(oldSche, newValue);
                    }
                }
                db.Train_Schedules.Update(oldSche);
                await db.SaveChangesAsync();
                return oldSche;
            }
            else
            {
                throw new ArgumentException("No ID found");
            }
        }
    }
}
