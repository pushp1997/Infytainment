using System;
using System.Collections.Generic;
using System.Text;
using InfytainmentDAL.Models;
using System.Linq;
using System.Collections.Generic;
using Exception_Logger;

namespace InfytainmentDAL
{
    public class InfytainmentRepository
    {
        private readonly InfytainmentDBContext _context;

        public InfytainmentRepository()
        {
            _context = new InfytainmentDBContext();
        }

        public InfytainmentRepository(InfytainmentDBContext context)
        {
            _context = context;
        }

        //Add New Movie 
        public bool AddMovieDetails(Movies movie)
        {
            bool status = false;
            Movies mov = null;
            try
            {
                mov = new Movies();
                mov.MovieId = movie.MovieId;
                mov.Title = movie.Title;
                mov.Category = movie.Category;
                mov.Duration = movie.Duration;
                mov.Rating = movie.Rating;
                mov.Description = movie.Description;
                //mov.ImageSmall = movie.ImageSmall;
                //mov.ImageLarge = movie.ImageLarge;
                _context.Movies.Add(mov);
                _context.SaveChanges();
                status = true;
            }
            catch (Exception e)
            {
                status = false;
                ExceptionLogger.Logger(e, "InfytainmentDAL > AddMovieDetails(Movies)");
            }
            return status;
        }

        //Add New Screens 
        public bool AddScreens(Screens screen)
        {
            bool status = false;
            Screens scr = null;
            try
            {
                scr = new Screens();
                scr.ScreenId = screen.ScreenId;
                scr.SeatingCapacity = screen.SeatingCapacity;
                _context.Screens.Add(scr);
                _context.SaveChanges();
                status = true;
            }
            catch (Exception e)
            {
                status = false;
                ExceptionLogger.Logger(e, "InfytainmentDAL > AddScreens(Screens)");
            }
            return status;
        }

        //Add New ShowTimings 
        public bool AddShowTimings(ShowTimings showtime)
        {
            bool status = false;
            ShowTimings st = null;
            try
            {
                st = new ShowTimings();
                st.ShowId = showtime.ShowId;
                st.MovieId = showtime.MovieId;
                st.DayofTheWeek = showtime.DayofTheWeek;
                st.Time = showtime.Time;
                st.ScreenId = showtime.ScreenId;
                _context.ShowTimings.Add(st);
                _context.SaveChanges();
                status = true;
            }
            catch (Exception e)
            {
                status = false;
                ExceptionLogger.Logger(e, "InfytainmentDAL > AddShowTimings(ShowTimings)");
            }
            return status;
        }

        //Add New Booking 
        public bool AddBooking(Booking booking, string seatId)
        {
            bool status = false;
            Booking bk = null;
            Seats seat = null;
            try
            {
                bk = new Booking();
                bk.BookId = booking.BookId;
                bk.MovieId = booking.MovieId;
                bk.ShowId = booking.ShowId;
                bk.ScreenId = booking.ScreenId;
                bk.EmployeeId = booking.EmployeeId;
                _context.Booking.Add(bk);
                _context.SaveChanges();

                seat = _context.Seats.Where(s => s.SeatId == seatId && s.ScreenId == booking.ScreenId).FirstOrDefault();
                seat.Status = 1;
                seat.BookId = booking.BookId;
                _context.Seats.Update(seat);
                _context.SaveChanges();

                status = true;
            }
            catch (Exception e)
            {
                status = false;
                ExceptionLogger.Logger(e, "InfytainmentDAL > AddBooking(Booking)");
            }
            return status;
        }

        //Get All Movies List 
        public List<Movies> FetchAllMovieDetails()
        {
            try
            {
                var MovieList = (from movies in _context.Movies
                                 orderby movies.MovieId
                                 select movies).ToList();
                return MovieList;
            }
            catch (Exception e)
            {
                ExceptionLogger.Logger(e, "InfytainmentDAL > FetchAllMovieDetails()");
                return null;
            }
        }

        //Get All Screens List 
        public List<Screens> FetchAllScreenDetails()
        {
            try
            {
                var ScreenList = (from Screens in _context.Screens orderby Screens.ScreenId select Screens).ToList();
                return ScreenList;
            }
            catch (Exception e)
            {
                ExceptionLogger.Logger(e, "InfytainmentDAL > FetchAllScreenDetails()");
                return null;
            }
        }

        //Get All ShowTiming List 
        public List<ShowTimings> FetchAllShowTimingDetails()
        {
            try
            {
                var ShowList = (from ShowTimings in _context.ShowTimings orderby ShowTimings.ShowId select ShowTimings).ToList();
                return ShowList;
            }
            catch (Exception e)
            {
                ExceptionLogger.Logger(e, "InfytainmentDAL > FetchAllShowTimingDetails()");
                return null;
            }
        }

        //Get All Booking Details
        public List<Booking> FetchAllBookingDetails()
        {
            try
            {
                var BookingList = (from Booking in _context.Booking orderby Booking.BookId select Booking).ToList();
                return BookingList;
            }
            catch (Exception e)
            {
                ExceptionLogger.Logger(e, "InfytainmentDAL > FetchAllBookingDetails()");
                return null;
            }
        }

        //Get Booking List 
        public Booking FetchBookingDetails(int bookId)
        {
            Booking book = null;
            try
            {
                book = _context.Booking.Find(bookId);
            }
            catch (Exception e)
            {
                book = null;
                ExceptionLogger.Logger(e, "InfytainmentDAL > FetchBookingDetails(bookId)");
            }
            return book;
        }

        //Delete Movies 
        public bool DeleteMovie(string Title)
        {
            Movies movie = null;
            List<Booking> booking = null;
            List<Seats> seats = null;
            bool status = false;
            try
            {
                movie = _context.Movies.Where(m => m.Title == Title).FirstOrDefault();
                booking = _context.Booking.Where(b => b.MovieId == movie.MovieId).ToList();
                foreach (var item in booking)
                {
                    seats.Append(_context.Seats.Where(s => s.BookId == item.BookId).FirstOrDefault());
                }
                if (movie != null)
                {
                    _context.Booking.RemoveRange(booking);

                    foreach (var item in seats)
                    {
                        item.Status = 0;
                        item.BookId = null;
                    }
                    _context.Seats.UpdateRange(seats);

                    _context.Movies.Remove(movie);
                    _context.SaveChanges();
                    status = true;
                }
                else
                {
                    status = false;
                }
            }
            catch (Exception e)
            {
                status = false;
                ExceptionLogger.Logger(e, "InfytainmentDAL > DeleteMovie(Title)");
            }
            return status;
        }

        //Check Admin
        public bool CheckAdmin(int employeeId)
        {
            Admin admin = null;
            bool status = false;
            try
            {
                admin = _context.Admin.Where(a => a.EmployeeId == employeeId).FirstOrDefault();
                if (admin != null)
                {
                    status = true;
                }
            }
            catch (Exception e)
            {
                status = false;
                ExceptionLogger.Logger(e, "InfytainmentDAL > CheckAdmin(int employeeId)");
            }
            return status;
        }

    }
}
