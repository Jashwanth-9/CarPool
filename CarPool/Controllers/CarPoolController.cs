using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
/*using Models;*/
using CarPool.Models;
/*using Services;*/
using CarPool.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.TagHelpers;

namespace CarPool.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarPoolController : ControllerBase
    {

        DBCarContext carContext;
        UserService user;
        BookingService bookingService;
        OfferingService offeringService;
        public CarPoolController()
        {
            this.carContext = new DBCarContext();
            user= new UserService();
            bookingService = new BookingService();
            offeringService = new OfferingService();

        }
        [HttpPost]
        public IActionResult Post(User signup)
        {
            if (user.IsValidSignup(signup)) {
                return Ok(signup);
            }
            return BadRequest();
        }
        [HttpPost]
        [Route("user")]
        public IActionResult Login(string emailId,string password)
        {
            
            if (user.IsValidLogin(emailId,password))
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpPost]
        [Route("ride")]
        public void OfferRide(Ride ride)
        {
            
            offeringService.OfferRide(ride);
        }

        [HttpPost]
        [Route("rides")]
        public IActionResult GetMatchingRides(Ride ride)
        { 
            try
            {
                return Ok(bookingService.GetMatchingRides(ride));
            }
            catch
            {
                return BadRequest();
            }

        }

        [HttpPost]
        [Route("book")]
        public IActionResult BookRide(int rideId)
        {
            try
            {
                bookingService.BookRide(rideId);
                return Ok();
               
            }
            catch
            {
               return BadRequest();
            }
            
        }

        [HttpGet]
        [Route("BookedRides")]
        public IActionResult GetBookedRides()
        {
            
            return Ok(bookingService.GetBookedRides());    
        }

        [HttpGet]
        [Route("OfferedRides")]
        public IActionResult GetOfferedRides()
        {
            return Ok(offeringService.GetOfferedRides());
        }

    }
}
