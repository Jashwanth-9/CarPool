using Microsoft.AspNetCore.Mvc;
using Models;
using Services;

namespace CarPool.Controllers
{
    [ApiController]
    [Route("api/ride")]
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
        [Route("offer")]
        public void OfferRide(RideDetails ride)
        {

            offeringService.OfferRide(ride);
        }

        [HttpPost]
        [Route("Matching")]
        public IActionResult GetMatchingRides(RideDetails ride)
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
        [Route("Booked")]
        public IActionResult GetBookedRides()
        {

            return Ok(bookingService.GetBookedRides());
        }

        [HttpGet]
        [Route("Offered")]
        public IActionResult GetOfferedRides()
        {
            return Ok(offeringService.GetOfferedRides());
        }
    }
}
