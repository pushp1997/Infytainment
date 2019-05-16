using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using InfytainmentDAL;
using InfytainmentDAL.Models;
using Exception_Logger;
using System.DirectoryServices;
using AutoMapper;

namespace InfytainmentAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class InfytainmentController : ControllerBase
    {

        private readonly InfytainmentRepository _repository;
        private readonly IMapper _mapper;

        public InfytainmentController(InfytainmentRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        #region Get Methods

        #region Fetch All Movie Details
        [HttpGet]
        public JsonResult FetchAllMovieDetails()
        {
            List<Models.Movies> movies = new List<Models.Movies>();
            try
            {
                List<Movies> movieList = _repository.FetchAllMovieDetails();
                if (movieList != null)
                {
                    foreach (var movie in movieList)
                    {
                        Models.Movies movieObj = _mapper.Map<Models.Movies>(movie);
                        movies.Add(movieObj);
                    }
                }
            }
            catch (Exception e)
            {
                movies = null;
                ExceptionLogger.Logger(e, "INfytainmentAPI > FetchAllScreenDetails");
            }
            return new JsonResult(movies);
        }
        #endregion

        #region Fetch All Screen Details
        [HttpGet]
        public JsonResult FetchAllScreenDetails()
        {
            List<Models.Screens> screens = new List<Models.Screens>();
            try
            {
                List<Screens> screenList = _repository.FetchAllScreenDetails();
                if (screenList != null)
                {
                    foreach (var screen in screenList)
                    {
                        Models.Screens screenObj = _mapper.Map<Models.Screens>(screen);
                        screens.Add(screenObj);
                    }
                }
            }
            catch (Exception e)
            {
                screens = null;
                ExceptionLogger.Logger(e, "INfytainmentAPI > FetchAllScreenDetails");
            }
            return new JsonResult(screens);
        }
        #endregion

        #region Fetch All ShowTiming Details
        [HttpGet]
        public JsonResult FetchAllShowTimingDetails()
        {
            List<Models.ShowTimings> showTimings = new List<Models.ShowTimings>();
            try
            {
                List<ShowTimings> showTimingList = _repository.FetchAllShowTimingDetails();
                if (showTimingList != null)
                {
                    foreach (var showTiming in showTimingList)
                    {
                        Models.ShowTimings showTimingObj = _mapper.Map<Models.ShowTimings>(showTiming);
                        showTimings.Add(showTimingObj);
                    }
                }
            }
            catch (Exception e)
            {
                showTimings = null;
                ExceptionLogger.Logger(e, "INfytainmentAPI > FetchAllShowTimingDetails");
            }
            return new JsonResult(showTimings);
        }
        #endregion

        //#region Fetch All ShowTiming Details
        //[HttpGet]
        //public JsonResult FetchShowTimingDetails(int movId)
        //{
        //    Models.ShowTimings showTimings = new Models.ShowTimings();
        //    try
        //    {
        //        ShowTimings showTimingList = _repository.FetchShowTimingDetails();
        //        if (showTimingList != null)
        //        {
        //            foreach (var showTiming in showTimingList)
        //            {
        //                Models.ShowTimings showTimingObj = _mapper.Map<Models.ShowTimings>(showTiming);
        //                showTimings.Add(showTimingObj);
        //            }
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        showTimings = null;
        //        ExceptionLogger.Logger(e, "INfytainmentAPI > FetchAllShowTimingDetails");
        //    }
        //    return new JsonResult(showTimings);
        //}
        //#endregion

        #region Fetch All Booking Details
        [HttpGet]
        public JsonResult FetchAllBookingDetails()
        {
            List<Models.Booking> bookings = new List<Models.Booking>();
            try
            {
                List<Booking> bookingList = _repository.FetchAllBookingDetails();
                if (bookingList != null)
                {
                    foreach (var booking in bookingList)
                    {
                        Models.Booking bookingObj = _mapper.Map<Models.Booking>(booking);
                        bookings.Add(bookingObj);
                    }
                }
            }
            catch (Exception e)
            {
                bookings = null;
                ExceptionLogger.Logger(e, "INfytainmentAPI > FetchAllBookingDetails");
            }
            return new JsonResult(bookings);
        }
        #endregion

        #region Fetch Booking Details
        [HttpGet]
        public JsonResult FetchBookingDetails(int bookId)
        {
            Models.Booking booking = null;
            try
            {
                booking = _mapper.Map<Models.Booking>(_repository.FetchBookingDetails(bookId));
            }
            catch (Exception e)
            {
                booking = null;
                ExceptionLogger.Logger(e, "InfytainmentAPI > FetchBookingDetails(int bookId)");
            }
            return new JsonResult(booking);
        }
        #endregion

        #region Fetch Employee Type
        [HttpGet]
        public JsonResult FetchEmployeeType()
        {
            /*
                {status, Admin (0/1)}
                0 -> Intern
                1 -> Trainee
                2 -> Employee
                -1 -> Error
            */
            int status = -1;
            try
            {
                string nickName = HttpContext.User.Identity.Name;

                // Check For Intern
                if (nickName.Substring(nickName.Length - 3).ToLower() == "trn")
                {
                    status = 0;
                }
                else
                {
                    DirectoryEntry entry = null;
                    DirectorySearcher searcher1 = new DirectorySearcher(entry);
                    searcher1.Filter = string.Format("(&(objectCategory=person)(objectClass=user)(SAMAccountname={0}))", nickName);  //"Arpan.Nema"
                    SearchResult results1;
                    results1 = searcher1.FindOne();

                    // Check For Trainee
                    if (((string)results1.Properties["department"][0]).ToLower() == "trpu")
                    {
                        status = 1;
                    }

                    // Employee
                    else
                    {
                        status = 2;
                    }
                }
            }
            catch (Exception e)
            {
                ExceptionLogger.Logger(e, "InfytainmentAPI > FetchEmployeeType");
            }
            return new JsonResult(status);
        }
        #endregion

        #region Fetch Employee Name
        [HttpGet]
        public JsonResult FetchEmployeeName()
        {
            string userName = null;
            try
            {
                userName = (HttpContext.User.Identity.Name).Substring(11);
            }
            catch (Exception e)
            {
                ExceptionLogger.Logger(e, "InfytainmentAPI > FetchEmployeeName");
            }
            return new JsonResult(userName);
        }
        #endregion

        #region Check Admin
        [HttpGet]
        public JsonResult CheckAdmin()
        {
            bool status = false;
            try
            {
                string nickName = (HttpContext.User.Identity.Name).Substring(11);
                DirectoryEntry entry = null;
                DirectorySearcher searcher1 = new DirectorySearcher(entry);
                searcher1.Filter = string.Format("(&(objectCategory=person)(objectClass=user)(SAMAccountname={0}))", nickName);  //"Arpan.Nema"
                SearchResult results1;
                results1 = searcher1.FindOne();

                status = _repository.CheckAdmin(Convert.ToInt32(results1.Properties["company"][0]));
            }
            catch (Exception e)
            {
                status = false;
                ExceptionLogger.Logger(e, "InfytainmentAPI > FetchBookingDetails(int bookId)");
            }
            return new JsonResult(status);
        }
        #endregion

        #endregion

        #region Post Methods

        #region Add Movie Details
        [HttpPost]
        public JsonResult AddMovieDetails(Models.Movies movie)
        {
            bool status = false;
            try
            {
                status = _repository.AddMovieDetails(_mapper.Map<Movies>(movie));
            }
            catch (Exception e)
            {
                status = false;
                ExceptionLogger.Logger(e, "InfytainmentAPI > AddMovieDetails(movie)");
            }
            return new JsonResult(status);
        }
        #endregion

        #region Add Screens
        [HttpPost]
        public JsonResult AddScreens(Models.Screens screen)
        {
            bool status = false;
            try
            {
                status = _repository.AddScreens(_mapper.Map<Screens>(screen));
            }
            catch (Exception e)
            {
                status = false;
                ExceptionLogger.Logger(e, "InfytainmentAPI > AddScreens(screen)");
            }
            return new JsonResult(status);
        }
        #endregion

        #region Add Show Timings
        [HttpPost]
        public JsonResult AddShowTimings(Models.ShowTimings showtime)
        {
            bool status = false;
            try
            {
                status = _repository.AddShowTimings(_mapper.Map<ShowTimings>(showtime));
            }
            catch (Exception e)
            {
                status = false;
                ExceptionLogger.Logger(e, "InfytainmentAPI > AddShowTimings(showtime)");
            }
            return new JsonResult(status);
        }
        #endregion

        #region Add Booking
        [HttpPost]
        public JsonResult AddBooking(Models.Booking booking, string seatId)
        {
            bool status = false;
            try
            {
                status = _repository.AddBooking(_mapper.Map<Booking>(booking), seatId);
            }
            catch (Exception e)
            {
                status = false;
                ExceptionLogger.Logger(e, "InfytainmentAPI > AddBooking(Booking)");
            }
            return new JsonResult(status);
        }
        #endregion

        #endregion

        #region Delete Methods
        [HttpDelete("{title}")]
        public JsonResult DeleteMovie(string Title)
        {
            bool status = false;
            try
            {
                status = _repository.DeleteMovie(Title);
            }
            catch (Exception e)
            {
                status = false;
                ExceptionLogger.Logger(e, "InfytainmentAPI > DeleteMovie(Title)");
            }
            return new JsonResult(status);
        }
        #endregion

    }
}
