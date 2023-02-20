using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using ViewModel;
using Services;

namespace CarPool.Controllers
{
    [Authorize]
    [ApiController]
    [Route("Api/Ride")]
    public class RideController : Controller
    {
        IBookingService bookingService;
        IOfferingService offeringService;
        public RideController(IBookingService bookingService, IOfferingService offeringService)
        {
            this.bookingService = bookingService;
            this.offeringService = offeringService;
        }
        [HttpPost]
        [Route("Offer")]
        public IActionResult OfferRide(OfferRide ride)
        {
            try
            {
                return Ok(offeringService.OfferRide(ride));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpPost]
        [Route("Matching")]
        public IActionResult GetMatchingRides(BookRide ride)
        {
            List<BookRide> rides = bookingService.GetMatchingRides(ride);
            if(rides!= null)
            {
                return Ok(rides);
            }
            return NotFound("No Matching Rides");
        }

        [HttpPost]
        [Route("Book")]
        public IActionResult BookRide(BookRide ride)
        {
            BookRide book= bookingService.BookRide(ride);
            if(book!= null)
            {
                return Ok(book);
            }
            return NotFound("Ride does not exist");

        }

        [HttpGet]
        [Route("Booked")]
        public IActionResult GetBookedRides()
        {
            List<BookRide> ride = bookingService.GetBookedRides();
            if (ride != null)
            {
                return Ok(ride);
            }
            return NotFound("No Rides booked");
        }

        [HttpGet]
        [Route("Offered")]
        public IActionResult GetOfferedRides()
        {
            List<BookRide> ride = offeringService.GetOfferedRides();
            if(ride != null)
            {
                return Ok(ride);
            }
            return NotFound("No Rides offered");
        }
    }
}
